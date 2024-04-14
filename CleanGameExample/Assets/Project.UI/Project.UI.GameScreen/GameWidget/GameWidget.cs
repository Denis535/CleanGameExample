﻿#nullable enable
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
                Application.Game!.IsPlaying = false;
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
                Application.Game!.IsPlaying = true;
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
            if (Game.IsPlaying) {
                {
                    Camera.Target = Character?.transform;
                    Camera.RotationDeltaInput = Actions.Game.Look.ReadValue<Vector2>();
                    Camera.DistanceDeltaInput = Actions.Game.ScrollWheel.ReadValue<Vector2>().y;
                }
                if (Character != null) {
                    if (Actions.Game.Move.IsPressed()) {
                        Character.Target = Actions.Game.Move.ReadValue<Vector2>().Convert( i => GetMoveDirection( i, Camera.transform ) ) * 1024 + Vector3.up * 1.8f;
                    }
                    if (Actions.Game.Fire.IsPressed() || Actions.Game.Aim.IsPressed() || Actions.Game.Interact.IsPressed()) {
                        Character.Target = Camera.HitPoint ?? Character.transform.TransformPoint( Vector3.forward * 1024 + Vector3.up * 1.8f );
                    }
                    Character.FireInput = Actions.Game.Fire.IsPressed();
                    Character.AimInput = Actions.Game.Aim.IsPressed();
                    Character.InteractInput = Actions.Game.Interact.WasPressedThisFrame();
                    Character.MoveDirectionInput = Actions.Game.Move.ReadValue<Vector2>().Convert( i => GetMoveDirection( i, Camera.transform ) );
                    Character.JumpInput = Actions.Game.Jump.IsPressed();
                    Character.CrouchInput = Actions.Game.Crouch.IsPressed();
                    Character.AccelerationInput = Actions.Game.Acceleration.IsPressed();
                }
            } else {
                {
                    Camera.Target = Character?.transform;
                    Camera.RotationDeltaInput = default;
                    Camera.DistanceDeltaInput = default;
                }
                if (Character != null) {
                    Character.Target = default;
                    Character.FireInput = false;
                    Character.AimInput = false;
                    Character.InteractInput = false;
                    Character.MoveDirectionInput = default;
                    Character.JumpInput = false;
                    Character.CrouchInput = false;
                    Character.AccelerationInput = false;
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
        // Helpers
        private static Vector3 GetMoveDirection(Vector2 direction, Transform camera) {
            var result = new Vector3( direction.x, 0, direction.y );
            result = camera.TransformDirection( result );
            result = new Vector3( result.x, 0, result.z );
            return result.normalized * direction.magnitude;
        }

    }
}
