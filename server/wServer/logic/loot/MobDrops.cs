using System;
using System.Collections.Generic;
using System.Linq;
using common.resources;
using log4net;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.loot
{
    public abstract class MobDrops
    {
        protected static XmlData XmlData;
        protected readonly IList<LootDef> LootDefs = new List<LootDef>(); 

        public static void Init(RealmManager manager)
        {
            if (XmlData != null)
                throw new Exception("MobDrops already initialized");

            XmlData = manager.Resources.GameData;
        }

        public virtual void Populate(IList<LootDef> lootDefs, LootDef overrides = null)
        {
            if (overrides == null)
            {
                foreach (var lootDef in LootDefs)
                    lootDefs.Add(lootDef);
                return;
            }

            foreach (var lootDef in LootDefs)
            {
                lootDefs.Add(new LootDef(
                    lootDef.Item, 
                    overrides.Probabilty >= 0 ? overrides.Probabilty : lootDef.Probabilty,
                    overrides.NumRequired >= 0 ? overrides.NumRequired : lootDef.NumRequired,
                    overrides.Threshold >= 0 ? overrides.Threshold : lootDef.Threshold));
            }
        }
    }

    public class ItemLoot : MobDrops
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ItemLoot));

        public ItemLoot(string item, double probability = 1, int numRequired = 0, double threshold = 0)
        {
            try
            {
                LootDefs.Add(new LootDef(
                    XmlData.Items[XmlData.IdToObjectType[item]],
                    probability,
                    numRequired,
                    threshold));
            }
            catch (Exception e)
            {
                Log.Warn($"Problem adding {item} to mob loot table.");
            }
        }
    }

    public class TierLoot : MobDrops
    {
        private static readonly int[] WeaponT = new int[] { 1, 2, 3, 8, 17, 24, };
        private static readonly int[] AbilityT = new int[] { 4, 5, 11, 12, 13, 15, 16, 18, 19, 20, 21, 22, 23, 25, };
        private static readonly int[] ArmorT = new int[] { 6, 7, 14, };
        private static readonly int[] RingT = new int[] { 9 };
        private static readonly int[] PotionT = new int[] { 10 };
        
        public TierLoot(byte tier, ItemType type, double probability = 1, int numRequired = 0, double threshold = 0)
        {
            int[] types;
            switch (type)
            {
                case ItemType.Weapon:
                    types = WeaponT; break;
                case ItemType.Ability:
                    types = AbilityT; break;
                case ItemType.Armor:
                    types = ArmorT; break;
                case ItemType.Ring:
                    types = RingT; break;
                case ItemType.Potion:
                    types = PotionT; break;
                default:
                    throw new NotSupportedException(type.ToString());
            }

            var items = XmlData.Items
                .Where(item => Array.IndexOf(types, item.Value.SlotType) != -1)
                .Where(item => item.Value.Tier == tier)
                .Select(item => item.Value)
                .ToArray();

            foreach (var item in items)
                LootDefs.Add(new LootDef(
                    item,
                    probability / items.Length,
                    numRequired,
                    threshold));
        }
    }

    internal class MostDamagers : MobDrops
    {
        private readonly MobDrops[] _loots;
        private readonly int _amount;

        public MostDamagers(int amount, params MobDrops[] loots)
        {
            _amount = amount;
            _loots = loots;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat, Random rand, IList<LootDef> lootDefs)
        {
            var data = enemy.DamageCounter.GetPlayerData();
            var mostDamage = GetMostDamage(data);
            foreach (var loot in mostDamage.Where(pl => pl.Equals(playerDat)).SelectMany(pl => _loots)) ;
                Populate(lootDefs, null);
        }

        private IEnumerable<Tuple<Player, int>> GetMostDamage(IEnumerable<Tuple<Player, int>> data)
        {
            var damages = data.Select(_ => _.Item2).ToList();
            var len = damages.Count < _amount ? damages.Count : _amount;
            for (var i = 0; i < len; i++)
            {
                var val = damages.Max();
                yield return data.FirstOrDefault(_ => _.Item2 == val);
                damages.Remove(val);
            }
        }
    }

    public class OnlyOne : MobDrops 
    {
        private readonly MobDrops [] _loots;

        public OnlyOne(params MobDrops [] loots)
        {
            _loots = loots;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat, Random rand, IList<LootDef> lootDefs)
        {
            _loots[rand.Next(0, _loots.Length)].Populate(lootDefs, null);
        }
    }

        public class Threshold : MobDrops
    {
        public Threshold(double threshold, params MobDrops[] children)
        {
            foreach (var i in children)
                i.Populate(LootDefs, new LootDef(null, -1, -1, threshold));
        }
    }
}
