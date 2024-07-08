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

        private UITheme Theme => Container.RequireDependency<UITheme>();
        private UIScreen Screen => Container.RequireDependency<UIScreen>();
        private Application2 Application { get; }
        private static SceneHandle Startup { get; } = new SceneHandle( R.Project.Scenes.Value_Startup );
        private SceneHandle MainScene { get; } = new SceneHandle( R.Project.Scenes.Value_MainScene );
        private SceneHandle GameScene { get; } = new SceneHandle( R.Project.Scenes.Value_GameScene );
        private SceneHandleDynamic WorldScene { get; } = new SceneHandleDynamic();
        public bool IsMainSceneLoaded => MainScene.IsSucceeded;
        public bool IsGameSceneLoaded => GameScene.IsSucceeded;
        public bool IsWorldSceneLoaded => WorldScene.IsValid && WorldScene.IsSucceeded;

        public UIRouter(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
        }
        public override void Dispose() {
            using (@lock.Enter()) {
                base.Dispose();
            }
        }

        public static async void LoadStartup() {
            using (@lock.Enter()) {
#if !UNITY_EDITOR
                Debug.LogFormat( "Load: Startup" );
#endif
                await LoadSceneAsync_Startup();
            }
        }

        public async void LoadMainScene() {
            using (@lock.Enter()) {
#if !UNITY_EDITOR
                Debug.LogFormat( "Load: MainScene" );
#endif
                Theme.PlayMainTheme();
                Screen.ShowMainScreen();
                await LoadSceneAsync_Main();
            }
        }

        public async void LoadGameScene(GameInfo gameInfo, PlayerInfo playerInfo) {
            using (@lock.Enter()) {
#if !UNITY_EDITOR
                Debug.LogFormat( "Load: GameScene: {0}, {1}", gameInfo, playerInfo );
#endif
                Theme.PlayLoadingTheme();
                Screen.ShowLoadingScreen();
                {
                    await UnloadSceneAsync_Main();
                }
                await LoadSceneAsync_Game();
                await LoadSceneAsync_World( GetWorldSceneAddress( gameInfo.Level ) );
                Application.RunGame( gameInfo, playerInfo );
                Application.Game!.OnPauseEvent += i => Theme.IsPaused = i;
                Theme.PlayGameTheme();
                Screen.ShowGameScreen();
            }
        }

        public async void ReloadGameScene(GameInfo gameInfo, PlayerInfo playerInfo) {
            using (@lock.Enter()) {
#if !UNITY_EDITOR
                Debug.LogFormat( "Reload: GameScene: {0}, {1}", gameInfo, playerInfo );
#endif
                Theme.PlayLoadingTheme();
                Screen.ShowLoadingScreen();
                {
                    Application.StopGame();
                    await UnloadSceneAsync_World();
                    await UnloadSceneAsync_Game();
                }
                await LoadSceneAsync_Game();
                await LoadSceneAsync_World( GetWorldSceneAddress( gameInfo.Level ) );
                Application.RunGame( gameInfo, playerInfo );
                Application.Game!.OnPauseEvent += i => Theme.IsPaused = i;
                Theme.PlayGameTheme();
                Screen.ShowGameScreen();
            }
        }

        public async void UnloadGameScene() {
            using (@lock.Enter()) {
#if !UNITY_EDITOR
                Debug.LogFormat( "Unload: GameScene" );
#endif
                Theme.PlayUnloadingTheme();
                Screen.ShowUnloadingScreen();
                {
                    Application.StopGame();
                    await UnloadSceneAsync_World();
                    await UnloadSceneAsync_Game();
                }
                await LoadSceneAsync_Main();
                Theme.PlayMainTheme();
                Screen.ShowMainScreen();
            }
        }

        public async void Quit() {
            using (@lock.Enter()) {
#if !UNITY_EDITOR
                Debug.Log( "Quit" );
#endif
                Theme.StopTheme();
                Screen.HideScreen();
                {
                    if (Application.Game != null) Application.StopGame();
                    if (WorldScene.IsValid) await UnloadSceneAsync_World();
                    if (GameScene.IsValid) await UnloadSceneAsync_Game();
                    if (MainScene.IsValid) await UnloadSceneAsync_Main();
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
            WorldScene.SetUp( key );
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
            await WorldScene.UnloadAsync();
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
