﻿#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class SettingsWidget : UIWidgetBase<SettingsWidgetView> {

        // Globals
        private UIFactory Factory { get; }
        // View
        protected override SettingsWidgetView View { get; }
        // Children
        private ProfileSettingsWidget ProfileSettingsWidget => View.ProfileSettingsSlot.Widget!;
        private VideoSettingsWidget VideoSettingsWidget => View.VideoSettingsSlot.Widget!;
        private AudioSettingsWidget AudioSettingsWidget => View.AudioSettingsSlot.Widget!;

        // Constructor
        public SettingsWidget() {
            Factory = this.GetDependencyContainer().Resolve<UIFactory>( null );
            View = CreateView( this, Factory );
            this.AttachChild( new ProfileSettingsWidget() );
            this.AttachChild( new VideoSettingsWidget() );
            this.AttachChild( new AudioSettingsWidget() );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
        }
        public override void OnDetach(object? argument) {
        }

        // ShowDescendantWidget
        protected override void ShowDescendantWidget(UIWidgetBase widget) {
            if (widget is ProfileSettingsWidget profileSettingsWidget) {
                View.ProfileSettingsSlot.Add( profileSettingsWidget );
                return;
            }
            if (widget is VideoSettingsWidget videoSettingsWidget) {
                View.VideoSettingsSlot.Add( videoSettingsWidget );
                return;
            }
            if (widget is AudioSettingsWidget audioSettingsWidget) {
                View.AudioSettingsSlot.Add( audioSettingsWidget );
                return;
            }
            base.ShowDescendantWidget( widget );
        }
        protected override void HideDescendantWidget(UIWidgetBase widget) {
            if (widget is ProfileSettingsWidget profileSettingsWidget) {
                View.ProfileSettingsSlot.Remove( profileSettingsWidget );
                return;
            }
            if (widget is VideoSettingsWidget videoSettingsWidget) {
                View.VideoSettingsSlot.Remove( videoSettingsWidget );
                return;
            }
            if (widget is AudioSettingsWidget audioSettingsWidget) {
                View.AudioSettingsSlot.Remove( audioSettingsWidget );
                return;
            }
            base.HideDescendantWidget( widget );
        }

        // Helpers
        private static SettingsWidgetView CreateView(SettingsWidget widget, UIFactory factory) {
            var view = new SettingsWidgetView( factory );
            view.Widget.OnChangeAny( evt => {
                view.Okey.SetValid( view.TabView.__GetVisualElement__().GetDescendants().All( i => i.IsValid() ) );
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
