using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Domain.Entities.Build
{
    public class BuildInfo
    {
        public bool Success { get; set; }
        public List<string> Result { get; set; } = new List<string>();
    }
}
