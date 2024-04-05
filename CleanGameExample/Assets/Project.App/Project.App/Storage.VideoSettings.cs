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
            public VideoSettings() {
                Load();
            }

            // Save
            public void Save() {
                Save( "VideoSettings.IsVSync", IsVSync );
            }
            public void Load() {
                IsVSync = Load( "VideoSettings.IsVSync", true );
            }

        }
    }
}
