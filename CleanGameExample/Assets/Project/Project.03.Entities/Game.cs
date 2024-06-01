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

        // Level
        public Level Level { get; }
        // Entities
        public Player Player { get; }
        public World World { get; }

        // Constructor
        public Game(IDependencyContainer container, Level level, string name, PlayerCharacterEnum character) {
            State = GameState.Running;
            Level = level;
            Player = new Player( name, character );
            World = container.RequireDependency<World>();
            {
                SpawnCamera();
                SpawnPlayerCharacter( World.PlayerPoints.First() );
                Player.IsInputEnabled = true;
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
            State = GameState.Stopped;
        }

        // Update
        public override void Update() {
            Player.Update();
        }
        public override void LateUpdate() {
            Player.LateUpdate();
        }

        // Pause
        public override void Pause() {
            base.Pause();
            Player.IsInputEnabled = false;
        }
        public override void UnPause() {
            base.UnPause();
            Player.IsInputEnabled = true;
        }

        // SpawnEntity
        private void SpawnCamera() {
            Player.Camera = CameraFactory.Camera();
        }
        private void SpawnPlayerCharacter(PlayerPoint point) {
            Player.Character = PlayerCharacterFactory.Create( this, Player, Player.CharacterEnum, point.transform.position, point.transform.rotation );
            OnSpawnPlayerCharacter( Player.Character );
        }
        private void SpawnEnemyCharacter(EnemyPoint point) {
            var character = EnemyCharacterFactory.Create( this, point.transform.position, point.transform.rotation );
            OnSpawnEnemyCharacter( character );
        }
        private void SpawnThing(ThingPoint point) {
            var thing = GunFactory.Create( point.transform.position, point.transform.rotation );
            OnSpawnThing( thing );
        }

        // OnSpawnEntity
        private void OnSpawnPlayerCharacter(PlayerCharacter character) {
        }
        private void OnSpawnEnemyCharacter(EnemyCharacter character) {
        }
        private void OnSpawnThing(Thing thing) {
        }

        // OnCharacterDamage
        //private void OnPlayerCharacterDamage(PlayerCharacter character, DamageInfo info) {
        //    if (!character.IsAlive) {
        //        Player.OnLose();
        //    }
        //}
        //private void OnEnemyCharacterDamage(EnemyCharacter character, DamageInfo info) {
        //    if (!character.IsAlive) {
        //        if (GameObject.FindObjectsByType<EnemyCharacter>( FindObjectsSortMode.None ).All( i => !i.IsAlive )) {
        //            Player.OnWin();
        //        }
        //    }
        //}

    }
    // Level
    public enum Level {
        Level1,
        Level2,
        Level3
    }
}
