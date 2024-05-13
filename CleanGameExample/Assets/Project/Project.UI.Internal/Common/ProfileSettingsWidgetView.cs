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
        public string Name => name.value;

        // Constructor
        public ProfileSettingsWidgetView(string name, Func<string?, bool> nameValidator) {
            VisualElement = VisualElementFactory_Common.ProfileSettingsWidgetView( out view, out this.name );
            this.name.value = name;
            this.name.SetValid( nameValidator( this.name.value ) );
            this.name.OnChange( evt => {
                this.name.SetValid( nameValidator( this.name.value ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
