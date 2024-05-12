#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class SettingsWidgetView : UIViewBase {

        // Root
        public ElementWrapper Root { get; }
        public LabelWrapper Title { get; }
        public ElementWrapper TabView { get; }
        public ViewSlotWrapper<ProfileSettingsWidgetView> ProfileSettingsSlot { get; }
        public ViewSlotWrapper<VideoSettingsWidgetView> VideoSettingsSlot { get; }
        public ViewSlotWrapper<AudioSettingsWidgetView> AudioSettingsSlot { get; }
        public ButtonWrapper Okey { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public SettingsWidgetView() {
            VisualElement = ViewFactory.SettingsWidget( out var root, out var title, out var tabView, out var profileSettingsSlot, out var videoSettingsSlot, out var audioSettingsSlot, out var okey, out var back );
            Root = root.Wrap();
            Title = title.Wrap();
            TabView = tabView.Wrap();
            ProfileSettingsSlot = profileSettingsSlot.AsViewSlot<ProfileSettingsWidgetView>();
            VideoSettingsSlot = videoSettingsSlot.AsViewSlot<VideoSettingsWidgetView>();
            AudioSettingsSlot = audioSettingsSlot.AsViewSlot<AudioSettingsWidgetView>();
            Okey = okey.Wrap();
            Back = back.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
