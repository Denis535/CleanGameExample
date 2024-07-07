#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class SettingsWidget : UIWidgetBase2<SettingsWidgetView> {

        public SettingsWidget(IDependencyContainer container) : base( container ) {
            View = CreateView( this );
            AddChild( new ProfileSettingsWidget( container ) );
            AddChild( new VideoSettingsWidget( container ) );
            AddChild( new AudioSettingsWidget( container ) );
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
        }

        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        protected override void ShowView(UIViewBase view) {
            if (view is ProfileSettingsWidgetView profileSettings) {
                View.ProfileSettingsEvent = profileSettings;
                return;
            }
            if (view is VideoSettingsWidgetView videoSettings) {
                View.VideoSettingsEvent = videoSettings;
                return;
            }
            if (view is AudioSettingsWidgetView audioSettings) {
                View.AudioSettingsEvent = audioSettings;
                return;
            }
            base.ShowView( view );
        }
        protected override void HideView(UIViewBase view) {
            if (view is ProfileSettingsWidgetView profileSettings) {
                View.ProfileSettingsEvent = null;
                return;
            }
            if (view is VideoSettingsWidgetView videoSettings) {
                View.VideoSettingsEvent = null;
                return;
            }
            if (view is AudioSettingsWidgetView audioSettings) {
                View.AudioSettingsEvent = null;
                return;
            }
            base.HideView( view );
        }

        // Helpers
        private static SettingsWidgetView CreateView(SettingsWidget widget) {
            var view = new SettingsWidgetView();
            view.OnOkeyEvent += evt => {
                if (evt.GetTarget().IsValidSelf()) {
                    widget.RemoveSelf( DeactivateReason.Submit );
                }
            };
            view.OnBackEvent += evt => {
                widget.RemoveSelf( DeactivateReason.Cancel );
            };
            return view;
        }

    }
}
