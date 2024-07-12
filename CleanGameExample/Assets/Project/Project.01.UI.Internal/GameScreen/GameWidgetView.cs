#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;

    public class GameWidgetView : UIViewBase2 {

        private InputActions_UI Input { get; }
        protected override VisualElement VisualElement => Widget;
        public Widget Widget { get; }
        public VisualElement Target { get; }
        public bool IsCursorVisible {
            get => UnityEngine.Cursor.lockState == CursorLockMode.None;
            set => UnityEngine.Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public GameWidgetView() {
            Input = new InputActions_UI();
            Input.UI.Cancel.performed += ctx => {
                if (Widget!.focusController.focusedElement == null) {
                    Widget.Focus();
                }
            };
            Widget = VisualElementFactory.Widget( "game-widget" ).UserData( this ).Pipe( i => i.focusable = true ).Children(
                Target = VisualElementFactory.Label( "+" )
                    .Classes( "font-size-400pc", "color-light", "margin-0pc", "border-0pc", "position-absolute", "left-50pc", "top-50pc" )
                    .Style( i => i.translate = new Translate( new Length( -50, LengthUnit.Percent ), new Length( -50, LengthUnit.Percent ) ) )
            );
            Widget.RegisterCallback<AttachToPanelEvent>( evt => {
                Input.Enable();
            } );
            Widget.UnregisterCallback<DetachFromPanelEvent>( evt => {
                Input.Disable();
            } );
        }
        public override void Dispose() {
            Input.Dispose();
            base.Dispose();
        }

    }
}
