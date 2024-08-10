#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class ProfileSettingsWidgetView : UIViewBase {

        public Widget Widget { get; }
        public TextField Name { get; }

        public ProfileSettingsWidgetView(Func<string?, bool> nameValidator) {
            Widget = VisualElementFactory.Widget( "profile-settings-widget" ).Classes( "grow-1" ).Children(
                VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).Children(
                    Name = VisualElementFactory.TextField( "Name", 16 ).Classes( "label-width-25pc" )
                )
            );
            Name.OnValidate( evt => {
                Name.SetValid( nameValidator( Name.value ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
