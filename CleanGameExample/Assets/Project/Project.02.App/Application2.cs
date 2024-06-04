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

    public class Application2 : ApplicationBase2<Game> {

        // App
        public Storage Storage { get; }
        public Storage.ProfileSettings ProfileSettings { get; }
        public Storage.VideoSettings VideoSettings { get; }
        public Storage.AudioSettings AudioSettings { get; }
        public Storage.Preferences Preferences { get; }
        // App
        public IAuthenticationService AuthenticationService => Unity.Services.Authentication.AuthenticationService.Instance;
        // Entities
        public override Game? Game { get; protected set; }

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
            base.CreateGame( new Game( Container, level, name, kind ) );
        }
        public new void DestroyGame() {
            base.DestroyGame();
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
