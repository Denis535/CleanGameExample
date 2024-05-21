#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class EntityFactory3 {

        private static readonly string[] Weapons = new[] {
            R.Project.Entities.Characters.Gun_Gray_Value,
            R.Project.Entities.Characters.Gun_Red_Value,
            R.Project.Entities.Characters.Gun_Green_Value,
            R.Project.Entities.Characters.Gun_Blue_Value,
        };

        // Initialize
        public static void Initialize() {
        }
        public static void Deinitialize() {
        }

        // Gun
        public static Gun Gun(Vector3 position, Quaternion rotation) {
            var weapon = Weapons[ UnityEngine.Random.Range( 0, Weapons.Length ) ];
            return Addressables2.Instantiate<Gun>( weapon, position, rotation );
        }
        public static void Gun(Slot slot) {
            var weapon = Weapons[ UnityEngine.Random.Range( 0, Weapons.Length ) ];
            Addressables2.Instantiate<Gun>( weapon, slot.transform );
        }

        // Bullet
        public static Bullet Bullet(Vector3 position, Quaternion rotation, float force) {
            using (Context.Begin( new Bullet.Args( force ) )) {
                return Addressables2.Instantiate<Bullet>( R.Project.Entities.Characters.Bullet_Value, position, rotation );
            }
        }

    }
}
