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
    using UnityEngine.InputSystem;

    public class Game : GameBase2<GameMode, GameLevel, GameState>, IGame {

        // Deps
        public Player Player { get; }
        public World World { get; }
        // Input
        private InputActions_Game Input { get; }
        // IsDirty
        private bool IsDirty { get; set; }

        // Constructor
        public Game(IDependencyContainer container, GameMode mode, GameLevel level, string name, PlayerCharacterKind kind) : base( container, "Game", mode, level ) {
            Player = new Player( container, name, kind, this, CameraFactory.Camera() );
            World = container.RequireDependency<World>();
            Input = new InputActions_Game();
            Input.Enable();
            {
                var point = World.PlayerPoints.First();
                SpawnPlayerCharacter( point );
            }
            foreach (var point in World.EnemyPoints) {
                SpawnEnemyCharacter( point );
            }
            foreach (var point in World.ThingPoints) {
                SpawnThing( point );
            }
        }
        public override void Dispose() {
            Time.timeScale = 1f;
            Input.Dispose();
            Player.Dispose();
            base.Dispose();
        }

        // Update
        public override void FixedUpdate() {
        }
        public override void Update() {
            Player.Update();
            if (IsDirty) {
                if (IsLoser()) {
                    OnLoser();
                }
                if (IsWinner()) {
                    OnWinner();
                }
                IsDirty = false;
            }
        }
        public override void LateUpdate() {
        }

        // OnStateChange
        protected override void OnStateChange(GameState state) {
        }

        // OnPauseChange
        protected override void OnPauseChange(bool isPaused) {
            Time.timeScale = isPaused ? 0f : 1f;
        }

        // Spawn
        private void SpawnPlayerCharacter(PlayerPoint point) {
            Player.Character = PlayerCharacterFactory.Create( Player.Kind, point.transform.position, point.transform.rotation );
            Player.Character.Game = this;
            Player.Character.Player = Player;
            Player.Character.OnDamageEvent += info => {
                IsDirty = true;
            };
        }
        private void SpawnEnemyCharacter(EnemyPoint point) {
            var character = EnemyCharacterFactory.Create( point.transform.position, point.transform.rotation );
            character.Game = this;
            character.OnDamageEvent += info => {
                IsDirty = true;
            };
        }
        private void SpawnThing(ThingPoint point) {
            var thing = GunFactory.Create( point.transform.position, point.transform.rotation );
        }

        // IsWinner
        private bool IsWinner() {
            if (State is GameState.Playing) {
                var enemies = GameObject.FindObjectsByType<EnemyCharacter>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
                if (enemies.All( i => !i.IsAlive )) {
                    return true;
                }
            }
            return false;
        }
        private bool IsLoser() {
            if (State is GameState.Playing) {
                if (!Player.Character!.IsAlive) {
                    return true;
                }
            }
            return false;
        }

        // OnWinner
        private void OnWinner() {
            SetState( Player, PlayerState.Winner );
            State = GameState.Completed;
        }
        private void OnLoser() {
            SetState( Player, PlayerState.Loser );
            State = GameState.Completed;
        }

    }
    // GameMode
    public enum GameMode {
        None
    }
    // GameLevel
    public enum GameLevel {
        Level1,
        Level2,
        Level3
    }
    public static class GameLevelExtensions {
        public static bool IsLast(this GameLevel level) {
            return level == GameLevel.Level3;
        }
        public static GameLevel GetNext(this GameLevel level) {
            Assert.Operation.Message( $"Level {level} must be non-last" ).Valid( level != GameLevel.Level3 );
            return level + 1;
        }
    }
    // GameState
    public enum GameState {
        Playing,
        Completed
    }
}
