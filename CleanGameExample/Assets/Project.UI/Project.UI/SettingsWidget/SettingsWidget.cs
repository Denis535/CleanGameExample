#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class SettingsWidget : UIWidgetBase<SettingsWidgetView> {

        // Children
        private ProfileSettingsWidget ProfileSettingsWidget => View.ProfileSettingsSlot.Widget!;
        private VideoSettingsWidget VideoSettingsWidget => View.VideoSettingsSlot.Widget!;
        private AudioSettingsWidget AudioSettingsWidget => View.AudioSettingsSlot.Widget!;

        // Constructor
        public SettingsWidget() {
            View = CreateView( this );
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
            base.ShowDescendantWidget( widget );
        }
        protected override void HideDescendantWidget(UIWidgetBase widget) {
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
            base.HideDescendantWidget( widget );
        }

        // Helpers
        private static SettingsWidgetView CreateView(SettingsWidget widget) {
            var view = new SettingsWidgetView();
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
