using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ BeerGod = () => Behav()
        .Init("Beer God",
            new State(
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                new State("default",
                     new PlayerWithinTransition(6, "grow")
                    ),
                new State("grow",
                     new ChangeSize(30, 145),
                     new TimedTransition(3000, "fight1")
                    )
                  ),
                new State("fight1",
                     new Taunt(probability: 0.3, text: "These drunken barrels are cylinders to perfection"),
                     new Wander(0.35),
                     new Shoot(10, count: 6, projectileIndex: 1, coolDown: 1000),
                     new Shoot(8.4, count: 1, projectileIndex: 0, coolDown: new Cooldown(500, 150))
                )
            ),
            new Threshold(0.01,
                new ItemLoot("Azeweria Special +++", 0.75),
                    new ItemLoot("Mad God Ale", 1.00),
                    new ItemLoot("50 gold", 0.25),
                    new ItemLoot("100 gold", 0.25),
                    new ItemLoot("Oryx Stout", 1.00),
                    new ItemLoot("Realm-wheat Hefeweizen", 1.00)
                )
            )

        ;
    }
}