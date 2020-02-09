using System;
using System.Collections.Generic;
using System.Linq;
using common.resources;
using wServer.realm.entities;
using wServer.realm;
using wServer.realm.worlds.logic;
using wServer.networking.packets.outgoing;
using wServer.networking;

namespace wServer.logic.loot
{
    public class LootDef
    {
        public readonly Item Item;
        public readonly double Probabilty;
        public readonly int NumRequired;
        public readonly double Threshold;

        public LootDef(Item item, double probabilty, int numRequired, double threshold)
        {
            Item = item;
            Probabilty = probabilty;
            NumRequired = numRequired;
            Threshold = threshold;
        }
    }

    public class Loot : List<MobDrops>
    {
        static readonly Random Rand = new Random();

        static readonly ushort BROWN_BAG = 0x0500;
        static readonly ushort PINK_BAG = 0x0506;
        static readonly ushort PURPLE_BAG = 0x0507;
        static readonly ushort EGG_BASKET = 0x0508;
        static readonly ushort CYAN_BAG = 0x0509;
        static readonly ushort POTION_BAG = 0x050B;
        static readonly ushort WHITE_BAG = 0x050C;
        static readonly ushort WHITE_BAG_BIG = 0x050E;
        static readonly ushort UNHOLY_BAG = 0x0513;
        static readonly ushort UNHOLY_BAG_BIG = 0x0512;
        static readonly ushort RED_BAG = 0x0510;

        public Loot(params MobDrops[] lootDefs)   //For independent loots(e.g. chests)
        {
            AddRange(lootDefs);
        }

        public IEnumerable<Item> GetLoots(RealmManager manager, int min, int max)   //For independent loots(e.g. chests)
        {
            var consideration = new List<LootDef>();
            foreach (var i in this)
            {
                i.Populate(consideration);
            }

            var retCount = Rand.Next(min, max);
            foreach (var i in consideration)
            {
                if (Rand.NextDouble() < i.Probabilty)
                {
                    yield return i.Item;
                    retCount--;
                }
                if (retCount == 0)
                {
                    yield break;
                }
            }
        }

        private List<LootDef> GetPossibleDrops()
        {
            var possibleDrops = new List<LootDef>();
            foreach (var i in this)
            {
                i.Populate(possibleDrops);
            }
            return possibleDrops;
        }

        public void Handle(Enemy enemy, RealmTime time, params Player[] players)
        {
            // enemies that shouldn't drop loot
            if (enemy.Owner is Arena || enemy.Owner is ArenaSolo)
            {
                return;
            }

            // generate a list of all possible loot drops
            var possibleDrops = GetPossibleDrops();
            var possibleWorldDrops = enemy.Owner.WorldLoot.GetPossibleDrops();
            foreach (var wDrop in possibleWorldDrops.Where(drop => drop.Item.ObjectType != 0xa22 && drop.Item.ObjectType != 0xa23))
            {
                // so we can override drops with world drops if we want to. For example, Oryx's arena key not dropping in OA.
                possibleDrops.RemoveAll(d => d.Item == wDrop.Item);
            }
            possibleDrops.AddRange(possibleWorldDrops); // adds world drops
            var reqDrops = possibleDrops.ToDictionary(drop => drop, drop => drop.NumRequired);

            // generate public loot
            var publicLoot = new List<Item>();
            foreach (var i in possibleDrops)
            {
                if (i.Threshold <= 0 && Rand.NextDouble() < i.Probabilty)
                {
                    publicLoot.Add(i.Item);
                    reqDrops[i]--;
                }
            }

            // generate individual player loot
            var eligiblePlayers = enemy.DamageCounter.GetPlayerData();
            var privateLoot = new Dictionary<Player, IList<Item>>();
            foreach (var player in eligiblePlayers)
            {
                var lootDropBoost = player.Item1.LDBoostTime > 0 ? 1.5 : 1;
                var luckStatBoost = 1 + player.Item1.Stats.Boost[10] / 100.0;

                var loot = new List<Item>();

                if (players.Rank == 10)
                {
                    foreach (var i in possibleDrops)
                    {
                        if (i.Threshold > 0 && i.Threshold <= player.Item2 &&
                            Rand.NextDouble() < i.Probabilty * lootDropBoost * luckStatBoost + (i.Probabilty / 100 * 10))
                        {
                            loot.Add(i.Item);
                            reqDrops[i]--;
                        }
                    }
                }
                else if (players.Rank == 20)
                {
                    foreach (var i in possibleDrops)
                    {
                        if (i.Threshold > 0 && i.Threshold <= player.Item2 &&
                            Rand.NextDouble() < i.Probabilty * lootDropBoost * luckStatBoost + (i.Probabilty / 100 * 15))
                        {
                            loot.Add(i.Item);
                            reqDrops[i]--;
                        }
                    }
                }
                else if (players.Rank == 30)
                {
                    foreach (var i in possibleDrops)
                    {
                        if (i.Threshold > 0 && i.Threshold <= player.Item2 &&
                            Rand.NextDouble() < i.Probabilty * lootDropBoost * luckStatBoost + (i.Probabilty / 100 * 30))
                        {
                            loot.Add(i.Item);
                            reqDrops[i]--;
                        }
                    }
                }
                else if (players.Rank == 60 || players.Rank == 70 || players.Rank == 80)
                {
                    foreach (var i in possibleDrops)
                    {
                        if (i.Threshold > 0 && i.Threshold <= player.Item2 &&
                            Rand.NextDouble() < i.Probabilty * lootDropBoost * luckStatBoost + (i.Probabilty / 100 * 40))
                        {
                            loot.Add(i.Item);
                            reqDrops[i]--;
                        }
                    }
                }
                else if (players.Rank >= 90)
                {
                    foreach (var i in possibleDrops)
                    {
                        if (i.Threshold > 0 && i.Threshold <= player.Item2 &&
                            Rand.NextDouble() < i.Probabilty * lootDropBoost * luckStatBoost + (i.Probabilty / 100 * 50))
                        {
                            loot.Add(i.Item);
                            reqDrops[i]--;
                        }
                    }
                }
                else
                    foreach (var i in possibleDrops)
                    {
                        if (i.Threshold > 0 && i.Threshold <= player.Item2 &&
                            Rand.NextDouble() < i.Probabilty * lootDropBoost * luckStatBoost)
                        {
                            loot.Add(i.Item);
                            reqDrops[i]--;
                        }
                    }
                
                privateLoot[player.Item1] = loot;
            }

            // add required drops that didn't drop already
            foreach (var i in possibleDrops)
            {
                if (i.Threshold <= 0)
                {
                    // add public required loot
                    while (reqDrops[i] > 0)
                    {
                        publicLoot.Add(i.Item);
                        reqDrops[i]--;
                    }
                    continue;
                }

                // add private required loot
                var ePlayers = eligiblePlayers.Where(p => i.Threshold <= p.Item2).ToList();
                if (ePlayers.Count() <= 0)
                {
                    continue;
                }

                while (reqDrops[i] > 0 && ePlayers.Count() > 0)
                {
                    // make sure a player doesn't recieve more than one required loot
                    var reciever = ePlayers.RandomElement(Rand);
                    ePlayers.Remove(reciever);

                    // don't assign item if player already recieved one with random chance
                    if (privateLoot[reciever.Item1].Contains(i.Item))
                    {
                        continue;
                    }

                    privateLoot[reciever.Item1].Add(i.Item);
                    reqDrops[i]--;
                }
            }

            AddBagsToWorld(enemy, publicLoot, privateLoot);
        }

        private void AddBagsToWorld(Enemy enemy, IList<Item> shared, IDictionary<Player, IList<Item>> playerLoot)
        {
            foreach (var i in playerLoot)
            {
                if (i.Value.Count > 0)
                {
                    ShowBags(enemy, i.Value, i.Key);
                }
            }
            ShowBags(enemy, shared);
        }

        private static readonly string[] ValuableItems = {
            "Apple"
        };

        private static void ShowBags(Enemy enemy, IEnumerable<Item> loots, params Player[] owners)
        {
            var ownerIds = owners.Select(x => x.AccountId).ToArray();
            var items = new Item[8];
            var idx = 0;
            var boosted = false;

            var bagType = 0;

            if (owners.Count() == 1 && owners[0].LDBoostTime > 0)
            {
                boosted = true;
            }


            foreach (var i in loots)
            {
                if (i.BagType > bagType)
                {
                    bagType = i.BagType;
                }
                if (i.BagType == 6)
                                    {
                    owners[0].BroadcastSync(new Notification()
                    {
                        ObjectId = owners[0].Id,
                           Color = new ARGB(0xffffff),
                            Message = "White Bag!",
                    });
                    owners[0].SendWhiteBag(i.ObjectId.ToString());
                                    }
                items[idx] = i;
                idx++;
                
                if (ValuableItems.Contains(i.ObjectId))
                    foreach (var p in enemy.Owner.Players.Values)
                        p.SendHelp("<" + owners[0].Name + "> has dropped a ["
                                                  + i.ObjectId + "]");

                if (idx != 8)
                {
                    continue;
                }

                ShowBag(enemy, ownerIds, bagType, items, boosted);

                bagType = 0;
                items = new Item[8];
                idx = 0;
            }

            if (idx > 0)
            {
                ShowBag(enemy, ownerIds, bagType, items, boosted);
            }
        }

        private static void ShowBag(Enemy enemy, int[] owners, int bagType, Item[] items, bool boosted)
        {
            ushort bag = BROWN_BAG;
            switch (bagType)
            {
                case 0: bag = BROWN_BAG; break;       //Brown Bag
                case 1: bag = PINK_BAG; break;        //Pink Bag
                case 2: bag = PURPLE_BAG; break;      //Purple Bag
                case 3: bag = EGG_BASKET; break;      //Basket of Eggs
                case 4: bag = CYAN_BAG; break;        //Cyan Bag
                case 5: bag = POTION_BAG; break;      //Blue Bag
                case 6: bag = WHITE_BAG; break;       //White Bag
                case 7: bag = WHITE_BAG_BIG; break;   //White Bag Big
                case 8: bag = UNHOLY_BAG; break;      //Unholy Bag Regular
                case 9: bag = UNHOLY_BAG_BIG; break;  //Unholy Bag Rare
            }

            // allow white bags to override boosted bag
            if (boosted && bag < 6)
            {
                bag = RED_BAG;
            }

            var container = new Container(enemy.Manager, bag, 1000 * 60, true);
            for (int j = 0; j < 8; j++)
            {
                container.Inventory[j] = items[j];
            }
            container.BagOwners = owners;
            container.Move(
                enemy.X + (float)((Rand.NextDouble() * 2 - 1) * 0.5),
                enemy.Y + (float)((Rand.NextDouble() * 2 - 1) * 0.5));

            container.SetDefaultSize(bagType > 3 ? 120 : 80);
            enemy.Owner.EnterWorld(container);
        }
    }
}
