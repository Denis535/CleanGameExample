#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class SettingsWidget : UIWidgetBase<SettingsWidgetView> {

        // View
        public override SettingsWidgetView View { get; }

        // Constructor
        public SettingsWidget(IDependencyContainer container) {
            View = CreateView( this );
            AddChild( new ProfileSettingsWidget( container ) );
            AddChild( new VideoSettingsWidget( container ) );
            AddChild( new AudioSettingsWidget( container ) );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        public override void OnActivate(object? argument) {
            ShowSelf();
        }
        public override void OnDeactivate(object? argument) {
            HideSelf();
        }

        // ShowView
        public override void ShowView(UIViewBase view) {
            if (view is ProfileSettingsWidgetView profileSettingsWidgetView) {
                View.Add( profileSettingsWidgetView );
                return;
            }
            if (view is VideoSettingsWidgetView videoSettingsWidgetView) {
                View.Add( videoSettingsWidgetView );
                return;
            }
            if (view is AudioSettingsWidgetView audioSettingsWidgetView) {
                View.Add( audioSettingsWidgetView );
                return;
            }
            base.ShowView( view );
        }
        public override void HideView(UIViewBase view) {
            if (view is ProfileSettingsWidgetView profileSettingsWidgetView) {
                View.Remove( profileSettingsWidgetView );
                return;
            }
            if (view is VideoSettingsWidgetView videoSettingsWidgetView) {
                View.Remove( videoSettingsWidgetView );
                return;
            }
            if (view is AudioSettingsWidgetView audioSettingsWidgetView) {
                View.Remove( audioSettingsWidgetView );
                return;
            }
            base.HideView( view );
        }

        // Helpers
        private static SettingsWidgetView CreateView(SettingsWidget widget) {
            var view = new SettingsWidgetView();
            view.OnOkey( evt => {
                if (evt.GetTarget().IsValidSelf()) {
                    widget.RemoveSelf( DeactivateReason.Submit );
                }
            } );
            view.OnBack( evt => {
                widget.RemoveSelf( DeactivateReason.Cancel );
            } );
            return view;
        }

    }
}
