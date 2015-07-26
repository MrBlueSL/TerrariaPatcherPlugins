using System;
using Microsoft.Xna.Framework.Input;
using PluginLoader;
using Terraria;

namespace MrBlueSLPlugins
{
    public class HotKeyBinder : MarshalByRefObject, IPluginChatCommand
    {
        public bool OnChatCommand(string command, string[] args)
        {        
            string a0 = args[0];
            string a1 = args[1];
            string a2 = args[2];
            string a3 = args[3];
            if (command != "bind") return false;

            if (args.Length < 1 || args.Length > 4 || args[0] == "help")
            {
                Main.NewText("Usage:");
                Main.NewText("  /bind modifier hotkey");
                Main.NewText("Example:");
                Main.NewText("  /bind Control T /time dusk");
                return true;
            }
            if (args[0] == a0 && args[1] == a1 && args[2] == a2 && args[3] == a3)
            {
                IniAPI.WriteIni("HotkeyBinds", a0 + "," + a1, a2 + " " + a3);
                if (args[0] == "Control" || args[0] == "control")
                {
                    a0 = "Ctrl";
                }
                else if (args[0] == "alt")
                {
                    a0 = "Alt";
                }
                else if (args[0] == "shift")
                {
                    a0 = "Shift";
                }
                Main.NewText(a0 + "+" + a1 + " set to " + a2 + " " + a3 + " (Restart required)");
                return true;
            }
            else if (args[0] == a0 && args[1] == a1 && args[2] == a2)
            {
                IniAPI.WriteIni("HotkeyBinds", a0 + "," + a1, a2);
                if (args[0] == "Control" || args[0] == "control")
                {
                    a0 = "Ctrl";
                }
                else if (args[0] == "alt")
                {
                    a0 = "Alt";
                }
                else if (args[0] == "shift")
                {
                    a0 = "Shift";
                }
                Main.NewText(a0 + "+" + a1 + " set to " + a2 + " (Restart required)");
                return true;
            }
            return true;
        }
    }
} 
