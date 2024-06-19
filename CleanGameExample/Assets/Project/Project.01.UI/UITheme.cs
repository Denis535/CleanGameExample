#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.UI;

    public class UITheme : UIThemeBase2 {

        private static readonly AssetHandle<AudioClip>[] MainThemes = GetShuffled( new[] {
             new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Value_Theme )
        } );
        private static readonly AssetHandle<AudioClip>[] GameThemes = GetShuffled( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_1 ),
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_2 ),
        } );

        // Themes
        private AssetHandle<AudioClip>[]? Themes { get; set; }
        // Theme
        private AssetHandle<AudioClip>? Theme { get; set; }

        // Constructor
        public UITheme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            PlayThemes( null );
            base.Dispose();
        }

        // PlayThemes
        public void PlayMainThemes() {
            PlayThemes( MainThemes );
        }
        public void PlayGameThemes() {
            PlayThemes( GameThemes );
        }
        public void Stop() {
            PlayThemes( null );
        }

        // SetPaused
        public void SetPaused(bool isPaused) {
            SetPaused( AudioSource, isPaused );
        }

        //// Fade
        //public void Fade() {
        //    AudioSource.volume = Mathf.MoveTowards( AudioSource.volume, 0, AudioSource.volume * 1.0f * Time.deltaTime );
        //    AudioSource.pitch = Mathf.MoveTowards( AudioSource.pitch, 0, AudioSource.pitch * 0.5f * Time.deltaTime );
        //}

        // Update
        public void Update() {
            if (Themes != null && Theme != null && Theme.IsSucceeded) {
                if (IsCompleted( AudioSource )) {
                    PlayTheme( GetNextValue( Themes, Theme ) );
                }
            }
        }
        public void LateUpdate() {
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

    }
}
