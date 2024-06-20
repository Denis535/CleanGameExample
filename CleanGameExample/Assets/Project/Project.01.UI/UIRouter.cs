#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Entities;
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
        // Scene
        private static SceneHandle Startup { get; } = new SceneHandle( R.Project.Scenes.Value_Startup );
        private SceneHandle MainScene { get; } = new SceneHandle( R.Project.Scenes.Value_MainScene );
        private SceneHandle GameScene { get; } = new SceneHandle( R.Project.Scenes.Value_GameScene );
        private SceneHandle? WorldScene { get; set; }
        // IsSceneLoaded
        public bool IsMainSceneLoaded => MainScene.IsSucceeded;
        public bool IsGameSceneLoaded => GameScene.IsSucceeded;
        public bool IsWorldSceneLoaded => WorldScene != null;

        // Constructor
        public UIRouter(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
        }
        public override void Dispose() {
            using (@lock.Enter()) {
                base.Dispose();
            }
        }

        // LoadStartup
        public static async void LoadStartup() {
            Release.LogFormat( "Load: Startup" );
            using (@lock.Enter()) {
                await LoadSceneAsync_Startup();
            }
        }

        // LoadMainScene
        public async void LoadMainScene() {
            Release.LogFormat( "Load: MainScene" );
            using (@lock.Enter()) {
                //Theme.PlayMainThemes();
                Screen.ShowMainWidget();
                {
                    // Load
                    await LoadSceneAsync_Main();
                }
            }
        }

        // LoadGameScene
        public async void LoadGameScene(string gameName, GameMode gameMode, GameLevel gameLevel, string playerName, PlayerKind playerKind) {
            Release.LogFormat( "Load: GameScene: {0}, {1}, {2}", gameName, gameMode, gameLevel );
            using (@lock.Enter()) {
                Screen.ShowLoadingWidget();
                {
                    // Unload
                    await UnloadSceneAsync_Main();
                    // Load
                    await LoadSceneAsync_Game();
                    await LoadSceneAsync_World( GetWorldSceneAddress( gameLevel ) );
                    Application.CreateGame( gameName, gameMode, gameLevel, playerName, playerKind );
                }
                //Theme.PlayGameThemes();
                Screen.ShowGameWidget();
            }
        }

        // ReloadGameScene
        public async void ReloadGameScene(string gameName, GameMode gameMode, GameLevel gameLevel, string playerName, PlayerKind playerKind) {
            Release.LogFormat( "Reload: GameScene: {0}, {1}, {2}", gameName, gameMode, gameLevel );
            using (@lock.Enter()) {
                //Theme.Stop();
                Screen.ShowLoadingWidget();
                {
                    // Unload
                    Application.DestroyGame();
                    await UnloadSceneAsync_World();
                    await UnloadSceneAsync_Game();
                    // Load
                    await LoadSceneAsync_Game();
                    await LoadSceneAsync_World( GetWorldSceneAddress( gameLevel ) );
                    Application.CreateGame( gameName, gameMode, gameLevel, playerName, playerKind );
                }
                //Theme.PlayGameThemes();
                Screen.ShowGameWidget();
            }
        }

        // UnloadGameScene
        public async void UnloadGameScene() {
            Release.LogFormat( "Unload: GameScene" );
            using (@lock.Enter()) {
                //Theme.Stop();
                Screen.ShowUnloadingWidget();
                {
                    // Unload
                    Application.DestroyGame();
                    await UnloadSceneAsync_World();
                    await UnloadSceneAsync_Game();
                    // Load
                    await LoadSceneAsync_Main();
                }
                //Theme.PlayMainThemes();
                Screen.ShowMainWidget();
            }
        }

        // Quit
        public async void Quit() {
            Release.Log( "Quit" );
            using (@lock.Enter()) {
                //Theme.Stop();
                Screen.ShowUnloadingWidget();
                if (Application.Game != null) Application.DestroyGame();
                if (WorldScene != null) await UnloadSceneAsync_World();
                if (GameScene.IsValid) await UnloadSceneAsync_Game();
                if (MainScene.IsValid) await UnloadSceneAsync_Main();
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
        private async Task LoadSceneAsync_Main() {
            await Task.Delay( 1_000 );
            await MainScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await MainScene.ActivateAsync();
            SceneManager.SetActiveScene( await MainScene.GetValueAsync() );
        }
        private async Task LoadSceneAsync_Game() {
            await Task.Delay( 3_000 );
            await GameScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await GameScene.ActivateAsync();
            SceneManager.SetActiveScene( await GameScene.GetValueAsync() );
        }
        private async Task LoadSceneAsync_World(string key) {
            WorldScene = new SceneHandle( key );
            await WorldScene.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await WorldScene.ActivateAsync();
            SceneManager.SetActiveScene( await WorldScene.GetValueAsync() );
        }
        // Helpers
        private async Task UnloadSceneAsync_Main() {
            await MainScene.UnloadAsync();
        }
        private async Task UnloadSceneAsync_Game() {
            await GameScene.UnloadAsync();
        }
        private async Task UnloadSceneAsync_World() {
            await WorldScene!.UnloadAsync();
            WorldScene = null;
        }
        // Helpers
        private static string GetWorldSceneAddress(GameLevel level) {
            switch (level) {
                case GameLevel.Level1: return R.Project.Entities.Worlds.Value_World_01;
                case GameLevel.Level2: return R.Project.Entities.Worlds.Value_World_02;
                case GameLevel.Level3: return R.Project.Entities.Worlds.Value_World_03;
                default: throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" );
            }
        }

    }
}
