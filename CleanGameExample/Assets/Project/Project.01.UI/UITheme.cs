#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.App;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.UI;

    public class UITheme : UIThemeBase2<UIThemeState> {

        private static readonly AssetHandle<AudioClip>[] MainThemes = GetShuffled( new[] {
             new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Value_Theme )
        } );
        private static readonly AssetHandle<AudioClip>[] GameThemes = GetShuffled( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_1 ),
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_2 ),
        } );

        private readonly Lock @lock = new Lock();

        // Container
        private UIRouter Router { get; }
        private Application2 Application { get; }
        private Game? Game => Application.Game;
        // Themes
        private AssetHandle<AudioClip>[]? Themes { get; set; }
        // Theme
        private AssetHandle<AudioClip>? Theme { get; set; }

        // Constructor
        public UITheme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
            Router.OnStateChangeEvent += i => {
                State = GetState( i ) ?? State;
            };
        }
        public override void Dispose() {
            PlayThemes( null );
            base.Dispose();
        }

        // Update
        public void Update() {
            if (State is UIThemeState.MainTheme) {
                if (Themes != MainThemes) PlayThemes( MainThemes );
            } else if (State is UIThemeState.GameTheme) {
                if (Themes != GameThemes) PlayThemes( GameThemes );
            } else {
                PlayThemes( null );
            }
            if (Themes != null && Theme != null && Theme.IsDone) {
                if (IsCompleted( AudioSource )) {
                    PlayTheme( GetNextValue( Themes, Theme ) );
                }
                if (Router.State is UIRouterState.GameSceneLoading) {
                    AudioSource.volume = Mathf.MoveTowards( AudioSource.volume, 0, AudioSource.volume * Time.deltaTime * 1.0f );
                    AudioSource.pitch = Mathf.MoveTowards( AudioSource.pitch, 0, AudioSource.pitch * Time.deltaTime * 0.5f );
                }
                if (Router.State is UIRouterState.GameSceneLoaded) {
                    Pause( AudioSource, Game!.IsPaused );
                }
            }
        }
        public void LateUpdate() {
        }

        // OnStateChange
        protected override void OnStateChange(UIThemeState state) {
        }

        // Helpers
        private void PlayThemes(AssetHandle<AudioClip>[]? themes) {
            Themes = themes;
            PlayTheme( Themes?.First() );
        }
        private async void PlayTheme(AssetHandle<AudioClip>? theme) {
            if (Theme != null) {
                if (AudioSource.clip != null) Stop( AudioSource );
                Theme.Release();
            }
            Theme = theme;
            if (Theme != null) {
                Theme.Load();
                Play( AudioSource, await Theme.GetValueAsync( DisposeCancellationToken ) );
            }
        }
        // Helpers
        private static UIThemeState? GetState(UIRouterState state) {
            if (state is UIRouterState.MainSceneLoading or UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoading) {
                return UIThemeState.MainTheme;
            }
            if (state is UIRouterState.GameSceneLoaded) {
                return UIThemeState.GameTheme;
            }
            return null;
        }

    }
    public enum UIThemeState {
        None,
        MainTheme,
        GameTheme
    }
}
