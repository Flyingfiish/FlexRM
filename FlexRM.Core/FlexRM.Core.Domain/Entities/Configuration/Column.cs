using FlexRM.Core.Domain.Entities.Common;
using FlexRM.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Domain.Entities.Configuration
{
    public class Column : Base
    {
        public ColumnTypes Type { get; set; }
        public string Description { get; set; }
        public Guid EntityId { get; set; }
        public Entity Entity { get; set; }
        public Guid? RelatedEntityId { get; set; }
        public Entity RelatedEntity { get; set; }
    }
}
