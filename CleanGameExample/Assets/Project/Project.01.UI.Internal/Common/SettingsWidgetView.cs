﻿#nullable enable
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
        private readonly TabView tabView;
        private readonly Tab profileSettingsTab;
        private readonly Tab videoSettingsTab;
        private readonly Tab audioSettingsTab;
        private readonly Button okey;
        private readonly Button back;

        // Layer
        public override int Layer => 0;
        // Props
        public ProfileSettingsWidgetView? ProfileSettings {
            get => profileSettingsTab.Children().FirstOrDefault()?.GetView<ProfileSettingsWidgetView>();
            set {
                if (value != null) {
                    profileSettingsTab.Add( value );
                } else {
                    profileSettingsTab.Clear();
                }
            }
        }
        public VideoSettingsWidgetView? VideoSettings {
            get => videoSettingsTab.Children().FirstOrDefault()?.GetView<VideoSettingsWidgetView>();
            set {
                if (value != null) {
                    videoSettingsTab.Add( value );
                } else {
                    videoSettingsTab.Clear();
                }
            }
        }
        public AudioSettingsWidgetView? AudioSettings {
            get => audioSettingsTab.Children().FirstOrDefault()?.GetView<AudioSettingsWidgetView>();
            set {
                if (value != null) {
                    audioSettingsTab.Add( value );
                } else {
                    audioSettingsTab.Clear();
                }
            }
        }
        // Props
        public Observable<ClickEvent> OnOkey => okey.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnBack => back.AsObservable<ClickEvent>();

        // Constructor
        public SettingsWidgetView() {
            VisualElement = VisualElementFactory_Common.Settings( out widget, out title, out tabView, out profileSettingsTab, out videoSettingsTab, out audioSettingsTab, out okey, out back );
            widget.OnValidate( evt => {
                okey.SetValid( tabView.GetDescendants().All( i => i.IsValidSelf() ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
