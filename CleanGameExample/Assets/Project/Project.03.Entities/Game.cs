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

    public abstract class GameBase2 : GameBase {

        // IsPaused
        public bool IsPaused { get; private set; }
        // OnPauseEvent
        public event Action? OnPauseEvent;
        public event Action? OnUnPauseEvent;

        // Constructor
        public GameBase2() {
        }

        // Start
        public abstract void Start();
        public abstract void Update();
        public abstract void LateUpdate();

        // Pause
        public virtual void Pause() {
            Assert.Operation.Message( $"Game must be non-paused" ).Valid( !IsPaused );
            IsPaused = true;
            OnPauseEvent?.Invoke();
        }
        public virtual void UnPause() {
            Assert.Operation.Message( $"Game must be paused" ).Valid( IsPaused );
            IsPaused = false;
            OnUnPauseEvent?.Invoke();
        }

    }
    public class Game : GameBase2, IGame {

        // Level
        public LevelEnum LevelEnum { get; private set; }
        // Entities
        public Player Player { get; private set; } = default!;
        public World World { get; private set; } = default!;
        // OnEntitySpawn
        public event Action<PlayerCharacter>? OnPlayerCharacterSpawn;
        public event Action<EnemyCharacter>? OnEnemyCharacterSpawn;
        public event Action<Thing>? OnThingSpawn;

        // Awake
        public override void Awake() {
            Awake( Context.GetValue<GameFactory.Args>() );
        }
        private void Awake(GameFactory.Args args) {
            LevelEnum = args.Level;
            Player = new Player( args.Name, args.Character );
            World = Utils.Container.RequireDependency<World>();
        }
        public override void OnDestroy() {
            Player.Dispose();
        }

        // Start
        public override void Start() {
            {
                Player.Start();
                Player.SetCamera( CameraFactory.Camera() );
                Player.SetCharacter( SpawnPlayerCharacter( World.PlayerPoints.First() ) );
            }
            foreach (var point in World.EnemyPoints) {
                SpawnEnemyCharacter( point );
            }
            foreach (var point in World.ThingPoints) {
                SpawnThing( point );
            }
        }
        public override void Update() {
            Player.Update();
        }
        public override void LateUpdate() {
            Player.LateUpdate();
        }

        // Pause
        public override void Pause() {
            base.Pause();
            Player.Pause();
        }
        public override void UnPause() {
            base.UnPause();
            Player.UnPause();
        }

        // SpawnEntity
        private PlayerCharacter SpawnPlayerCharacter(PlayerPoint point) {
            var character_ = PlayerCharacterFactory.Create( this, Player, Player.CharacterEnum, point.transform.position, point.transform.rotation );
            OnPlayerCharacterSpawn?.Invoke( character_ );
            return character_;
        }
        private void SpawnEnemyCharacter(EnemyPoint point) {
            var character = EnemyCharacterFactory.Create( this, point.transform.position, point.transform.rotation );
            OnEnemyCharacterSpawn?.Invoke( character );
        }
        private void SpawnThing(ThingPoint point) {
            var thing = GunFactory.Create( point.transform.position, point.transform.rotation );
            OnThingSpawn?.Invoke( thing );
        }

    }
    // LevelEnum
    public enum LevelEnum {
        Level1,
        Level2,
        Level3
    }
}
