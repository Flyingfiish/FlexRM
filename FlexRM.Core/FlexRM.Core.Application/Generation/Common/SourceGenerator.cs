using FlexRM.Core.Application.Generation.Interfaces;
using FlexRM.Core.Application.Generation.Snippets;
using FlexRM.Core.Domain.Entities.Configuration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Application.Generation.Common
{
    public class SourceGenerator
    {
        private CompilationUnitSyntax _root;
        private NamespaceDeclarationSyntax? _namespaceDeclaration;
        private ClassDeclarationSyntax? _classDeclaration;
        private List<PropertyDeclarationSyntax> _properties;
        private List<MethodDeclarationSyntax> _methods;

        public SourceGenerator()
        {
            _root = SyntaxFactory.CompilationUnit();
            _properties = new List<PropertyDeclarationSyntax>();
            _methods = new List<MethodDeclarationSyntax>();
        }

        protected void AddUsings(List<string> usings)
        {
            _root = _root.AddUsings(usings.Select(us => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(us))).ToArray());
        }

        protected void AddNamespace(string name)
        {
            _namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(name));
        }

        protected void AddClass(string name, List<string>? baseTypes = null)
        {
            _classDeclaration = SyntaxFactory.ClassDeclaration(name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            if (baseTypes != null)
                _classDeclaration = _classDeclaration.AddBaseListTypes(baseTypes.Select((type, index) => SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseName(type))).ToArray());
        }

        protected void AddProperty(string type, string name)
        {
            _properties.Add(SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(type), name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                ));
        }

        protected void AddMethod(string name, string returnType, List<string> bodyStatements/*, params string[] modifiers*/)
        {
            _methods.Add(SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(returnType), name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddBodyStatements(bodyStatements.Select(GetStatementFromText).ToArray()));
        }

        private StatementSyntax GetStatementFromText(string statement)
        {
            return (StatementSyntax)CSharpSyntaxTree.ParseText(statement).GetRoot()
                .ChildNodes().First()
                .ChildNodes().First();
        }

        public override string ToString()
        {
            _classDeclaration = _classDeclaration.AddMembers(_properties.ToArray());
            _classDeclaration = _classDeclaration.AddMembers(_methods.ToArray());
            _namespaceDeclaration = _namespaceDeclaration.AddMembers(_classDeclaration);
            _root = _root.AddMembers(_namespaceDeclaration);

            return _root.NormalizeWhitespace().ToFullString();
        }
    }
}
