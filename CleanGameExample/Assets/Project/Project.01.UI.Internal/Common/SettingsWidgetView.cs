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
        private readonly TabView tabView;
        private readonly Tab profileSettings;
        private readonly Tab videoSettings;
        private readonly Tab audioSettings;
        private readonly Button okey;
        private readonly Button back;

        // Props
        public override int Layer => 0;
        // Props
        public string Title => title.text;
        public ProfileSettingsWidgetView? ProfileSettingsWidgetView => profileSettings.Children().FirstOrDefault()?.GetView<ProfileSettingsWidgetView>();
        public VideoSettingsWidgetView? VideoSettingsWidgetView => videoSettings.Children().FirstOrDefault()?.GetView<VideoSettingsWidgetView>();
        public AudioSettingsWidgetView? AudioSettingsWidgetView => audioSettings.Children().FirstOrDefault()?.GetView<AudioSettingsWidgetView>();

        // Constructor
        public SettingsWidgetView() {
            VisualElement = VisualElementFactory_Common.SettingsWidget( out widget, out title, out tabView, out profileSettings, out videoSettings, out audioSettings, out okey, out back );
            widget.OnValidate( evt => {
                okey.SetValid( tabView.GetDescendants().All( i => i.IsValidSelf() ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // AddView
        public void AddView(ProfileSettingsWidgetView view) {
            profileSettings.Add( view );
        }
        public void AddView(VideoSettingsWidgetView view) {
            videoSettings.Add( view );
        }
        public void AddView(AudioSettingsWidgetView view) {
            audioSettings.Add( view );
        }

        // RemoveView
        public void RemoveView(ProfileSettingsWidgetView view) {
            profileSettings.Remove( view );
        }
        public void RemoveView(VideoSettingsWidgetView view) {
            videoSettings.Remove( view );
        }
        public void RemoveView(AudioSettingsWidgetView view) {
            audioSettings.Remove( view );
        }

        // OnEvent
        public void OnOkey(EventCallback<ClickEvent> callback) {
            okey.OnClick( callback );
        }
        public void OnBack(EventCallback<ClickEvent> callback) {
            back.OnClick( callback );
        }

    }
}
