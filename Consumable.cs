using System;
using PluginLoader;
using Terraria;
using Terraria.ID;

namespace MrBlueSLPlugins
{
    public class Consumable : MarshalByRefObject, IPluginChatCommand, IPluginItemSetDefaults
    {
        private bool im;
        private bool i = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].consumable;
        private string itm = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].name;
        public bool OnChatCommand(string command, string[] args)
        {
           /* string itm = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].name;
            if (command != "consumable") return false;
            if (command == "consumable" && i == true)
            {
                im = false;
                Main.NewText(itm + " will not consume on use");
            }
            else
            {
                im = true;
                Main.NewText(itm + " will consume on use");
            }*/
            return true;
        }
        public void OnItemSetDefaults(Item item)
        {
            /*if (im)
            {
                item.consumable = false;
            }
            else
            {
                item.consumable = true;
            }*/
        }
    }
}
