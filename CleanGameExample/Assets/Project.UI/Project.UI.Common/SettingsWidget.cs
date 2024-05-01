﻿#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class SettingsWidget : UIWidgetBase<SettingsWidgetView> {

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
            ShowWidget( this );
        }
        public override void OnDetach(object? argument) {
            HideWidget( this );
        }

        // ShowWidget
        public override void ShowWidget(UIWidgetBase widget) {
            if (widget is ProfileSettingsWidget profileSettingsWidget) {
                View.ProfileSettingsSlot.Set( profileSettingsWidget );
                return;
            }
            if (widget is VideoSettingsWidget videoSettingsWidget) {
                View.VideoSettingsSlot.Set( videoSettingsWidget );
                return;
            }
            if (widget is AudioSettingsWidget audioSettingsWidget) {
                View.AudioSettingsSlot.Set( audioSettingsWidget );
                return;
            }
            base.ShowWidget( widget );
        }
        public override void HideWidget(UIWidgetBase widget) {
            if (widget is ProfileSettingsWidget profileSettingsWidget) {
                View.ProfileSettingsSlot.Clear( profileSettingsWidget );
                return;
            }
            if (widget is VideoSettingsWidget videoSettingsWidget) {
                View.VideoSettingsSlot.Clear( videoSettingsWidget );
                return;
            }
            if (widget is AudioSettingsWidget audioSettingsWidget) {
                View.AudioSettingsSlot.Clear( audioSettingsWidget );
                return;
            }
            base.HideWidget( widget );
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