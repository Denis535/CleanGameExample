#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

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
                AudioSettings.MasterVolume = View.MasterVolume.value;
                AudioSettings.MusicVolume = View.MusicVolume.value;
                AudioSettings.SfxVolume = View.SfxVolume.value;
                AudioSettings.GameVolume = View.GameVolume.value;
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
            var view = new AudioSettingsWidgetView();
            view.MasterVolume.value = widget.AudioSettings.MasterVolume;
            view.MasterVolume.SetMinMax( 0, 1 );
            view.MusicVolume.value = widget.AudioSettings.MusicVolume;
            view.MusicVolume.SetMinMax( 0, 1 );
            view.SfxVolume.value = widget.AudioSettings.SfxVolume;
            view.SfxVolume.SetMinMax( 0, 1 );
            view.GameVolume.value = widget.AudioSettings.GameVolume;
            view.GameVolume.SetMinMax( 0, 1 );
            view.MasterVolume.RegisterCallback<ChangeEvent<float>>( evt => {
                widget.AudioSettings.MasterVolume = evt.newValue;
            } );
            view.MusicVolume.RegisterCallback<ChangeEvent<float>>( evt => {
                widget.AudioSettings.MusicVolume = evt.newValue;
            } );
            view.SfxVolume.RegisterCallback<ChangeEvent<float>>( evt => {
                widget.AudioSettings.SfxVolume = evt.newValue;
            } );
            view.GameVolume.RegisterCallback<ChangeEvent<float>>( evt => {
                widget.AudioSettings.GameVolume = evt.newValue;
            } );
            return view;
        }

    }
}
