#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Project.App;
    using Project.Entities;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.UI;

    public class UITheme : UIThemeBase2 {

        private static readonly AssetHandle<AudioClip>[] MainThemes = GetShuffled( new[] {
             new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Theme_Value )
        } );
        private static readonly AssetHandle<AudioClip>[] GameThemes = GetShuffled( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Theme_1_Value ),
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Theme_2_Value ),
        } );

        private readonly Lock @lock = new Lock();

        // Container
        private IDependencyContainer Container { get; }
        // UI
        private UIRouter Router { get; }
        // App
        private Application2 Application { get; }
        // Entities
        private Game? Game => Application.Game;
        // Theme
        private AssetHandle<AudioClip>? Theme { get; set; }

        // Constructor
        public UITheme(IDependencyContainer container) : base( container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
            Container = container;
            Router = container.RequireDependency<UIRouter>();
            Application = container.RequireDependency<Application2>();
        }
        public override void Dispose() {
            if (Theme != null) {
                Stop( AudioSource );
                Theme.Release();
                Theme = null;
            }
        }

        // Update
        public override async void Update() {
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
            if (Theme == null || !MainThemes.Contains( Theme )) {
                if (Theme != null) {
                    Stop( AudioSource );
                    Theme.Release();
                    Theme = null;
                }
                Theme = MainThemes.First();
                await Theme.Load().WaitAsync( DisposeCancellationToken );
                Play( AudioSource, Theme.GetValue() );
            }
            if (!IsPlaying( AudioSource )) {
                var next = GetNextValue( MainThemes, Theme );
                if (Theme != null) {
                    Stop( AudioSource );
                    Theme.Release();
                    Theme = null;
                }
                Theme = next;
                await Theme.Load().WaitAsync( DisposeCancellationToken );
                Play( AudioSource, Theme.GetValue() );
            }
            if (Router.IsGameSceneLoading) {
                AudioSource.volume = Mathf.MoveTowards( AudioSource.volume, 0, AudioSource.volume * Time.deltaTime * 1.0f );
                AudioSource.pitch = Mathf.MoveTowards( AudioSource.pitch, 0, AudioSource.pitch * Time.deltaTime * 0.5f );
            }
        }
        private async Task Update_GameTheme() {
            if (Theme == null || !GameThemes.Contains( Theme )) {
                if (Theme != null) {
                    Stop( AudioSource );
                    Theme.Release();
                    Theme = null;
                }
                Theme = GameThemes.First();
                await Theme.Load().WaitAsync( DisposeCancellationToken );
                Play( AudioSource, Theme.GetValue() );
            }
            if (!IsPlaying( AudioSource )) {
                var next = GetNextValue( GameThemes, Theme );
                if (Theme != null) {
                    Stop( AudioSource );
                    Theme.Release();
                    Theme = null;
                }
                Theme = next;
                await Theme.Load().WaitAsync( DisposeCancellationToken );
                Play( AudioSource, Theme.GetValue() );
            }
            Pause( AudioSource, Game!.IsPaused );
        }
        public override void LateUpdate() {
        }

    }
}
