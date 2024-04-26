#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterView : EntityViewBase {

        // Components
        private Transform Body { get; set; } = default!;
        private Transform Head { get; set; } = default!;

        // Awake
        public void Awake() {
            Body = transform.Require( "Body" );
            Head = transform.Require( "Head" );
        }
        public void OnDestroy() {
        }

    }
}
