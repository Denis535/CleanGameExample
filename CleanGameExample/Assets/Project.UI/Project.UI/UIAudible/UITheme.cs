#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Entities;
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

        // Deps
        private UIRouter Router { get; set; } = default!;
        private Application2 Application { get; set; } = default!;
        private AudioSource AudioSource { get; set; } = default!;
        private Game? Game => Application.Game;

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
                if (IsMainTheme( Router.State )) {
                    await PlayMainTheme();
                } else
                if (IsGameTheme( Router.State )) {
                    await PlayGameTheme();
                }
            }
        }

        // PlayMainTheme
        private async Task PlayMainTheme() {
            var clip = (AssetHandle<AudioClip>?) null;
            while (IsMainTheme( Router.State )) {
                clip = GetNextValue( MainThemes, clip );
                var clip_ = await clip.LoadAssetAsync( destroyCancellationToken );
                Play( AudioSource, clip_ );
                while (IsMainTheme( Router.State ) && IsPlaying( AudioSource )) {
                    if (Router.IsGameSceneLoading) {
                        AudioSource.volume = Mathf.MoveTowards( AudioSource.volume, 0, AudioSource.volume * Time.deltaTime * 1.0f );
                        AudioSource.pitch = Mathf.MoveTowards( AudioSource.pitch, 0, AudioSource.pitch * Time.deltaTime * 0.5f );
                    }
                    await Task.Yield();
                }
                Stop( AudioSource );
                clip.Release();
            }
        }
        // PlayGameTheme
        private async Task PlayGameTheme() {
            var clip = (AssetHandle<AudioClip>?) null;
            while (IsGameTheme( Router.State )) {
                clip = GetNextValue( GameThemes, clip );
                var clip_ = await clip.LoadAssetAsync( destroyCancellationToken );
                Play( AudioSource, clip_ );
                while (IsGameTheme( Router.State ) && IsPlaying( AudioSource )) {
                    Pause( AudioSource, !Game!.IsPlaying );
                    await Task.Yield();
                }
                Stop( AudioSource );
                clip.Release();
            }
        }

        // Helpers
        private static bool IsMainTheme(UIRouterState state) {
            if (state is UIRouterState.MainSceneLoading or UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoading) {
                return true;
            }
            return false;
        }
        private static bool IsGameTheme(UIRouterState state) {
            if (state is UIRouterState.GameSceneLoaded) {
                return true;
            }
            return false;
        }
        // Helpers
        private static bool IsPlaying(AudioSource source) {
            return source.clip is not null && !Mathf.Approximately( source.time, source.clip.length );
        }
        private static bool IsPaused(AudioSource source) {
            return source.clip is not null && !Mathf.Approximately( source.time, source.clip.length ) && !source.isPlaying;
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
}
