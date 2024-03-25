#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class SettingsWidgetView : UIViewBase {

        // VisualElement
        protected override VisualElement VisualElement { get; }
        public ElementWrapper Widget { get; }
        public LabelWrapper Title { get; }
        public ElementWrapper TabView { get; }
        public SlotWrapper<ProfileSettingsWidgetView> ProfileSettingsTab { get; }
        public SlotWrapper<VideoSettingsWidgetView> VideoSettingsTab { get; }
        public SlotWrapper<AudioSettingsWidgetView> AudioSettingsTab { get; }
        public ButtonWrapper Okey { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public SettingsWidgetView(UIFactory factory) {
            VisualElement = factory.SettingsWidget( out var widget, out var title, out var tabView, out var profileSettingsTab, out var videoSettingsTab, out var audioSettingsTab, out var okey, out var back );
            Widget = widget.Wrap();
            Title = title.Wrap();
            TabView = tabView.Wrap();
            ProfileSettingsTab = profileSettingsTab.AsSlot<ProfileSettingsWidgetView>();
            VideoSettingsTab = videoSettingsTab.AsSlot<VideoSettingsWidgetView>();
            AudioSettingsTab = audioSettingsTab.AsSlot<AudioSettingsWidgetView>();
            Okey = okey.Wrap();
            Back = back.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
