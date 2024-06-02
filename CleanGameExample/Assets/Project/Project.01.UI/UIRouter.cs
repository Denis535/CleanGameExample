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
        public UIRouter(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
        }
        public override void Dispose() {
            base.Dispose();
        }

        // LoadStartupAsync
        public static async Task LoadStartupAsync() {
            Release.LogFormat( "Load: Startup" );
            using (@lock.Enter()) {
                await Startup.Load( LoadSceneMode.Single, false ).WaitAsync();
                await Startup.ActivateAsync();
                SceneManager.SetActiveScene( await Startup.GetValueAsync() );
            }
        }

        // LoadMainSceneAsync
        public async Task LoadMainSceneAsync() {
            Release.LogFormat( "Load: MainScene" );
            State = UIRouterState.MainSceneLoading;
            using (@lock.Enter()) {
                if (Application.Game != null) {
                    Application.DestroyGame();
                }
                {
                    if (World.IsValid) await World.Handle.UnloadSafeAsync();
                    await GameScene.UnloadSafeAsync();
                    await MainScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
                    await MainScene.ActivateAsync();
                    SceneManager.SetActiveScene( await MainScene.GetValueAsync() );
                }
            }
            State = UIRouterState.MainSceneLoaded;
        }

        // LoadGameSceneAsync
        public async Task LoadGameSceneAsync(Level level, string name, PlayerCharacterKind kind) {
            Release.LogFormat( "Load: GameScene: {0}, {1}", level, kind );
            State = UIRouterState.GameSceneLoading;
            using (@lock.Enter()) {
                {
                    await MainScene.UnloadSafeAsync();
                    await Task.Delay( 3_000 );
                    await GameScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
                    await GameScene.ActivateAsync();
                    await World.SetUp( GetWorldAddress( level ) ).Load( LoadSceneMode.Additive, false ).WaitAsync();
                    await World.Handle.ActivateAsync();
                    SceneManager.SetActiveScene( await World.Handle.GetValueAsync() );
                }
                {
                    Application.CreateGame( level, name, kind );
                }
            }
            State = UIRouterState.GameSceneLoaded;
        }

        // Quit
        public async void Quit() {
            Release.Log( "Quit" );
            State = UIRouterState.Quitting;
            using (@lock.Enter()) {
                if (Application.Game != null) {
                    Application.DestroyGame();
                }
                {
                    if (World.IsValid) await World.Handle.UnloadSafeAsync();
                    await GameScene.UnloadSafeAsync();
                    await MainScene.UnloadSafeAsync();
                }
            }
            State = UIRouterState.Quited;
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            UnityEngine.Application.Quit();
#endif
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
