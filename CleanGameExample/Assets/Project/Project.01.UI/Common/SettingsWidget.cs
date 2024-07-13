﻿#nullable enable
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
                View.ProfileSettingsTab.AddView( profileSettings );
                return;
            }
            if (view is VideoSettingsWidgetView videoSettings) {
                View.VideoSettingsTab.AddView( videoSettings );
                return;
            }
            if (view is AudioSettingsWidgetView audioSettings) {
                View.AudioSettingsTab.AddView( audioSettings );
                return;
            }
            base.ShowView( view );
        }
        protected override void HideView(UIViewBase view) {
            if (view is ProfileSettingsWidgetView profileSettings) {
                View.ProfileSettingsTab.Clear();
                return;
            }
            if (view is VideoSettingsWidgetView videoSettings) {
                View.VideoSettingsTab.Clear();
                return;
            }
            if (view is AudioSettingsWidgetView audioSettings) {
                View.AudioSettingsTab.Clear();
                return;
            }
            base.HideView( view );
        }

        // Helpers
        private static SettingsWidgetView CreateView(SettingsWidget widget) {
            var view = new SettingsWidgetView();
            view.Okey.RegisterCallback<ClickEvent>( evt => {
                if (evt.GetTarget().IsValidSelf()) {
                    widget.RemoveSelf( DeactivateReason.Submit );
                }
            } );
            view.Back.RegisterCallback<ClickEvent>( evt => {
                widget.RemoveSelf( DeactivateReason.Cancel );
            } );
            return view;
        }

    }
}
