#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class AudioSettingsWidgetView : UIViewBase {

        private readonly VisualElement widget;
        private readonly Slider masterVolume;
        private readonly Slider musicVolume;
        private readonly Slider sfxVolume;
        private readonly Slider gameVolume;

        // Layer
        public override int Layer => 0;
        // Values
        public float MasterVolume {
            get => masterVolume.value;
            init => masterVolume.value = value;
        }
        public float MusicVolume {
            get => musicVolume.value;
            init => musicVolume.value = value;
        }
        public float SfxVolume {
            get => sfxVolume.value;
            init => sfxVolume.value = value;
        }
        public float GameVolume {
            get => gameVolume.value;
            init => gameVolume.value = value;
        }
        // Events
        public Observable<ChangeEvent<float>> OnMasterVolume => masterVolume.Observable<ChangeEvent<float>>();
        public Observable<ChangeEvent<float>> OnMusicVolume => musicVolume.Observable<ChangeEvent<float>>();
        public Observable<ChangeEvent<float>> OnSfxVolume => sfxVolume.Observable<ChangeEvent<float>>();
        public Observable<ChangeEvent<float>> OnGameVolume => gameVolume.Observable<ChangeEvent<float>>();

        // Constructor
        public AudioSettingsWidgetView() {
            VisualElement = VisualElementFactory_Common.AudioSettings( out widget, out masterVolume, out musicVolume, out sfxVolume, out gameVolume );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
