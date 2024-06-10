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
        public ProfileSettingsWidget(IDependencyContainer container) {
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
                ProfileSettings.Name = View.Name;
                ProfileSettings.Save();
            } else {
                ProfileSettings.Load();
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
        private static ProfileSettingsWidgetView CreateView(ProfileSettingsWidget widget) {
            var view = new ProfileSettingsWidgetView() {
                Name = widget.ProfileSettings.Name,
                NameValidator = widget.ProfileSettings.IsNameValid
            };
            return view;
        }

    }
}
