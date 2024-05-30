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
    using UnityEngine.AddressableAssets;
    using UnityEngine.Audio;
    using UnityEngine.Framework.App;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class Application2 : ApplicationBase {

        private Storage storage = default!;
        private Storage.ProfileSettings profileSettings = default!;
        private Storage.VideoSettings videoSettings = default!;
        private Storage.AudioSettings audioSettings = default!;
        private Storage.Preferences preferences = default!;
        private Game? game;

        // App
        public Storage Storage => this.Validate().storage;
        public Storage.ProfileSettings ProfileSettings => this.Validate().profileSettings;
        public Storage.VideoSettings VideoSettings => this.Validate().videoSettings;
        public Storage.AudioSettings AudioSettings => this.Validate().audioSettings;
        public Storage.Preferences Preferences => this.Validate().preferences;
        public IAuthenticationService AuthenticationService {
            get {
                this.Validate();
                return Unity.Services.Authentication.AuthenticationService.Instance;
            }
        }
        // Entities
        public Game? Game => this.Validate().game;

        // Awake
        public override void Awake() {
            storage = new Storage();
            profileSettings = new Storage.ProfileSettings();
            videoSettings = new Storage.VideoSettings();
            audioSettings = new Storage.AudioSettings( Addressables.LoadAssetAsync<AudioMixer>( R.UnityEngine.Audio.AudioMixer_Value ).GetResult() );
            preferences = new Storage.Preferences();
        }
        public override void OnDestroy() {
        }

        // RunGame
        public void RunGame(LevelEnum level, string name, PlayerCharacterEnum character) {
            Assert.Operation.Message( $"Game must be null" ).Valid( game is null );
            GameFactory.Initialize();
            CameraFactory.Initialize();
            PlayerCharacterFactory.Initialize();
            EnemyCharacterFactory.Initialize();
            GunFactory.Initialize();
            BulletFactory.Initialize();
            game = GameFactory.Create( level, name, character );
            game.RunGame();
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( game is not null );
            game.StopGame();
            GameObject.DestroyImmediate( game );
            game = null;
            GameFactory.Deinitialize();
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
