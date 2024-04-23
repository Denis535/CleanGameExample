#nullable enable
namespace Project.Entities.Characters.Primary {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    [RequireComponent( typeof( CharacterBody ) )]
    [RequireComponent( typeof( CharacterView ) )]
    public class Character : EntityBase {
        public interface IInputActions : CharacterBody.IInputActions {
            bool IsFirePressed();
            bool IsAimPressed();
            bool IsInteractPressed();
        }

        // View
        private CharacterBody Body { get; set; } = default!;
        private CharacterView View { get; set; } = default!;
        // Actions
        public IInputActions? Actions { get; set; }

        // Awake
        public void Awake() {
            Body = gameObject.RequireComponent<CharacterBody>();
            View = gameObject.RequireComponent<CharacterView>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void FixedUpdate() {
            Body.UpdatePosition( Actions );
        }
        public void Update() {
            Body.UpdateRotation( Actions );
        }

    }
}
