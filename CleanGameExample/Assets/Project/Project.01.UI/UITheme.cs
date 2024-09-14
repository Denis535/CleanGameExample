#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.UI;

    public class UITheme : UIThemeBase2 {

        private new UIPlayListBase2? PlayList => (UIPlayListBase2?) base.PlayList;
        public new bool IsPaused {
            set => base.IsPaused = value;
        }

        public UITheme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            SetPlayList( null );
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
        }

        public void PlayMainTheme() {
            SetPlayList( new MainThemePlayList( this ) );
        }
        public void PlayGameTheme() {
            SetPlayList( new GameThemePlayList( this ) );
        }
        public void PlayLoadingTheme() {
            if (PlayList is MainThemePlayList mainStrategy) {
                mainStrategy.IsFading = true;
            } else {
                SetPlayList( null );
            }
        }
        public void PlayUnloadingTheme() {
            SetPlayList( null );
        }
        public void StopTheme() {
            SetPlayList( null );
        }

    }
    internal abstract class UIPlayListBase2 : UIPlayListBase {

        protected new UIThemeBase2 Context => (UIThemeBase2) base.Context;
        protected AssetHandle<AudioClip>[] Clips { get; }
        public bool IsFading { get; internal set; }

        public UIPlayListBase2(UITheme context, AssetHandle<AudioClip>[] clips) : base( context ) {
            Clips = clips;
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override async void OnActivate(object? argument) {
            try {
                for (var i = 0; true; i++) {
                    await PlayClipAsync( Clips[ i % Clips.Length ] );
                }
            } catch (OperationCanceledException) {
            }
        }
        protected override void OnDeactivate(object? argument) {
            Dispose();
        }

        private async Task PlayClipAsync(AssetHandle<AudioClip> clip) {
            try {
                await PlayClipAsync( await clip.Load().GetValueAsync( DisposeCancellationToken ) );
            } finally {
                clip.Release();
            }
        }
        private async Task PlayClipAsync(AudioClip clip) {
            try {
                Play( clip );
                IsFading = false;
                Volume = 1;
                Pitch = 1;
                while (!IsCompleted) {
                    if (IsFading) {
                        Volume = Mathf.MoveTowards( Volume, 0, Volume * 1.0f * Time.deltaTime );
                        Pitch = Mathf.MoveTowards( Pitch, 0, Pitch * 0.5f * Time.deltaTime );
                    }
                    await Awaitable.NextFrameAsync( DisposeCancellationToken );
                }
            } finally {
                Stop();
            }
        }

    }
    internal class MainThemePlayList : UIPlayListBase2 {

        private static readonly new AssetHandle<AudioClip>[] Clips = Shuffle( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Value_Theme )
        } );

        public MainThemePlayList(UITheme context) : base( context, Clips ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    internal class GameThemePlayList : UIPlayListBase2 {

        private static readonly new AssetHandle<AudioClip>[] Clips = Shuffle( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_1 ),
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_2 ),
        } );

        public GameThemePlayList(UITheme context) : base( context, Clips ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
