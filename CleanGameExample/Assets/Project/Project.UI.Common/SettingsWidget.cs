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
        public SettingsWidget() {
            View = CreateView( this );
            AttachChild( new ProfileSettingsWidget() );
            AttachChild( new VideoSettingsWidget() );
            AttachChild( new AudioSettingsWidget() );
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
        }

        // ShowView
        public override void ShowView(UIViewBase view) {
            if (view is ProfileSettingsWidgetView profileSettingsWidgetView) {
                View.ProfileSettingsSlot.Set( profileSettingsWidgetView );
                return;
            }
            if (view is VideoSettingsWidgetView videoSettingsWidgetView) {
                View.VideoSettingsSlot.Set( videoSettingsWidgetView );
                return;
            }
            if (view is AudioSettingsWidgetView audioSettingsWidgetView) {
                View.AudioSettingsSlot.Set( audioSettingsWidgetView );
                return;
            }
            base.ShowView( view );
        }
        public override void HideView(UIViewBase view) {
            if (view is ProfileSettingsWidgetView) {
                View.ProfileSettingsSlot.Clear();
                return;
            }
            if (view is VideoSettingsWidgetView) {
                View.VideoSettingsSlot.Clear();
                return;
            }
            if (view is AudioSettingsWidgetView) {
                View.AudioSettingsSlot.Clear();
                return;
            }
            base.HideView( view );
        }

        // Helpers
        private static SettingsWidgetView CreateView(SettingsWidget widget) {
            var view = new SettingsWidgetView();
            view.Root.OnChangeAny( evt => {
                view.Okey.SetValid( view.TabView.IsContentValid() );
            } );
            view.Okey.OnClick( evt => {
                if (view.Okey.IsValid()) {
                    widget.DetachSelf( DetachReason.Submit );
                }
            } );
            view.Back.OnClick( evt => {
                widget.DetachSelf( DetachReason.Cancel );
            } );
            return view;
        }

    }
}
