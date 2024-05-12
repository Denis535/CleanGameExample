#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class AudioSettingsWidgetView : UIViewBase {

        // Root
        public ElementWrapper Root { get; }
        public SliderFieldWrapper<float> MasterVolume { get; }
        public SliderFieldWrapper<float> MusicVolume { get; }
        public SliderFieldWrapper<float> SfxVolume { get; }
        public SliderFieldWrapper<float> GameVolume { get; }

        // Constructor
        public AudioSettingsWidgetView() {
            VisualElement = ViewFactory.AudioSettingsWidget( out var root, out var masterVolume, out var musicVolume, out var sfxVolume, out var gameVolume );
            Root = root.Wrap();
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
