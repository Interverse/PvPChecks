using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace PvPChecks {
    [ApiVersion(2, 1)]
    public class AntiPvPWeapons : TerrariaPlugin {
        public Config config;
        public List<string> weaponbans;
        public List<int> buffbans;
        List<int> illegalMeleePrefixes = new List<int>();
        List<int> illegalRangedPrefixes = new List<int>();
        List<int> illegalMagicPrefixes = new List<int>();

        public override string Name => "PvPChecks";
        public override string Author => "Johuan";
        public override string Description => "Bans Weapons and disables PvPers from using illegitimate stuff.";
        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        
        public AntiPvPWeapons(Main game) : base(game) {
        }

        public override void Initialize() {
            string path = Path.Combine(TShock.SavePath, "pvpchecks.json");
            config = Config.Read(path);
            if (!File.Exists(path))
                config.Write(path);
            
            weaponbans = new List<string>(config.WeaponList);
            buffbans = new List<int>(config.BuffList);

            illegalMeleePrefixes.AddRange(DataIDs.RangedPrefixIDs);
            illegalMeleePrefixes.AddRange(DataIDs.MagicPrefixIDs);

            illegalRangedPrefixes.AddRange(DataIDs.MeleePrefixIDs);
            illegalRangedPrefixes.AddRange(DataIDs.MagicPrefixIDs);

            illegalMagicPrefixes.AddRange(DataIDs.MeleePrefixIDs);
            illegalMagicPrefixes.AddRange(DataIDs.RangedPrefixIDs);

            GetDataHandlers.PlayerUpdate += OnPlayerUpdate;

            Commands.ChatCommands.Add(new Command(PvPWeaponBans, "pvpweaponbans"));
            Commands.ChatCommands.Add(new Command("pvpchecks.banweapon", BanWeapon, "banweapon"));
            Commands.ChatCommands.Add(new Command(PvPBuffBans, "pvpbuffbans"));
            Commands.ChatCommands.Add(new Command("pvpchecks.banbuff", BanBuff, "banbuff"));
        }

        protected override void Dispose(bool disposing) {
            GetDataHandlers.PlayerUpdate -= OnPlayerUpdate;

            string path = Path.Combine(TShock.SavePath, "pvpchecks.json");
            config.WeaponList = weaponbans.ToArray();
            config.BuffList = buffbans.ToArray();
            config.Write(path);

            base.Dispose(disposing);
        }

        public void OnPlayerUpdate(object sender, GetDataHandlers.PlayerUpdateEventArgs args) {
            TSPlayer player = TShock.Players[args.PlayerId];
            if (player == null) return;

            //If the player isn't in pvp or using an item, skip pvp checking
            if (!player.TPlayer.hostile || (args.Control & 32) == 0) return;

            //If the player has this permission, skip pvp checking
            if (player.HasPermission("pvpchecks.useall")) return;

            //Checks whether a player is using a banned item
            if (!player.HasPermission("pvpchecks.usebannedweps")) {
                foreach (string weapon in weaponbans) {
                    if (player.ItemInHand.Name == weapon || player.SelectedItem.Name == weapon) {
                        player.Disable("Used banned pvp weapon.", DisableFlags.WriteToLog);
                        player.SendErrorMessage(weapon + " cannot be used in PvP. See /pvpweaponbans.");
                        break;
                    }
                }
            }

            //Checks whether a player has a banned buff
            if(!player.HasPermission("pvpchecks.usebannedbuffs")) {
                foreach(int buff in buffbans) {
                    foreach (int playerbuff in player.TPlayer.buffType) {
                        if (playerbuff == buff) {
                            player.Disable("Used banned buff.", DisableFlags.WriteToLog);
                            player.SendErrorMessage(TShock.Utils.GetBuffName(playerbuff) + " cannot be used in PvP. See /pvpbuffbans.");
                            break;
                        }
                    }
                }
            }

            //Checks whether a player has illegal prefixed items
            if (!player.HasPermission("pvpchecks.useillegalweps")) {
                if(player.ItemInHand.maxStack > 1 || player.SelectedItem.maxStack > 1) {
                    if(player.ItemInHand.prefix != 0 || player.SelectedItem.prefix != 0) {
                        player.Disable("Used illegal weapon.", DisableFlags.WriteToLog);
                        player.SendErrorMessage("Illegally prefixed weapons are not allowed in PvP");
                    }
                } else if (player.ItemInHand.melee || player.SelectedItem.melee) {
                    foreach (int prefixes in illegalMeleePrefixes) {
                        if (player.ItemInHand.prefix == prefixes || player.SelectedItem.prefix == prefixes) {
                            player.Disable("Used illegal weapon.", DisableFlags.WriteToLog);
                            player.SendErrorMessage("Illegally prefixed weapons are not allowed in PvP");
                            break;
                        }
                    }
                } else if (player.ItemInHand.ranged || player.SelectedItem.ranged) {
                    foreach (int prefixes in illegalRangedPrefixes) {
                        if (player.ItemInHand.prefix == prefixes || player.SelectedItem.prefix == prefixes) {
                            player.Disable("Used illegal weapon.", DisableFlags.WriteToLog);
                            player.SendErrorMessage("Illegally prefixed weapons are not allowed in PvP");
                            break;
                        }
                    }
                } else if (player.ItemInHand.magic || player.SelectedItem.magic || player.ItemInHand.summon || player.SelectedItem.summon || player.ItemInHand.DD2Summon || player.SelectedItem.DD2Summon) {
                    foreach (int prefixes in illegalMagicPrefixes) {
                        if (player.ItemInHand.prefix == prefixes || player.SelectedItem.prefix == prefixes) {
                            player.Disable("Used illegal weapon.", DisableFlags.WriteToLog);
                            player.SendErrorMessage("Illegally prefixed weapons are not allowed in PvP");
                            break;
                        }
                    }
                }
            }

            //Checks whether a player has prefixed ammo
            if (!player.HasPermission("pvpchecks.useprefixedammo") && (player.ItemInHand.ranged || player.SelectedItem.ranged)) {
                foreach (int ammo in DataIDs.ammoIDs) {
                    foreach (Item inventory in player.TPlayer.inventory) {
                        if (inventory.netID == ammo && inventory.prefix != 0) {
                            player.Disable("Used prefixed ammo.", DisableFlags.WriteToLog);
                            player.SendErrorMessage("Please remove the prefixed ammo for PvP: " + inventory.Name);
                            break;
                        }
                    }
                }
            }

            //Checks whether a player is wearing prefixed armour
            if (!player.HasPermission("pvpchecks.useprefixedarmor")) {
                for (int index = 0; index < 3; index++) {
                    if (player.TPlayer.armor[index].prefix != 0) {
                        player.Disable("Used prefixed armour.", DisableFlags.WriteToLog);
                        player.SendErrorMessage("Please remove the prefixed armour for PvP: " + player.TPlayer.armor[index].Name);
                        break;
                    }
                }
            }

            //Checks whether a player is wearing duplicate accessories/armor
            //To all you code diggers, the bool in the Dictionary serves no purpose here
            if (!player.HasPermission("pvpchecks.havedupeaccessories")) {
                Dictionary<int, bool> duplicate = new Dictionary<int, bool>();
                foreach (Item equips in player.TPlayer.armor) {
                    if (duplicate.ContainsKey(equips.netID)) {
                        player.Disable("Used duplicate accessories.", DisableFlags.WriteToLog);
                        player.SendErrorMessage("Please remove the duplicate accessory for PvP: " + equips.Name);
                        break;
                    } else if (equips.netID != 0) {
                        duplicate.Add(equips.netID, true);
                    }
                }
            }

            //Checks whether the player is using the unobtainable 7th accessory slot
            if (!player.HasPermission("pvpchecks.use7thslot")) {
                if (player.TPlayer.armor[9].netID != 0) {
                    player.Disable("Used 7th accessory slot.", DisableFlags.WriteToLog);
                    player.SendErrorMessage("The 7th accessory slot cannot be used in PvP.");
                }
            }
        }

        private void BanWeapon(CommandArgs args) {
            if (args.Parameters.Count >= 2 && (args.Parameters[0] == "add" || args.Parameters[0] == "del")) {
                string input =  string.Join(" ", args.Parameters.Skip(1).ToArray());
                string weaponname;
                
                List<string> weaponlist = new List<string>();
                List<Item> foundweapons = TShock.Utils.GetItemByName(input);

                foreach (Item item in foundweapons) {
                    weaponlist.Add(item.Name);
                }

                if (weaponlist.Count < 1) {
                    args.Player.SendErrorMessage("No items by that name were found.");
                    return;
                } else if (weaponlist.Count > 1) {
                    TShock.Utils.SendMultipleMatchError(args.Player, weaponlist);
                    return;
                } else {
                    weaponname = weaponlist[0];
                }

                switch (args.Parameters[0]) {
                    case "add":
                        weaponbans.Add(weaponname);
                        args.Player.SendSuccessMessage("Banned " + weaponname + " in pvp.");
                        break;

                    case "del":
                        weaponbans.Remove(weaponname);
                        args.Player.SendSuccessMessage("Unbanned " + weaponname + " in pvp.");
                        break;
                }
            } else {
                args.Player.SendErrorMessage("Incorrect syntax. /banweapon <add/del> <Weapon Name>");
            }
        }

        private void PvPWeaponBans(CommandArgs args) {
            if (args.Player == null)
                return;
            string str = "The following weapons cannot be used in PvP: ";
            int num = 0;
            foreach (string weapon in weaponbans) {
                if (num != 0) str += ", ";
                str += weapon;
                ++num;
            }
            args.Player.SendInfoMessage(str + ".");
        }

        private void BanBuff(CommandArgs args) {
            if (args.Parameters.Count == 2) {
                int buffid;

                if (!Int32.TryParse(args.Parameters[1], out buffid)) {
                    List<int> bufflist = new List<int>();
                    bufflist = TShock.Utils.GetBuffByName(args.Parameters[1]);

                    if (bufflist.Count < 1) {
                        args.Player.SendErrorMessage("No buffs by that name were found.");
                        return;
                    } else if (bufflist.Count > 1) {
                        TShock.Utils.SendMultipleMatchError(args.Player, bufflist.Select(p => TShock.Utils.GetBuffName(p)));
                        return;
                    } else {
                        buffid = bufflist[0];
                    }
                }

                switch(args.Parameters[0]) {
                    case "add":
                        buffbans.Add(buffid);
                        args.Player.SendSuccessMessage("Banned " + TShock.Utils.GetBuffName(buffid) + " in pvp.");
                        break;

                    case "del":
                        buffbans.Remove(buffid);
                        args.Player.SendSuccessMessage("Unbanned " + TShock.Utils.GetBuffName(buffid) + " in pvp.");
                        break;

                    default:
                        args.Player.SendErrorMessage("Incorrect syntax. /banbuff <add/del> <Buff Name/id>");
                        break;
                }
            } else {
                args.Player.SendErrorMessage("Incorrect syntax. /banbuff <add/del> <Buff Name/id>");
            }
        }

        private void PvPBuffBans(CommandArgs args) {
            if (args.Player == null)
                return;
            string str = "The following buffs cannot be used in PvP: ";
            int num = 0;
            foreach (int buff in buffbans) {
                if (num != 0) str += ", ";
                str += TShock.Utils.GetBuffName(buff);
                ++num;
            }
            args.Player.SendInfoMessage(str + ".");
        }
    }
}
