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
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;
    using UnityEngine.SceneManagement;

    public class UIRouter : UIRouterBase {

        private static readonly Lock @lock = new Lock();
        private static readonly SceneHandleAsync program = new SceneHandleAsync( R.Project.Scenes.Program_Value );
        private readonly SceneHandleAsync mainScene = new SceneHandleAsync( R.Project.Scenes.MainScene_Value );
        private readonly SceneHandleAsync gameScene = new SceneHandleAsync( R.Project.Scenes.GameScene_Value );
        private readonly SceneHandleAsync world1 = new SceneHandleAsync( R.Project.Entities.Worlds.World_01_Value );
        private readonly SceneHandleAsync world2 = new SceneHandleAsync( R.Project.Entities.Worlds.World_02_Value );
        private readonly SceneHandleAsync world3 = new SceneHandleAsync( R.Project.Entities.Worlds.World_03_Value );
        private UIRouterState state;

        // Globals
        private Application2 Application { get; set; } = default!;
        // State
        public UIRouterState State {
            get {
                return state;
            }
            private set {
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
        public new void Awake() {
            base.Awake();
            Application = this.GetDependencyContainer().RequireDependency<Application2>( null );
#if !UNITY_EDITOR
            UnityEngine.Application.wantsToQuit += OnQuit;
#endif
        }
        public new void OnDestroy() {
            base.OnDestroy();
        }

        // LoadScene
        public static async Task LoadProgramAsync() {
            Release.LogFormat( "Load: Program" );
            using (@lock.Enter()) {
                await LoadSceneAsync_Program();
            }
        }
        public async Task LoadMainSceneAsync() {
            Release.LogFormat( "Load: MainScene" );
            if (Application.Game != null) {
                Application.StopGame();
            }
            State = UIRouterState.MainSceneLoading;
            using (@lock.Enter()) {
                await UnloadSceneAsync_GameScene();
                await UnloadSceneAsync_World();
                await LoadSceneAsync_MainScene();
            }
            State = UIRouterState.MainSceneLoaded;
        }
        public async Task LoadGameSceneAsync(Level level, Character character) {
            Release.LogFormat( "Load: GameScene: {0}, {1}", level, character );
            State = UIRouterState.GameSceneLoading;
            using (@lock.Enter()) {
                await UnloadSceneAsync_MainScene();
                await Task.Delay( 3_000 );
                using (InitializationContext.Enter<Game, Game.Arguments>( new Game.Arguments( level ) )) {
                    using (InitializationContext.Enter<Player, Player.Arguments>( new Player.Arguments( character ) )) {
                        await LoadSceneAsync_World( level );
                        await LoadSceneAsync_GameScene();
                    }
                }
            }
            State = UIRouterState.GameSceneLoaded;
            Application.RunGame();
        }

#if UNITY_EDITOR
        // Quit
        public void Quit() {
            Release.Log( "Quit" );
            OnQuitAsync();
        }
#else
        // Quit
        public void Quit() {
            Release.Log( "Quit" );
            UnityEngine.Application.Quit();
        }
        private bool OnQuit() {
            if (!IsQuited) {
                OnQuitAsync();
                return false;
            }
            return true;
        }
#endif
        private async void OnQuitAsync() {
            if (Application.Game != null) {
                Application.StopGame();
            }
            State = UIRouterState.Quitting;
            using (@lock.Enter()) {
                await UnloadSceneAsync_MainScene();
                await UnloadSceneAsync_GameScene();
                await UnloadSceneAsync_World();
            }
            State = UIRouterState.Quited;
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            UnityEngine.Application.Quit();
#endif
        }

        // Helpers
        private static async Task LoadSceneAsync_Program() {
            await program.LoadAsync( LoadSceneMode.Single, false );
            await program.ActivateAsync();
            SceneManager.SetActiveScene( program.SceneAsync.Result );
        }
        public async Task LoadSceneAsync_MainScene() {
            await mainScene.LoadAsync( LoadSceneMode.Additive, false );
            await mainScene.ActivateAsync();
            SceneManager.SetActiveScene( mainScene.SceneAsync.Result );
        }
        public async Task LoadSceneAsync_GameScene() {
            await gameScene.LoadAsync( LoadSceneMode.Additive, false );
            await gameScene.ActivateAsync();
            SceneManager.SetActiveScene( gameScene.SceneAsync.Result );
        }
        public async Task LoadSceneAsync_World(Level level) {
            switch (level) {
                case Level.Level1: {
                    await world1.LoadAsync( LoadSceneMode.Additive, false );
                    await world1.ActivateAsync();
                    SceneManager.SetActiveScene( world1.SceneAsync.Result );
                    break;
                }
                case Level.Level2: {
                    await world2.LoadAsync( LoadSceneMode.Additive, false );
                    await world2.ActivateAsync();
                    SceneManager.SetActiveScene( world2.SceneAsync.Result );
                    break;
                }
                case Level.Level3: {
                    await world3.LoadAsync( LoadSceneMode.Additive, false );
                    await world3.ActivateAsync();
                    SceneManager.SetActiveScene( world3.SceneAsync.Result );
                    break;
                }
                default:
                    throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" );
            }
        }
        // Helpers
        private async Task UnloadSceneAsync_MainScene() {
            await mainScene.UnloadAsync();
        }
        private async Task UnloadSceneAsync_GameScene() {
            await gameScene.UnloadAsync();
        }
        private async Task UnloadSceneAsync_World() {
            await world1.UnloadAsync();
            await world2.UnloadAsync();
            await world3.UnloadAsync();
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
