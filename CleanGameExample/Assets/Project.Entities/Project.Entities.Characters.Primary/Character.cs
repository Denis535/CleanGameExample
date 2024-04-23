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
        public interface IContext {
            bool IsFirePressed();
            bool IsAimPressed();
            bool IsInteractPressed();
        }
        public record Arguments(IContext Context);

        // Args
        private Arguments Args { get; set; } = default!;
        // View
        private CharacterBody Body { get; set; } = default!;
        private CharacterView View { get; set; } = default!;

        // Awake
        public void Awake() {
            Args = Context.Get<Character, Arguments>();
            Body = gameObject.RequireComponent<CharacterBody>();
            View = gameObject.RequireComponent<CharacterView>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void FixedUpdate() {
            Body.UpdatePosition( Time.fixedDeltaTime );
        }
        public void Update() {
            Body.UpdateRotation( Time.deltaTime );
        }

    }
}
