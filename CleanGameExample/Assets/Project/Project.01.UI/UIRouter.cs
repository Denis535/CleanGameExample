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
        // IsSceneLoaded
        public bool IsMainSceneLoaded => MainScene.IsSucceeded;
        public bool IsGameSceneLoaded => GameScene.IsSucceeded;
        public bool IsWorldLoaded => World != null;
        // Scene
        private static SceneHandle Startup { get; } = new SceneHandle( R.Project.Scenes.Value_Startup );
        private SceneHandle MainScene { get; } = new SceneHandle( R.Project.Scenes.Value_MainScene );
        private SceneHandle GameScene { get; } = new SceneHandle( R.Project.Scenes.Value_GameScene );
        private SceneHandle? World { get; set; }

        // Constructor
        public UIRouter(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
        }
        public override void Dispose() {
            base.Dispose();
        }

        // LoadStartupAsync
        public static async void LoadStartupAsync() {
            Release.LogFormat( "Load: Startup" );
            using (@lock.Enter()) {
                await LoadSceneAsync_Startup();
            }
        }

        // LoadMainSceneAsync
        public async void LoadMainSceneAsync() {
            Release.LogFormat( "Load: MainScene" );
            using (@lock.Enter()) {
                Screen.ShowMainScreen();
                Theme.PlayMainThemes();
                {
                    Application.DestroyGame();
                    await UnloadAsync();
                }
                await LoadSceneAsync_Main();
            }
        }

        // LoadGameSceneAsync
        public async void LoadGameSceneAsync(GameLevel level, string name, PlayerCharacterKind kind) {
            Release.LogFormat( "Load: GameScene: {0}, {1}, {2}", level, name, kind );
            using (@lock.Enter()) {
                {
                    Application.DestroyGame();
                    await UnloadAsync();
                }
                await LoadSceneAsync_Game();
                await LoadSceneAsync_World( GetWorldAddress( level ) );
                Application.CreateGame( level, name, kind );
                Screen.ShowGameScreen();
                Theme.PlayGameThemes();
            }
        }

        // Quit
        public async void Quit() {
            Release.Log( "Quit" );
            using (@lock.Enter()) {
                Theme.Stop();
                Screen.Hide();
                Application.DestroyGame();
                await UnloadAsync();
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
            World = new SceneHandle( key );
            await World.Load( LoadSceneMode.Additive, false ).WaitAsync();
            await World.ActivateAsync();
            SceneManager.SetActiveScene( await World.GetValueAsync() );
        }
        // Helpers
        private async Task UnloadAsync() {
            if (MainScene.IsValid) {
                await MainScene.UnloadAsync();
            }
            if (World != null) {
                await World.UnloadAsync();
                World = null;
            }
            if (GameScene.IsValid) {
                await GameScene.UnloadAsync();
            }
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
