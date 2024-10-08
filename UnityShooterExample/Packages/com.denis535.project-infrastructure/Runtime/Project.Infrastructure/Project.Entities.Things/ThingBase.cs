#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [DefaultExecutionOrder( 100 )]
    public abstract class ThingBase : MonoBehaviour {

        protected Rigidbody Rigidbody { get; private set; } = default!;
        public bool IsRigidbody {
            get => !Rigidbody.isKinematic;
            set {
                Rigidbody.isKinematic = !value;
            }
        }

        protected virtual void Awake() {
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        protected virtual void OnDestroy() {
        }

    }
}
