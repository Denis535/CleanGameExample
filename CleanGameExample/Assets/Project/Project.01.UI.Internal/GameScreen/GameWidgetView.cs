#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;
    using UnityEngine.Framework.UI;

    public class GameWidgetView : WidgetView {

        public VisualElement Target { get; }
        private InputActions_UI Input { get; }
        public bool IsCursorVisible {
            get => UnityEngine.Cursor.lockState == CursorLockMode.None;
            set => UnityEngine.Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public GameWidgetView() : base( "game-widget-view" ) {
            focusable = true;
            Add(
                Target = VisualElementFactory.Label( "+" )
                    .Classes( "font-size-400pc", "color-light", "margin-0pc", "border-0pc", "position-absolute", "left-50pc", "top-50pc" )
                    .Style( i => i.translate = new Translate( new Length( -50, LengthUnit.Percent ), new Length( -50, LengthUnit.Percent ) ) )
            );
            RegisterCallback<AttachToPanelEvent>( evt => {
                Input!.Enable();
            } );
            UnregisterCallback<DetachFromPanelEvent>( evt => {
                Input!.Disable();
            } );
            Input = new InputActions_UI();
            Input.UI.Cancel.performed += ctx => {
                if (focusController.focusedElement == null) Focus();
            };
        }
        public override void Dispose() {
            Input.Dispose();
            base.Dispose();
        }

    }
}
