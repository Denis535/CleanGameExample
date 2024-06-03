#nullable enable
namespace UnityEngine.Framework.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayerBase2 : PlayerBase {

        // Constructor
        public PlayerBase2() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Update
        public abstract void FixedUpdate();
        public abstract void Update();
        public abstract void LateUpdate();

    }
}
