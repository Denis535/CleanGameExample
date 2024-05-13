#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class AudioSettingsWidgetView : UIViewBase {

        private readonly Slider masterVolume;
        private readonly Slider musicVolume;
        private readonly Slider sfxVolume;
        private readonly Slider gameVolume;

        // Props
        public float MasterVolume => masterVolume.value;
        public float MusicVolume => musicVolume.value;
        public float SfxVolume => sfxVolume.value;
        public float GameVolume => gameVolume.value;

        // Constructor
        public AudioSettingsWidgetView((float Value, float Min, float Max) masterVolume, (float Value, float Min, float Max) musicVolume, (float Value, float Min, float Max) sfxVolume, (float Value, float Min, float Max) gameVolume) {
            VisualElement = VisualElementFactory_Common.AudioSettingsView( out this.masterVolume, out this.musicVolume, out this.sfxVolume, out this.gameVolume );
            this.masterVolume.value = masterVolume.Value;
            this.masterVolume.lowValue = masterVolume.Min;
            this.masterVolume.highValue = masterVolume.Max;
            this.musicVolume.value = musicVolume.Value;
            this.musicVolume.lowValue = musicVolume.Min;
            this.musicVolume.highValue = musicVolume.Max;
            this.sfxVolume.value = sfxVolume.Value;
            this.sfxVolume.lowValue = sfxVolume.Min;
            this.sfxVolume.highValue = sfxVolume.Max;
            this.gameVolume.value = gameVolume.Value;
            this.gameVolume.lowValue = gameVolume.Min;
            this.gameVolume.highValue = gameVolume.Max;
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
