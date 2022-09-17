using FlexRM.Core.Application.Generation.Common;
using FlexRM.Core.Application.Generation.Interfaces;
using FlexRM.Core.Domain.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Application.Generation
{
    public class EFCoreSourceGenerator : SourceGenerator, IEntitySourceGenerator
    {
        private readonly List<Entity> _entities;
        public EFCoreSourceGenerator(List<Entity> entities)
        {
            _entities = entities;
        }

        public string Generate()
        {
            AddUsings(new List<string>()
            {
                "Microsoft.EntityFrameworkCore",
                "System",
                "System.Collections.Generic",
                "System.Linq",
                "System.Text",
                "System.Threading.Tasks"
            });

            AddNamespace("FlexRM.Generated.Infrastructure.EFCore");
            AddClass("ApplicationContext", new List<string>
            {
                "DbContext"
            });
            foreach(var entity in _entities)
                AddProperty($"DbSet<{entity.Name}>", $"Entity{entity.Name}PluralForm");

            return base.ToString();
        }
    }
}
