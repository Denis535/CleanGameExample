#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class AudioSettingsWidgetView : UIViewBase {

        // VisualElement
        protected override VisualElement VisualElement { get; }
        public ElementWrapper Group { get; }
        public SliderFieldWrapper<float> MasterVolume { get; }
        public SliderFieldWrapper<float> MusicVolume { get; }
        public SliderFieldWrapper<float> SfxVolume { get; }
        public SliderFieldWrapper<float> GameVolume { get; }

        // Constructor
        public AudioSettingsWidgetView(UIFactory factory) {
            VisualElement = factory.AudioSettingsWidget( out var group, out var masterVolume, out var musicVolume, out var sfxVolume, out var gameVolume );
            Group = group.Wrap();
            MasterVolume = masterVolume.Wrap();
            MusicVolume = musicVolume.Wrap();
            SfxVolume = sfxVolume.Wrap();
            GameVolume = gameVolume.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
