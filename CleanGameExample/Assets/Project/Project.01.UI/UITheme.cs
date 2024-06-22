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

        // Constructor
        public UITheme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            StopTheme();
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

            protected abstract UITheme Self { get; }
            protected abstract AssetHandle<AudioClip>[] Clips { get; }

            public Strategy() {
            }
            public override void Dispose() {
                base.Dispose();
            }

            public abstract void Play();

        }
        private class MainStrategy : Strategy {

            protected override UITheme Self { get; }
            protected override AssetHandle<AudioClip>[] Clips { get; } = Shuffle( new[] {
                new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Value_Theme )
            } );

            public bool IsFading { get; set; }

            public MainStrategy(UITheme self) {
                Self = self;
            }
            public override void Dispose() {
                base.Dispose();
            }

            public override async void Play() {
                try {
                    for (var i = 0; true; i++) {
                        var clip = Clips[ i % Clips.Length ];
                        await PlayClipAsync( clip );
                    }
                } catch (OperationCanceledException) {
                }
            }
            private async Task PlayClipAsync(AssetHandle<AudioClip> clip) {
                try {
                    await PlayClipAsync_( clip );
                    while (!Self.IsCompleted) {
                        await Awaitable.NextFrameAsync( DisposeCancellationToken );
                        if (IsFading) {
                            Self.AudioSource.volume = Mathf.MoveTowards( Self.AudioSource.volume, 0, Self.AudioSource.volume * 1.0f * Time.deltaTime );
                            Self.AudioSource.pitch = Mathf.MoveTowards( Self.AudioSource.pitch, 0, Self.AudioSource.pitch * 0.5f * Time.deltaTime );
                        }
                    }
                } finally {
                    Self.Stop();
                    clip.ReleaseSafe();
                }
            }
            private async Task PlayClipAsync_(AssetHandle<AudioClip> clip) {
                Self.Play( await clip.Load().GetValueAsync( DisposeCancellationToken ) );
                Self.AudioSource.volume = 1;
                Self.AudioSource.pitch = 1;
            }

        }
        private class GameStrategy : Strategy {

            protected override UITheme Self { get; }
            protected override AssetHandle<AudioClip>[] Clips { get; } = Shuffle( new[] {
                new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_1 ),
                new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_2 ),
            } );

            public GameStrategy(UITheme self) {
                Self = self;
            }
            public override void Dispose() {
                base.Dispose();
            }

            public override async void Play() {
                try {
                    for (var i = 0; true; i++) {
                        var clip = Clips[ i % Clips.Length ];
                        await PlayClipAsync( clip );
                    }
                } catch (OperationCanceledException) {
                }
            }
            private async Task PlayClipAsync(AssetHandle<AudioClip> clip) {
                try {
                    await PlayClipAsync_( clip );
                    while (!Self.IsCompleted) {
                        await Awaitable.NextFrameAsync( DisposeCancellationToken );
                    }
                } finally {
                    Self.Stop();
                    clip.ReleaseSafe();
                }
            }
            private async Task PlayClipAsync_(AssetHandle<AudioClip> clip) {
                Self.Play( await clip.Load().GetValueAsync( DisposeCancellationToken ) );
                Self.AudioSource.volume = 1;
                Self.AudioSource.pitch = 1;
            }

        }
    }
}
