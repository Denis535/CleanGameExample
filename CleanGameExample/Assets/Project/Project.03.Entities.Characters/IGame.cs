#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;

    public interface IGame {

        void OnDamage(Character character, Character damager, Weapon weapon);
        void OnKill(Character character, Character killer, Weapon weapon);

    }
}
