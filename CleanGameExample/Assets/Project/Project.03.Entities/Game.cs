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
        protected bool IsDirty { get; set; }

        // Constructor
        public Game(IDependencyContainer container, Level level, string name, PlayerCharacterKind kind) : base( container ) {
            Level = level;
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
            Player.Update();
            if (IsDirty) {
                if (IsLoser()) {
                    OnLose();
                }
                if (IsWinner()) {
                    OnWin();
                }
                IsDirty = false;
            }
        }
        public override void LateUpdate() {
            Player.LateUpdate();
        }

        // Spawn
        protected virtual void SpawnPlayerCharacter(PlayerPoint point) {
            Player.Character = PlayerCharacterFactory.Create( Player.Kind, point.transform.position, point.transform.rotation );
            Player.Character.Game = this;
            Player.Character.Player = Player;
            Player.Character.OnDamageEvent += info => {
                IsDirty = true;
            };
        }
        protected virtual void SpawnEnemyCharacter(EnemyPoint point) {
            var character = EnemyCharacterFactory.Create( point.transform.position, point.transform.rotation );
            character.Game = this;
            character.OnDamageEvent += info => {
                IsDirty = true;
            };
        }
        protected virtual void SpawnThing(ThingPoint point) {
            var thing = GunFactory.Create( point.transform.position, point.transform.rotation );
        }

        // IsWinner
        protected virtual bool IsWinner() {
            if (State is GameState.Playing) {
                var enemies = GameObject.FindObjectsByType<EnemyCharacter>( FindObjectsInactive.Exclude, FindObjectsSortMode.None );
                if (enemies.All( i => !i.IsAlive )) {
                    return true;
                }
            }
            return false;
        }
        protected virtual bool IsLoser() {
            if (State is GameState.Playing) {
                if (!Player.Character!.IsAlive) {
                    return true;
                }
            }
            return false;
        }

        // OnWin
        protected virtual void OnWin() {
            Player.State = PlayerState.Winner;
            State = GameState.Completed;
        }
        protected virtual void OnLose() {
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
            if (level == Level.Level3) return null;
            return level + 1;
        }
    }
}
