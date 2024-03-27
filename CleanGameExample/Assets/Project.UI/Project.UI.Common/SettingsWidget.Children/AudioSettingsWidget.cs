#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;

    public class AudioSettingsWidget : UIWidgetBase<AudioSettingsWidgetView> {

        // Globals
        private UIFactory Factory { get; }
        private Globals.AudioSettings AudioSettings { get; }

        // Constructor
        public AudioSettingsWidget() {
            Factory = this.GetDependencyContainer().Resolve<UIFactory>( null );
            AudioSettings = this.GetDependencyContainer().Resolve<Globals.AudioSettings>( null );
            View = CreateView( this, Factory, AudioSettings );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
        }
        public override void OnDetach(object? argument) {
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
        private static AudioSettingsWidgetView CreateView(AudioSettingsWidget widget, UIFactory factory, Globals.AudioSettings audioSettings) {
            var view = new AudioSettingsWidgetView( factory );
            view.Group.OnAttachToPanel( evt => {
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
