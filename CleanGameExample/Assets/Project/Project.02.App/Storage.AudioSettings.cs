#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Audio;
    using UnityEngine.Framework.App;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public partial class Storage {
        public class AudioSettings : StorageBase {

            private float masterVolume;
            private float musicVolume;
            private float sfxVolume;
            private float gameVolume;

            // AudioMixer
            private AudioMixer AudioMixer { get; }
            // Volume
            public float MasterVolume {
                get => masterVolume;
                set {
                    masterVolume = value;
                    SetVolume( AudioMixer, "MasterVolume", value );
                }
            }
            public float MusicVolume {
                get => musicVolume;
                set {
                    musicVolume = value;
                    SetVolume( AudioMixer, "MusicVolume", value );
                }
            }
            public float SfxVolume {
                get => sfxVolume;
                set {
                    sfxVolume = value;
                    SetVolume( AudioMixer, "SfxVolume", value );
                }
            }
            public float GameVolume {
                get => gameVolume;
                set {
                    gameVolume = value;
                    SetVolume( AudioMixer, "GameVolume", value );
                }
            }

            // Constructor
            internal AudioSettings() {
                AudioMixer = Addressables.LoadAssetAsync<AudioMixer>( R.UnityEngine.Audio.Value_AudioMixer ).GetResult();
                Load();
            }
            public override void Dispose() {
                Addressables.Release( AudioMixer );
                base.Dispose();
            }

            // Load
            public void Load() {
                MasterVolume = PlayerPrefs.GetFloat( "AudioSettings.MasterVolume", 0.5f );
                MusicVolume = PlayerPrefs.GetFloat( "AudioSettings.MusicVolume", 0.5f );
                SfxVolume = PlayerPrefs.GetFloat( "AudioSettings.SfxVolume", 0.5f );
                GameVolume = PlayerPrefs.GetFloat( "AudioSettings.GameVolume", 0.5f );
            }
            public void Save() {
                PlayerPrefs.SetFloat( "AudioSettings.MasterVolume", MasterVolume );
                PlayerPrefs.SetFloat( "AudioSettings.MusicVolume", MusicVolume );
                PlayerPrefs.SetFloat( "AudioSettings.SfxVolume", SfxVolume );
                PlayerPrefs.SetFloat( "AudioSettings.GameVolume", GameVolume );
            }

            // Helpers
            private static void SetVolume(AudioMixer mixer, string name, float value) {
                Assert.Argument.Message( $"Volume {value} must be in [0-1] range" ).InRange( value >= 0 && value <= 1 );
                var isSucceeded = mixer.SetFloat( name, ToDecibels( value ) );
                if (!isSucceeded) throw Exceptions.Internal.Exception( $"Volume {name} was not set" );
            }
            private static float ToDecibels(float value) {
                value = Mathf.Clamp( value, 0.0001f, 1 );
                return Mathf.Log10( value ) * 20f;
            }

        }
    }
}
