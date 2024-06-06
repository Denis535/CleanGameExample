#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class ProfileSettingsWidgetView : UIViewBase {

        private readonly VisualElement view;
        private readonly TextField name;

        // Props
        public override int Priority => 0;
        public override bool IsAlwaysVisible => false;
        public override bool IsModal => false;
        public string Name {
            get => name.value;
            init => name.value = value;
        }
        public Func<string?, bool> NameValidator { get; init; } = default!;

        // Constructor
        public ProfileSettingsWidgetView() {
            VisualElement = VisualElementFactory_Common.ProfileSettingsWidgetView( out view, out name );
            view.OnAttachToPanel( evt => {
                name.SetValid( NameValidator( name.value ) );
            } );
            name.OnChange( evt => {
                name.SetValid( NameValidator( name.value ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
