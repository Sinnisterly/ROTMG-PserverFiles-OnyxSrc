#region

using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;
using wServer.logic.behaviors.PetBehaviors;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Misc = () => Behav()
            .Init("White Fountain",
                new State(
                    new HealPlayer(5, 450, 100)
                )
            )
            .Init("Winter Fountain Frozen",
                new State(
                    new HealPlayer(5, 450, 100)
                )
            )
            .Init("Nexus Crier",
                new State("Active",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new BackAndForth(.2, 3)
                )
            )
            .Init("Sheep",
                new State(
                    new PlayerWithinTransition(15, "player_nearby"),
                    new State("player_nearby",
                        new Prioritize(
                            new StayCloseToSpawn(0.1, 2),
                            new Wander(0.1)
                        ),
                        new Taunt(0.001, 1000, "baa", "baa baa")
                    )
                )
            )
            .Init("Target Spawner",
                new State(
                    new State("Default",
                        new EntityNotExistsTransition("Target Strong", 1, "Spawn")
                    ),
                    new State("Spawn",
                        new Spawn("Target Strong", 1, 1),
                        new TimedTransition(200, "Default")
                    )
                )
            )
            .Init("Dummy Spawner",
                new State(
                    new State("Default",
                        new EntityNotExistsTransition("Dummy Strong", 1, "Spawn")
                    ),
                    new State("Spawn",
                        new Spawn("Dummy Strong", 1, 1),
                        new TimedTransition(200, "Default")
                    )
                )
            )


        //Test for Jokerwu
            .Init("Jokerwu test01",
                new State(
                    new State(
                        new TimedTransition(9000, "Jokerwu03"),
                    new State("Jokerwu01",
                        new TimedTransition(500, "Jokerwu02")
                    ),
                    new State("Jokerwu02",
                        new Shoot(25, 1, fixedAngle: 0, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 0, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 180, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 180, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 180, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 180, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 0, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 0, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 90, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 90, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 270, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 270, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 270, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 270, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 90, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 90, coolDown: 1500, coolDownOffset: 850),
                        new TimedTransition(4500, "Jokerwu01")
                    )
                  ),//todo? test
                    new State("Jokerwu03",
                        new Shoot(25, 1, fixedAngle: 0, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 0, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 180, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 180, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 180, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 180, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 0, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 0, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 90, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 90, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 270, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 270, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 270, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 270, coolDown: 1500, coolDownOffset: 100),
                        new Shoot(25, 1, fixedAngle: 90, coolDown: 1500, coolDownOffset: 850),
                        new Shoot(25, 1, fixedAngle: 90, coolDown: 1500, coolDownOffset: 850),
                        new TimedTransition(4500, "Jokerwu01")

                        )

                )
            )

            .Init("Ghost Lantern Nexus",
                new State(
                    new PlayerWithinTransition(15, "player_nearby"),
                    new State("player_nearby",
                        new Prioritize(
                            new StayCloseToSpawn(0.1, 2),
                            new Wander(0.1)
                        )
                    )
                )
            )

        //marketplace tips guy
            .Init("WeebsSU",
            new State(
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new StayCloseToSpawn(.2),
                    new BackAndForth(.2, 4),
                new State("M1",
                    new Taunt(broadcast: true, cooldown: 15000, text: "I will give you the marketplace commands if you type 'help', give it a try!"),
                    new PlayerTextTransition("M2", "help", 50, true)
                    ),
                new State("M2",
                    new Flash(0xffffff, 1, 10),
                    new Taunt(broadcast: true, text: "Marketplace commands: 'market', 'marketall', 'mymarket', 'oops', 'rmarket' and 'marketplace'"),
                    new NoPlayerWithinTransition(50, "M1"),
                    new TimedTransition(20000, "M1")
                    )
                )
            )

        ;
    }
}