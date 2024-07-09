#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class SettingsWidgetView : UIViewBase2 {

        private readonly Widget widget;
        private readonly Label title;
        private readonly VisualElement profileSettings;
        private readonly VisualElement videoSettings;
        private readonly VisualElement audioSettings;
        private readonly Button okey;
        private readonly Button back;

        protected override VisualElement VisualElement => widget;
        public ProfileSettingsWidgetView? ProfileSettingsEvent {
            get => profileSettings.GetViews<ProfileSettingsWidgetView>().FirstOrDefault();
            set {
                if (value != null) {
                    profileSettings.AddView( value );
                } else {
                    profileSettings.Clear();
                }
            }
        }
        public VideoSettingsWidgetView? VideoSettingsEvent {
            get => videoSettings.GetViews<VideoSettingsWidgetView>().FirstOrDefault();
            set {
                if (value != null) {
                    videoSettings.AddView( value );
                } else {
                    videoSettings.Clear();
                }
            }
        }
        public AudioSettingsWidgetView? AudioSettingsEvent {
            get => audioSettings.GetViews<AudioSettingsWidgetView>().FirstOrDefault();
            set {
                if (value != null) {
                    audioSettings.AddView( value );
                } else {
                    audioSettings.Clear();
                }
            }
        }
        public event EventCallback<ClickEvent> OnOkeyEvent {
            add => okey.RegisterCallback( value );
            remove => okey.RegisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBackEvent {
            add => back.RegisterCallback( value );
            remove => back.RegisterCallback( value );
        }

        public SettingsWidgetView() {
            VisualElementFactory_Common.Settings( this, out widget, out title, out profileSettings, out videoSettings, out audioSettings, out okey, out back );
            widget.OnValidate( evt => {
                okey.SetValid(
                    profileSettings.GetDescendants().All( i => i.IsValidSelf() ) &&
                    videoSettings.GetDescendants().All( i => i.IsValidSelf() ) &&
                    audioSettings.GetDescendants().All( i => i.IsValidSelf() ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
