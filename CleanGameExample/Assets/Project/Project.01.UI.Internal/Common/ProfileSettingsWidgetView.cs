#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class ProfileSettingsWidgetView : UIViewBase2 {

        protected override VisualElement VisualElement => Widget;
        private Widget Widget { get; }
        private TextField Name_ { get; }

        public string Name {
            get => Name_.value;
            init => Name_.value = value;
        }

        public ProfileSettingsWidgetView(Func<string?, bool> nameValidator) {
            Widget = VisualElementFactory.Widget( "profile-settings-widget" ).Classes( "grow-1" ).UserData( this ).Children(
                VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).Children(
                    Name_ = VisualElementFactory.TextField( "Name", 16 ).Classes( "label-width-25pc" )
                )
            );
            Name_.OnValidate( evt => {
                Name_.SetValid( nameValidator( Name_.value ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
