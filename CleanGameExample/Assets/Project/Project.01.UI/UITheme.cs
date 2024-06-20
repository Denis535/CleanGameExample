#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.UI;

    public class UITheme : UIThemeBase2 {

        private static readonly AssetHandle<AudioClip>[] MainPlayList = GetShuffled( new[] {
             new AssetHandle<AudioClip>( R.Project.UI.MainScreen.Music.Value_Theme )
        } );
        private static readonly AssetHandle<AudioClip>[] GamePlayList = GetShuffled( new[] {
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_1 ),
            new AssetHandle<AudioClip>( R.Project.UI.GameScreen.Music.Value_Theme_2 ),
        } );

        // Constructor
        public UITheme(IDependencyContainer container) : base( container, container.RequireDependency<AudioSource>( "MusicAudioSource" ) ) {
        }
        public override void Dispose() {
            //PlayList( null );
            base.Dispose();
        }

        // PlayTheme
        public void PlayMainTheme() {
            PlayList( AudioSource, MainPlayList, DisposeCancellationToken );
        }
        public void PlayGameTheme() {
            PlayList( AudioSource, GamePlayList, DisposeCancellationToken );
        }
        public void PlayLoadingTheme() {
            //PlayList( null );
        }
        public void Stop() {
            //PlayList( null );
        }
        public void SetPaused(bool isPaused) {
            SetPaused( AudioSource, isPaused );
        }
        //public void Fade() {
        //    AudioSource.volume = Mathf.MoveTowards( AudioSource.volume, 0, AudioSource.volume * 1.0f * Time.deltaTime );
        //    AudioSource.pitch = Mathf.MoveTowards( AudioSource.pitch, 0, AudioSource.pitch * 0.5f * Time.deltaTime );
        //}

        // Update
        public void Update() {
            //if (Themes != null && Theme != null && Theme.IsSucceeded) {
            //    if (IsCompleted( AudioSource )) {
            //        Play( GetNextValue( Themes, Theme ) );
            //    }
            //}
        }
        public void LateUpdate() {
        }

        // Helpers
        private static async void PlayList(AudioSource audioSource, AssetHandle<AudioClip>[] clips, CancellationToken cancellationToken) {
            for (var i = 0; true; i++) {
                var clip = clips[ i % clips.Length ];
                await Play( audioSource, clip, cancellationToken );
            }
        }
        private static async Task Play(AudioSource audioSource, AssetHandle<AudioClip> clip, CancellationToken cancellationToken) {
            try {
                Play( audioSource, await clip.Load().GetValueAsync( cancellationToken ) );
                audioSource.volume = 1;
                while (audioSource.time < audioSource.clip.length) {
                    await Awaitable.NextFrameAsync( cancellationToken );
                }
            } finally {
                clip.ReleaseSafe();
            }
        }

    }
}
