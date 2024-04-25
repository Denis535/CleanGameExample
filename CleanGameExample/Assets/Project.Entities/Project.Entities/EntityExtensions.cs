#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class EntityExtensions {

        public static bool IsInteractable(this GameObject gameObject) {
            return gameObject.name.Contains( "Gun" );
        }

    }
}
