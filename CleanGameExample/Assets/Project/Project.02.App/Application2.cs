#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities;
    using Project.Entities.Characters;
    using Project.Entities.Things;
    using Unity.Services.Authentication;
    using UnityEngine;
    using UnityEngine.Framework.App;

    public class Application2 : ApplicationBase2 {

        // App
        public Storage Storage { get; }
        public Storage.ProfileSettings ProfileSettings { get; }
        public Storage.VideoSettings VideoSettings { get; }
        public Storage.AudioSettings AudioSettings { get; }
        public Storage.Preferences Preferences { get; }
        public IAuthenticationService AuthenticationService => Unity.Services.Authentication.AuthenticationService.Instance;
        // Entities
        public Game? Game { get; set; }
        public event Action<Game>? OnGameCreate;
        public event Action<Game>? OnGameDestroy;

        // Constructor
        public Application2(IDependencyContainer container) : base( container ) {
            Storage = new Storage();
            ProfileSettings = new Storage.ProfileSettings();
            VideoSettings = new Storage.VideoSettings();
            AudioSettings = new Storage.AudioSettings();
            Preferences = new Storage.Preferences();
        }
        public override void Dispose() {
            Storage.Dispose();
            ProfileSettings.Dispose();
            VideoSettings.Dispose();
            AudioSettings.Dispose();
            Preferences.Dispose();
            base.Dispose();
        }

        // CreateGame
        public void CreateGame(Level level, string name, PlayerCharacterKind kind) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            CameraFactory.Initialize();
            PlayerCharacterFactory.Initialize();
            EnemyCharacterFactory.Initialize();
            GunFactory.Initialize();
            BulletFactory.Initialize();
            Game = new Game( Container, level, name, kind );
            OnGameCreate?.Invoke( Game );
        }
        public void DestroyGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            OnGameDestroy?.Invoke( Game );
            Game.Dispose();
            Game = null;
            CameraFactory.Deinitialize();
            PlayerCharacterFactory.Deinitialize();
            EnemyCharacterFactory.Deinitialize();
            GunFactory.Deinitialize();
            BulletFactory.Deinitialize();
            Array.Clear( Physics2.RaycastHitBuffer, 0, Physics2.RaycastHitBuffer.Length );
            Array.Clear( Physics2.ColliderBuffer, 0, Physics2.ColliderBuffer.Length );
        }

    }
}
