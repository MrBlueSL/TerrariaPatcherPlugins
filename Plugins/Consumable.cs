using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PluginLoader;
using Terraria;
using Terraria.ID;

namespace MrBlueSLPlugins
{
    public class Consumable : MarshalByRefObject, IPluginItemSetDefaults, IPluginPlayerUpdate
    {
        Color color = Color.Gold;
        private bool con = false;
        private Keys consumableKey;
        public Consumable()
        {
            if (!Keys.TryParse(IniAPI.ReadIni("Consumable", "ConsumableKey", "B", writeIt: true), out consumableKey))
                consumableKey = Keys.B;

            Loader.RegisterHotkey(() =>
                {
                    con = !con;
                    Main.NewText("Items will " + (con ? "not consume" : "consume") + " on use", color.R, color.G, color.B);
                }, consumableKey);
        }
        public void OnItemSetDefaults(Item item)
        {
            Item i = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem];
            Loader.RegisterHotkey(() =>
                {
                    if (i.consumable == false && con == true)
                    {
                        i.consumable = true;
                    }
                    else if (i.consumable == true && con == false)
                    {
                        i.consumable = false;
                    }
                }, consumableKey);
        }
        public void OnPlayerUpdate(Player player)
        {   
            Item i2 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem];
            if (i2.pick > 0 || i2.axe > 0 || i2.hammer > 0 || i2.magic != false || i2.melee != false || i2.ranged != false)// saveguard against deleting weapons/tools?)
            {
                i2.consumable = false;
            }
        }
    }
}
