using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.Bridge;
using ToolBox.Platform;

namespace FlexRM.Core.Application.Terminal
{
    public class CommandHelper
    {

        private FXRMOS _currentOS
        {
            get
            {
                switch (OS.GetCurrent())
                {
                    case "win":
                        return FXRMOS.Win;
                    case "mac":
                        return FXRMOS.Mac;
                    case "gnu":
                        return FXRMOS.Gnu;
                    default:
                        return FXRMOS.Win;
                }
            }
        }

        public string GeneratedDirectory
        {
            get
            {
                if (_currentOS == FXRMOS.Win)
                    return "cd ../../../../../SourceGeneratorTest";
                else
                    return "cd ../../../../../SourceGeneratorTest";
            }
        }

        public string Build
        {
            get
            {
                if (_currentOS == FXRMOS.Win)
                    return "dotnet cake";
                else
                    return "dotnet cake";
            }
        }
    }

    public enum FXRMOS
    {
        Win,
        Mac,
        Gnu
    }
}
