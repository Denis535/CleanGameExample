#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class AudioSettingsWidget : UIWidgetBase<AudioSettingsWidgetView> {

        // View
        public override AudioSettingsWidgetView View { get; }

        // Storage
        private Storage.AudioSettings AudioSettings { get; }

        // Constructor
        public AudioSettingsWidget() {
            AudioSettings = Utils.Container.RequireDependency<Storage.AudioSettings>( null );
            View = CreateView( this, AudioSettings );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
            Show();
        }
        public override void OnDetach(object? argument) {
            Hide();
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
            var view = new AudioSettingsWidgetView();
            view.Root.OnAttachToPanel( evt => {
                view.MasterVolume.ValueMinMax = (audioSettings.MasterVolume, 0, 1);
                view.MusicVolume.ValueMinMax = (audioSettings.MusicVolume, 0, 1);
                view.SfxVolume.ValueMinMax = (audioSettings.SfxVolume, 0, 1);
                view.GameVolume.ValueMinMax = (audioSettings.GameVolume, 0, 1);
            } );
            view.MasterVolume.OnChange( evt => {
                audioSettings.MasterVolume = evt.newValue;
            } );
            view.MusicVolume.OnChange( evt => {
                audioSettings.MusicVolume = evt.newValue;
            } );
            view.SfxVolume.OnChange( evt => {
                audioSettings.SfxVolume = evt.newValue;
            } );
            view.GameVolume.OnChange( evt => {
                audioSettings.GameVolume = evt.newValue;
            } );
            return view;
        }

    }
}
