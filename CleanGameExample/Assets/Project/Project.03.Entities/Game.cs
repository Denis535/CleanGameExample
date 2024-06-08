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

    public abstract class GameBase3 : GameBase2 {

        // Level
        public Level Level { get; }
        // Input
        protected InputActions_Game Input { get; }

        // Constructor
        public GameBase3(IDependencyContainer container, Level level) : base( container ) {
            Level = level;
            Input = new InputActions_Game();
            Input.Enable();
        }
        public override void Dispose() {
            Input.Dispose();
            base.Dispose();
        }

        // Update
        public override void FixedUpdate() {
        }
        public override void Update() {
        }
        public override void LateUpdate() {
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
        protected abstract void OnLooser();

        // OnStateChange
        protected override void OnStateChange(GameState state) {
        }

        // OnPauseChange
        protected override void OnPauseChange(bool isPaused) {
        }

    }
    public class Game : GameBase3, IGame {

        // Entities
        public Player Player { get; }
        public World World { get; }
        // IsDirty
        private bool IsDirty { get; set; }

        // Constructor
        public Game(IDependencyContainer container, Level level, string name, PlayerCharacterKind kind) : base( container, level ) {
            Player = new Player( container, name, kind, this, CameraFactory.Camera() );
            World = container.RequireDependency<World>();
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
            Player.IsInputEnabled = true;
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
            if (Input.Game.Pause.WasPressedThisFrame()) {
                IsPaused = true;
            }
            Player.Update();
            if (IsDirty) {
                if (IsLoser()) {
                    OnLooser();
                }
                if (IsWinner()) {
                    OnWinner();
                }
                IsDirty = false;
            }
        }
        public override void LateUpdate() {
            Player.LateUpdate();
        }

        // OnStateChange
        protected override void OnStateChange(GameState state) {
        }

        // OnPauseChange
        protected override void OnPauseChange(bool isPaused) {
            Input.SetEnabled( !isPaused );
            Player.IsInputEnabled = !isPaused && Player.Character != null;
        }

        // Spawn
        protected override void SpawnPlayerCharacter(PlayerPoint point) {
            Player.Character = PlayerCharacterFactory.Create( Player.Kind, point.transform.position, point.transform.rotation );
            Player.Character.Game = this;
            Player.Character.Player = Player;
            Player.Character.OnDamageEvent += info => {
                IsDirty = true;
            };
        }
        protected override void SpawnEnemyCharacter(EnemyPoint point) {
            var character = EnemyCharacterFactory.Create( point.transform.position, point.transform.rotation );
            character.Game = this;
            character.OnDamageEvent += info => {
                IsDirty = true;
            };
        }
        protected override void SpawnThing(ThingPoint point) {
            var thing = GunFactory.Create( point.transform.position, point.transform.rotation );
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
        protected override void OnLooser() {
            Player.State = PlayerState.Looser;
            State = GameState.Completed;
        }

    }
    // Level
    public enum Level {
        Level1,
        Level2,
        Level3
    }
    public static class LevelExtensions {
        public static Level? GetNext(this Level level) {
            if (level < Level.Level3) return level + 1;
            return null;
        }
    }
}
