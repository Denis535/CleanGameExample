#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IThing {
        GameObject gameObject { get; }
        Transform transform { get; }
    }
    public interface IWeapon : IThing {
        void Fire(ICharacter character);
    }
}
