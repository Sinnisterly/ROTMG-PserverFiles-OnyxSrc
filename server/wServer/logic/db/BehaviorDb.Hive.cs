#region
using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;
#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Hive = () => Behav()
        #region Realm
        .Init("Warrior Bee",
                new State(
                    new DropPortalOnDeath("The Hive Portal", 0.5),
                    new StayCloseToSpawn(0.25),
                    new Wander(0.25),
                    new Reproduce("Fighter Bee", densityMax: 2, coolDown: 900),
                    new Reproduce("Scout Bee", densityMax: 5, coolDown: 900),
                    new Shoot(8, 1, coolDown: 1800)
                    )
                    //
                    )

        .Init("Scout Bee",
                new State(
                    new Orbit(0.6, 3, target: "Warrior Bee", speedVariance: 0.1, radiusVariance: 0.3),
                    new Wander(0.3),
                    new Shoot(8, 1, coolDown: 1800)
                    )
                    //
                    )

        .Init("Fighter Bee",
                new State(
                    new Orbit(0.6, 3, target: "Warrior Bee", speedVariance: 0.1, radiusVariance: 0.3),
                    new Wander(0.3),
                    new Shoot(8, 1, coolDown: 1400)
                    )
                    //
                    )
        #endregion

        #region Minions
            .Init("TH Fat Bees",
                new State(
                new TransformOnDeath("TH Red Fat Bees", probability: 0.3),
                    new State("1",
                        new StayCloseToSpawn(0.35),
                        new Wander(0.35),
                        new Shoot(8, 1, coolDown: 2000)
                        )
                    ),
                    new ItemLoot("Health Potion", 0.2)
                    )
            .Init("TH Red Fat Bees",
                new State(
                    new State("1",
                        new StayCloseToSpawn(0.5, 8),
                        new Wander(0.5),
                        new Shoot(8, 1, coolDown: 1500)
                        )
                    )
                    //
                    )
            .Init("TH Small Bees",
                new State(
                    new State("1",
                        new StayCloseToSpawn(0.4, 10),
                        new Wander(0.4)
                        )
                    ),
                new ItemLoot("TH Honey bottle", 0.1)
                    )

            .Init("TH Maggot Egg1",
                new State(
                    new State("1",
                        new TransformOnDeath("TH Maggots", 1,3)
                        )
                    )
                    )
            .Init("TH Maggots",
                new State(
                    new State("1",
                        new Shoot(3, 1, predictive: 0.4, coolDown: 500),
                        new Charge(0.8, 8, 400)
                        )
                    )
                    )
            .Init("TH Mini Hive",
                new State(
                    new TransformOnDeath("TH Fat Bees", 0, 3),
                    new TransformOnDeath("TH Red Fat Bees", probability: 0.5),
                    new State("1",
                        new Reproduce("TH Small Bees", 15, 5, 850)
                        )
                    ),
                new ItemLoot("TH Honey bottle", 0.1)
                    )
        #endregion

            .Init("TH Queen Bee",
                new State(
                    new Grenade(3, 80, 7, coolDown: 2000),
                    //
                    new HpLessTransition(0.7, "2"),
                    new HpLessTransition(0.4, "3"),
                    //new HpLessTransition(0.15, "4"),
                    //
                    new ScaleHP(700, 10000),
                    new RealmPortalDrop(),
                    //
                    new State("1",
                        new Shoot(10, 8, fixedAngle: 0, coolDown: 1400),
                        new Wander(0.6)
                        ),
                    new State("2",
                        new Flash(0x0000ff, 1.5, 8),
                        new Shoot(10, 8, fixedAngle: 0, projectileIndex: 0, coolDown: 1700),
                        new Shoot(10, 1, projectileIndex: 1, coolDown: 1200),
                        new Wander(0.5)
                        ),
                    new State("3",
                        new TossObject("TH Maggot Egg1", 15, coolDown: 3500, minAngle: 0, maxAngle: 360, minRange: 3, maxRange: 3, maxDensity: 3),
                        new Flash(0xff0000, 1.5, 20),
                        new Shoot(10, 8, fixedAngle: 0, projectileIndex: 0, coolDown: 1300),
                        new Shoot(10, 1, projectileIndex: 1, coolDown: 800),
                        new Wander(0.9)
                        )
                    ),
                new ItemLoot("TH Honey bottle", 1),
                new ItemLoot("Potion of Dexterity", 0.25),
                new ItemLoot("HoneyScepter", 0.03),
                new ItemLoot("50 gold", 0.25),
                    new ItemLoot("100 gold", 0.25),
                new ItemLoot("Orb of Sweet Demise", 0.03)
                    )

            ;
    }
}