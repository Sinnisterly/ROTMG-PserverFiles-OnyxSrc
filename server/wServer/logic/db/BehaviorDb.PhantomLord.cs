

using wServer.logic.behaviors;
using wServer.logic.transitions;
using wServer.logic.loot;
using common.resources;
namespace wServer.logic
{
    partial class BehaviorDb
    {
        _ PhantomLord = () => Behav()
            .Init("The Phantom Lord",
               new State(
                    new ScaleHP(30000, 0),
                    new RealmPortalDrop(),
                    new State("default",
                        new PlayerWithinTransition(8, "fight1")
                        ),
                    new State("fight1",
                        new Taunt(1.00, "You shouldn't have come here..."),
                        new Shoot(25, projectileIndex: 3, count: 12, shootAngle: 45, coolDown: 2000),
                        new Shoot(50, 24, projectileIndex: 3, coolDown: 6000, coolDownOffset: 2000),
                        new TimedTransition(7000, "fight2")
                       ),
                    new State("fight2",
                        new StayBack(.1, distance: 4),
                            new Wander(0.2),
                            new Shoot(22, count: 10, projectileIndex: 3, shootAngle: 10, coolDown: 1000),
                            new Shoot(25, projectileIndex: 1, count: 12, shootAngle: 45, coolDown: 2000),
                        new TimedTransition(7000, "fight3")
                       ),
                    new State("fight3",
                        new Taunt(1.00, "You've gone and made me angry..."),
                        new Follow(1.5, 20, 1),
                        new Shoot(50, 8, projectileIndex: 2, coolDown: 1000, coolDownOffset: 500),
                        new Shoot(50, 4, projectileIndex: 1, coolDown: 3000, coolDownOffset: 500),
                        new Shoot(50, 6, projectileIndex: 0, coolDown: 3100, coolDownOffset: 500),
                        new Spawn("Mini Bot", 5),
                        new Shoot(16, count: 5, shootAngle: 360 / 3, projectileIndex: 1, coolDown: 2000),
                        new Shoot(16, count: 7, shootAngle: 360 / 4, projectileIndex: 2, coolDown: 2400),
                        new TimedTransition(7000, "fight4")
                       ),
                    new State("fight4",
                        new Follow(1.5, 10, 1),
                        new Shoot(50, 8, projectileIndex: 2, coolDown: 1000, coolDownOffset: 500),
                        new Shoot(50, 4, projectileIndex: 1, coolDown: 3000, coolDownOffset: 500),
                        new Shoot(50, 6, projectileIndex: 0, coolDown: 3100, coolDownOffset: 500),
                        new Spawn("Mini Bot", 5),
                        new Shoot(16, count: 5, shootAngle: 360 / 3, projectileIndex: 1, coolDown: 2000),
                        new Shoot(16, count: 7, shootAngle: 360 / 4, projectileIndex: 2, coolDown: 2400),
                        new HpLessTransition(0.10, "fight5")
                       ),
                    new State("fight5",
                        new Taunt(1.00, "I need to end this soon..."),
                        new Flash(0xFF0FF0, 2, 2),
                        new Shoot(50, 8, projectileIndex: 2, coolDown: 1000, coolDownOffset: 500),
                        new Shoot(50, 4, projectileIndex: 1, coolDown: 3000, coolDownOffset: 500),
                        new Shoot(50, 6, projectileIndex: 0, coolDown: 3100, coolDownOffset: 500),
                        new Spawn("Mini Bot", 5),
                        new Shoot(16, count: 5, shootAngle: 360 / 3, projectileIndex: 1, coolDown: 2000),
                        new Shoot(16, count: 7, shootAngle: 360 / 4, projectileIndex: 2, coolDown: 2400),
                        new Shoot(50, 8, projectileIndex: 2, coolDown: 1000, coolDownOffset: 500),
                        new Shoot(50, 4, projectileIndex: 1, coolDown: 3000, coolDownOffset: 500),
                        new Shoot(50, 6, projectileIndex: 0, coolDown: 3100, coolDownOffset: 500),
                        new Spawn("Mini Bot", 5),
                        new Shoot(16, count: 5, shootAngle: 360 / 3, projectileIndex: 1, coolDown: 2000),
                        new Shoot(16, count: 7, shootAngle: 360 / 4, projectileIndex: 2, coolDown: 2400)
                       )
                    ),
                new Threshold(0.15,
                    new TierLoot(5, ItemType.Ring, 0.2),
                    new TierLoot(12, ItemType.Armor, 0.2),
                    new TierLoot(12, ItemType.Weapon, 0.2),
                    new TierLoot(6, ItemType.Ability, 0.1),
                    new TierLoot(8, ItemType.Armor, 0.1),
                    new TierLoot(4, ItemType.Ring, 0.05),
                    new TierLoot(9, ItemType.Armor, 0.03),
                    new TierLoot(5, ItemType.Ability, 0.03),
                    new TierLoot(9, ItemType.Weapon, 0.03),
                    new TierLoot(10, ItemType.Armor, 0.02),
                    new TierLoot(10, ItemType.Weapon, 0.02),
                    new TierLoot(11, ItemType.Armor, 0.01),
                    new TierLoot(11, ItemType.Weapon, 0.01), //The Phantom Amulet
                    new TierLoot(5, ItemType.Ring, 0.01),
                    new ItemLoot("Phantom Edge", 0.006),
                    new ItemLoot("Phantom Warp", 0.006), 
                    new ItemLoot("Phantom Armor", 0.006),
                    new ItemLoot("Phantom Lance", 0.007),
                    new ItemLoot("Phantom Staff", 0.006),
                    new ItemLoot("Phase Shield", 0.006),
                    new ItemLoot("Fragment of the Phantom", 0.006),
                    new ItemLoot("Phantom Lord Plate", 0.005),
                    new ItemLoot("Soul of the Phantom Lord", 0.006),
                    new ItemLoot("Phantom Research Robes", 0.006),
                    new ItemLoot("The Phantom Amulet", 0.006),
                    new ItemLoot("The Phantom Necklace", 0.006),
                    new ItemLoot("Potion of Speed", 1.00)
                )
         );
    }
}