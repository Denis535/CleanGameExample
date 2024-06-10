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
        protected override void OnActivate(object? argument) {
            ShowSelf();
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
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

        // ShowView
        protected override void ShowView(UIViewBase view) {
            if (view is ProfileSettingsWidgetView profileSettingsWidgetView) {
                View.AddView( profileSettingsWidgetView );
                return;
            }
            if (view is VideoSettingsWidgetView videoSettingsWidgetView) {
                View.AddView( videoSettingsWidgetView );
                return;
            }
            if (view is AudioSettingsWidgetView audioSettingsWidgetView) {
                View.AddView( audioSettingsWidgetView );
                return;
            }
            base.ShowView( view );
        }
        protected override void HideView(UIViewBase view) {
            if (view is ProfileSettingsWidgetView profileSettingsWidgetView) {
                View.RemoveView( profileSettingsWidgetView );
                return;
            }
            if (view is VideoSettingsWidgetView videoSettingsWidgetView) {
                View.RemoveView( videoSettingsWidgetView );
                return;
            }
            if (view is AudioSettingsWidgetView audioSettingsWidgetView) {
                View.RemoveView( audioSettingsWidgetView );
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
