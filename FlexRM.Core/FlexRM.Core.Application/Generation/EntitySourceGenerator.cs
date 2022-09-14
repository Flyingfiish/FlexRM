using FlexRM.Core.Application.Generation.Interfaces;
using FlexRM.Core.Application.Generation.Snippets;
using FlexRM.Core.Domain.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Application.Generation
{
    public class EntitySourceGenerator : IEntitySourceGenerator
    {
        private readonly Entity _entity;
        private readonly StringBuilder _result;
        public EntitySourceGenerator(Entity entity)
        {
            _entity = entity;
            _result = new StringBuilder();
        }

        public string Generate()
        {
            AddUsing("Sustem");
            AddUsing("System.Collections.Generic");
            AddUsing("System.Linq");
            AddUsing("System.Text");
            AddUsing("System.Threading.Tasks");

            AddNamespace("FlexRM.Generated.Domain.Entities");
            AddClass(_entity.Name);

            foreach(var column in _entity.Columns)
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
            }

            CloseBrackets();

            return _result.ToString();
        }

        protected void AddUsing(string name)
        {
            _result.AppendLine(EntitySourceSnippets.GetUsing(name));
        }

        protected void AddNamespace(string name)
        {
            _result.AppendLine(EntitySourceSnippets.GetNamespace(name));
            _result.AppendLine(EntitySourceSnippets.OpenBracket);
        }

        protected void AddClass(string name)
        {
            _result.AppendLine(EntitySourceSnippets.GetClass(name));
            _result.AppendLine(EntitySourceSnippets.OpenBracket);
        }

        protected void AddProperty(string type, string name)
        {
            _result.AppendLine(EntitySourceSnippets.GetProperty(type, name));
        }

        protected void CloseBrackets()
        {
            var result = _result.ToString();
            int countOpen = result.Count(c => c.ToString() == EntitySourceSnippets.OpenBracket);
            int countClose = result.Count(c => c.ToString() == EntitySourceSnippets.CloseBracket);
            for(int i = 0; i < countOpen - countClose; i++)
                _result.AppendLine(EntitySourceSnippets.CloseBracket);
        }
    }
}
