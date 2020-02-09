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
        private _ Events = () => Behav()
        #region Skull Shrine

            .Init("Skull Shrine",
            new State(
                    new ScaleHP(20000, 0),
                    new Spawn("Red Flaming Skull", 8, coolDown: 5000),
                    new Spawn("Blue Flaming Skull", 10, coolDown: 1000),
                    new Reproduce("Red Flaming Skull", 10, 8, 4400),
                    new Reproduce("Blue Flaming Skull", 10, 10, 2000),
                new State("basic",
                    new Shoot(25, 9, 10, predictive: 1, coolDown: 1400),
                    new HpLessTransition(0.55, "immune")
                   ),
                new State("immune",
                    new ConditionalEffect(ConditionEffectIndex.StunImmune, true),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Shoot(25, 9, 10, predictive: 1, coolDown: 1800),
                    new Flash(0x0000FF, 1, 3),
                    new HealSelf(coolDown: 1000, amount: 10000),
                    new TimedTransition(3000, "immune2")
                   ),
                new State("immune2",
                    new HpLessTransition(0.3, "pausebreak"),
                    new Shoot(25, 9, 10, predictive: 1, coolDown: 1300),
                    new Flash(0x0000FF, 1, 100)
                   ),
                new State("pausebreak",
                    new ConditionalEffect(ConditionEffectIndex.StasisImmune, true),
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Shoot(25, 9, 10, predictive: 1, coolDown: 1800),
                    new Flash(0xFF0000, 1, 3),
                    new HealSelf(coolDown: 1000, amount: 10000),
                    new TimedTransition(3000, "immune3")
                   ),
                new State("immune3",
                    new TossObject("SkullShrineGrenade", 5, coolDown: 1200, tossInvis: true, minAngle: 0, maxAngle: 360),
                    new Shoot(25, 9, 10, predictive: 1, coolDown: 1000),
                    new Flash(0xFF0000, 1, 100)
                   )
                ),
                new Threshold(0.05,
                    new ItemLoot("Potion of Defense", .15),
                    new ItemLoot("Potion of Attack", .15),
                    new ItemLoot("Potion of Speed", .15),
                    new ItemLoot("Potion of Vitality", .1),
                    new ItemLoot("Potion of Wisdom", .1),
                    new ItemLoot("Potion of Dexterity", .1)
                    ),
                new Threshold(0.02,
                    new TierLoot(8, ItemType.Weapon, 0.2),
                    new TierLoot(9, ItemType.Weapon, 0.03),
                    new TierLoot(10, ItemType.Weapon, 0.02),
                    new TierLoot(11, ItemType.Weapon, 0.01),
                    new TierLoot(3, ItemType.Ring, 0.2),
                    new TierLoot(4, ItemType.Ring, 0.05),
                    new TierLoot(5, ItemType.Ring, 0.01),
                    new TierLoot(7, ItemType.Armor, 0.2),
                    new TierLoot(8, ItemType.Armor, 0.1),
                    new TierLoot(9, ItemType.Armor, 0.03),
                    new TierLoot(10, ItemType.Armor, 0.02),
                    new TierLoot(11, ItemType.Armor, 0.01),
                    new TierLoot(4, ItemType.Ability, 0.1),
                    new TierLoot(5, ItemType.Ability, 0.03),
                    new ItemLoot("Orb of Conflict", 0.0075),
                    new ItemLoot("Flaming Boomerang", 0.008)
                    )
            )
        #region Misc
            .Init("SkullShrineGrenade",
                new State(
                    new State("1",
                        new TimedTransition(500, "2"),
                        new Grenade(3, 0, 0, 0, 1000, ConditionEffectIndex.Weak, 10000, 0xffffff)
                        ),
                    new State("2",
                        new Decay(200)
                        )


                    )
            )
        #endregion
            .Init("Red Flaming Skull",
                new State(
                    new Prioritize(
                        new Wander(.6),
                        new Follow(.6, 20, 3)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 1150)
                    )
            )
            .Init("Blue Flaming Skull",
                new State(
                    new Prioritize(
                        new Orbit(1, 20, target: "Skull Shrine", radiusVariance: 0.5),
                        new Wander(.6)
                        ),
                    new Shoot(15, 2, 5, 0, predictive: 1, coolDown: 1150)
                    )
            )
        #endregion

        #region Hermit God
        /*
            .Init("Hermit God",
                new State(
                    new HpLessTransition(0.15, "die"),
                    new ScaleHP(20000, 0),
                    new ChangeGroundOnDeath(new[] { "Dark Water" }, new[] { "Shallow Water" }, 15),
                    new DropPortalOnDeath("Ocean Trench Portal", 0.75),
                    new State("invis",
                        new SetAltTexture(3),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new InvisiToss("Hermit Minion", 9, 0, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 45, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 135, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 225, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 270, 90000001, coolDownOffset: 0),
                          new InvisiToss("Hermit Minion", 9, 315, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 15, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 30, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 120, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 150, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 210, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 240, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 50, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 100, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 150, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 200, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 250, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit Minion", 9, 300, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 45, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 135, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 225, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 270, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 315, 90000001, coolDownOffset: 0),
                        new TimedTransition(1000, "check")
                        ),
                    new State("invis2",
                        new Order(14, "Whirlpool", "despawn"),
                        new SetAltTexture(3),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new InvisiToss("Hermit God Tentacle", 5, 45, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 90, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 135, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 180, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 225, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 270, 90000001, coolDownOffset: 0),
                        new InvisiToss("Hermit God Tentacle", 5, 315, 90000001, coolDownOffset: 0),
                        new TimedTransition(1000, "check")
                        ),
                    new State("check",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new EntityNotExistsTransition("Hermit God Tentacle", 20, "active")
                        ),
                    new State("active",
                        new SetAltTexture(2),
                        new TimedTransition(500, "active2")
                        ),
                    new State("active2",
                        new SetAltTexture(0),
                        new Shoot(25, 3, 10, 0, coolDown: 200),
                        new Wander(.2),
                        new TossObject("Whirlpool", 6, 0, 90000001, 100),
                        new TossObject("Whirlpool", 6, 45, 90000001, 100),
                        new TossObject("Whirlpool", 6, 90, 90000001, 100),
                        new TossObject("Whirlpool", 6, 135, 90000001, 100),
                        new TossObject("Whirlpool", 6, 180, 90000001, 100),
                        new TossObject("Whirlpool", 6, 225, 90000001, 100),
                        new TossObject("Whirlpool", 6, 270, 90000001, 100),
                        new TossObject("Whirlpool", 6, 315, 90000001, 100),
                        new TimedTransition(20000, "invis2")
                        ),
                    new State("die",
                        new Decay(200)
                        )
                    ),
                new Threshold(0.01,
                    new ItemLoot("Potion of Dexterity", 1),
                    new ItemLoot("Potion of Vitality", 1),
                    new ItemLoot("Helm of the Juggernaut", 0.005)
            )
            )
            .Init("Whirlpool",
                new State(
                    new State("active",
                        new Shoot(25, 8, projectileIndex: 0, coolDown: 1250),
                        new Orbit(.5, 4.25, target: "Hermit God"),
                        new EntityNotExistsTransition("Hermit God", 50, "despawn")
                        ),
                    new State("despawn",
                        new Suicide()
                        )
                    )
            )
            .Init("Hermit God Tentacle",
                new State(
                    new Prioritize(
                        new Orbit(.5, 5, target: "Hermit God", radiusVariance: 0.5),
                        new Follow(0.85, range: 1, duration: 2000, coolDown: 0)
                        ),
                    new Shoot(4, 8, projectileIndex: 0, coolDown: 1000)
                    )
            )
            .Init("Hermit Minion",
                new State(
                    new Prioritize(
                        new Wander(.5),
                        new Follow(0.85, 3, 1, 2000, 0)
                        ),
                    new Shoot(5, 1, 1, 1, coolDown: 2300),
                    new Shoot(5, 3, 1, 0, coolDown: 1000)
                    )
            )
            .Init("Hermit God Drop",
                new State(
                    new State("idle",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new EntityNotExistsTransition("Hermit God", 10, "despawn")
                        ),
                    new State("despawn",
                        new Suicide()
                        )
                    ),
                new Threshold(0.01,
                    new ItemLoot("Potion of Dexterity", 1),
                    new ItemLoot("Potion of Vitality", 1),
                    new ItemLoot("Helm of the Juggernaut", 0.005)
                )
            )
            */
        #endregion

        #region Pentaract
                .Init("Pentaract Eye",
                    new State(
                        new Prioritize(
                            new Swirl(2, 8, 20, true),
                            new Protect(2, "Pentaract Tower", 20, 6, 4)
                            ),
                        new Shoot(9, 1, coolDown: 1000)
                        )
                )
                .Init("Pentaract Tower",
                    new State(
                        new Spawn("Pentaract Eye", 5, coolDown: 5000, givesNoXp: false),
                        new Grenade(4, 100, 8, coolDown: 5000),
                        new TransformOnDeath("Pentaract Tower Corpse"),
                        new TransferDamageOnDeath("Pentaract"),
                        // needed to avoid crash, Oryx.cs needs player name otherwise hangs server (will patch that later)
                        new TransferDamageOnDeath("Pentaract Tower Corpse")
                        )
                )
                .Init("Pentaract",
                    new State(
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new State("Waiting",
                            new EntityNotExistsTransition("Pentaract Tower", 50, "Die")
                            ),
                        new State("Die",
                            new Suicide()
                            )
                        )
                )
                .Init("Pentaract Tower Corpse",
                    new State(
                        //new DropPortalOnDeath("The Unspeakable Portal", 0.33),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new State("Waiting",
                            new TimedTransition(15000, "Spawn"),
                            new EntityNotExistsTransition("Pentaract Tower", 50, "Die")
                            ),
                        new State("Spawn",
                            new Transform("Pentaract Tower")
                            ),
                        new State("Die",
                            new Suicide()
                            )
                        ),
                    new Threshold(0.01,
                        new TierLoot(8, ItemType.Weapon, .15),
                        new TierLoot(9, ItemType.Weapon, .1),
                        new TierLoot(10, ItemType.Weapon, .07),
                        new TierLoot(11, ItemType.Weapon, .05),
                        new TierLoot(4, ItemType.Ability, .15),
                        new TierLoot(5, ItemType.Ability, .07),
                        new TierLoot(8, ItemType.Armor, .2),
                        new TierLoot(9, ItemType.Armor, .15),
                        new TierLoot(10, ItemType.Armor, .10),
                        new TierLoot(11, ItemType.Armor, .07),
                        new TierLoot(12, ItemType.Armor, .04),
                        new TierLoot(3, ItemType.Ring, .15),
                        new TierLoot(4, ItemType.Ring, .07),
                        new TierLoot(5, ItemType.Ring, .03),
                        new ItemLoot("Potion of Defense", .15),
                        new ItemLoot("Potion of Attack", .15),
                        new ItemLoot("Potion of Vitality", .1),
                        new ItemLoot("Potion of Wisdom", .1),
                        new ItemLoot("Potion of Speed", .15),
                        new ItemLoot("Potion of Dexterity", .1),
                        new ItemLoot("Seal of Blasphemous Prayer", .015)
                        )
                )
        #endregion

        #region Cube God
                     .Init("Cube God",
                 new State(
                    new ScaleHP(20000, 0),
                     new StayCloseToSpawn(0.3, range: 7),
                    new State("Jeebs",
                            new Wander(0.5),
                              new Shoot(10, count: 9, predictive: 0.9, shootAngle: 6.5, coolDown: 1000),
                              new Shoot(10, count: 6, predictive: 0.9, shootAngle: 6.5, projectileIndex: 1, coolDown: 1000, coolDownOffset: 200),
                              new Spawn("Cube Overseer", maxChildren: 5, initialSpawn: 3, coolDown: 100000),
                              new Spawn("Cube Defender", maxChildren: 5, initialSpawn: 5, coolDown: 100000),
                              new Spawn("Cube Blaster", maxChildren: 5, initialSpawn: 5, coolDown: 100000)
                        )

                 ),
                new Threshold(0.05,
                    new ItemLoot("Potion of Speed", .5),
                    new ItemLoot("Potion of Wisdom", .3),
                    new ItemLoot("Potion of Dexterity", .3)
                    ),

                 new Threshold(0.15,
                     new TierLoot(3, ItemType.Ring, 0.2),
                     new TierLoot(7, ItemType.Armor, 0.2),
                     new TierLoot(8, ItemType.Weapon, 0.2),
                     new TierLoot(4, ItemType.Ability, 0.1),
                     new TierLoot(8, ItemType.Armor, 0.1),
                     new TierLoot(4, ItemType.Ring, 0.05),
                     new TierLoot(9, ItemType.Armor, 0.03),
                     new TierLoot(5, ItemType.Ability, 0.03),
                     new TierLoot(9, ItemType.Weapon, 0.03),
                     new TierLoot(10, ItemType.Armor, 0.02),
                     new TierLoot(10, ItemType.Weapon, 0.02),
                     new TierLoot(11, ItemType.Armor, 0.01),
                     new TierLoot(11, ItemType.Weapon, 0.01),
                     new TierLoot(5, ItemType.Ring, 0.01),

                     new ItemLoot("Dirk of Cronus", 0.0072),
                     new ItemLoot("Skull of the Cubes", 0.013)
                 )
             )
             .Init("Cube Overseer",
                 new State(
                     new StayCloseToSpawn(0.3, range: 7),
                              new Wander(1),
                              new Shoot(10, count: 4, predictive: 0.9, projectileIndex: 0, coolDown: 1250)
                 )
             )
             .Init("Cube Defender",
                 new State(
                     new Wander(0.5),
                              new StayCloseToSpawn(0.03, range: 7),
                              new Follow(0.4, acquireRange: 9, range: 2),
                              new Shoot(10, count: 1, coolDown: 1000, predictive: 0.9, projectileIndex: 0)
                 )
             )
             .Init("Cube Blaster",
                 new State(
                     new Wander(0.5),
                              new StayCloseToSpawn(0.03, range: 7),
                              new Follow(0.4, acquireRange: 9, range: 2),
                              new Shoot(10, count: 2, predictive: 0.9, projectileIndex: 0, coolDown: 1500),
                              new Shoot(10, count: 1, predictive: 0.9, projectileIndex: 0, coolDown: 1500)
                 )
             )
        #endregion

        #region Grand Sphinx
                    .Init("Grand Sphinx",
                new State(
                    new ScaleHP(35000, 0),
                    new DropPortalOnDeath("Tomb of the Ancients Portal", 0.33),
                    new State("Spawned",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Reproduce("Horrid Reaper", 30, 4, coolDown: 100),
                        new TimedTransition(500, "Attack1")
                        ),
                    new State("Attack1",
                        new Prioritize(
                            new Wander(0.5)
                            ),
                        new Shoot(12, count: 1, coolDown: 800),
                        new Shoot(12, count: 3, shootAngle: 10, coolDown: 1000),
                        new Shoot(12, count: 1, shootAngle: 130, coolDown: 1000),
                        new Shoot(12, count: 1, shootAngle: 230, coolDown: 1000),
                        new TimedTransition(6000, "TransAttack2")
                        ),
                    new State("TransAttack2",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Wander(0.5),
                        new Flash(0x00FF0C, .25, 8),
                        new Taunt(0.99, "You hide behind rocks like cowards but you cannot hide from this!"),
                        new TimedTransition(2000, "Attack2")
                        ),
                    new State("Attack2",
                        new Prioritize(
                            new Wander(0.5)
                            ),
                        new Shoot(0, count: 8, shootAngle: 10, fixedAngle: 0, rotateAngle: 70, coolDown: 2000,
                            projectileIndex: 1),
                        new Shoot(0, count: 8, shootAngle: 10, fixedAngle: 180, rotateAngle: 70, coolDown: 2000,
                            projectileIndex: 1),
                        new TimedTransition(6200, "TransAttack3")
                        ),
                    new State("TransAttack3",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Wander(0.5),
                        new Flash(0x00FF0C, .25, 8),
                        new TimedTransition(2000, "Attack3")
                        ),
                    new State("Attack3",
                        new Prioritize(
                            new Wander(0.5)
                            ),
                        new Shoot(20, count: 9, fixedAngle: 360 / 9, projectileIndex: 2, coolDown: 2300),
                        new TimedTransition(6000, "TransAttack1"),
                        new State("Shoot1",
                            new Shoot(20, count: 2, shootAngle: 4, projectileIndex: 2, coolDown: 700),
                            new TimedRandomTransition(1000, false,
                                "Shoot1",
                                "Shoot2"
                                )
                            ),
                        new State("Shoot2",
                            new Shoot(20, count: 8, shootAngle: 5, projectileIndex: 2, coolDown: 1100),
                            new TimedRandomTransition(1000, false,
                                "Shoot1",
                                "Shoot2"
                                )
                            )
                        ),
                    new State("TransAttack1",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Wander(0.5),
                        new Flash(0x00FF0C, .25, 8),
                        new TimedTransition(2000, "Attack1"),
                        new HpLessTransition(0.15, "Order")
                        ),
                    new State("Order",
                        new Wander(0.5),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Order(30, "Horrid Reaper", "Die"),
                        new TimedTransition(1900, "Attack1")
                        )
                    ),
                new Threshold(0.01,
                    new ItemLoot("Potion of Vitality", 0.75),
                    new ItemLoot("Potion of Wisdom", 0.75),
                    new ItemLoot("Helm of the Juggernaut", 0.005)
                    )
            )
            .Init("Horrid Reaper",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new State("Move",
                        new Prioritize(
                            new StayCloseToSpawn(3, 10),
                            new Wander(3)
                            ),
                        new EntityNotExistsTransition("Grand Sphinx", 50, "Die"), //Just to be sure
                        new TimedRandomTransition(2000, true, "Attack")
                        ),
                    new State("Attack",
                        new Shoot(0, count: 6, fixedAngle: 360 / 6, coolDown: 700),
                        new PlayerWithinTransition(2, "Follow"),
                        new TimedRandomTransition(5000, true, "Move")
                        ),
                    new State("Follow",
                        new Prioritize(
                            new Follow(0.7, 10, 3)
                            ),
                        new Shoot(7, count: 1, coolDown: 700),
                        new TimedRandomTransition(5000, true, "Move")
                        ),
                    new State("Die",
                        new Taunt(0.99, "OOaoaoAaAoaAAOOAoaaoooaa!!!"),
                        new Decay(1000)
                        )
                    )
            )
        #endregion

        #region Lord of the Lost Lands
                .Init("Lord of the Lost Lands",
                new State(
                    new ScaleHP(20000, 0),
                    new DropPortalOnDeath("Ice Cave Portal", 0.5, 20000),
                    new HpLessTransition(0.15, "IMDONELIKESOOOODONE!"),
                    new State("timetogeticey",
                        new PlayerWithinTransition(8, "startupandfireup")
                        ),
                    new State("startupandfireup",
                        new SetAltTexture(0),
                        new Wander(0.3),
                        new Shoot(10, count: 7, shootAngle: 7, coolDownOffset: 1100, angleOffset: 270, coolDown: 2250),
                        new Shoot(10, count: 7, shootAngle: 7, coolDownOffset: 1100, angleOffset: 90, coolDown: 2250),

                        new Shoot(10, count: 7, shootAngle: 7, coolDown: 2250),
                        new Shoot(10, count: 7, shootAngle: 7, angleOffset: 180, coolDown: 2250),
                        new TimedTransition(8500, "GatherUp")
                        ),
                    new State("GatherUp",
                        new SetAltTexture(3),
                        new Taunt("GATHERING POWER!"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(8.4, count: 6, shootAngle: 60, projectileIndex: 1, coolDown: 4550),
                        new Shoot(8.4, count: 6, shootAngle: 60, predictive: 2, projectileIndex: 1, coolDown: 2700),
                        new TimedTransition(5750, "protect")
                        ),
                    new State("protect",
                        //Minions spawn
                        new ConditionalEffect(ConditionEffectIndex.StunImmune),
                        new TossObject("Guardian of the Lost Lands", 5, 180, coolDown: 9999999),
                        new TossObject("Guardian of the Lost Lands", 5, 270, coolDown: 9999999),
                        new TimedTransition(1000, "crystals")
                        ),
                    new State("crystals",
                        new SetAltTexture(1),
                        new ConditionalEffect(ConditionEffectIndex.StunImmune),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TossObject("Protection Crystal", 4, 0, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 45, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 90, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 135, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 180, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 225, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 270, coolDown: 9999999),
                        new TossObject("Protection Crystal", 4, 315, coolDown: 9999999),
                        new TimedTransition(2100, "checkforcrystals")
                        ),
                    new State("checkforcrystals",
                        new SetAltTexture(1),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntitiesNotExistsTransition(9999, "startupandfireup", "Protection Crystal")
                        ),
                    new State("IMDONELIKESOOOODONE!",
                        new Taunt("NOOOOOOOOOOOOOOO!"),
                        new SetAltTexture(3),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new Flash(0xFF0000, 0.2, 3),
                        new TimedTransition(5250, "dead")
                        ),
                    new State("dead",
                        new Shoot(8.4, count: 6, shootAngle: 60, projectileIndex: 1),
                        new Suicide()
                        )
                    ),
                new Threshold(0.05,
                    new ItemLoot("Potion of Defense", .15),
                    new ItemLoot("Potion of Attack", .15),
                    new ItemLoot("Potion of Speed", .15),
                    new ItemLoot("Potion of Vitality", .1),
                    new ItemLoot("Potion of Wisdom", .1),
                    new ItemLoot("Potion of Dexterity", .1)
                    ),

                new Threshold(0.02,
                    new ItemLoot("Shield of Ogmur", 0.006),
                    new TierLoot(8, ItemType.Weapon, 0.3),
                    new TierLoot(9, ItemType.Weapon, 0.275),
                    new TierLoot(10, ItemType.Weapon, 0.225),
                    new TierLoot(11, ItemType.Weapon, 0.08),
                    new TierLoot(8, ItemType.Armor, 0.2),
                    new TierLoot(9, ItemType.Armor, 0.175),
                    new TierLoot(10, ItemType.Armor, 0.15),
                    new TierLoot(11, ItemType.Armor, 0.1),
                    new TierLoot(12, ItemType.Armor, 0.05),
                    new TierLoot(4, ItemType.Ability, 0.15),
                    new TierLoot(5, ItemType.Ability, 0.1),
                    new TierLoot(5, ItemType.Ring, 0.05)
                )
            )
            .Init("Protection Crystal",
                new State(
                    new Prioritize(
                        new Orbit(0.3, 4, 10, "Lord of the Lost Lands")
                        ),
                    new Shoot(8, count: 4, shootAngle: 7, coolDown: 500)
                    )
            )
            .Init("Guardian of the Lost Lands",
                new State(
                    new State("Full",
                        new Spawn("Knight of the Lost Lands", 2, 1, coolDown: 4000),
                        new Prioritize(
                            new Follow(0.6, 20, 6),
                            new Wander(0.2)
                            ),
                        new Shoot(10, count: 8, fixedAngle: 360 / 8, coolDown: 3000, projectileIndex: 1),
                        new Shoot(10, count: 5, shootAngle: 10, coolDown: 1500),
                        new HpLessTransition(0.25, "Low")
                        ),
                    new State("Low",
                        new Prioritize(
                            new StayBack(0.6, 5),
                            new Wander(0.2)
                            ),
                        new Shoot(10, count: 8, fixedAngle: 360 / 8, coolDown: 3000, projectileIndex: 1),
                        new Shoot(10, count: 5, shootAngle: 10, coolDown: 1500)
                        )
                    ),
                new ItemLoot("Health Potion", 0.1),
                new ItemLoot("Magic Potion", 0.1)
            )
            .Init("Knight of the Lost Lands",
                new State(
                    new Prioritize(
                        new Follow(1, 20, 4),
                        new StayBack(0.5, 2),
                        new Wander(0.3)
                        ),
                    new Shoot(13, 1, coolDown: 700)
                    ),
                new ItemLoot("Health Potion", 0.1),
                new ItemLoot("Magic Potion", 0.1)
            )
        #endregion

        #region Ghost Ship
                            .Init("Vengeful Spirit",
                new State(
                    new State("Start",
                        new ChangeSize(50, 120),
                        new Prioritize(
                            new Follow(0.48, 8, 1),
                            new Wander(0.45)
                            ),
                        new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 16, coolDown: 1000),
                        new TimedTransition(1000, "Vengeful")
                        ),
                    new State("Vengeful",
                        new Prioritize(
                            new Follow(1, 8, 1),
                            new Wander(0.45)
                            ),
                        new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 16, coolDown: 1645),
                        new TimedTransition(3000, "Vengeful2")
                        ),
                        new State("Vengeful2",
                        new ReturnToSpawn(speed: 0.6),
                        new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 16, coolDown: 1500),
                        new TimedTransition(1500, "Vengeful")
                        )))
                   .Init("Water Mine",
                    new State(
                       new State("Seek",
                        new Prioritize(
                            new Follow(0.45, 8, 1),
                            new Wander(0.55)
                            ),
                        new TimedTransition(3750, "Boom")
                        ),
                        new State("Boom",
                        new Shoot(8.4, count: 10, projectileIndex: 0, coolDown: 1000),
                        new Suicide()
                 )))
                 .Init("Beach Spectre",
                    new State(
                       new State("Fight",
                           new Wander(0.03),
                       new ChangeSize(10, 120),
                       new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 14, coolDown: 1750)
                 )))

                 .Init("Beach Spectre Spawner",
                    new State(
                       new ConditionalEffect(ConditionEffectIndex.Invincible),
                       new State("Spawn",
                       new Reproduce("Beach Spectre", densityMax: 1, densityRadius: 3, coolDown: 1250)
                 )))
                  .Init("Tempest Cloud",
                    new State(
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new State("Start1",
                       new ChangeSize(70, 130),
                       new TimedTransition(3000, "Start2")
                 ),
                    new State("Start2",
                       new SetAltTexture(1),
                       new TimedTransition(1, "Start3")
                 ),
                    new State("Start3",
                       new SetAltTexture(2),
                       new TimedTransition(1, "Start4")
                 ),
                     new State("Start4",
                       new SetAltTexture(3),
                       new TimedTransition(1, "Start5")
                 ),
                     new State("Start5",
                       new SetAltTexture(4),
                       new TimedTransition(1, "Start6")
                 ),
                     new State("Start6",
                       new SetAltTexture(5),
                       new TimedTransition(1, "Start7")
                 ),
                     new State("Start7",
                       new SetAltTexture(6),
                       new TimedTransition(1, "Start8")
                 ),
                     new State("Start8",
                       new SetAltTexture(7),
                       new TimedTransition(1, "Start9")
                 ),
                     new State("Start9",
                       new SetAltTexture(8),
                       new TimedTransition(1, "Final")
                 ),
                     new State("Final",
                       new SetAltTexture(9),
                       new TimedTransition(1, "CircleAndStorm")
                 ),
                     new State("CircleAndStorm",
                       new Orbit(0.25, 9, 20, "Ghost Ship Anchor", speedVariance: 0.1),
                       new Shoot(8.4, count: 7, projectileIndex: 0, coolDown: 1000)
                 )))
                .Init("Ghost Ship Anchor",
                    new State(
                       new State("idle",
                       new ConditionalEffect(ConditionEffectIndex.Invincible)
                 ),
                    new State("tempestcloud",
                        new InvisiToss("Tempest Cloud", 9, 0, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 45, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 90, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 135, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 180, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 225, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 270, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 315, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 350, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 250, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 110, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 200, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 10, coolDown: 9999999),
                        new InvisiToss("Tempest Cloud", 9, 290, coolDown: 9999999),

                        //Spectre Spawner
                        new InvisiToss("Beach Spectre Spawner", 17, 0, coolDown: 9999999),
                        new InvisiToss("Beach Spectre Spawner", 17, 45, coolDown: 9999999),
                        new InvisiToss("Beach Spectre Spawner", 17, 90, coolDown: 9999999),
                        new InvisiToss("Beach Spectre Spawner", 17, 135, coolDown: 9999999),
                        new InvisiToss("Beach Spectre Spawner", 17, 180, coolDown: 9999999),
                        new InvisiToss("Beach Spectre Spawner", 17, 225, coolDown: 9999999),
                        new InvisiToss("Beach Spectre Spawner", 17, 270, coolDown: 9999999),
                        new InvisiToss("Beach Spectre Spawner", 17, 315, coolDown: 9999999),
                        new InvisiToss("Beach Spectre Spawner", 17, 250, coolDown: 9999999),
                        new InvisiToss("Beach Spectre Spawner", 17, 110, coolDown: 9999999),
                        new InvisiToss("Beach Spectre Spawner", 17, 200, coolDown: 9999999),
                       new ConditionalEffect(ConditionEffectIndex.Invincible)
                 )

                        ))
                    .Init("Ghost Ship",
                new State(
                    new DropPortalOnDeath("Davy Jones' Locker Portal", .40),
                    new OnDeathBehavior(
                        new RemoveEntity(100, "Tempest Cloud")
                        ),
                    new OnDeathBehavior(
                        new RemoveEntity(100, "Beach Spectre")
                        ),
                     new OnDeathBehavior(
                        new RemoveEntity(100, "Beach Spectre Spawner")
                        ),
                    new State("idle",
                        new SetAltTexture(1),
                        new Wander(0.1),
                        new DamageTakenTransition(2000, "pause")
                        ),
                    new State("pause",
                         new SetAltTexture(2),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(2000, "start")
                        ),
                      new State("start",
                          new Taunt(1.00, "There are many treasures aboard this ship!"),
                           new SetAltTexture(0),
                      new Reproduce("Vengeful Spirit", densityMax: 2, coolDown: 1000),
                      new TimedTransition(15000, "midfight"),
                       new State("2",
                        new SetAltTexture(0),
                        new Prioritize(
                             new Wander(0.45),
                             new StayBack(0.3, 5)
                            ),
                        new Shoot(8.4, count: 1, projectileIndex: 0, coolDown: 450),
                        new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 20, coolDown: 1750),
                        new TimedTransition(3250, "1")
                        ),
                     new State("1",
                        new TossObject("Water Mine", 5, coolDown: 1500),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ReturnToSpawn(speed: 0.4),
                        new Shoot(8.4, count: 1, projectileIndex: 0, coolDown: 450),
                        new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 20, coolDown: 1750),
                        new TimedTransition(1500, "2")
                         )
                        ),

                       new State("midfight",
                     new Order(100, "Ghost Ship Anchor", "tempestcloud"),
                      new Reproduce("Vengeful Spirit", densityMax: 1, coolDown: 1000),
                      new TossObject("Water Mine", 5, coolDown: 2250),
                      new TimedTransition(10000, "countdown"),
                       new State("2",
                        new SetAltTexture(0),
                        new ReturnToSpawn(speed: 0.4),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(10, count: 4, projectileIndex: 0, coolDownOffset: 1100, angleOffset: 270, coolDown: 1250),
                        new Shoot(10, count: 4, projectileIndex: 0, coolDownOffset: 1100, angleOffset: 90, coolDown: 1250),
                        new Shoot(8.4, count: 1, projectileIndex: 1, coolDown: 1250),
                        new TimedTransition(3000, "1")
                        ),
                     new State("1",
                        new Prioritize(
                             new Follow(0.45, 8, 1),
                             new Wander(0.3)
                            ),
                        new Taunt(1.00, "Fire at will!"),
                        new Shoot(8.4, count: 2, shootAngle: 25, projectileIndex: 1, coolDown: 3850),
                        new Shoot(8.4, count: 6, projectileIndex: 0, shootAngle: 10, coolDown: 2750),
                        new TimedTransition(4000, "2")
                         )
                        ),
                    new State("countdown",
                        new Wander(0.1),
                        new Timed(1000,
                            new Taunt(1.00, "Ready..")
                            ),
                         new Timed(2000,
                            new Taunt(1.00, "Aim..")
                            ),
                        new Shoot(8.4, count: 1, projectileIndex: 0, coolDown: 450),
                        new Shoot(8.4, count: 3, projectileIndex: 0, shootAngle: 20, coolDown: 750),
                        new TimedTransition(2000, "fire")
                        ),
                    new State("fire",
                       new Prioritize(
                             new Follow(0.36, 8, 1),
                             new Wander(0.12)
                            ),
                         new Shoot(10, count: 4, projectileIndex: 1, coolDownOffset: 1100, angleOffset: 270, coolDown: 1250),
                        new Shoot(10, count: 4, projectileIndex: 1, coolDownOffset: 1100, angleOffset: 90, coolDown: 1250),
                        new Shoot(8.4, count: 10, projectileIndex: 0, coolDown: 3400),
                        new TimedTransition(3400, "midfight")
                        )

               ),
                new ItemLoot("Ghost Pirate Rum", 1.0),

                new Threshold(0.025,
                    new ItemLoot("Thirsty Ghost Trap", 0.0115),
                    new ItemLoot("Potion of Wisdom", 1.0),
                    new ItemLoot("Potion of Speed", 1.0),
                    new TierLoot(9, ItemType.Weapon, 0.1),
                    new TierLoot(4, ItemType.Ability, 0.1),
                    new TierLoot(5, ItemType.Ability, 0.05),
                    new TierLoot(9, ItemType.Armor, 0.1),
                    new TierLoot(3, ItemType.Ring, 0.05),
                    new TierLoot(10, ItemType.Armor, 0.05),
                    new TierLoot(11, ItemType.Armor, 0.04),
                    new TierLoot(10, ItemType.Weapon, 0.05),
                    new TierLoot(11, ItemType.Weapon, 0.04),
                    new TierLoot(4, ItemType.Ring, 0.025),
                    new TierLoot(5, ItemType.Ring, 0.02)
                )
            )
        #endregion

        #region Rock Dragon
                    .Init("Dragon Head",
                new State(
                    new ScaleHP(20000, 0),
                    new Reproduce("Rock Dragon Bat", 5, 5, coolDown: 10000),
                    //still fixing
                    //new DropPortalOnDeath("Lair of Draconis Portal", 0.5, 35000),
                    new State("default",
                        new PlayerWithinTransition(10, "spawnbody")
                        ),
                    new State("spawnbody",
                        new ChangeSize(60, 120),
                        new SetAltTexture(0),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Spawn("Body Segment A", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment B", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment C", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment D", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment E", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment F", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment G", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment H", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment I", 1, 1, coolDown: 99999),
                        new Spawn("Body Segment Tail", 1, 1, coolDown: 99999),
                        new TimedTransition(400, "weirdmovement")
                        ),
                    new State("weirdmovement",

                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                       new StayAbove(2, 265),
                        new Sequence(
                            new Timed(1000,
                                new ReturnToSpawn(0.6)
                                ),
                            new Timed(5000,
                            new Prioritize(
                                new Swirl(1.25, 8, targeted: false),
                                new Wander(0.2)
                                )
                                ),
                            new Timed(700,
                                new Prioritize(
                                    new Follow(0.9, 11, 1),
                                    new StayCloseToSpawn(0.5, 3)
                                    )
                                )
                            ),
                        new Shoot(4, count: 8, shootAngle: 8, projectileIndex: 1, coolDown: 1250),
                        new EntitiesNotExistsTransition(9999, "vulnerable", "Body Segment A", "Body Segment B", "Body Segment C", "Body Segment D", "Body Segment E", "Body Segment F", "Body Segment G", "Body Segment H", "Body Segment I")
                        ),
                    new State("vulnerable",
                        new ChangeSize(60, 165),
                        new SetAltTexture(1),
                        new RemoveEntity(9999, "Body Segment Tail"),
                        new Sequence(
                            new Timed(1250,
                                new ReturnToSpawn(0.6)
                                ),
                            new Timed(1000,
                                new BackAndForth(0.96, 5)
                                ),
                            new Timed(2700,
                                new Prioritize(
                                    new Follow(1.1, 11, 1),
                                    new StayCloseToSpawn(0.95, 8)
                                    )
                                )
                            ),
                        new Shoot(8, count: 6, projectileIndex: 3, coolDown: 3000),
                        new Shoot(10, 1, projectileIndex: 2, coolDown: 4123),
                        new TimedTransition(11000, "spawnbody")
                        )
                    ),
                new Threshold(0.05,
                    new ItemLoot("Potion of Defense", .1),
                    new ItemLoot("Potion of Attack", .1),
                    new ItemLoot("Potion of Speed", .1),
                    new ItemLoot("Potion of Vitality", .1),
                    new ItemLoot("Potion of Wisdom", .1),
                    new ItemLoot("Potion of Dexterity", .1)
                    ),
                new Threshold(0.02,
                    new TierLoot(8, ItemType.Weapon, .15),
                    new TierLoot(9, ItemType.Weapon, .1),
                    new TierLoot(10, ItemType.Weapon, .07),
                    new TierLoot(11, ItemType.Weapon, .05),
                    new TierLoot(4, ItemType.Ability, .15),
                    new TierLoot(5, ItemType.Ability, .07),
                    new TierLoot(8, ItemType.Armor, .2),
                    new TierLoot(9, ItemType.Armor, .15),
                    new TierLoot(10, ItemType.Armor, .10),
                    new TierLoot(11, ItemType.Armor, .07),
                    new TierLoot(12, ItemType.Armor, .04),
                    new TierLoot(5, ItemType.Ring, .03),
                    new ItemLoot("Ray Katana", 0.0125)
                )
            )
                .Init("Body Segment Bomb",
                    new State(
                      new State("BoutToBlow",
                      new TimedTransition(1620, "bom")
                        ),
                    new State("bom",
                       new Shoot(8.4, count: 13, projectileIndex: 0),
                       new Suicide()
                    )))
       .Init("Body Segment A",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment B",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment A", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment A")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )

                 )
            )
         .Init("Body Segment C",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment B", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment B")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment D",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment C", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment C")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment E",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment D", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment D")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment F",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment E", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment E")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment G",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment E", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment F")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment H",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment G", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment G")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
         .Init("Body Segment I",
             new State(
              new TransformOnDeath("Body Segment Bomb", 1, 1, 1),
               new State("go",
                   new Protect(0.97, "Body Segment H", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment H")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
        .Init("Body Segment Tail",
             new State(
              new ConditionalEffect(ConditionEffectIndex.Invulnerable),
               new State("go",
                   new Protect(0.97, "Body Segment I", protectionRange: 1),
                   new EntitiesNotExistsTransition(9999, "2plan", "Body Segment I")
                        ),
               new State("2plan",
                   new Protect(0.97, "Dragon Head", protectionRange: 1)
                        )
                 )
            )
           .Init("Rock Dragon Bat",
                    new State(
                      new State("BoutToBlow",
                      new Prioritize(
                        new Follow(0.5, 8, 1),
                        new Wander(0.2)
                        ),
                      new Shoot(7, count: 3, shootAngle: 8, projectileIndex: 3, coolDown: 1300),
                      new HpLessTransition(0.11, "bom"),
                      new TimedTransition(5500, "bom")
                        ),
                    new State("bom",
                       new Shoot(8.4, count: 7, projectileIndex: 2),
                       new Suicide()
                    )))
        #endregion

         ;
    }
}