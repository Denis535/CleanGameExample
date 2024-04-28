#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.UI;

    public class UITheme : UIAudioThemeBase {

        private static readonly string[] MainThemes = GetShuffled( new[] {
             R.Project.UI.MainScreen.Music.Theme_Value,
        } );
        private static readonly string[] GameThemes = GetShuffled( new[] {
            R.Project.UI.GameScreen.Music.Theme_1_Value,
            R.Project.UI.GameScreen.Music.Theme_2_Value,
        } );

        private readonly Lock @lock = new Lock();

        // UI
        private UIRouter Router { get; set; } = default!;
        // Application
        private Application2 Application { get; set; } = default!;
        // Entities
        private Game? Game => Application.Game;
        // AudioSource
        private AudioSource AudioSource { get; set; } = default!;
        // Theme
        private AssetHandleDynamic<AudioClip> Theme { get; } = new AssetHandleDynamic<AudioClip>();

        // Awake
        public new void Awake() {
            base.Awake();
            Router = Utils.Container.RequireDependency<UIRouter>( null );
            Application = Utils.Container.RequireDependency<Application2>( null );
            AudioSource = gameObject.RequireComponentInChildren<AudioSource>();
        }
        public new void OnDestroy() {
            Stop( AudioSource, Theme );
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
                    await Update_MainTheme();
                } else
                if (IsGameTheme( Router.State )) {
                    await Update_GameTheme();
                }
            }
        }
        private async Task Update_MainTheme() {
            if (!Theme.IsValid) {
                await Play( AudioSource, Theme, MainThemes.First(), destroyCancellationToken );
            } else
            if (!MainThemes.Contains( Theme.Handle.Key )) {
                Stop( AudioSource, Theme );
                await Play( AudioSource, Theme, MainThemes.First(), destroyCancellationToken );
            } else
            if (!IsPlaying( AudioSource )) {
                var next = GetNextValue( MainThemes, Theme.Handle.Key );
                Stop( AudioSource, Theme );
                await Play( AudioSource, Theme, next, destroyCancellationToken );
            }
            if (Router.IsGameSceneLoading) {
                AudioSource.volume = Mathf.MoveTowards( AudioSource.volume, 0, AudioSource.volume * Time.deltaTime * 1.0f );
                AudioSource.pitch = Mathf.MoveTowards( AudioSource.pitch, 0, AudioSource.pitch * Time.deltaTime * 0.5f );
            }
        }
        private async Task Update_GameTheme() {
            if (!Theme.IsValid) {
                await Play( AudioSource, Theme, GameThemes.First(), destroyCancellationToken );
            } else
            if (!GameThemes.Contains( Theme.Handle.Key )) {
                Stop( AudioSource, Theme );
                await Play( AudioSource, Theme, GameThemes.First(), destroyCancellationToken );
            } else
            if (!IsPlaying( AudioSource )) {
                var next = GetNextValue( GameThemes, Theme.Handle.Key );
                Stop( AudioSource, Theme );
                await Play( AudioSource, Theme, next, destroyCancellationToken );
            }
            Pause( AudioSource, Game!.IsPaused );
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
        // Helpers
        private static async Task Play(AudioSource source, AssetHandleDynamic<AudioClip> clip, string key, CancellationToken cancellationToken) {
            var value = await clip.SetHandle( key ).Load().GetValueAsync( cancellationToken );
            Play( source, value );
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
        private static void Stop(AudioSource source, AssetHandleDynamic<AudioClip> clip) {
            Stop( source );
            clip.Handle.Release();
        }
        private static void Stop(AudioSource source) {
            if (source.clip != null) {
                source.Stop();
                source.clip = null;
            }
        }

    }
}
