using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PluginLoader;
using Terraria;

namespace MrBlueSLPlugins
{
    public class Flashlight : MarshalByRefObject, IPluginPlayerUpdate
    {
        private bool flashlight = false;
        private Keys flashlightKey;

        public Flashlight()
        {
            if (!Keys.TryParse(IniAPI.ReadIni("Flashlight", "Toggle Key", "U", writeIt: true), out flashlightKey))
                flashlightKey = Keys.U;

            Loader.RegisterHotkey(() =>
            {
                if (!flashlight)
                {
                    flashlight = true;
                    Main.NewText("Flashlight Enabled", 150, 150, 150);
                }
                else
                {
                    flashlight = false;
                    Main.NewText("Flashlight Disabled", 150, 150, 150);
                }
            }, flashlightKey);
        }

        public void OnPlayerUpdate(Player player)
        {
            if (flashlight) 
            {
                Lighting.AddLight((int)((double)(Main.mouseState.X + Main.screenPosition.X) + (double)(Player.defaultWidth / 2)) / 16, (int)((double)(Main.mouseState.Y + Main.screenPosition.Y) + (double)(Player.defaultHeight / 2)) / 16, 1f, 1f, 1f);
            }
        }
    }
}
