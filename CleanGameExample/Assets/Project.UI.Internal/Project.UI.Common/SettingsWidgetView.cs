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
        public WidgetSlotWrapper<UIWidgetBase> ProfileSettingsSlot { get; }
        public WidgetSlotWrapper<UIWidgetBase> VideoSettingsSlot { get; }
        public WidgetSlotWrapper<UIWidgetBase> AudioSettingsSlot { get; }
        public ButtonWrapper Okey { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public SettingsWidgetView() {
            VisualElement = CommonViewFactory.SettingsWidget( out var root, out var title, out var tabView, out var profileSettingsSlot, out var videoSettingsSlot, out var audioSettingsSlot, out var okey, out var back );
            Root = root.Wrap();
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
