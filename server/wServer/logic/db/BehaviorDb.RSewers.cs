using common.resources;
using wServer.logic.transitions;
using wServer.logic.behaviors;
using wServer.logic.loot;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ RSewers = () => Behav()
             .Init("rs GoldenRat",
                 new State(
                    new ScaleHP(50000, 0),
                    new State("1",
                        new PlayerWithinTransition(12, "2"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable)
                        ),
                    new State("2",
                        new Wander(0.65),
                        new Shoot(15, 2, 5, 1, 45, 15, coolDown: 300),
                        new Shoot(15, 3, 8, 0, 90, 15, coolDown: 300),
                        new HpLessTransition(0.9, "3.0")
                        ),
                    new State("3.0",
                        new Taunt("Who is here wrecking my sewers?!"),
                        new TimedTransition(2000, "3.1"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(15, 150),
                        new HealSelf(1000, 15000),
                        new Shoot(15, 2, 5, 1, 45, 15, coolDown: 300),
                        new Shoot(15, 3, 8, 0, 90, 15, coolDown: 300),
                        new Shoot(15, 2, 5, 1, 135, 15, coolDown: 300),
                        new Shoot(15, 3, 8, 0, 225, 15, coolDown: 300),
                        new Shoot(15, 2, 5, 1, 270, 15, coolDown: 300)
                        ),
                    new State("3.1",
                        new Taunt("Prepare youself!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TossObject("rs Slime 01", 6, minAngle:0, maxAngle:360, coolDown: 800, tossInvis: true),
                        new EntityExistsTransition("rs Slime 01", 10, "4") //works 100% of the time now?
                        ),
                    new State("4",
                        new Taunt("You better know that this is my sewers and I cleaned it up, you better not try and kill my slimes!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntityNotExistsTransition("rs Slime 01", 10, "4.5")
                        ),
                    new State("4.5",
                        new Taunt("You better not stay close if you kill these slimes..."),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntityNotExistsTransition("rs Slime 01_2", 10, "5")
                        ),
                    new State("5",
                        new HpLessTransition(0.5, "6"),
                        new Taunt("Impossible!"),
                        new ChangeSize(15, 200),
                        new StayCloseToSpawn(0.15, 2),
                        new Wander(0.15),
                        new Shoot(15, 2, 5, 1, 45, 15, coolDown: 300),
                        new Shoot(15, 3, 8, 0, 90, 15, coolDown: 300),
                        new Shoot(15, 2, 5, 1, 135, 15, coolDown: 300),
                        new Shoot(15, 3, 8, 0, 225, 15, coolDown: 300),
                        new Shoot(15, 2, 5, 1, 270, 15, coolDown: 300)
                        ),
                    new State("6",
                        new HpLessTransition(0.3, "7"),
                        new TimedTransition(5000, "7"),
                        new HealSelf(2000, 2500),
                        new Taunt("This will lead to a single path, your death!"),
                        new ChangeSize(15, 250),
                        new Swirl(0.5, 3, targeted: false),
                        new Shoot(15, 6, 5, 0, coolDown: 500)
                        ),
                    new State("7",
                        new Follow(0.5, 10, 2),
                        new Shoot(15, 4, 10, 1, 0, 25, coolDown: 500),
                        new Shoot(15, 6, 5, 0, coolDown: 800)
                        )

                 ),
                new Threshold(0.025,
                    //new ItemLoot("Sewer Cocktail", 0.0017),
                    new ItemLoot("Void Blade", 0.0095),
                    new ItemLoot("Murky Toxin", 0.014),
                    new ItemLoot("Gold Flake Armor", 0.006),
                    new ItemLoot("Remember Death", 0.008),
                    new ItemLoot("Plague Staff", 0.006),
                    new ItemLoot("Plague Doctors Garbs", 0.007),
                    new ItemLoot("Doctors Note", 0.007),
                    new ItemLoot("Golden Blade", 0.006),
                    new ItemLoot("Seal of the Rat", 0.006),
                    new ItemLoot("Plague Orb", 0.007),

                    new ItemLoot("Solution of Gulpord", 0.3),
                    new ItemLoot("50 gold", 0.25),
                    new ItemLoot("100 gold", 0.25),
                    new ItemLoot("Solution of Gulpord", 0.3),
                    new ItemLoot("Potion of Defense", 1)
                    ),

                 new Threshold(0.15,
                     new TierLoot(4, ItemType.Ability, 0.1),
                     new TierLoot(3, ItemType.Ring, 0.1),
                     new TierLoot(4, ItemType.Ring, 0.042),
                     new TierLoot(9, ItemType.Armor, 0.051),
                     new TierLoot(9, ItemType.Weapon, 0.051),
                     new TierLoot(10, ItemType.Armor, 0.03),
                     new TierLoot(10, ItemType.Weapon, 0.03)
                 )
             )

             .Init("rs Slime 01",
                 new State(
                     new ScaleHP(8500, 0),
                     new TransformOnDeath("rs Slime 01_2", 2, 2, 1),
                     new TransformOnDeath("rs Slime 02", 1, 1, 0.05),
                     new State("Vul",
                         new Orbit(0.6, 4, target: "rs GoldenRat"),
                         new Shoot(8, 2, 8, predictive: 0.7, coolDown: 1300)
                         )
                 )
             )
             .Init("rs Slime 01_2",
                 new State(
                     new ScaleHP(8000, 0),
                     new State("Vul",
                         new Orbit(0.6, 5.25, target: "rs GoldenRat"),
                         new Shoot(8, 1, predictive: 0.7, coolDown: 1150)
                         )
                 )
             )
             .Init("rs Slime 02",
                 new State(
                     new State("Vul",
                         new Follow(1.5, 10, 1.5),
                         new Shoot(8, 6, fixedAngle: 0, coolDown: 1000)
                         )
                 ),
                 new ItemLoot("Potion of Defense", 0.65),
                 new ItemLoot("Sewer Cocktail", 0.0017), //the only enemy that drops it.
                 new ItemLoot("Void Blade", 0.015),
                 new ItemLoot("Murky Toxin", 0.02)
             )

        #region Minions
             .Init("rs Ennemy 01",
                 new State(
                     new State("1",
                         new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                         new PlayerWithinTransition(9, "2"),
                         new Wander(0.1)
                         ),
                     new State("2",
                         new Shoot(5.2, 2, 8, coolDown: 700),
                         new Follow(1.1, 9, 1),
                         new Wander(0.15)
                         )
                 ),
                 new ItemLoot("Health Potion", 0.04)
             )
             .Init("rs Ennemy 02",
                 new State(
                     new State("1",
                         new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                         new PlayerWithinTransition(9, "2"),
                         new Wander(0.1)
                         ),
                     new State("2",
                         new Shoot(5.2, 1, coolDown: 550),
                         new Follow(1.1, 9, 1),
                         new Wander(0.15)
                         )
                 ),
                 new ItemLoot("Health Potion", 0.04)
             )
             .Init("rs Ennemy 03",
                 new State(
                     new State("1",
                         new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                         new PlayerWithinTransition(10, "2"),
                         new Wander(0.1)
                         ),
                     new State("2",
                         new Shoot(9, 2, 4, coolDown: 700),
                         new Follow(1.0, 10.5, 4.4),
                         new Wander(0.15)
                         )
                 ),
                 new ItemLoot("Magic Potion", 0.02)
             )
             .Init("rs Ennemy 04",
                 new State(
                     new State("1",
                         new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                         new PlayerWithinTransition(9, "2"),
                         new Wander(0.1)
                         ),
                     new State("2",
                         new Shoot(5, 1, coolDown: 700),
                         new Follow(1.0, 9, 1.3),
                         new Wander(0.15)
                         )
                 ),
                 new ItemLoot("Health Potion", 0.02)
             )
             .Init("rs Ennemy 05",
                 new State(
                     new State("1",
                         new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                         new PlayerWithinTransition(10, "2"),
                         new Wander(0.1)
                         ),
                     new State("2",
                         new Shoot(9, 2, 4, coolDown: 550),
                         new Follow(1.05, 10.9, 4),
                         new Wander(0.15)
                         )
                 ),
                 new ItemLoot("Magic Potion", 0.04)
             )
        #endregion

        ;
    }
}