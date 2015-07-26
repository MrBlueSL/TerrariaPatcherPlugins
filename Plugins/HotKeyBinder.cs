using System;
using System.Linq;
using PluginLoader;
using Terraria;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MrBlueSLPlugins
{
    public class HotKeyBinder : MarshalByRefObject, IPluginChatCommand
    {
        public bool OnChatCommand(string command, string[] args)
        {
            if (command != "bind") return false;

            if (args.Length < 2 || (args.Length > 0 && args[0] == "help"))
            {
                Main.NewText("Usage:");
                Main.NewText("  /bind modifier,hotkey command");
                Main.NewText("Example:");
                Main.NewText("  /bind Control,T /time dusk");
                return true;
            }

            BindHotkey(args[0], string.Join(" ", args.Skip(1)));
            return true;
        }

        private void BindHotkey(string hotkey, string cmd)
        {
            var key = Keys.None;
            var control = false;
            var shift = false;
            var alt = false;
            bool hotkeyParseFailed = false;
            foreach (var keyStr in hotkey.Split(','))
            {
                switch (keyStr.ToLower())
                {
                    case "control":
                        control = true;
                        break;
                    case "shift":
                        shift = true;
                        break;
                    case "alt":
                        alt = true;
                        break;
                    default:
                        if (key != Keys.None || !Keys.TryParse(keyStr, out key)) hotkeyParseFailed = true;
                        break;
                }
            }

            if (string.IsNullOrEmpty(cmd) || !cmd.StartsWith("/") || hotkeyParseFailed || key == Keys.None)
                Main.NewText("Invalid hotkey binding");
            else
            {
                IniAPI.WriteIni("HotkeyBinds", hotkey, cmd);
                Loader.RegisterHotkey(cmd, key, control, shift, alt);
                Main.NewText(hotkey + " set to " + cmd);
            }
        }
    }
}
