#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FirePoint : Point {

#if UNITY_EDITOR
        protected override void OnValidate() {
            base.OnValidate();
        }
#endif

#if UNITY_EDITOR
        protected override void OnDrawGizmos() {
            base.OnDrawGizmos();
        }
#endif

    }
}
