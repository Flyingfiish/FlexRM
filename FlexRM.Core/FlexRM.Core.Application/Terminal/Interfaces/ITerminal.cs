using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Application.Terminal.Interfaces
{
    public interface ITerminal
    {
        public List<string> Term(List<string> cmds);
    }
}
