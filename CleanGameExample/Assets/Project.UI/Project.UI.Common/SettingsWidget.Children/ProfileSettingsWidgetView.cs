#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class ProfileSettingsWidgetView : UIViewBase {

        // View
        public ElementWrapper Group { get; }
        public TextFieldWrapper<string> Name { get; }

        // Constructor
        public ProfileSettingsWidgetView(UIFactory factory) {
            VisualElement = factory.ProfileSettingsWidget( out var group, out var name );
            Group = group.Wrap();
            Name = name.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
