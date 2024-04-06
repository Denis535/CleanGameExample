#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
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
            base.OnDestroy();
        }

        // Start
        public void Start() {
            using (@lock.Enter()) {
            }
        }
        public async void Update() {
            if (@lock.IsLocked) return;
            using (@lock.Enter()) {
                if (IsMainTheme) {
                    await PlayMainTheme();
                } else
                if (IsGameTheme) {
                    await PlayGameTheme();
                }
            }
        }

        // PlayMainTheme
        private async Task PlayMainTheme() {
            var clip = (AssetHandle<AudioClip>?) null;
            while (IsMainTheme) {
                clip = GetNextValue( MainThemes, clip );
                try {
                    Play( AudioSource, await clip.LoadAsync( destroyCancellationToken ) );
                    while (IsMainTheme && IsPlaying( AudioSource )) {
                        if (Router.IsGameSceneLoading) {
                            AudioSource.volume = Mathf.MoveTowards( AudioSource.volume, 0, AudioSource.volume * Time.deltaTime * 1.0f );
                            AudioSource.pitch = Mathf.MoveTowards( AudioSource.pitch, 0, AudioSource.pitch * Time.deltaTime * 0.5f );
                        }
                        await Task.Yield();
                    }
                } finally {
                    Stop( AudioSource );
                    clip.Release();
                }
            }
        }
        // PlayGameTheme
        private async Task PlayGameTheme() {
            var clip = (AssetHandle<AudioClip>?) null;
            while (IsGameTheme) {
                clip = GetNextValue( GameThemes, clip );
                try {
                    Play( AudioSource, await clip.LoadAsync( destroyCancellationToken ) );
                    while (IsGameTheme && IsPlaying( AudioSource )) {
                        Pause( AudioSource, !Application.Game!.IsPlaying );
                        await Task.Yield();
                    }
                } finally {
                    Stop( AudioSource );
                    clip.Release();
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
        private static void Play(AudioSource source, AudioClip clip) {
            Assert.Operation.Message( $"You are trying to play {clip.name} clip but first you must stop old clip" ).Valid( source.clip == null );
            source.clip = clip;
            source.volume = 1;
            source.pitch = 1;
            source.Play();
        }
        private static void Pause(AudioSource source, bool value) {
            if (value) {
                source.Pause();
            } else {
                source.UnPause();
            }
        }
        private static void Stop(AudioSource source) {
            if (source.clip != null) {
                source.Stop();
                source.clip = null;
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
