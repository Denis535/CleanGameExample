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

    public abstract class GameBase3 : GameBase2, IGame {

        private GameState state;
        private bool isPaused;

        // Name
        public string Name { get; }
        public GameMode Mode { get; }
        public GameLevel Level { get; }
        // State
        public GameState State {
            get => state;
            protected set {
                Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( value != state );
                state = value;
                OnStateChangeEvent?.Invoke( state );
            }
        }
        public event Action<GameState>? OnStateChangeEvent;
        // IsPaused
        public bool IsPaused {
            get => isPaused;
            set {
                if (value != isPaused) {
                    isPaused = value;
                    Time.timeScale = isPaused ? 0f : 1f;
                    OnPauseEvent?.Invoke( isPaused );
                }
            }
        }
        public event Action<bool>? OnPauseEvent;

        // Constructor
        public GameBase3(IDependencyContainer container, string name, GameMode mode, GameLevel level) : base( container ) {
            Name = name;
            Mode = mode;
            Level = level;
        }
        public override void Dispose() {
            Time.timeScale = 1f;
            base.Dispose();
        }

    }
    public class Game : GameBase3 {

        // Framework
        public Player Player { get; }
        public World World { get; }
        // Input
        private InputActions_Game Input { get; }
        // IsDirty
        protected bool IsDirty { get; set; }

        // Constructor
        public Game(IDependencyContainer container, string gameName, GameMode gameMode, GameLevel gameLevel, string playerName, PlayerKind playerKind) : base( container, gameName, gameMode, gameLevel ) {
            Player = new Player( container, playerName, playerKind ) {
                Camera = Camera2.Factory.Create()
            };
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
            Input.Dispose();
            Player.Dispose();
            base.Dispose();
        }

        // Update
        public void FixedUpdate() {
        }
        public void Update() {
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
        public void LateUpdate() {
        }

        // Spawn
        protected void SpawnPlayerCharacter(PlayerPoint point) {
            Player.Character = PlayerCharacter.Factory.Create( (PlayerCharacterType) Player.Kind, point.transform.position, point.transform.rotation );
            Player.Character.Game = this;
            Player.Character.Player = Player;
            Player.Character.OnDamageEvent += info => {
                IsDirty = true;
            };
        }
        protected void SpawnEnemyCharacter(EnemyPoint point) {
            var character = EnemyCharacter.Factory.Create( point.transform.position, point.transform.rotation );
            character.Game = this;
            character.OnDamageEvent += info => {
                IsDirty = true;
            };
        }
        protected void SpawnThing(ThingPoint point) {
            var thing = Gun.Factory.Create( point.transform.position, point.transform.rotation, null );
        }

        // IsWinner
        protected bool IsWinner() {
            if (State is GameState.Playing) {
                var enemies = GameObject.FindObjectsByType<EnemyCharacter>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
                if (enemies.All( i => !i.IsAlive )) {
                    return true;
                }
            }
            return false;
        }
        protected bool IsLoser() {
            if (State is GameState.Playing) {
                if (!Player.Character!.IsAlive) {
                    return true;
                }
            }
            return false;
        }

        // OnWinner
        protected void OnWinner() {
            Player.State = PlayerState.Winner;
            State = GameState.Completed;
        }
        protected void OnLoser() {
            Player.State = PlayerState.Loser;
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
