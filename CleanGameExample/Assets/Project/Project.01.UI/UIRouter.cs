#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Entities;
    using Project.Entities.Characters;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.UI;
    using UnityEngine.SceneManagement;

    public class UIRouter : UIRouterBase2 {

        private static readonly Lock @lock = new Lock();

        // App
        private Application2 Application { get; }
        // Scene
        private static SceneHandle Startup { get; } = new SceneHandle( R.Project.Scenes.Startup_Value );
        private SceneHandle MainScene { get; } = new SceneHandle( R.Project.Scenes.MainScene_Value );
        private SceneHandle GameScene { get; } = new SceneHandle( R.Project.Scenes.GameScene_Value );
        private SceneHandleDynamic World { get; } = new SceneHandleDynamic();

        // Constructor
        public UIRouter(IDependencyContainer container) {
            Application = container.RequireDependency<Application2>();
        }
        public override void Dispose() {
            base.Dispose();
        }

        // LoadStartupAsync
        public static async Task LoadStartupAsync() {
            Release.LogFormat( "Load: Startup" );
            using (@lock.Enter()) {
                await LoadSceneAsync_Startup();
            }
        }

        // LoadMainSceneAsync
        public async Task LoadMainSceneAsync() {
            Release.LogFormat( "Load: MainScene" );
            using (@lock.Enter()) {
                if (Application.Game != null) {
                    Application.DestroyGame();
                }
                {
                    SetMainSceneLoading();
                    await LoadSceneAsync_MainScene();
                    SetMainSceneLoaded();
                }
            }
        }

        // LoadGameSceneAsync
        public async Task LoadGameSceneAsync(Level level, string name, PlayerCharacterEnum character) {
            Release.LogFormat( "Load: GameScene: {0}, {1}", level, character );
            using (@lock.Enter()) {
                {
                    SetGameSceneLoading();
                    await LoadSceneAsync_GameScene( GetWorldAddress( level ) );
                    SetGameSceneLoaded();
                }
                Application.CreateGame( level, name, character );
            }
        }

        // Quit
        public override async void Quit() {
            Release.Log( "Quit" );
            base.Quit();
            using (@lock.Enter()) {
                if (Application.Game != null) {
                    Application.DestroyGame();
                }
                {
                    SetUnloading();
                    if (World.IsValid) await World.Handle.UnloadSafeAsync();
                    await GameScene.UnloadSafeAsync();
                    await MainScene.UnloadSafeAsync();
                    SetUnloaded();
                }
            }
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            UnityEngine.Application.Quit();
#endif
        }

        // Helpers
        private static async Task LoadSceneAsync_Startup() {
            await Startup.Load( LoadSceneMode.Single, false ).WaitAsync();
            await Startup.ActivateAsync();
            SceneManager.SetActiveScene( await Startup.GetValueAsync() );
        }
        public async Task LoadSceneAsync_MainScene() {
            if (World.IsValid) await World.Handle.UnloadSafeAsync();
            await GameScene.UnloadSafeAsync();

            await MainScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await MainScene.ActivateAsync();
            SceneManager.SetActiveScene( await MainScene.GetValueAsync() );
        }
        public async Task LoadSceneAsync_GameScene(string world) {
            await MainScene.UnloadSafeAsync();
            await Task.Delay( 3_000 );

            await GameScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await GameScene.ActivateAsync();
            SceneManager.SetActiveScene( await GameScene.GetValueAsync() );

            await World.SetUp( world ).Load( LoadSceneMode.Additive, false ).WaitAsync();
            await World.Handle.ActivateAsync();
            SceneManager.SetActiveScene( await World.Handle.GetValueAsync() );
        }
        // Helpers
        private static string GetWorldAddress(Level level) {
            switch (level) {
                case Level.Level1: return R.Project.Entities.Worlds.World_01_Value;
                case Level.Level2: return R.Project.Entities.Worlds.World_02_Value;
                case Level.Level3: return R.Project.Entities.Worlds.World_03_Value;
                default: throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" );
            }
        }

    }
}
