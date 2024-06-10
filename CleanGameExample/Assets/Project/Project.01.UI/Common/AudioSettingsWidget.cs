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
        public AudioSettingsWidget(IDependencyContainer container) {
            Application = container.RequireDependency<Application2>();
            View = CreateView( this );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
            if (argument is DeactivateReason.Submit) {
                AudioSettings.MasterVolume = View.MasterVolume.Value;
                AudioSettings.MusicVolume = View.MusicVolume.Value;
                AudioSettings.SfxVolume = View.SfxVolume.Value;
                AudioSettings.GameVolume = View.GameVolume.Value;
                AudioSettings.Save();
            } else {
                AudioSettings.Load();
            }
        }

        // OnDescendantActivate
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
                MasterVolume = (widget.AudioSettings.MasterVolume, 0, 1),
                MusicVolume = (widget.AudioSettings.MusicVolume, 0, 1),
                SfxVolume = (widget.AudioSettings.SfxVolume, 0, 1),
                GameVolume = (widget.AudioSettings.GameVolume, 0, 1),
            };
            view.OnMasterVolume( evt => {
                widget.AudioSettings.MasterVolume = evt.newValue;
            } );
            view.OnMusicVolume( evt => {
                widget.AudioSettings.MusicVolume = evt.newValue;
            } );
            view.OnSfxVolume( evt => {
                widget.AudioSettings.SfxVolume = evt.newValue;
            } );
            view.OnGameVolume( evt => {
                widget.AudioSettings.GameVolume = evt.newValue;
            } );
            return view;
        }

    }
}
