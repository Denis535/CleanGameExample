#nullable enable
namespace Project.Entities.Characters {
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
        public Vector3 MoveDeltaInput { get => Body.MoveDeltaInput; set => Body.MoveDeltaInput = value; }
        public Vector3 LookVectorInput { get => Body.LookVectorInput; set => Body.LookVectorInput = value; }
        public bool FireInput { get; set; }

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
