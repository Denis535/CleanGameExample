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
        // Program
        private static SceneHandle Program { get; } = new SceneHandle( R.Project.Scenes.Program_Value );
        private SceneHandle MainScene { get; } = new SceneHandle( R.Project.Scenes.MainScene_Value );
        private SceneHandle GameScene { get; } = new SceneHandle( R.Project.Scenes.GameScene_Value );
        private DynamicSceneHandle World { get; } = new DynamicSceneHandle();

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
            await Program.LoadAsync( LoadSceneMode.Single, false, default );
            await Program.ActivateAsync( default );
            SceneManager.SetActiveScene( Program.Result );
        }
        public async Task LoadSceneAsync_MainScene() {
            await MainScene.LoadAsync( LoadSceneMode.Additive, false, default );
            await MainScene.ActivateAsync( default );
            SceneManager.SetActiveScene( MainScene.Result );
        }
        public async Task LoadSceneAsync_GameScene() {
            await GameScene.LoadAsync( LoadSceneMode.Additive, false, default );
            await GameScene.ActivateAsync( default );
            SceneManager.SetActiveScene( GameScene.Result );
        }
        public async Task LoadSceneAsync_World(Level level) {
            await World.LoadAsync( GetWorldAddress( level ), LoadSceneMode.Additive, false, default );
            await World.ActivateAsync( default );
            SceneManager.SetActiveScene( World.Result );
        }
        private static string GetWorldAddress(Level level) {
            switch (level) {
                case Level.Level1: return R.Project.Entities.Worlds.World_01_Value;
                case Level.Level2: return R.Project.Entities.Worlds.World_02_Value;
                case Level.Level3: return R.Project.Entities.Worlds.World_03_Value;
                default: throw Exceptions.Internal.NotSupported( $"Level {level} is not supported" );
            }
        }
        // Helpers
        private async Task UnloadSceneAsync_MainScene() {
            await MainScene.UnloadSafeAsync();
        }
        private async Task UnloadSceneAsync_GameScene() {
            await GameScene.UnloadSafeAsync();
        }
        private async Task UnloadSceneAsync_World() {
            await World.UnloadSafeAsync();
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
