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

        // Storage
        public Storage Storage { get; }
        public Storage.ProfileSettings ProfileSettings { get; }
        public Storage.VideoSettings VideoSettings { get; }
        public Storage.AudioSettings AudioSettings { get; }
        public Storage.Preferences Preferences { get; }
        // Service
        public IAuthenticationService AuthenticationService => Unity.Services.Authentication.AuthenticationService.Instance;
        // Game
        public Game? Game { get; private set; }

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
        public void CreateGame(GameLevel level, string name, PlayerCharacterKind kind) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            CameraFactory.Initialize();
            PlayerCharacterFactory.Initialize();
            EnemyCharacterFactory.Initialize();
            GunFactory.Initialize();
            BulletFactory.Initialize();
            Game = new Game( Container, GameMode.None, level, name, kind );
        }
        public void DestroyGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            Game.Dispose();
            Game = null;
            CameraFactory.Deinitialize();
            PlayerCharacterFactory.Deinitialize();
            EnemyCharacterFactory.Deinitialize();
            GunFactory.Deinitialize();
            BulletFactory.Deinitialize();
            Array.Clear( Utils.RaycastHitBuffer, 0, Utils.RaycastHitBuffer.Length );
            Array.Clear( Utils.ColliderBuffer, 0, Utils.ColliderBuffer.Length );
        }

    }
}
