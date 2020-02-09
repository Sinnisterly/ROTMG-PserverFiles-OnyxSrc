

using wServer.logic.behaviors;
using wServer.logic.transitions;
using wServer.logic.loot;
using common.resources;
namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ RebornFlower = () => Behav()
        .Init("Reborn Flower",
                new State(
                    new State("StartQueen",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(1000, "Taunt")
                        ),
                    new State("Taunt",
                        new TimedTransition(100, "Phase One")
                        ),
                    new State("Phase One",                       
                        new Shoot(25, projectileIndex: 2, count: 8, shootAngle: 45, coolDown: 2000),
                        new HpLessTransition(.800, "Taunt2")
                        ),
                    new State("Taunt2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),                       
                        new TimedTransition(100, "Phase Two")
                        ),
                    new State("Phase Two",
                        new Shoot(22, count: 5, projectileIndex: 1, shootAngle: 10, coolDown: 2000),
                        new Shoot(22, count: 3, projectileIndex: 0, shootAngle: 15, coolDown: 1000),
                        new StayAbove(.6, 1),
                        new Wander(.2),
                        new StayBack(.1, distance: 4),
                        new StayCloseToSpawn(0.4, 8),
                        new TimedTransition(2000, "Phase Two"),
                        new HpLessTransition(.500, "Phase Three")
                        ),
                    new State("Phase Three",
                        new Shoot(22, count: 3, projectileIndex: 2, shootAngle: 15, coolDown: 750),
                        new Shoot(25, projectileIndex: 0, count: 8, shootAngle: 45, coolDown: 2000),
                        new Wander(.2),
                        new StayCloseToSpawn(0.4, 3),
                        new TimedTransition(750, "Phase Three"),
                        new HpLessTransition(.150, "Taunt3")
                        ),
                    new State("Taunt3",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Flash(0xC90015, 4, 4),
                        new TimedTransition(5000, "Phase Four")
                        ),
                        new State("Phase Four",
                            new StayBack(.1, distance: 4),
                            new Wander(0.2),                            
                            new Shoot(22, count: 2, projectileIndex: 2, shootAngle: 10, coolDown: 1000),
                            new Shoot(25, projectileIndex: 1, count: 8, shootAngle: 45, coolDown: 2000),
                            new HpLessTransition(.005, "Last Taunt")
                                ),
                            new State("Last Taunt",
                                new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                new TimedTransition(3000, "Suicide")
                                ),
                    new State("Suicide",
                        new Suicide()
                        )
                    ),
                    new Threshold(0.1,
                    new TierLoot(10, ItemType.Weapon, 0.07),
                    new TierLoot(11, ItemType.Weapon, 0.06),
                    new TierLoot(12, ItemType.Weapon, 0.05),
                    new TierLoot(5, ItemType.Ability, 0.07),
                    new TierLoot(6, ItemType.Ability, 0.05),
                    new TierLoot(11, ItemType.Armor, 0.07),
                    new TierLoot(12, ItemType.Armor, 0.06),
                    new TierLoot(13, ItemType.Armor, 0.05),
                    new TierLoot(5, ItemType.Ring, 0.06)
                    ),
                new Threshold(0.05,
                    new ItemLoot("Potion of Attack", 0.5),
                    new ItemLoot("100 Gold", 0.05),
                    new ItemLoot("Potion of Defense", 0.5),
                    new ItemLoot("Potion of Wisdom", 0.5)
                    //new ItemLoot("QueensHead", 0.01),
                    //new ItemLoot("RabbitsFoot", 0.01),
                   // new ItemLoot("Card Armor", 0.01)
            )
            )       
        ;
    }
}

