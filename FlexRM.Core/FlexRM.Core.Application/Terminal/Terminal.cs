using FlexRM.Core.Application.Terminal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.Bridge;
using ToolBox.Notification;
using ToolBox.Platform;

namespace FlexRM.Core.Application.Terminal
{
    public class Terminal : ITerminal
    {
        private INotificationSystem _notificationSystem { get; set; }
        private IBridgeSystem _bridgeSystem { get; set; }
        private ShellConfigurator _shell { get; set; }
        public Terminal()
        {
            _notificationSystem = NotificationSystem.Default;
            switch (OS.GetCurrent())
            {
                case "win":
                    _bridgeSystem = BridgeSystem.Bat;
                    break;
                case "mac":
                    _bridgeSystem = BridgeSystem.Bash;
                    break;
                case "gnu":
                    _bridgeSystem = BridgeSystem.Bash;
                    break;
                default:
                    _bridgeSystem = BridgeSystem.Bash;
                    break;
            }
            _shell = new ShellConfigurator(_bridgeSystem, _notificationSystem);
        }

        public List<string> Term(List<string> cmds)
        {
            var result = new List<string>();
            foreach (string cmd in cmds)
            {
                result.Add($"cmd: {cmd}");
                var response = _shell.Term(cmd);
                if (response.code == 0)
                    result.Add(response.stdout);
                else
                    result.Add(response.stderr);
            }
            return result;
        }
    }
}
