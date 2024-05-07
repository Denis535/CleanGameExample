#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class SpawnPoint : MonoBehaviour {

#if UNITY_EDITOR
        // OnValidate
        public void OnValidate() {
            gameObject.name = GetType().Name;
            gameObject.isStatic = true;
            transform.localPosition = Snapping.Snap( transform.localPosition, Vector3.one * 0.5f );
            transform.localEulerAngles = Snapping.Snap( transform.localEulerAngles, Vector3.one * 45f );
            transform.localEulerAngles = new Vector3( 0, transform.localEulerAngles.y, 0 );
            if (transform.parent == null) transform.parent = FindAnyObjectByType<WorldBase>()?.transform;
        }
#endif

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

    }
}
