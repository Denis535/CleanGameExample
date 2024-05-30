﻿#nullable enable
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

        // Container
        private IDependencyContainer Container { get; }
        // App
        public Storage Storage { get; }
        public Storage.ProfileSettings ProfileSettings { get; }
        public Storage.VideoSettings VideoSettings { get; }
        public Storage.AudioSettings AudioSettings { get; }
        public Storage.Preferences Preferences { get; }
        public IAuthenticationService AuthenticationService => Unity.Services.Authentication.AuthenticationService.Instance;
        // Entities
        public Game? Game { get; private set; }

        // Awake
        public Application2(IDependencyContainer container) {
            Container = container;
            Storage = new Storage();
            ProfileSettings = new Storage.ProfileSettings();
            VideoSettings = new Storage.VideoSettings();
            AudioSettings = new Storage.AudioSettings( Addressables.LoadAssetAsync<AudioMixer>( R.UnityEngine.Audio.AudioMixer_Value ).GetResult() );
            Preferences = new Storage.Preferences();
        }
        public override void Dispose() {
        }

        // RunGame
        public void RunGame(LevelEnum level, string name, PlayerCharacterEnum character) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            CameraFactory.Initialize();
            PlayerCharacterFactory.Initialize();
            EnemyCharacterFactory.Initialize();
            GunFactory.Initialize();
            BulletFactory.Initialize();
            Game = new Game( Container, level, name, character );
            Game.RunGame();
        }
        public void StopGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            Game.StopGame();
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
