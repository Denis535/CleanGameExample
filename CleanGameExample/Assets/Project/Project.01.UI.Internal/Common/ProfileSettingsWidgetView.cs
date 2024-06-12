﻿#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class ProfileSettingsWidgetView : UIViewBase {

        private readonly VisualElement widget;
        private readonly TextField name;

        // Layer
        public override int Layer => 0;
        // Props
        public string Name {
            get => name.value;
            init => name.value = value;
        }
        public Func<string?, bool> NameValidator { get; init; } = default!;

        // Constructor
        public ProfileSettingsWidgetView() {
            VisualElement = VisualElementFactory_Common.ProfileSettings( out widget, out name );
            name.OnValidate( evt => {
                name.SetValid( NameValidator( name.value ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
