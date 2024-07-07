#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class ProfileSettingsWidgetView : UIViewBase2 {

        private readonly Widget widget;
        private readonly TextField name;

        public string Name {
            get => name.value;
            init => name.value = value;
        }

        public ProfileSettingsWidgetView(Func<string?, bool> nameValidator) {
            VisualElement = VisualElementFactory_Common.ProfileSettings( out widget, out name );
            name.OnValidate( evt => {
                name.SetValid( nameValidator( name.value ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
