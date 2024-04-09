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
        private static readonly SceneHandle program = new SceneHandle( R.Project.Scenes.Program_Value );
        private readonly SceneHandle mainScene = new SceneHandle( R.Project.Scenes.MainScene_Value );
        private readonly SceneHandle gameScene = new SceneHandle( R.Project.Scenes.GameScene_Value );
        private readonly DynamicSceneHandle world = new DynamicSceneHandle();
        private UIRouterState state;

        // Deps
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
                using (Context.Begin<Game, Game.Arguments>( new Game.Arguments( level ) )) {
                    using (Context.Begin<Player, Player.Arguments>( new Player.Arguments( character ) )) {
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
            await program.LoadSceneAsync( LoadSceneMode.Single, false, default );
            await program.ActivateAsync();
            SceneManager.SetActiveScene( program.Scene );
        }
        public async Task LoadSceneAsync_MainScene() {
            await mainScene.LoadSceneAsync( LoadSceneMode.Additive, false, default );
            await mainScene.ActivateAsync();
            SceneManager.SetActiveScene( mainScene.Scene );
        }
        public async Task LoadSceneAsync_GameScene() {
            await gameScene.LoadSceneAsync( LoadSceneMode.Additive, false, default );
            await gameScene.ActivateAsync();
            SceneManager.SetActiveScene( gameScene.Scene );
        }
        public async Task LoadSceneAsync_World(Level level) {
            switch (level) {
                case Level.Level1: {
                    await world.LoadSceneAsync( R.Project.Entities.Worlds.World_01_Value, LoadSceneMode.Additive, false, default );
                    await world.ActivateAsync();
                    SceneManager.SetActiveScene( world.Scene );
                    break;
                }
                case Level.Level2: {
                    await world.LoadSceneAsync( R.Project.Entities.Worlds.World_02_Value, LoadSceneMode.Additive, false, default );
                    await world.ActivateAsync();
                    SceneManager.SetActiveScene( world.Scene );
                    break;
                }
                case Level.Level3: {
                    await world.LoadSceneAsync( R.Project.Entities.Worlds.World_03_Value, LoadSceneMode.Additive, false, default );
                    await world.ActivateAsync();
                    SceneManager.SetActiveScene( world.Scene );
                    break;
                }
                default:
                    throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" );
            }
        }
        // Helpers
        private async Task UnloadSceneAsync_MainScene() {
            await mainScene.UnloadSafeAsync();
        }
        private async Task UnloadSceneAsync_GameScene() {
            await gameScene.UnloadSafeAsync();
        }
        private async Task UnloadSceneAsync_World() {
            await world.UnloadSafeAsync();
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
