﻿#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class SettingsWidgetView : UIViewBase {

        // View
        public ElementWrapper Widget { get; }
        public LabelWrapper Title { get; }
        public ElementWrapper TabView { get; }
        public WidgetSlotWrapper<ProfileSettingsWidget> ProfileSettingsSlot { get; }
        public WidgetSlotWrapper<VideoSettingsWidget> VideoSettingsSlot { get; }
        public WidgetSlotWrapper<AudioSettingsWidget> AudioSettingsSlot { get; }
        public ButtonWrapper Okey { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public SettingsWidgetView(UIFactory factory) {
            VisualElement = factory.SettingsWidget( out var widget, out var title, out var tabView, out var profileSettingsSlot, out var videoSettingsSlot, out var audioSettingsSlot, out var okey, out var back );
            Widget = widget.Wrap();
            Title = title.Wrap();
            TabView = tabView.Wrap();
            ProfileSettingsSlot = profileSettingsSlot.AsWidgetSlot<ProfileSettingsWidget>();
            VideoSettingsSlot = videoSettingsSlot.AsWidgetSlot<VideoSettingsWidget>();
            AudioSettingsSlot = audioSettingsSlot.AsWidgetSlot<AudioSettingsWidget>();
            Okey = okey.Wrap();
            Back = back.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
