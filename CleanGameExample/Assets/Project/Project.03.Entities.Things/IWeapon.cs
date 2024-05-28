#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IWeapon {
        void Fire(IDamager damager, BulletHitCallback? callback);
    }
}
