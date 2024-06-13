#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class SettingsWidgetView : UIViewBase {

        private readonly Widget widget;
        private readonly Label title;
        private readonly TabView view;
        private readonly Tab profileSettings;
        private readonly Tab videoSettings;
        private readonly Tab audioSettings;
        private readonly Button okey;
        private readonly Button back;

        // Layer
        public override int Layer => 0;
        // Props
        public ProfileSettingsWidgetView? ProfileSettings {
            get => profileSettings.Children2<ProfileSettingsWidgetView>().FirstOrDefault();
            set {
                if (value != null) {
                    profileSettings.Add( value );
                } else {
                    profileSettings.Clear();
                }
            }
        }
        public VideoSettingsWidgetView? VideoSettings {
            get => videoSettings.Children2<VideoSettingsWidgetView>().FirstOrDefault();
            set {
                if (value != null) {
                    videoSettings.Add( value );
                } else {
                    videoSettings.Clear();
                }
            }
        }
        public AudioSettingsWidgetView? AudioSettings {
            get => audioSettings.Children2<AudioSettingsWidgetView>().FirstOrDefault();
            set {
                if (value != null) {
                    audioSettings.Add( value );
                } else {
                    audioSettings.Clear();
                }
            }
        }
        public event EventCallback<ClickEvent> OnOkey {
            add => okey.RegisterCallback( value );
            remove => okey.RegisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBack {
            add => back.RegisterCallback( value );
            remove => back.RegisterCallback( value );
        }

        // Constructor
        public SettingsWidgetView() {
            VisualElement = VisualElementFactory_Common.Settings( out widget, out title, out view, out profileSettings, out videoSettings, out audioSettings, out okey, out back );
            widget.OnValidate( evt => {
                okey.SetValid( view.GetDescendants().All( i => i.IsValidSelf() ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
