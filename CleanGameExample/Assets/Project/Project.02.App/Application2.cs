﻿#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.Entities;
    using Project.Entities.Characters;
    using Project.Entities.Things;
    using Unity.Services.Authentication;
    using Unity.Services.Core;
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

        // InitializeAsync
        public async Task InitializeAsync(CancellationToken cancellationToken) {
            cancellationToken = CancellationTokenSource.CreateLinkedTokenSource( DisposeCancellationToken, cancellationToken ).Token;
            if (UnityServices.State == ServicesInitializationState.Uninitialized) {
                var options = new InitializationOptions();
                if (Storage.Profile != null) options.SetProfile( Storage.Profile );
                await UnityServices.InitializeAsync( options ).WaitAsync( cancellationToken );
            }
            if (!AuthenticationService.IsSignedIn) {
                var options = new SignInOptions();
                options.CreateAccount = true;
                await AuthenticationService.SignInAnonymouslyAsync( options ).WaitAsync( cancellationToken );
            }
        }

        // CreateGame
        public void CreateGame(string gameName, GameMode gameMode, GameLevel gameLevel, string playerName, PlayerKind playerKind) {
            Assert.Operation.Message( $"Game must be null" ).Valid( Game is null );
            Camera2.Factory.Load();
            PlayerCharacter.Factory.Load();
            EnemyCharacter.Factory.Load();
            Gun.Factory.Load();
            Bullet.Factory.Load();
            Game = new Game( Container, gameName, gameMode, gameLevel, playerName, playerKind );
        }
        public void DestroyGame() {
            Assert.Operation.Message( $"Game must be non-null" ).Valid( Game is not null );
            Game.Dispose();
            Game = null;
            Camera2.Factory.Unload();
            PlayerCharacter.Factory.Unload();
            EnemyCharacter.Factory.Unload();
            Gun.Factory.Unload();
            Bullet.Factory.Unload();
            Array.Clear( Utils.RaycastHitBuffer, 0, Utils.RaycastHitBuffer.Length );
            Array.Clear( Utils.ColliderBuffer, 0, Utils.ColliderBuffer.Length );
        }

    }
}
