namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IWeapon {
        GameObject gameObject { get; }
        Transform transform { get; }

        void Fire(IDamager damager);
    }
}
