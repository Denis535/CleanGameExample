#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.App;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class UITheme : UIAudioThemeBase {

        private static readonly string[] MainThemes = GetShuffled( new[] {
            R.Project.UI.MainScreen.Music.Theme,
        } );
        private static readonly string[] GameThemes = GetShuffled( new[] {
            R.Project.UI.GameScreen.Music.Theme_1,
            R.Project.UI.GameScreen.Music.Theme_2,
        } );

        // Globals
        private UIRouter Router { get; set; } = default!;
        private Application2 Application { get; set; } = default!;
        // State
        public UIThemeState State => GetState( Router.State );
        private ValueTracker2<UIThemeState, UITheme> StateTracker { get; } = new ValueTracker2<UIThemeState, UITheme>( i => i.State );
        public bool IsMainTheme => State == UIThemeState.MainTheme;
        public bool IsGameTheme => State == UIThemeState.GameTheme;

        // Awake
        public new void Awake() {
            base.Awake();
            Router = this.GetDependencyContainer().Resolve<UIRouter>( null );
            Application = this.GetDependencyContainer().Resolve<Application2>( null );
        }
        public new void OnDestroy() {
            Stop();
            base.OnDestroy();
        }

        // Start
        public void Start() {
        }
        public void Update() {
            if (StateTracker.IsChanged( this )) {
                if (IsMainTheme) {
                    Play( MainThemes.First() );
                } else
                if (IsGameTheme) {
                    Play( GameThemes.First() );
                } else {
                    Stop();
                }
            }
            if (IsMainTheme) {
                if (!AudioSource.isPlaying && IsPlaying && IsUnPaused) {
                    PlayNext( MainThemes );
                }
                if (Router.IsGameSceneLoading) {
                    Volume = Mathf.MoveTowards( Volume, 0, Volume * UnityEngine.Time.deltaTime * 0.5f );
                }
            } else if (IsGameTheme) {
                if (!AudioSource.isPlaying && IsPlaying && IsUnPaused) {
                    PlayNext( GameThemes );
                }
                SetPaused( Application.IsGamePaused );
            }
        }

        // Play
        private async void Play(string key) {
            Stop();
            var clip = await Addressables2.LoadAssetAsync<AudioClip>( key ).GetResultAsync( default, i => i.Result.name = key, (i, ex) => Addressables2.Release( i ) );
            Play( clip );
            Volume = 1;
        }
        private void PlayNext(string[] keys) {
            Play( GetNextValue( keys, Clip?.name ) );
        }
        private new void Stop() {
            if (AudioSource.clip != null) {
                var clip = AudioSource.clip;
                base.Stop();
                Addressables2.Release( clip );
            }
        }

        // Helpers
        private static UIThemeState GetState(UIRouterState state) {
            if (state is UIRouterState.MainSceneLoading or UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoading) {
                return UIThemeState.MainTheme;
            }
            if (state is UIRouterState.GameSceneLoaded) {
                return UIThemeState.GameTheme;
            }
            return UIThemeState.None;
        }

    }
    // UIThemeState
    public enum UIThemeState {
        None,
        MainTheme,
        GameTheme,
    }
}
