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

        // Deps
        private UITheme Theme => Container.RequireDependency<UITheme>();
        private UIScreen Screen => Container.RequireDependency<UIScreen>();
        private Application2 Application { get; }
        // IsLoaded
        public bool IsMainSceneLoaded => MainScene.IsSucceeded;
        public bool IsGameSceneLoaded => GameScene.IsSucceeded;
        public bool IsWorldLoaded => World.IsValid && World.Handle.IsSucceeded;
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
            Theme.PlayMainThemes();
            Screen.ShowMainScreen();
            using (@lock.Enter()) {
                await UnloadMainSceneAsync();
                await UnloadGameSceneAsync();
                await MainScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
                await MainScene.ActivateAsync();
                SceneManager.SetActiveScene( await MainScene.GetValueAsync() );
            }
        }

        // LoadGameSceneAsync
        public async Task LoadGameSceneAsync(GameLevel level, string name, PlayerCharacterKind kind) {
            Release.LogFormat( "Load: GameScene: {0}, {1}", level, kind );
            using (@lock.Enter()) {
                await UnloadMainSceneAsync();
                await UnloadGameSceneAsync();
                await Task.Delay( 3_000 );
                await GameScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
                await GameScene.ActivateAsync();
                await World.SetUp( GetWorldAddress( level ) ).Load( LoadSceneMode.Additive, false ).WaitAsync();
                await World.Handle.ActivateAsync();
                SceneManager.SetActiveScene( await World.Handle.GetValueAsync() );
                Application.CreateGame( level, name, kind );
            }
            Theme.PlayGameThemes();
            Screen.ShowGameScreen();
        }

        // Quit
        public async void Quit() {
            Release.Log( "Quit" );
            Theme.Stop();
            Screen.Hide();
            using (@lock.Enter()) {
                await UnloadMainSceneAsync();
                await UnloadGameSceneAsync();
            }
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            UnityEngine.Application.Quit();
#endif
        }

        // Helpers
        private async Task UnloadMainSceneAsync() {
            await MainScene.UnloadSafeAsync();
        }
        private async Task UnloadGameSceneAsync() {
            if (Application.Game != null) Application.DestroyGame();
            if (World.IsValid) await World.Handle.UnloadSafeAsync();
            await GameScene.UnloadSafeAsync();
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
}
