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

    public class UITheme : UIThemeBase2 {

        private static readonly AssetHandle<AudioClip>[] MainThemes = GetShuffled( new[] {
             new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Value_Theme )
        } );
        private static readonly AssetHandle<AudioClip>[] GameThemes = GetShuffled( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_1 ),
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_2 ),
        } );

        private readonly Lock @lock = new Lock();

        // UI
        private UIRouter Router { get; }
        // App
        private Application2 Application { get; }
        // Entities
        private Game? Game => Application.Game;
        // Themes
        private AssetHandle<AudioClip>[]? Themes { get; set; }
        // Theme
        private AssetHandle<AudioClip>? Theme { get; set; }

        // Constructor
        public UITheme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
            Router.OnStateChangeEvent += state => {
                if (IsMainTheme( state )) {
                    if (Themes != MainThemes) PlayThemes( MainThemes );
                } else if (IsGameTheme( state )) {
                    if (Themes != GameThemes) PlayThemes( GameThemes );
                } else {
                    PlayThemes( null );
                }
            };
        }
        public override void Dispose() {
            PlayThemes( null );
            base.Dispose();
        }

        // Update
        public override void Update() {
            if (Theme != null && !Theme.IsDone) {
                return;
            }
            if (Themes != null && IsCompleted( AudioSource )) {
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
        public override void LateUpdate() {
        }

        // Play
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
