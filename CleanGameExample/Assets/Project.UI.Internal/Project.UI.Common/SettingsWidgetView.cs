#nullable enable
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
        public WidgetSlotWrapper<UIWidgetBase> ProfileSettingsSlot { get; }
        public WidgetSlotWrapper<UIWidgetBase> VideoSettingsSlot { get; }
        public WidgetSlotWrapper<UIWidgetBase> AudioSettingsSlot { get; }
        public ButtonWrapper Okey { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public SettingsWidgetView() {
            VisualElement = ViewFactory.SettingsWidget( out var widget, out var title, out var tabView, out var profileSettingsSlot, out var videoSettingsSlot, out var audioSettingsSlot, out var okey, out var back );
            Widget = widget.Wrap();
            Title = title.Wrap();
            TabView = tabView.Wrap();
            ProfileSettingsSlot = profileSettingsSlot.AsWidgetSlot<UIWidgetBase>();
            VideoSettingsSlot = videoSettingsSlot.AsWidgetSlot<UIWidgetBase>();
            AudioSettingsSlot = audioSettingsSlot.AsWidgetSlot<UIWidgetBase>();
            Okey = okey.Wrap();
            Back = back.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
