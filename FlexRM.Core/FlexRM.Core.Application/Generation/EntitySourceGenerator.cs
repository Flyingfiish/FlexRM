using FlexRM.Core.Application.Generation.Common;
using FlexRM.Core.Application.Generation.Interfaces;
using FlexRM.Core.Application.Generation.Snippets;
using FlexRM.Core.Domain.Entities.Configuration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Application.Generation
{
    public class EntitySourceGenerator : SourceGenerator, IEntitySourceGenerator
    {
        private readonly Entity _entity;
        private readonly StringBuilder _result;
        public EntitySourceGenerator(Entity entity) : base()
        {
            _entity = entity;
            _result = new StringBuilder();
        }

        public string Generate()
        {
            AddUsings(new List<string>()
            {
                "System",
                "System.Collections.Generic",
                "System.Linq",
                "System.Text",
                "System.Threading.Tasks"
            });

            AddNamespace("FlexRM.Generated.Domain.Entities");
            AddClass(_entity.Name);

            foreach (var column in _entity.Columns)
            {
                string type = string.Empty;
                switch (column.Type)
                {
                    case Domain.Enums.ColumnTypes.String:
                        type = "string";
                        break;
                    case Domain.Enums.ColumnTypes.Double:
                        type = "double";
                        break;
                    case Domain.Enums.ColumnTypes.DateTime:
                        type = "DateTime";
                        break;
                    case Domain.Enums.ColumnTypes.Entity:
                        type = column.RelatedEntity.Name;
                        break;
                    case Domain.Enums.ColumnTypes.Integer:
                        type = "int";
                        break;
                }
                if (column.Type == Domain.Enums.ColumnTypes.Entity)
                    AddProperty("Guid", $"{column.Name}Id");
                AddProperty(type, column.Name);
                AddMethod("Test", "void", new List<string>() { "Console.WriteLine(\"ss\");" });
            }

            return base.ToString();
        }
    }
}
