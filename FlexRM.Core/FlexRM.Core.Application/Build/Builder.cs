using FlexRM.Core.Application.Build.Interfaces;
using FlexRM.Core.Application.Terminal;
using FlexRM.Core.Application.Terminal.Interfaces;
using FlexRM.Core.Domain.Entities.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Application.Build
{
    public class Builder : IBuilder
    {
        private ITerminal _terminal;
        private CommandHelper _commandHelper;
        public Builder(ITerminal terminal, CommandHelper commandHelper)
        {
            _terminal = terminal;
            _commandHelper = commandHelper;
        }
        public BuildInfo Build(BuildConfig buildConfig)
        {
            var result = _terminal.Term(new List<string>
            {
                _commandHelper.GeneratedDirectory,
                _commandHelper.Build,
            });
            return new BuildInfo()
            {
                Result = result,
                Success = true
            };
        }

        public BuildInfo Build()
        {
            return Build(new BuildConfig());
        }
    }
}
