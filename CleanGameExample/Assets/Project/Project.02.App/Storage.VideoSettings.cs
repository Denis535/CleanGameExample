#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.App;

    public partial class Storage {
        public class VideoSettings : StorageBase {

            private bool isVSync;

            // IsFullScreen
            public bool IsFullScreen {
                get => Screen.fullScreen;
                set {
                    Screen.fullScreen = value;
                }
            }
            // ScreenResolution
            public Resolution ScreenResolution {
                get => Screen.currentResolution;
                set {
                    Screen.SetResolution( value.width, value.height, Screen.fullScreenMode, value.refreshRateRatio );
                }
            }
            public Resolution[] ScreenResolutions {
                //get => Screen.resolutions.SkipWhile( i => i.width < 1000 ).Reverse().ToArray();
                get => Screen.resolutions.Reverse().ToArray();
            }
            // IsVSync
            public bool IsVSync {
                get => isVSync;
                set {
                    isVSync = value;
                    QualitySettings.vSyncCount = value == true ? 1 : 0;
                }
            }

            // Constructor
            internal VideoSettings() {
                Load();
            }

            // Load
            public void Load() {
                IsVSync = PlayerPrefs2.GetBool( "VideoSettings.IsVSync", true );
            }
            public void Save() {
                PlayerPrefs2.SetBool( "VideoSettings.IsVSync", IsVSync );
            }

        }
    }
}
