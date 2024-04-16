#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.App;
    using Project.Entities;
    using Project.Entities.Characters.Primary;
    using UnityEngine;
    using UnityEngine.Framework;
    using UnityEngine.Framework.UI;
    using UnityEngine.InputSystem;

    public class GameWidget : UIWidgetBase<GameWidgetView> {

        // Deps
        private Application2 Application { get; }
        private Game Game => Application.Game!;
        private Camera2 Camera => Application.Game!.Camera;
        private Character? Character => Application.Game?.Player.Character;
        // Actions
        private InputActions Actions { get; }
        private InputAction Fire => Actions.Game.Fire;
        private InputAction Aim => Actions.Game.Aim;
        private InputAction Interact => Actions.Game.Interact;
        private InputAction Look => Actions.Game.Look;
        private InputAction Zoom => Actions.Game.Zoom;
        private InputAction Move => Actions.Game.Move;
        private InputAction Jump => Actions.Game.Jump;
        private InputAction Crouch => Actions.Game.Crouch;
        private InputAction Accelerate => Actions.Game.Accelerate;

        // Constructor
        public GameWidget() {
            Application = this.GetDependencyContainer().RequireDependency<Application2>( null );
            View = CreateView( this );
            Actions = new InputActions();
        }
        public override void Dispose() {
            Actions.Dispose();
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
            Actions.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
        public override void OnDetach(object? argument) {
            Actions.Disable();
            Cursor.lockState = CursorLockMode.None;
        }

        // OnDescendantWidgetAttach
        public override void OnBeforeDescendantAttach(UIWidgetBase descendant) {
            base.OnBeforeDescendantAttach( descendant );
            if (descendant is GameMenuWidget) {
                Game.IsPlaying = false;
                Actions.Disable();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        public override void OnAfterDescendantAttach(UIWidgetBase descendant) {
            base.OnAfterDescendantAttach( descendant );
        }
        public override void OnBeforeDescendantDetach(UIWidgetBase descendant) {
            base.OnBeforeDescendantDetach( descendant );
        }
        public override void OnAfterDescendantDetach(UIWidgetBase descendant) {
            if (IsAttached && descendant is GameMenuWidget) {
                Game.IsPlaying = true;
                Actions.Enable();
                Cursor.lockState = CursorLockMode.Locked;
            }
            base.OnAfterDescendantDetach( descendant );
        }

        // Update
        public void FixedUpdate() {
        }
        public void Update() {
            if (Actions.UI.Cancel.WasPressedThisFrame()) {
                this.AttachChild( new GameMenuWidget() );
            }
            {
                Camera.Rotate( Look.ReadValue<Vector2>() );
                Camera.Zoom( Zoom.ReadValue<Vector2>().y );
            }
            if (Character != null) {
                {
                    Character.Fire( Fire.IsPressed(), Fire.WasPressedThisFrame() );
                    Character.Aim( Aim.IsPressed(), Aim.WasPressedThisFrame() );
                    Character.Interact( Interact.IsPressed(), Interact.WasPressedThisFrame() );
                }
                {
                    Character.Move( Move.ReadValue<Vector2>(), Move.IsPressed(), Move.WasPressedThisFrame() );
                    Character.LookAt( null, false );
                    if (Fire.IsPressed() || Aim.IsPressed() || Interact.IsPressed()) {
                        Character.LookAt( Camera.HitPoint, Fire.WasPerformedThisFrame() || Aim.WasPerformedThisFrame() || Interact.WasPerformedThisFrame() );
                    }
                    if (Character.MoveVector != null && Character.MoveVector.Value != default) {
                        if (Character.LookTarget == null) {
                            Character.LookAt( Character.transform.position + Character.MoveVector.Value * 128f, Move.WasPressedThisFrame() );
                        }
                    }
                    Character.Jump( Jump.IsPressed(), Jump.WasPressedThisFrame() );
                    Character.Crouch( Crouch.IsPressed(), Crouch.WasPressedThisFrame() );
                    Character.Accelerate( Accelerate.IsPressed(), Accelerate.WasPressedThisFrame() );
                }
            }
        }
        public void LateUpdate() {
        }

        // Helpers
        private static GameWidgetView CreateView(GameWidget widget) {
            var view = new GameWidgetView();
            return view;
        }

    }
}
