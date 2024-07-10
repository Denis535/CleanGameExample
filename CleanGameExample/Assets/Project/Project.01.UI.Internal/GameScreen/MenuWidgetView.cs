#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MenuWidgetView : UIViewBase2 {

        protected override VisualElement VisualElement => Widget;
        private Widget Widget { get; }
        private Label Title { get; }
        private Button Resume { get; }
        private Button Settings { get; }
        private Button Back { get; }

        public event EventCallback<ClickEvent> OnResumeEvent {
            add => Resume.RegisterCallback( value );
            remove => Resume.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnSettingsEvent {
            add => Settings.RegisterCallback( value );
            remove => Settings.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBackEvent {
            add => Back.RegisterCallback( value );
            remove => Back.UnregisterCallback( value );
        }

        public MenuWidgetView() {
            Widget = VisualElementFactory.LeftWidget( "menu-widget" ).UserData( this ).Children(
                VisualElementFactory.Card().Children(
                    VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Menu" )
                    ),
                    VisualElementFactory.Content().Children(
                        Resume = VisualElementFactory.Resume( "Resume" ),
                        Settings = VisualElementFactory.Select( "Settings" ),
                        Back = VisualElementFactory.Back( "Back To Menu" )
                    )
                )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
