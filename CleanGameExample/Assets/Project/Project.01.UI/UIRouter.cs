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

    public class UIRouter : UIRouterBase2<UIRouterState> {

        private static readonly Lock @lock = new Lock();

        // Container
        private Application2 Application { get; }
        // Scene
        private static SceneHandle Startup { get; } = new SceneHandle( R.Project.Scenes.Value_Startup );
        private SceneHandle MainScene { get; } = new SceneHandle( R.Project.Scenes.Value_MainScene );
        private SceneHandle GameScene { get; } = new SceneHandle( R.Project.Scenes.Value_GameScene );
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
        public async Task LoadGameSceneAsync(GameLevel level, string name, PlayerCharacterKind kind) {
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
        private static string GetWorldAddress(GameLevel level) {
            switch (level) {
                case GameLevel.Level1: return R.Project.Entities.Worlds.Value_World_01;
                case GameLevel.Level2: return R.Project.Entities.Worlds.Value_World_02;
                case GameLevel.Level3: return R.Project.Entities.Worlds.Value_World_03;
                default: throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" );
            }
        }

    }
    public enum UIRouterState {
        None,
        // MainSceneLoading
        MainSceneLoading,
        MainSceneLoaded,
        // GameSceneLoading
        GameSceneLoading,
        GameSceneLoaded,
        // Quitting
        Quitting,
        Quited,
    }
}
