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

    public class UITheme : UIAudioThemeBase {

        private static readonly AssetHandle<AudioClip>[] MainThemes = GetShuffled( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Theme_Value ),
        } );
        private static readonly AssetHandle<AudioClip>[] GameThemes = GetShuffled( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Theme_1_Value ),
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Theme_2_Value ),
        } );

        private readonly Lock @lock = new Lock();

        // Globals
        private UIRouter Router { get; set; } = default!;
        private Application2 Application { get; set; } = default!;
        private AudioSource AudioSource { get; set; } = default!;
        // State
        public UIThemeState State => GetState( Router.State );
        private ValueTracker2<UIThemeState, UITheme> StateTracker { get; } = new ValueTracker2<UIThemeState, UITheme>( i => i.State );
        public bool IsMainTheme => State == UIThemeState.MainTheme;
        public bool IsGameTheme => State == UIThemeState.GameTheme;

        // Awake
        public new void Awake() {
            base.Awake();
            Router = this.GetDependencyContainer().RequireDependency<UIRouter>( null );
            Application = this.GetDependencyContainer().RequireDependency<Application2>( null );
            AudioSource = gameObject.RequireComponentInChildren<AudioSource>();
        }
        public new void OnDestroy() {
            Stop( AudioSource );
            base.OnDestroy();
        }

        // Start
        public void Start() {
            using (@lock.Enter()) {
            }
        }
        public void Update() {
            if (@lock.IsLocked) return;
            using (@lock.Enter()) {
                if (StateTracker.IsChanged( this )) {
                    Stop( AudioSource );
                }
                if (!IsPlaying( AudioSource )) {
                    if (IsMainTheme) {
                        Stop( AudioSource );
                        PlayNext( AudioSource, MainThemes );
                    } else if (IsGameTheme) {
                        Stop( AudioSource );
                        PlayNext( AudioSource, GameThemes );
                    }
                }
                if (IsMainTheme) {
                    if (Router.IsGameSceneLoading) {
                        AudioSource.volume = Mathf.MoveTowards( AudioSource.volume, 0, AudioSource.volume * Time.deltaTime * 0.5f );
                    }
                } else if (IsGameTheme) {
                    if (Application.Game!.IsPlaying) {
                        AudioSource.UnPause();
                    } else {
                        AudioSource.Pause();
                    }
                }
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
        // Helpers
        private static bool IsPlaying(AudioSource source) {
            return source.clip is not null && !Mathf.Approximately( source.time, source.clip.length );
        }
        private static void Play(AudioSource source, AssetHandle<AudioClip> clip) {
            Assert.Operation.Message( $"You are trying to play {clip} clip but first you must unload old clip" ).Valid( source.clip == null );
            source.clip = clip.Load();
            source.volume = 1;
            source.Play();
        }
        private static void PlayNext(AudioSource source, AssetHandle<AudioClip>[] clips) {
            var clip = MainThemes.Concat( GameThemes ).FirstOrDefault( i => i.IsActive && i.IsSucceeded && i.Asset == source.clip );
            Play( source, GetNextValue( clips, clip ) );
        }
        private static void Stop(AudioSource source) {
            if (source.clip != null) {
                source.Stop();
                source.clip = null;
                MainThemes.Concat( GameThemes ).Release();
            }
        }

    }
    // UIThemeState
    public enum UIThemeState {
        None,
        MainTheme,
        GameTheme,
    }
}
