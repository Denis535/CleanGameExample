#nullable enable
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

        // ShowWidget
        protected override void ShowWidget(UIWidgetBase widget) {
            if (widget is ProfileSettingsWidget) {
                View.ProfileSettingsTab.Add( widget );
                return;
            }
            if (widget is VideoSettingsWidget) {
                View.VideoSettingsTab.Add( widget );
                return;
            }
            if (widget is AudioSettingsWidget) {
                View.AudioSettingsTab.Add( widget );
                return;
            }
            base.ShowWidget( widget );
        }
        protected override void HideWidget(UIWidgetBase widget) {
            if (widget is ProfileSettingsWidget) {
                View.ProfileSettingsTab.Remove( widget );
                return;
            }
            if (widget is VideoSettingsWidget) {
                View.VideoSettingsTab.Remove( widget );
                return;
            }
            if (widget is AudioSettingsWidget) {
                View.AudioSettingsTab.Remove( widget );
                return;
            }
            base.HideWidget( widget );
        }

        // Helpers
        private static SettingsWidgetView CreateView(SettingsWidget widget, UIFactory factory) {
            var view = new SettingsWidgetView( factory );
            view.Widget.OnChangeAny( evt => {
                view.Okey.SetValid( view.ProfileSettingsTab.__GetVisualElement__().GetDescendants().All( i => i.IsValid() ) &&
                    view.VideoSettingsTab.__GetVisualElement__().GetDescendants().All( i => i.IsValid() ) &&
                    view.AudioSettingsTab.__GetVisualElement__().GetDescendants().All( i => i.IsValid() ) );
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
