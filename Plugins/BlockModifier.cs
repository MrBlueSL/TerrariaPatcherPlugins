using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PluginLoader;
using Terraria;

namespace MrBlueSLPlugins
{
    public class BlockModifier : MarshalByRefObject, IPluginItemSetDefaults
    {
        private bool con = false;
        private Keys conKey;
        private Color gold = Color.Gold;

        public BlockModifier()
        {
            if (!Keys.TryParse(IniAPI.ReadIni("Consumable", "ToggleKey", "B", writeIt: true), out conKey))
                conKey = Keys.B;

            Loader.RegisterHotkey(() =>
            {
                con = !con;
                foreach (var item in GetAllItems())
                {
                    if (con)
                        ChangeConsumable(item);
                    else
                        ChangeConsumable(item);
                }

                Main.NewText("Blocks and Walls will " + (con ? "no longer consume" : "consume") + " on use", gold.R, gold.G, gold.B);
            }, Keys.B);
        }

        public void OnItemSetDefaults(Item item)
        {
            if (con)
                ChangeConsumable(item);
            else
                ChangeConsumable(item);
        }

        private void ChangeConsumable(Item item)
        {
            if ((item.createTile >= 1 || item.createWall >= 1) && con == true)
                item.consumable = false;
            else if ((item.createTile >= 1 || item.createWall >= 1) && con == false)
                item.consumable = true;
        }


        private IEnumerable<Item> GetAllItems()
        {
            return Main.item
                .Concat(new[] { Main.reforgeItem, Main.mouseItem, Main.guideItem })
                .Concat(Main.chest.Where(chest => chest != null).SelectMany(chest => chest.item))
                .Concat(Main.player.SelectMany(player => player.inventory
                    .Concat(player.armor)
                    .Concat(player.miscEquips)
                    .Concat(new[] { player.trashItem })))
                .Where(item => item != null && item.type != 0);
        }
    }
}
