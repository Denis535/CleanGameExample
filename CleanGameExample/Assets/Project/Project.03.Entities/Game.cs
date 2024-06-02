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
        public new GameState State { get => base.State; set => base.State = value; }
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

        // Constructor
        public Game(IDependencyContainer container, Level level, string name, PlayerCharacterKind kind) {
            Level = level;
            Player = new Player( name, kind, this, CameraFactory.Camera() );
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
            Player.IsInputEnabled = !IsPaused && Player.Character != null;
            State = GameState.Running;
        }
        public override void Dispose() {
            State = GameState.Stopped;
            Player.Dispose();
            base.Dispose();
        }

        // Update
        public override void Update() {
            Player.Update();
        }
        public override void LateUpdate() {
            Player.LateUpdate();
        }

        // Helpers
        private static void SpawnPlayerCharacter(PlayerPoint point, Game game, Player player) {
            player.Character = PlayerCharacterFactory.Create( player.Kind, point.transform.position, point.transform.rotation );
            player.Character.Game = game;
            player.Character.Player = player;
        }
        private static void SpawnEnemyCharacter(EnemyPoint point, Game game) {
            var character = EnemyCharacterFactory.Create( point.transform.position, point.transform.rotation );
            character.Game = game;
        }
        private static void SpawnThing(ThingPoint point) {
            var thing = GunFactory.Create( point.transform.position, point.transform.rotation );
        }

    }
    // Level
    public enum Level {
        Level1,
        Level2,
        Level3
    }
}
