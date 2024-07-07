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

        private ThemeStateBase? State { get; set; }
        public new bool IsPaused {
            set => base.IsPaused = value;
        }
        public bool IsFading { get; set; }

        public UITheme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            State?.Dispose();
            State = null;
            base.Dispose();
        }

        public void PlayMainTheme() {
            StopTheme();
            State = new MainThemeState( this );
            State.Play();
        }
        public void PlayGameTheme() {
            StopTheme();
            State = new GameThemeState( this );
            State.Play();
        }
        public void PlayLoadingTheme() {
            if (State is MainThemeState mainStrategy) {
                IsFading = true;
            } else {
                StopTheme();
            }
        }
        public void PlayUnloadingTheme() {
            StopTheme();
        }
        public void StopTheme() {
            State?.Dispose();
            State = null;
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
        }

        private abstract class ThemeStateBase : Disposable {

            protected UITheme Context { get; }
            protected AssetHandle<AudioClip>[] Clips { get; }

            public ThemeStateBase(UITheme context, AssetHandle<AudioClip>[] clips) {
                Context = context;
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
                    Context.Play( clip );
                    Context.Volume = 1;
                    Context.Pitch = 1;
                    Context.IsFading = false;
                    while (!Context.IsCompleted) {
                        if (Context.IsFading) {
                            Context.Volume = Mathf.MoveTowards( Context.Volume, 0, Context.Volume * 1.0f * Time.deltaTime );
                            Context.Pitch = Mathf.MoveTowards( Context.Pitch, 0, Context.Pitch * 0.5f * Time.deltaTime );
                        }
                        await Awaitable.NextFrameAsync( DisposeCancellationToken );
                    }
                } finally {
                    Context.Stop();
                }
            }

        }
        private class MainThemeState : ThemeStateBase {

            private static readonly new AssetHandle<AudioClip>[] Clips = Shuffle( new[] {
                new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Value_Theme )
            } );

            public MainThemeState(UITheme context) : base( context, Clips ) {
            }
            public override void Dispose() {
                base.Dispose();
            }

        }
        private class GameThemeState : ThemeStateBase {

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
}
