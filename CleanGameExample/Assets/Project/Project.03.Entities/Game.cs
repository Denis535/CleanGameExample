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

        // Name
        public abstract string Name { get; }
        public abstract GameMode Mode { get; }
        public abstract GameLevel Level { get; }
        // State
        public virtual GameState State {
            get => state;
            protected set {
                Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( value != state );
                state = value;
                OnStateChangeEvent?.Invoke( state );
            }
        }
        public event Action<GameState>? OnStateChangeEvent;
        // Framework
        public abstract Player Player { get; }
        public abstract World World { get; }
        // IsDirty
        protected bool IsDirty { get; set; }

        // Constructor
        public GameBase3(IDependencyContainer container) : base( container ) {
            OnPauseEvent += value => {
                Time.timeScale = value ? 0f : 1f;
            };
        }
        public override void Dispose() {
            Time.timeScale = 1f;
            base.Dispose();
        }

        // Spawn
        protected abstract void SpawnPlayerCharacter(PlayerPoint point);
        protected abstract void SpawnEnemyCharacter(EnemyPoint point);
        protected abstract void SpawnThing(ThingPoint point);

        // IsWinner
        protected abstract bool IsWinner();
        protected abstract bool IsLoser();

        // OnWinner
        protected abstract void OnWinner();
        protected abstract void OnLoser();

    }
    public class Game : GameBase3 {

        // Name
        public override string Name { get; }
        public override GameMode Mode { get; }
        public override GameLevel Level { get; }
        // State
        public override GameState State { get => base.State; protected set => base.State = value; }
        // Framework
        public override Player Player { get; }
        public override World World { get; }
        // Input
        private InputActions_Game Input { get; }

        // Constructor
        public Game(IDependencyContainer container, string gameName, GameMode gameMode, GameLevel gameLevel, string playerName, PlayerKind playerKind) : base( container ) {
            Name = gameName;
            Mode = gameMode;
            Level = gameLevel;
            Player = new Player( container, playerName, playerKind, Camera2.Factory.Create() );
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
        protected override void SpawnPlayerCharacter(PlayerPoint point) {
            Player.Character = PlayerCharacter.Factory.Create( (PlayerCharacterType) Player.Kind, point.transform.position, point.transform.rotation );
            Player.Character.Game = this;
            Player.Character.Player = Player;
            Player.Character.OnDamageEvent += info => {
                IsDirty = true;
            };
        }
        protected override void SpawnEnemyCharacter(EnemyPoint point) {
            var character = EnemyCharacter.Factory.Create( point.transform.position, point.transform.rotation );
            character.Game = this;
            character.OnDamageEvent += info => {
                IsDirty = true;
            };
        }
        protected override void SpawnThing(ThingPoint point) {
            var thing = Gun.Factory.Create( point.transform.position, point.transform.rotation, null );
        }

        // IsWinner
        protected override bool IsWinner() {
            if (State is GameState.Playing) {
                var enemies = GameObject.FindObjectsByType<EnemyCharacter>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
                if (enemies.All( i => !i.IsAlive )) {
                    return true;
                }
            }
            return false;
        }
        protected override bool IsLoser() {
            if (State is GameState.Playing) {
                if (!Player.Character!.IsAlive) {
                    return true;
                }
            }
            return false;
        }

        // OnWinner
        protected override void OnWinner() {
            Player.State = PlayerState.Winner;
            State = GameState.Completed;
        }
        protected override void OnLoser() {
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
