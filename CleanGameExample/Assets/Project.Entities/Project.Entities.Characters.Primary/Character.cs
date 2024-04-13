#nullable enable
namespace Project.Entities.Characters.Primary {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    [RequireComponent( typeof( CharacterView ) )]
    [RequireComponent( typeof( CharacterBody ) )]
    public class Character : EntityBase {

        // View
        private CharacterView View { get; set; } = default!;
        private CharacterBody Body { get; set; } = default!;
        // Input
        public Vector3 MoveInput { get => Body.MoveInput; set => Body.MoveInput = value; }
        public Vector3 LookInput { get => Body.LookInput; set => Body.LookInput = value; }
        public bool FireInput { get; set; }
        public bool AimInput { get; set; }
        public bool JumpInput { get => Body.JumpInput; set => Body.JumpInput = value; }
        public bool CrouchInput { get => Body.CrouchInput; set => Body.CrouchInput = value; }
        public bool InteractInput { get; set; }

        // Awake
        public void Awake() {
            View = gameObject.RequireComponent<CharacterView>();
            Body = gameObject.RequireComponent<CharacterBody>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
        }

    }
}
