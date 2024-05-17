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
            if (!gameObject.name.EndsWith( GetType().Name )) gameObject.name += GetType().Name;
        }
#endif

    }
}
