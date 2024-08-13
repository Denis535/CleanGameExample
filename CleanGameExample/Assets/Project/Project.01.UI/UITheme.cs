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

        private new ThemeState? State {
            get => (ThemeState?) base.State;
            set => base.State = value;
        }
        public bool IsPaused {
            set {
                if (value) {
                    AudioSource.Pause();
                } else {
                    AudioSource.UnPause();
                }
            }
        }

        public UITheme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            State?.Dispose();
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
        }

        public void PlayMainTheme() {
            State?.Dispose();
            State = new MainThemeState( this );
            State.Play();
        }
        public void PlayGameTheme() {
            State?.Dispose();
            State = new GameThemeState( this );
            State.Play();
        }
        public void PlayLoadingTheme() {
            if (State is MainThemeState mainStrategy) {
                State.IsFading = true;
            } else {
                State?.Dispose();
                State = null;
            }
        }
        public void PlayUnloadingTheme() {
            State?.Dispose();
            State = null;
        }
        public void StopTheme() {
            State?.Dispose();
            State = null;
        }

    }
    internal abstract class ThemeState : UIThemeStateBase {

        public AssetHandle<AudioClip>[] Clips { get; }
        public bool IsFading { get; internal set; }

        public ThemeState(UITheme context, AssetHandle<AudioClip>[] clips) : base( context ) {
            Clips = clips;
        }
        public override void Dispose() {
            base.Dispose();
        }

        public async void Play() {
            try {
                for (var i = 0; true; i++) {
                    await PlayClipAsync( Clips[ i % Clips.Length ] );
                }
            } catch (OperationCanceledException) {
            }
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
    internal class MainThemeState : ThemeState {

        private static readonly new AssetHandle<AudioClip>[] Clips = Shuffle( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Value_Theme )
        } );

        public MainThemeState(UITheme context) : base( context, Clips ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    internal class GameThemeState : ThemeState {

        private static readonly new AssetHandle<AudioClip>[] Clips = Shuffle( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_1 ),
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_2 ),
        } );

        public GameThemeState(UITheme context) : base( context, Clips ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
