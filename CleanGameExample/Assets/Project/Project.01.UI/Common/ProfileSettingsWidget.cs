#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class ProfileSettingsWidget : UIWidgetBase<ProfileSettingsWidgetView> {

        // App
        private Application2 Application { get; }
        private Storage.ProfileSettings ProfileSettings => Application.ProfileSettings;
        // View
        public override ProfileSettingsWidgetView View { get; }

        // Constructor
        public ProfileSettingsWidget() {
            Application = Utils.Container.RequireDependency<Application2>();
            View = CreateView( this, ProfileSettings );
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
                ProfileSettings.Name = View.Name;
                ProfileSettings.Save();
            } else {
                ProfileSettings.Load();
            }
        }

        // Helpers
        private static ProfileSettingsWidgetView CreateView(ProfileSettingsWidget widget, Storage.ProfileSettings profileSettings) {
            var view = new ProfileSettingsWidgetView() {
                Name = profileSettings.Name,
                NameValidator = profileSettings.IsNameValid
            };
            return view;
        }

    }
}
