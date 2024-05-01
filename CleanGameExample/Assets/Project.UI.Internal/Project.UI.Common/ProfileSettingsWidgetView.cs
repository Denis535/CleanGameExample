﻿#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class ProfileSettingsWidgetView : UIViewBase {

        // Root
        public ElementWrapper Root { get; }
        public TextFieldWrapper<string> Name { get; }

        // Constructor
        public ProfileSettingsWidgetView() {
            VisualElement = CommonViewFactory.ProfileSettingsWidget( out var root, out var name );
            Root = root.Wrap();
            Name = name.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}