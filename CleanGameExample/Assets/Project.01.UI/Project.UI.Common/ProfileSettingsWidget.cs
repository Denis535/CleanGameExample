#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class ProfileSettingsWidget : UIWidgetBase<ProfileSettingsWidgetView> {

        // Storage
        private Storage.ProfileSettings ProfileSettings { get; }
        // View
        public override ProfileSettingsWidgetView View { get; }

        // Constructor
        public ProfileSettingsWidget() {
            ProfileSettings = Utils.Container.RequireDependency<Storage.ProfileSettings>( null );
            View = CreateView( this, ProfileSettings );
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
                ProfileSettings.Name = View.Name.Value!;
                ProfileSettings.Save();
            } else {
                ProfileSettings.Load();
            }
        }

        // Helpers
        private static ProfileSettingsWidgetView CreateView(ProfileSettingsWidget widget, Storage.ProfileSettings profileSettings) {
            var view = new ProfileSettingsWidgetView();
            view.Root.OnAttachToPanel( evt => {
                view.Name.Value = profileSettings.Name;
                view.Name.SetValid( profileSettings.IsNameValid( view.Name.Value ) );
            } );
            view.Name.OnChange( evt => {
                view.Name.SetValid( profileSettings.IsNameValid( evt.newValue! ) );
            } );
            return view;
        }

    }
}
