

using wServer.logic.behaviors;
using wServer.logic.transitions;
using wServer.logic.loot;
using common.resources;
namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ DarkLord = () => Behav()
        .Init("Dark Lord",
                new State(
                    new State(
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("default",
                        new PlayerWithinTransition(6, "taunt1")
                        ),
                    new State("taunt1",
                        new Taunt("Welcome to my lair!"),
                        new TimedTransition(10000, "fight1")
                        )
                    ),
                    new State(                        
                        new HpLessTransition(0.2, "ragestart"),
                    new State("fight1",
                        new Prioritize(
                            new StayCloseToSpawn(0.5, 3),
                            new Wander(0.05)
                          ),
                        new Shoot(10, 8, shootAngle: 10, projectileIndex: 0, predictive: 0.2, coolDown: new Cooldown(1000, 2000)),
                        new Grenade(6, 100, range: 6, coolDown: 2000, effect: ConditionEffectIndex.Weak, effectDuration: 6000, color: 0x000000),
                        new TimedTransition(6000, "fight2")
                        ),
                      new State("fight2",
                        new Prioritize(
                            new StayCloseToSpawn(0.5, 3),
                            new Wander(0.05)
                          ),
                        new TimedTransition(10000, "segment"),
                        new State("Quadforce1",
                            new Shoot(0, projectileIndex: 1, count: 5, shootAngle: 70, fixedAngle: 0, coolDown: 300),
                            new TimedTransition(70, "Quadforce2")
                        ),
                        new State("Quadforce2",
                            new Shoot(0, projectileIndex: 1, count: 5, shootAngle: 70, fixedAngle: 15, coolDown: 300),
                            new TimedTransition(70, "Quadforce3")
                        ),
                        new State("Quadforce3",
                            new Shoot(0, projectileIndex: 1, count: 5, shootAngle: 70, fixedAngle: 30, coolDown: 300),
                            new TimedTransition(70, "Quadforce4")
                        ),
                        new State("Quadforce4",
                            new Shoot(0, projectileIndex: 1, count: 5, shootAngle: 70, fixedAngle: 45, coolDown: 300),
                            new TimedTransition(100, "Quadforce41")
                        ),
                        new State("Quadforce41",
                            new Shoot(0, projectileIndex: 1, count: 5, shootAngle: 70, fixedAngle: 60, coolDown: 300),
                            new TimedTransition(70, "Quadforce51")
                        ),
                        new State("Quadforce51",
                            new Shoot(0, projectileIndex: 1, count: 5, shootAngle: 70, fixedAngle: 60, coolDown: 300),
                            new TimedTransition(70, "Quadforce5")
                        ),
                        new State("Quadforce5",
                            new Shoot(0, projectileIndex: 1, count: 5, shootAngle: 70, fixedAngle: 45, coolDown: 300),
                            new TimedTransition(70, "Quadforce6")
                        ),
                        new State("Quadforce6",
                            new Shoot(0, projectileIndex: 1, count: 5, shootAngle: 70, fixedAngle: 30, coolDown: 300),
                            new TimedTransition(70, "Quadforce7")
                        ),
                        new State("Quadforce7",
                            new Shoot(0, projectileIndex: 1, count: 5, shootAngle: 70, fixedAngle: 15, coolDown: 300),
                            new TimedTransition(70, "Quadforce8")
                        ),
                        new State("Quadforce8",
                            new Shoot(0, projectileIndex: 1, count: 5, shootAngle: 70, fixedAngle: 0, coolDown: 300),
                            new TimedTransition(70, "Quadforce1")
                        )
                    ),
                   new State("segment",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("You're pissing me off!"),
                        new Flash(0xFFFFFF, 0.25, 6),
                        new TimedTransition(4000, "fight3")
                        ),
                    new State(
                        new Prioritize(
                            new StayCloseToSpawn(0.5, 3),
                            new Wander(0.05)
                          ),
                    new ConditionalEffect(ConditionEffectIndex.Armored),
                    new TimedTransition(16000, "fight4"),
                    new Shoot(10, count: 18, projectileIndex: 4, coolDown: 4000),
                    new State("fight3",
                        new Follow(1.5, 20, 1),
                        new Shoot(10, 14, shootAngle: 12, projectileIndex: 2, fixedAngle: 0, coolDown: 400),
                        new TimedTransition(2000, "fight3a")
                        ),
                    new State("fight3a",
                        new Shoot(10, 14, shootAngle: 12, projectileIndex: 2, fixedAngle: 45, coolDown: 400),
                        new TimedTransition(2000, "fight3b")
                        ),
                    new State("fight3b",
                        new Shoot(10, 14, shootAngle: 12, projectileIndex: 2, fixedAngle: 90, coolDown: 400),
                        new TimedTransition(2000, "fight3c")
                        ),
                    new State("fight3c",
                        new Shoot(10, 14, shootAngle: 12, projectileIndex: 2, fixedAngle: 135, coolDown: 400),
                        new TimedTransition(2000, "fight3d")
                        ),
                    new State("fight3d",
                        new Shoot(10, 14, shootAngle: 12, projectileIndex: 2, fixedAngle: 180, coolDown: 400),
                        new TimedTransition(2000, "fight3e")
                        ),
                    new State("fight3e",
                        new Shoot(10, 14, shootAngle: 12, projectileIndex: 2, fixedAngle: 225, coolDown: 400),
                        new TimedTransition(2000, "fight3f")
                        ),
                    new State("fight3f",
                        new Shoot(10, 14, shootAngle: 12, projectileIndex: 2, fixedAngle: 270, coolDown: 400),
                        new TimedTransition(2000, "fight3e")
                        ),
                    new State("fight3e",
                        new Shoot(10, 14, shootAngle: 12, projectileIndex: 2, fixedAngle: 315, coolDown: 400),
                        new TimedTransition(2000, "fight3")
                        )
                    ),
                    new State("fight4",
                        new Prioritize(
                            new StayCloseToSpawn(0.5, 3),
                            new Wander(0.05)
                          ),
                        new Shoot(10, 6, shootAngle: 10, projectileIndex: 2, predictive: 0.6, coolDown: new Cooldown(1000, 2000)),
                        new Shoot(10, 4, shootAngle: 12, projectileIndex: 3, coolDownOffset: 800, coolDown: 2000),
                        new Shoot(10, 6, shootAngle: 10, projectileIndex: 3, coolDownOffset: 1600, coolDown: 2000),
                        new Shoot(10, 8, shootAngle: 8, projectileIndex: 3, coolDownOffset: 2400, coolDown: 2000),
                        new TimedTransition(10000, "fight5")
                        ),
                    new State("fight5",
                        new Prioritize(
                            new StayCloseToSpawn(0.5, 3),
                            new Wander(0.05)
                          ),
                        new Shoot(10, count: 1, projectileIndex: 0, fixedAngle: 0, coolDown: 200),
                        new Shoot(10, count: 1, projectileIndex: 0, fixedAngle: 90, coolDown: 200),
                        new Shoot(10, count: 1, projectileIndex: 0, fixedAngle: 270, coolDown: 200),
                        new Shoot(10, count: 1, projectileIndex: 0, fixedAngle: 180, coolDown: 200),
                        new Shoot(10, 5, shootAngle: 10, projectileIndex: 0, coolDown: 400),
                        new Grenade(6, 100, range: 6, coolDown: 2000, effect: ConditionEffectIndex.Sick, effectDuration: 6000, color: 0x000000),
                        new TimedTransition(6000, "fight1")
                            )
                        ),
                      new State("ragestart",
                        new HealSelf(coolDown: 9999, amount: 20000),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Flash(0xFF0000, 0.25, 8),
                        new TimedTransition(6000, "rage")
                        ),
                      new State("rage",
                        new Prioritize(
                            new Follow(0.4, 10),
                            new Wander(0.05)
                          ),
                        new Shoot(10, 8, projectileIndex: 3, coolDown: 1400),
                        new Shoot(10, 5, shootAngle: 10, projectileIndex: 4, predictive: 0.6, coolDown: new Cooldown(1000, 2000)),
                        new Shoot(10, 3, shootAngle: 30, projectileIndex: 2, coolDown: 600)
                        )
                    ),
                new Threshold(0.025,
                    new ItemLoot("Potion of Speed", 0.33),
                    new ItemLoot("Potion of Defense", 1.0),
                    new ItemLoot("Demon Blade", 0.01),
                    new ItemLoot("Magma Bow", 0.008),
                    new ItemLoot("Fireball Trap", 0.008),
                    new ItemLoot("Fire Cloth Armor", 0.007),
                    new ItemLoot("The Lords Gemstone", 0.008),
                    new ItemLoot("Rose of Flames", 0.007),
                    new ItemLoot("Abyssal Flames Spell", 0.007),
                    new ItemLoot("Robes of the Abyss", 0.008),
                    new ItemLoot("Binding Stone", 0.007),
                    new ItemLoot("Demons Wand", 0.007),
                    new ItemLoot("Satanic Robe", 0.007),
                    new ItemLoot("Tome of Sins", 0.007),
                    new TierLoot(9, ItemType.Weapon, 0.1),
                    new TierLoot(6, ItemType.Ability, 0.1),
                    new TierLoot(9, ItemType.Armor, 0.1),
                    new TierLoot(3, ItemType.Ring, 0.05),
                    new TierLoot(13, ItemType.Armor, 0.05),
                    new TierLoot(13, ItemType.Weapon, 0.05),
                    new TierLoot(5, ItemType.Ring, 0.025)
                )
            )
           
            ;
    }
}
    