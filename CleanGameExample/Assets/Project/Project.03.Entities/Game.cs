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

    public class Game : GameBase2<Mode, Level>, IGame {

        // Input
        private InputActions_Game Input { get; }
        // Entities
        public Player Player { get; }
        public World World { get; }
        // IsDirty
        private bool IsDirty { get; set; }

        // Constructor
        public Game(IDependencyContainer container, Mode mode, Level level, string name, PlayerCharacterKind kind) : base( container, "Game", mode, level ) {
            Input = new InputActions_Game();
            Input.Enable();
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
        }
        public override void Dispose() {
            Player.Dispose();
            Input.Dispose();
            base.Dispose();
        }

        // Update
        public override void FixedUpdate() {
        }
        public override void Update() {
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
            if (Input.Game.Pause.WasPressedThisFrame()) {
                IsPaused = !IsPaused;
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
            Player.State = PlayerState.Winner;
            State = GameState.Completed;
        }
        private void OnLooser() {
            Player.State = PlayerState.Looser;
            State = GameState.Completed;
        }

    }
    // Mode
    public enum Mode {
        None
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
