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

        // View
        private CharacterBody Body { get; set; } = default!;
        private CharacterView View { get; set; } = default!;
        // Input
        public bool FireInput {
            get => View.FireInput;
            set => View.FireInput = value;
        }
        public bool AimInput {
            get => View.AimInput;
            set => View.AimInput = value;
        }
        public bool InteractInput {
            get => View.InteractInput;
            set => View.InteractInput = value;
        }
        public Vector3 LookDirectionInput {
            get => View.LookDirectionInput;
            set => View.LookDirectionInput = value;
        }
        public Vector3 MoveDirectionInput {
            get => View.MoveDirectionInput;
            set { Body.MoveDirectionInput = value; View.MoveDirectionInput = value; }
        }
        public bool JumpInput {
            get => View.JumpInput;
            set { Body.JumpInput = value; View.JumpInput = value; }
        }
        public bool CrouchInput {
            get => View.CrouchInput;
            set { Body.CrouchInput = value; View.CrouchInput = value; }
        }
        public bool AccelerationInput {
            get => View.AccelerationInput;
            set { Body.AccelerationInput = value; View.AccelerationInput = value; }
        }

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
        public void Update() {
        }

    }
}
