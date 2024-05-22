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

    public class Game : GameBase {
        public record Args(PlayerCharacterEnum Character, LevelEnum Level);

        // State
        public bool IsPaused { get; private set; }
        // Entities
        public Player Player { get; private set; } = default!;
        public LevelEnum LevelEnum { get; private set; }
        public World World { get; private set; } = default!;

        // Awake
        public override void Awake() {
            var args = Context.GetValue<Args>();
            LevelEnum = args.Level;
            Player = new Player( args.Character );
            World = Utils.Container.RequireDependency<World>( null );
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
                Player.SetCamera( EntityFactory.Camera() );
                Player.SetCharacter( CharacterFactory.PlayerCharacter( Player.CharacterEnum, point.transform.position, point.transform.rotation ) );
                Player.SetInputEnabled( !IsPaused && Player.Camera != null );
            }
            foreach (var point in World.EnemyPoints) {
                CharacterFactory.EnemyCharacter( point.transform.position, point.transform.rotation );
            }
            foreach (var point in World.LootPoints) {
                ThingFactory.Gun( point.transform.position, point.transform.rotation );
            }
        }
        public void Update() {
            Player.Update();
        }
        public void LateUpdate() {
            Player.LateUpdate();
        }

    }
    // LevelEnum
    public enum LevelEnum {
        Level1,
        Level2,
        Level3
    }
}
