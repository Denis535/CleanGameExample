#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class AudioSettingsWidget : UIWidgetBase<AudioSettingsWidgetView> {

        // App
        private Application2 Application { get; }
        private Storage.AudioSettings AudioSettings => Application.AudioSettings;
        // View
        public override AudioSettingsWidgetView View { get; }

        // Constructor
        public AudioSettingsWidget() {
            Application = Utils.Container.RequireDependency<Application2>();
            View = CreateView( this, AudioSettings );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
            ShowSelf();
        }
        public override void OnDetach(object? argument) {
            HideSelf();
            if (argument is DetachReason.Submit) {
                AudioSettings.MasterVolume = View.MasterVolume.Value;
                AudioSettings.MusicVolume = View.MusicVolume.Value;
                AudioSettings.SfxVolume = View.SfxVolume.Value;
                AudioSettings.GameVolume = View.GameVolume.Value;
                AudioSettings.Save();
            } else {
                AudioSettings.Load();
            }
        }

        // Helpers
        private static AudioSettingsWidgetView CreateView(AudioSettingsWidget widget, Storage.AudioSettings audioSettings) {
            var view = new AudioSettingsWidgetView() {
                MasterVolume = (audioSettings.MasterVolume, 0, 1),
                MusicVolume = (audioSettings.MusicVolume, 0, 1),
                SfxVolume = (audioSettings.SfxVolume, 0, 1),
                GameVolume = (audioSettings.GameVolume, 0, 1),
            };
            view.OnMasterVolume( evt => {
                audioSettings.MasterVolume = evt.newValue;
            } );
            view.OnMusicVolume( evt => {
                audioSettings.MusicVolume = evt.newValue;
            } );
            view.OnSfxVolume( evt => {
                audioSettings.SfxVolume = evt.newValue;
            } );
            view.OnGameVolume( evt => {
                audioSettings.GameVolume = evt.newValue;
            } );
            return view;
        }

    }
}
