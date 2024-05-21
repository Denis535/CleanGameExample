#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Slot : MonoBehaviour {

#if UNITY_EDITOR
        // OnValidate
        public void OnValidate() {
            Debug.Assert( gameObject.name.EndsWith( "Slot" ), $"GameObject {gameObject} must have name ending with 'Slot'", gameObject );
        }
#endif

    }
}
