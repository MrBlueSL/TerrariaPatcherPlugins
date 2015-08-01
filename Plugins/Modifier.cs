using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PluginLoader;
using Terraria;

namespace MrBlueSLPlugins
{
    public class Modifier : MarshalByRefObject, IPluginItemSetDefaults, IPluginChatCommand
    {
        private bool modifierB = false;
        private bool modifierP = false;
        private Color gold = Color.Gold;

        public bool OnChatCommand(string command, string[] args)
        {
            if (command != "modifier") return false;

            if (args.Length < 1 || args.Length > 2 || args[0] == "help")
            {
                Main.NewText("Usage:");
                Main.NewText("  /modifier bw (Toggles Blocks/Walls consumable or not)");
                Main.NewText("  /modifier pt (Toggles Potions consumable state)");
                return true;
            }
            switch (args[0].ToLower())
            {
                case "bw":
                    {
                        modifierB = !modifierB;
                        foreach (var item in GetAllItems())
                        {
                            if (modifierB)
                                ItemModifier(item);
                            else
                                ItemModifier(item);
                        }
                        Main.NewText("Blocks and Walls will " + (modifierB ? "no longer consume" : "consume") + " on use", gold.R, gold.G, gold.B);
                        break;
                    }
                case "pt":
                    {
                        modifierP = !modifierP;
                        foreach (var item in GetAllItems())
                        {
                            if (modifierP)
                                ItemModifier(item);
                            else
                                ItemModifier(item);
                        }
                        Main.NewText("Potions will " + (modifierP ? "no longer consume" : "consume") + " on use", gold.R, gold.G, gold.B);
                        break;
                    }
            }
            return true;
        }

        public void OnItemSetDefaults(Item item)
        {
            if (modifierB || modifierP)
                ItemModifier(item);
            else
                ItemModifier(item);
        }

        private void ItemModifier(Item item)
        {
            if ((item.createTile >= 1 || item.createWall >= 1) && modifierB)
                item.consumable = false;
            else if ((item.createTile >= 1 || item.createWall >= 1) && !modifierB)
                item.consumable = true;
            if (item.name.Contains("Potion") && modifierP)
                item.consumable = false;
            else if (item.name.Contains("Potion") && !modifierP)
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
