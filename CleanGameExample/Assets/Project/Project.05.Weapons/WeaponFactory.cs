#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class WeaponFactory {

        // Gun
        public static Gun Gun(Vector3 position, Quaternion rotation) {
            var keys = new[] {
                R.Project.Entities.Weapons.Gun_Gray_Value,
                R.Project.Entities.Weapons.Gun_Red_Value,
                R.Project.Entities.Weapons.Gun_Green_Value,
                R.Project.Entities.Weapons.Gun_Blue_Value,
            };
            var key = keys[ UnityEngine.Random.Range( 0, keys.Length ) ];
            return EntityFactory.Instantiate<Gun>( key, position, rotation );
        }
        public static void Gun(Slot slot) {
            var keys = new[] {
                R.Project.Entities.Weapons.Gun_Gray_Value,
                R.Project.Entities.Weapons.Gun_Red_Value,
                R.Project.Entities.Weapons.Gun_Green_Value,
                R.Project.Entities.Weapons.Gun_Blue_Value,
            };
            var key = keys[ UnityEngine.Random.Range( 0, keys.Length ) ];
            EntityFactory.Instantiate<Gun>( key, slot.transform );
        }

        // Bullet
        public static Bullet Bullet(Vector3 position, Quaternion rotation, float force) {
            using (Context.Begin( new Bullet.Args( force ) )) {
                return EntityFactory.Instantiate<Bullet>( R.Project.Entities.Weapons.Bullet_Value, position, rotation );
            }
        }

    }
}
