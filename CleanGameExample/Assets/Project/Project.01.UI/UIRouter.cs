#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Entities;
    using Project.Entities.Characters;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.UI;
    using UnityEngine.SceneManagement;

    public abstract class UIRouterBase2 : UIRouterBase {

        private UIRouterState state = UIRouterState.None;

        // State
        public UIRouterState State {
            get => state;
            protected set {
                switch (value) {
                    // MainScene
                    case UIRouterState.MainSceneLoading:
                        Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( state is UIRouterState.None or UIRouterState.GameSceneLoaded );
                        state = value;
                        break;
                    case UIRouterState.MainSceneLoaded:
                        Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( state is UIRouterState.MainSceneLoading );
                        state = value;
                        break;
                    // GameScene
                    case UIRouterState.GameSceneLoading:
                        Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( state is UIRouterState.None or UIRouterState.MainSceneLoaded );
                        state = value;
                        break;
                    case UIRouterState.GameSceneLoaded:
                        Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( state is UIRouterState.GameSceneLoading );
                        state = value;
                        break;
                    // Quit
                    case UIRouterState.Quitting:
                        Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( state is UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoaded );
                        state = value;
                        break;
                    case UIRouterState.Quited:
                        Assert.Operation.Message( $"Transition from {state} to {value} is invalid" ).Valid( state is UIRouterState.Quitting );
                        state = value;
                        break;
                    // Misc
                    default:
                        throw Exceptions.Internal.NotSupported( $"Transition from {state} to {value} is not supported" );
                }
            }
        }
        // State/MainScene
        public bool IsMainSceneLoading => state == UIRouterState.MainSceneLoading;
        public bool IsMainSceneLoaded => state == UIRouterState.MainSceneLoaded;
        // State/GameScene
        public bool IsGameSceneLoading => state == UIRouterState.GameSceneLoading;
        public bool IsGameSceneLoaded => state == UIRouterState.GameSceneLoaded;
        // State/Quit
        public bool IsQuitting => state == UIRouterState.Quitting;
        public bool IsQuited => state == UIRouterState.Quited;

        // Awake
        public override void Awake() {
            Application.wantsToQuit += OnQuit;
        }
        public override void OnDestroy() {
        }

        // Quit
        public virtual void Quit() {
            if (OnQuit()) {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif
            }
        }
        protected virtual bool OnQuit() {
            return true;
        }

    }
    public class UIRouter : UIRouterBase2 {

        private static readonly Lock @lock = new Lock();

        // App
        private Application2 Application { get; set; } = default!;
        // Scene
        private static SceneHandle Startup { get; } = new SceneHandle( R.Project.Scenes.Startup_Value );
        private SceneHandle MainScene { get; } = new SceneHandle( R.Project.Scenes.MainScene_Value );
        private SceneHandle GameScene { get; } = new SceneHandle( R.Project.Scenes.GameScene_Value );
        private SceneHandleDynamic World { get; } = new SceneHandleDynamic();

        // Awake
        public override void Awake() {
            base.Awake();
            Application = Utils.Container.RequireDependency<Application2>();
        }
        public override void OnDestroy() {
            base.OnDestroy();
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
                    Application.StopGame();
                }
                {
                    State = UIRouterState.MainSceneLoading;
                    await LoadSceneAsync_MainScene();
                    State = UIRouterState.MainSceneLoaded;
                }
            }
        }

        // LoadGameSceneAsync
        public async Task LoadGameSceneAsync(LevelEnum level, string name, PlayerCharacterEnum character) {
            Release.LogFormat( "Load: GameScene: {0}, {1}", level, character );
            using (@lock.Enter()) {
                {
                    State = UIRouterState.GameSceneLoading;
                    await LoadSceneAsync_GameScene( GetWorldAddress( level ) );
                    State = UIRouterState.GameSceneLoaded;
                }
                Application.RunGame( level, name, character );
            }
        }

        // Quit
        public override void Quit() {
            Release.Log( "Quit" );
            base.Quit();
        }
        protected override bool OnQuit() {
            if (State != UIRouterState.Quited) {
                OnQuitAsync();
                return false;
            }
            return true;
        }
        private async void OnQuitAsync() {
            using (@lock.Enter()) {
                if (Application.Game != null) {
                    Application.StopGame();
                }
                {
                    State = UIRouterState.Quitting;
                    if (World.IsValid) await World.Handle.UnloadSafeAsync();
                    await GameScene.UnloadSafeAsync();
                    await MainScene.UnloadSafeAsync();
                    State = UIRouterState.Quited;
                }
            }
            Quit();
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
        private static string GetWorldAddress(LevelEnum level) {
            switch (level) {
                case LevelEnum.Level1: return R.Project.Entities.Worlds.World_01_Value;
                case LevelEnum.Level2: return R.Project.Entities.Worlds.World_02_Value;
                case LevelEnum.Level3: return R.Project.Entities.Worlds.World_03_Value;
                default: throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" );
            }
        }

    }
    // UIRouterState
    public enum UIRouterState {
        None,
        // MainScene
        MainSceneLoading,
        MainSceneLoaded,
        // GameScene
        GameSceneLoading,
        GameSceneLoaded,
        // Quit
        Quitting,
        Quited,
    }
}
