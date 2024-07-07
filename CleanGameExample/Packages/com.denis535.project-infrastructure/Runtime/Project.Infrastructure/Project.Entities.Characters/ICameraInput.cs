#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface ICameraInput {
        Vector2 GetLookDelta();
        float GetZoomDelta();
    }
}
