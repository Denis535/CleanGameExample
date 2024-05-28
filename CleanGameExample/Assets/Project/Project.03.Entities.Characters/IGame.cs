#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IGame {
        void OnDamage(Character damager, Character character, bool isKilled);
    }
}
