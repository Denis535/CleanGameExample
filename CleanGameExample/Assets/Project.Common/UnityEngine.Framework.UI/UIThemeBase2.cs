#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class UIThemeBase2 : UIThemeBase {

        // AudioSource
        protected AudioSource AudioSource { get; }

        // Constructor
        public UIThemeBase2(AudioSource audioSource) {
            AudioSource = audioSource;
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Update
        public abstract void Update();
        public abstract void LateUpdate();

        // Helpers
        protected static bool IsMainTheme(UIRouterState state) {
            if (state is UIRouterState.MainSceneLoading or UIRouterState.MainSceneLoaded or UIRouterState.GameSceneLoading) {
                return true;
            }
            return false;
        }
        protected static bool IsGameTheme(UIRouterState state) {
            if (state is UIRouterState.GameSceneLoaded) {
                return true;
            }
            return false;
        }
        // Helpers
        protected static bool IsPlaying(AudioSource source) {
            return source.clip is not null && !Mathf.Approximately( source.time, source.clip.length );
        }
        protected static bool IsPaused(AudioSource source) {
            return source.clip is not null && !source.isPlaying && !Mathf.Approximately( source.time, source.clip.length );
        }
        protected static void Play(AudioSource source, AudioClip clip) {
            Assert.Operation.Message( $"AudioClip {source.clip} must be null" ).Valid( source.clip == null );
            source.clip = clip;
            source.volume = 1;
            source.pitch = 1;
            source.Play();
        }
        protected static void Pause(AudioSource source, bool value) {
            if (value) {
                source.Pause();
            } else {
                source.UnPause();
            }
        }
        protected static void Stop(AudioSource source) {
            Assert.Operation.Message( $"AudioClip must be non-null" ).Valid( source.clip != null );
            source.Stop();
            source.clip = null;
        }

    }
}
