﻿#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class WeaponSlot : Slot {

#if UNITY_EDITOR
        protected override void OnValidate() {
            base.OnValidate();
        }
#endif

    }
}
