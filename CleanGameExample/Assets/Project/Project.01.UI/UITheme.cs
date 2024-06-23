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

        // Strategy
        private Strategy? Strategy_ { get; set; }
        // IsPaused
        public new bool IsPaused {
            set => base.IsPaused = value;
        }

        // Constructor
        public UITheme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            Strategy_?.Dispose();
            Strategy_ = null;
            base.Dispose();
        }

        // PlayTheme
        public void PlayMainTheme() {
            StopTheme();
            Strategy_ = new MainStrategy( this );
            Strategy_.Play();
        }
        public void PlayGameTheme() {
            StopTheme();
            Strategy_ = new GameStrategy( this );
            Strategy_.Play();
        }
        public void PlayLoadingTheme() {
            if (Strategy_ is MainStrategy mainStrategy) {
                mainStrategy.IsFading = true;
            } else {
                StopTheme();
            }
        }
        public void PlayUnloadingTheme() {
            StopTheme();
        }
        public void StopTheme() {
            Strategy_?.Dispose();
            Strategy_ = null;
        }

        // Update
        public void Update() {
        }
        public void LateUpdate() {
        }

        // Strategy
        private abstract class Strategy : Disposable {

            protected abstract UITheme Context { get; }
            protected abstract AssetHandle<AudioClip>[] Clips { get; }
            public bool IsFading { get; set; }

            public Strategy() {
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
                    while (!Context.IsCompleted) {
                        if (IsFading) {
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
        private class MainStrategy : Strategy {

            protected override UITheme Context { get; }
            protected override AssetHandle<AudioClip>[] Clips { get; } = Shuffle( new[] {
                new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Value_Theme )
            } );

            public MainStrategy(UITheme ñontext) {
                Context = ñontext;
            }
            public override void Dispose() {
                base.Dispose();
            }

        }
        private class GameStrategy : Strategy {

            protected override UITheme Context { get; }
            protected override AssetHandle<AudioClip>[] Clips { get; } = Shuffle( new[] {
                new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_1 ),
                new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_2 ),
            } );

            public GameStrategy(UITheme context) {
                Context = context;
            }
            public override void Dispose() {
                base.Dispose();
            }

        }
    }
}
