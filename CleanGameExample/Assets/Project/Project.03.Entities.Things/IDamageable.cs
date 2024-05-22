#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IDamageable {

        void OnDamage(float damage, Vector3 point, Vector3 direction);

    }
}
