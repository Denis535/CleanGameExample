#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IThing {

        // GameObject
        GameObject gameObject { get; }
        // Transform
        Transform transform { get; }

    }
}
