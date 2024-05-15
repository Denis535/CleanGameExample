#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class AudioSettingsWidgetView : UIViewBase {

        private readonly VisualElement view;
        private readonly Slider masterVolume;
        private readonly Slider musicVolume;
        private readonly Slider sfxVolume;
        private readonly Slider gameVolume;

        // Props
        public (float Value, float Min, float Max) MasterVolume {
            get => masterVolume.GetValueMinMax();
            init => masterVolume.GetValueMinMax( value );
        }
        public (float Value, float Min, float Max) MusicVolume {
            get => musicVolume.GetValueMinMax();
            init => musicVolume.GetValueMinMax( value );
        }
        public (float Value, float Min, float Max) SfxVolume {
            get => sfxVolume.GetValueMinMax();
            init => sfxVolume.GetValueMinMax( value );
        }
        public (float Value, float Min, float Max) GameVolume {
            get => gameVolume.GetValueMinMax();
            init => gameVolume.GetValueMinMax( value );
        }

        // Constructor
        public AudioSettingsWidgetView() {
            VisualElement = VisualElementFactory_Common.AudioSettingsWidgetView( out view, out masterVolume, out musicVolume, out sfxVolume, out gameVolume );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnEvent
        public void OnMasterVolume(EventCallback<ChangeEvent<float>> callback) {
            masterVolume.OnChange( callback );
        }
        public void OnMusicVolume(EventCallback<ChangeEvent<float>> callback) {
            musicVolume.OnChange( callback );
        }
        public void OnSfxVolume(EventCallback<ChangeEvent<float>> callback) {
            sfxVolume.OnChange( callback );
        }
        public void OnGameVolume(EventCallback<ChangeEvent<float>> callback) {
            gameVolume.OnChange( callback );
        }

    }
}
