#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class Socket : MonoBehaviour {

#if UNITY_EDITOR
        protected virtual void OnValidate() {
            gameObject.name = GetType().Name;
        }
#endif

    }
}
