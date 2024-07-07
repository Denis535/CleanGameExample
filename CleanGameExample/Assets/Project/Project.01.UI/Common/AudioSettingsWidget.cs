#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class AudioSettingsWidget : UIWidgetBase2<AudioSettingsWidgetView> {

        private Application2 Application { get; }
        private Storage.AudioSettings AudioSettings => Application.AudioSettings;

        public AudioSettingsWidget(IDependencyContainer container) : base( container ) {
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
            if (argument is DeactivateReason.Submit) {
                AudioSettings.MasterVolume = View.MasterVolume;
                AudioSettings.MusicVolume = View.MusicVolume;
                AudioSettings.SfxVolume = View.SfxVolume;
                AudioSettings.GameVolume = View.GameVolume;
                AudioSettings.Save();
            } else {
                AudioSettings.Load();
            }
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        // Helpers
        private static AudioSettingsWidgetView CreateView(AudioSettingsWidget widget) {
            var view = new AudioSettingsWidgetView() {
                MasterVolume = widget.AudioSettings.MasterVolume,
                MasterVolumeMinMax = (0, 1),
                MusicVolume = widget.AudioSettings.MusicVolume,
                MusicVolumeMinMax = (0, 1),
                SfxVolume = widget.AudioSettings.SfxVolume,
                SfxVolumeMinMax = (0, 1),
                GameVolume = widget.AudioSettings.GameVolume,
                GameVolumeMinMax = (0, 1),
            };
            view.OnMasterVolume += evt => {
                widget.AudioSettings.MasterVolume = evt.newValue;
            };
            view.OnMusicVolume += evt => {
                widget.AudioSettings.MusicVolume = evt.newValue;
            };
            view.OnSfxVolume += evt => {
                widget.AudioSettings.SfxVolume = evt.newValue;
            };
            view.OnGameVolume += evt => {
                widget.AudioSettings.GameVolume = evt.newValue;
            };
            return view;
        }

    }
}
