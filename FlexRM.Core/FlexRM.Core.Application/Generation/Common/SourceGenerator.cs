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
        private NamespaceDeclarationSyntax _namespaceDeclaration;
        private ClassDeclarationSyntax _classDeclaration;
        private List<PropertyDeclarationSyntax> _properties;
        public SourceGenerator()
        {
            _root = SyntaxFactory.CompilationUnit();
            _properties = new List<PropertyDeclarationSyntax>();
        }
        protected void AddUsings(List<string> usings)
        {
            _root = _root.AddUsings(usings.Select(us => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(us))).ToArray());
        }

        protected void AddNamespace(string name)
        {
            _namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(name));
        }

        protected void AddClass(string name)
        {
            _classDeclaration = SyntaxFactory.ClassDeclaration(name)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
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

        public override string ToString()
        {
            _classDeclaration = _classDeclaration.AddMembers(_properties.ToArray());
            _namespaceDeclaration = _namespaceDeclaration.AddMembers(_classDeclaration);
            _root = _root.AddMembers(_namespaceDeclaration);

            return _root.NormalizeWhitespace().ToFullString();
        }
    }
}
