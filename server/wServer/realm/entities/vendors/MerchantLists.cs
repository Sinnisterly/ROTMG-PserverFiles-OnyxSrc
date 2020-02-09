using System;
using System.Collections.Generic;
using System.Linq;
using common;
using common.resources;
using log4net;
using wServer.realm.terrain;

namespace wServer.realm.entities.vendors
{
    public class ShopItem : ISellableItem
    {
        public ushort ItemId { get; private set; }
        public int Price { get; }
        public int Count { get; }
        public string Name { get; }

        public ShopItem(string name, ushort price, int count = -1)
        {
            ItemId = ushort.MaxValue;
            Price = price;
            Count = count;
            Name = name;
        }

        public void SetItem(ushort item)
        {
            if (ItemId != ushort.MaxValue)
                throw new AccessViolationException("Can't change item after it has been set.");

            ItemId = item;
        }
    }
    
    internal static class MerchantLists
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MerchantLists));

            /*
             * 20 Fame = 1 Gold    
             */

        private static readonly List<ISellableItem> Weapons = new List<ISellableItem>
        {
            new ShopItem("Dagger of Foul Malevolence", 50),     //1000 fame
            new ShopItem("Bow of Covert Havens", 50),           //1000 fame
            new ShopItem("Staff of the Cosmic Whole", 50),      //1000 fame
            new ShopItem("Wand of Recompense", 50),             //1000 fame
            new ShopItem("Sword of Acclaim", 50),               //1000 fame
            new ShopItem("Masamune", 50),                        //1000 fam
            new ShopItem("Rose of Flames", 8500),
            new ShopItem("Icebound Wand", 8000),
            new ShopItem("Icicle", 9000),
            new ShopItem("Malphas Respite", 9000),
            new ShopItem("Wand of the Apex Bulwark", 13000),
            new ShopItem("Ember Blade", 6000)
        };

        private static readonly List<ISellableItem> Abilities = new List<ISellableItem>
        {
            new ShopItem("Cloak of Ghostly Concealment", 50),  //1000 fame
            new ShopItem("Quiver of Elvish Mastery", 50),      //1000 fame
            new ShopItem("Elemental Detonation Spell", 50),    //1000 fame
            new ShopItem("Tome of Holy Guidance", 50),         //1000 fame
            new ShopItem("Helm of the Great General", 50),     //1000 fame
            new ShopItem("Colossus Shield", 50),               //1000 fame
            new ShopItem("Seal of the Blessed Champion", 50),  //1000 fame
            new ShopItem("Baneserpent Poison", 50),            //1000 fame
            new ShopItem("Bloodsucker Skull", 50),             //1000 fame
            new ShopItem("Giantcatcher Trap", 50),             //1000 fame
            new ShopItem("Planefetter Orb", 50),               //1000 fame
            new ShopItem("Prism of Apparitions", 50),          //1000 fame
            new ShopItem("Scepter of Storms", 50),             //1000 fame
            new ShopItem("Ember Helmet", 5000),
            new ShopItem("Abyssal Flames Spell", 5000),
            new ShopItem("Frozen In Time", 5000),
            new ShopItem("Dry Ice Toxin", 3000),
            new ShopItem("Doom Circle", 50)                    //1000 fame
        };

        private static readonly List<ISellableItem> Armor = new List<ISellableItem>
        {
            new ShopItem("Robe of the Illusionist", 5),        //100 fame
            new ShopItem("Robe of the Grand Sorcerer", 50),    //1000 fame
            new ShopItem("Studded Leather Armor", 5),          //100 fame
            new ShopItem("Hydra Skin Armor", 50),              //1000 fame
            new ShopItem("Mithril Armor", 5),                  //100 fame
            new ShopItem("Ember Armor", 5000),
            new ShopItem("Robes of the Abyss", 5000),
            new ShopItem("Frozen Robes", 5000),
            new ShopItem("Frozen Armor", 5000),
            new ShopItem("Acropolis Armor", 50)                //1000 fame
        };

        private static readonly List<ISellableItem> Rings = new List<ISellableItem>
        {
            new ShopItem("Ring of Paramount Attack", 5),       //100 fame
            new ShopItem("Ring of Paramount Defense", 5),      //100 fame
            new ShopItem("Ring of Paramount Speed", 5),        //100 fame
            new ShopItem("Ring of Paramount Dexterity", 5),    //100 fame
            new ShopItem("Ring of Paramount Vitality", 5),     //100 fame
            new ShopItem("Ring of Paramount Wisdom", 5),       //100 fame
            new ShopItem("Ring of Paramount Health", 5),       //100 fame
            new ShopItem("Ring of Paramount Magic", 5),        //100 fame
            new ShopItem("Ring of Unbound Attack", 75),        //1500 fame
            new ShopItem("Ring of Unbound Defense", 75),       //1500 fame
            new ShopItem("Ring of Unbound Speed", 75),         //1500 fame
            new ShopItem("Ring of Unbound Dexterity", 75),     //1500 fame
            new ShopItem("Ring of Unbound Vitality", 75),      //1500 fame
            new ShopItem("Ring of Unbound Wisdom", 75),        //1500 fame
            new ShopItem("Ring of Unbound Health", 75),        //1500 fame
            new ShopItem("The Ember Pendant", 5000),
            new ShopItem("Droplet Pendant", 5000),
            new ShopItem("Hellfire Necklace", 5000),
            new ShopItem("Ring of Unbound Magic", 75)          //1500 fame
        };

        private static readonly List<ISellableItem> Keys = new List<ISellableItem>
        {
            new ShopItem("Undead Lair Key", 150),                //40 fame
            new ShopItem("Sprite World Key", 30),               //
            new ShopItem("Davy's Key", 30),                     //
            new ShopItem("The Crawling Depths Key", 30),       //
            new ShopItem("Candy Key", 40),                      //
            new ShopItem("Abyss of Demons Key", 100),            //
            new ShopItem("Totem Key", 30),                      //60 fame
            new ShopItem("Pirate Cave Key", 10),                //20 fame
            new ShopItem("Ivory Wyvern Key", 80),               //160 fame
            new ShopItem("Lab Key", 20),                        //40 fame
            new ShopItem("Manor Key", 30),                      //60 fame
            new ShopItem("Cemetery Key", 50),                   //100 fame
            new ShopItem("Ocean Trench Key", 10),              //200 fame
            new ShopItem("Snake Pit Key", 20),                  //40 fame
            new ShopItem("Bella's Key", 50),                    //100 fame
            new ShopItem("Shaitan's Key", 55),                 //300 fame
            new ShopItem("Spider Den Key", 10),                 //20 fame
            new ShopItem("Tomb of the Ancients Key", 100),      //200 fame
            new ShopItem("Theatre Key", 80),                    //80 fame
            new ShopItem("Ice Cave Key", 150),                   //160 fame
            //Custom / X8
            new ShopItem("Renewed Sewers Key", 350),
            new ShopItem("Vampire Underling Key", 1000),     //60 fame
            new ShopItem("Phantom Lord Key", 1000),     //60 fame
            new ShopItem("Dark Lord Key", 1000),     //60 fame
            new ShopItem("The Hive Key", 150)                    //20 fame
        };

        private static readonly List<ISellableItem> PurchasableCurrency = new List<ISellableItem>
        {
            //Gold
            //new ShopItem("10 Gold", 10),                //200 fame
            //Fame
            new ShopItem("40 Fame", 42),                 //40 fame
            new ShopItem("100 Fame", 105),                //100 fame
            new ShopItem("500 Fame", 525),               //500 fame
            new ShopItem("1000 Fame", 1050),              //1000 fame
            new ShopItem("5000 Fame", 5250),             //5000 fame
        };

        private static readonly List<ISellableItem> Consumables = new List<ISellableItem>
        {
            new ShopItem("Saint Patty's Brew", 5),      //100 fame
            new ShopItem("Mad God Ale", 5),             //100 fame
            new ShopItem("Backpack", 1250),              //2500 fame
            new ShopItem("Loot Tier Potion", 100),
            new ShopItem("Loot Drop Potion", 100),
            new ShopItem("XP Booster 1 hr", 10),
        };
        private static readonly List<ISellableItem> Extras = new List<ISellableItem>
        {
            new ShopItem("Backpack", 5000),      //100 fame
            new ShopItem("Greater Potion of Life", 3000),             //100 fame
            new ShopItem("Greater Potion of Mana", 3000),              //2500 fame
            new ShopItem("Greater Potion of Vitality", 1000),
            new ShopItem("Greater Potion of Attack", 1000),
            new ShopItem("Greater Potion of Defense", 1000),
            new ShopItem("Greater Potion of Wisdom", 1000),
            //new ShopItem("Vault Chest Unlocker", 2000),
            new ShopItem("Greater Potion of Dexterity", 1000),
            new ShopItem("Loot Tier Potion", 200),
            new ShopItem("Loot Drop Potion", 200),
            new ShopItem("XP Booster 1 hr", 100),
            new ShopItem("Greater Potion of Speed", 1000),
        };

        private static readonly List<ISellableItem> Unholy_LGs = new List<ISellableItem>
        {
            new ShopItem("Dark Helm", 5),               //100 fame
            new ShopItem("Unholy Spell", 5),            //100 fame
        };

        private static readonly List<ISellableItem> DonatorIsleCurrency = new List<ISellableItem>
        {
            //Gold
            new ShopItem("10 Gold", 5),                //10 gold
            new ShopItem("20 Gold", 10),                //20 gold
            new ShopItem("40 Gold", 20),                //40 gold
            new ShopItem("Backpack", 50),      //100 fame
            new ShopItem("Greater Potion of Life", 30),             //100 fame
            new ShopItem("Greater Potion of Mana", 30),              //2500 fame
            new ShopItem("Greater Potion of Vitality", 10),
            new ShopItem("Greater Potion of Attack", 10),
            new ShopItem("Greater Potion of Defense", 10),
            new ShopItem("Greater Potion of Wisdom", 10),
            //new ShopItem("Vault Chest Unlocker", 2000),
            new ShopItem("Greater Potion of Dexterity", 10),
            new ShopItem("Loot Tier Potion", 20),
            new ShopItem("Loot Drop Potion", 20),
            new ShopItem("XP Booster 1 hr", 10),
            new ShopItem("Greater Potion of Speed", 10),
            new ShopItem("Undead Lair Key", 15),                //40 fame
            new ShopItem("Sprite World Key", 30),               //
            new ShopItem("Davy's Key", 30),                     //
            new ShopItem("The Crawling Depths Key", 30),       //
            new ShopItem("Candy Key", 40),                      //
            new ShopItem("Abyss of Demons Key", 10),            //
            new ShopItem("Totem Key", 30),                      //60 fame
            new ShopItem("Pirate Cave Key", 10),                //20 fame
            new ShopItem("Ivory Wyvern Key", 80),               //160 fame
            new ShopItem("Lab Key", 20),                        //40 fame
            new ShopItem("Manor Key", 30),                      //60 fame
            new ShopItem("Cemetery Key", 50),                   //100 fame
            new ShopItem("Ocean Trench Key", 10),              //200 fame
            new ShopItem("Snake Pit Key", 20),                  //40 fame
            new ShopItem("Bella's Key", 50),                    //100 fame
            new ShopItem("Shaitan's Key", 55),                 //300 fame
            new ShopItem("Spider Den Key", 10),                 //20 fame
            new ShopItem("Tomb of the Ancients Key", 10),      //200 fame
            new ShopItem("Theatre Key", 80),                    //80 fame
            new ShopItem("Ice Cave Key", 150),                   //160 fame
            //Custom / X8
            new ShopItem("Renewed Sewers Key", 35),
            new ShopItem("Vampire Underling Key", 100),     //60 fame
            new ShopItem("Phantom Lord Key", 100),     //60 fame
            new ShopItem("Dark Lord Key", 100),
        };

        public static readonly Dictionary<TileRegion, Tuple<List<ISellableItem>, CurrencyType, /*Rank Req*/int>> Shops = 
            new Dictionary<TileRegion, Tuple<List<ISellableItem>, CurrencyType, int>>()
        {
            { TileRegion.Store_1, new Tuple<List<ISellableItem>, CurrencyType, int>(Weapons, CurrencyType.Fame, 0) },
            { TileRegion.Store_2, new Tuple<List<ISellableItem>, CurrencyType, int>(Abilities, CurrencyType.Fame, 0) },
            { TileRegion.Store_3, new Tuple<List<ISellableItem>, CurrencyType, int>(Armor, CurrencyType.Fame, 0) },
            { TileRegion.Store_4, new Tuple<List<ISellableItem>, CurrencyType, int>(Rings, CurrencyType.Fame, 0) },
            { TileRegion.Store_5, new Tuple<List<ISellableItem>, CurrencyType, int>(Keys, CurrencyType.Gold, 0) },
            { TileRegion.Store_6, new Tuple<List<ISellableItem>, CurrencyType, int>(PurchasableCurrency, CurrencyType.Fame, 5) },
            { TileRegion.Store_7, new Tuple<List<ISellableItem>, CurrencyType, int>(Consumables, CurrencyType.Fame, 2) },
            {TileRegion.Store_8, new Tuple<List<ISellableItem>, CurrencyType, int>(Extras, CurrencyType.Gold, 0) },
            //Store 9 - Market
            //Store 10 - Market
            //Store 11 - Market
            //Store 12 - Market
            //Store 13 - Market
            //Store 14 - Market
            { TileRegion.Store_15, new Tuple<List<ISellableItem>, CurrencyType, int>(Unholy_LGs, CurrencyType.Gold, 28) },
            //Donator Island
            { TileRegion.Store_16, new Tuple<List<ISellableItem>, CurrencyType, int>(DonatorIsleCurrency, CurrencyType.Fame, 0) },
        };
        
        public static void Init(RealmManager manager)
        {
            foreach (var shop in Shops)
                foreach (var shopItem in shop.Value.Item1.OfType<ShopItem>())
                {
                    if (!manager.Resources.GameData.IdToObjectType.TryGetValue(shopItem.Name, out ushort id))
                        Log.WarnFormat("Item name: {0}, not found.", shopItem.Name);
                    else
                        shopItem.SetItem(id);
                }
        }
    }
}