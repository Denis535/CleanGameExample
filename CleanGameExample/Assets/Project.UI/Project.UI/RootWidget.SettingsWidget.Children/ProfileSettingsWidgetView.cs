#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class ProfileSettingsWidgetView : UIViewBase {

        // View
        public ElementWrapper Root { get; }
        public TextFieldWrapper<string> Name { get; }

        // Constructor
        public ProfileSettingsWidgetView() {
            VisualElement = UIFactory.Common.ProfileSettingsWidget( out var root, out var name );
            Root = root.Wrap();
            Name = name.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
