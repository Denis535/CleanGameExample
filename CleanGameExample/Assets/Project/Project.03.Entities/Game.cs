#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.Entities.Characters;
    using Project.Entities.Things;
    using Project.Entities.Worlds;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class Game : GameBase2, IGame {

        // State
        public new GameState State { get => base.State; private set => base.State = value; }
        // IsPaused
        public override bool IsPaused {
            get => base.IsPaused;
            set {
                Player.IsInputEnabled = !value && Player.Character != null;
                base.IsPaused = value;
            }
        }
        // Level
        public Level Level { get; }
        // Entities
        public Player Player { get; }
        public World World { get; }
        // IsDirty
        private bool IsDirty { get; set; }

        // Constructor
        public Game(IDependencyContainer container, Level level, string name, PlayerCharacterKind kind) : base( container ) {
            Level = level;
            Player = new Player( container, name, kind, this, CameraFactory.Camera() );
            World = container.RequireDependency<World>();
            {
                var point = World.PlayerPoints.First();
                SpawnPlayerCharacter( point, this, Player );
            }
            foreach (var point in World.EnemyPoints) {
                SpawnEnemyCharacter( point, this );
            }
            foreach (var point in World.ThingPoints) {
                SpawnThing( point );
            }
        }
        public override void Dispose() {
            Player.Dispose();
            base.Dispose();
        }

        // Update
        public override void FixedUpdate() {
            Player.FixedUpdate();
        }
        public override void Update() {
            Player.Update();
            if (IsDirty) {
                if (State is GameState.Playing) {
                    if (IsLoser( Player )) {
                        Player.State = PlayerState.Looser;
                        State = GameState.Completed;
                    } else if (IsWinner( Player )) {
                        Player.State = PlayerState.Winner;
                        State = GameState.Completed;
                    }
                }
                IsDirty = false;
            }
        }
        public override void LateUpdate() {
            Player.LateUpdate();
        }

        // Helpers
        private static void SpawnPlayerCharacter(PlayerPoint point, Game game, Player player) {
            player.Character = PlayerCharacterFactory.Create( player.Kind, point.transform.position, point.transform.rotation );
            player.Character.Game = game;
            player.Character.Player = player;
            player.Character.OnDamageEvent += info => {
                game.IsDirty = true;
            };
            player.IsInputEnabled = true;
        }
        private static void SpawnEnemyCharacter(EnemyPoint point, Game game) {
            var character = EnemyCharacterFactory.Create( point.transform.position, point.transform.rotation );
            character.Game = game;
            character.OnDamageEvent += info => {
                game.IsDirty = true;
            };
        }
        private static void SpawnThing(ThingPoint point) {
            var thing = GunFactory.Create( point.transform.position, point.transform.rotation );
        }
        // Helpers
        private static bool IsWinner(Player player) {
            var enemies = GameObject.FindObjectsByType<EnemyCharacter>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
            if (enemies.All( i => !i.IsAlive )) {
                return true;
            }
            return false;
        }
        private static bool IsLoser(Player player) {
            if (!player.Character!.IsAlive) {
                return true;
            }
            return false;
        }

    }
    // Level
    public enum Level {
        Level1,
        Level2,
        Level3
    }
}
