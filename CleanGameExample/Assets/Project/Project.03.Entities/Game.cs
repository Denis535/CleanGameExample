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

    public class Game : GameBase, IGame {

        // State
        public bool IsPaused { get; private set; }
        // Level
        public LevelEnum LevelEnum { get; private set; }
        // Entities
        public Player Player { get; private set; } = default!;
        private List<EnemyCharacter> Enemies_ { get; set; } = default!;
        public IReadOnlyList<EnemyCharacter> Enemies => Enemies_;
        public World World { get; private set; } = default!;

        // Awake
        public override void Awake() {
            Awake( Context.GetValue<GameFactory.Args>() );
        }
        private void Awake(GameFactory.Args args) {
            LevelEnum = args.Level;
            Player = new Player( args.Name, args.Character );
            Enemies_ = new List<EnemyCharacter>();
            World = Utils.Container.RequireDependency<World>();
        }
        public override void OnDestroy() {
            Player.Dispose();
        }

        // Pause
        public void Pause() {
            Assert.Operation.Message( $"Game must be non-paused" ).Valid( !IsPaused );
            IsPaused = true;
            Player.SetInputEnabled( !IsPaused && Player.Camera != null );
        }
        public void UnPause() {
            Assert.Operation.Message( $"Game must be paused" ).Valid( IsPaused );
            IsPaused = false;
            Player.SetInputEnabled( !IsPaused && Player.Camera != null );
        }

        // Start
        public void Start() {
            {
                var point = World.PlayerPoints.First();
                Player.SetCamera( CameraFactory.Camera() );
                Player.SetCharacter( PlayerCharacterFactory.Create( this, Player, Player.CharacterEnum, point.transform.position, point.transform.rotation ) );
                Player.SetInputEnabled( !IsPaused && Player.Camera != null );
            }
            foreach (var point in World.EnemyPoints) {
                Enemies_.Add( EnemyCharacterFactory.Create( this, point.transform.position, point.transform.rotation ) );
            }
            foreach (var point in World.ThingPoints) {
                GunFactory.Create( point.transform.position, point.transform.rotation );
            }
        }
        public void Update() {
            Player.Update();
        }
        public void LateUpdate() {
            Player.LateUpdate();
        }

        // IGame
        void IGame.OnDamage(Character damager, Character character, bool isKilled) {
        }

    }
    // LevelEnum
    public enum LevelEnum {
        Level1,
        Level2,
        Level3
    }
}
