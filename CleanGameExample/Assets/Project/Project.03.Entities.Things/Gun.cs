#nullable enable
namespace Project.Entities.Things {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.Entities;

    public partial class Gun {
        public static class Factory {

            private static readonly PrefabListHandle<Gun> Prefabs = new PrefabListHandle<Gun>( new[] {
                R.Project.Entities.Things.Value_Gun_Gray,
                R.Project.Entities.Things.Value_Gun_Red,
                R.Project.Entities.Things.Value_Gun_Green,
                R.Project.Entities.Things.Value_Gun_Blue,
            } );

            public static void Load() {
                Prefabs.Load().Wait();
            }
            public static void Unload() {
                Prefabs.Release();
            }

            public static Gun Create(Transform? parent) {
                var result = GameObject.Instantiate<Gun>( Prefabs.GetValues().GetRandom(), parent );
                return result;
            }
            public static Gun Create(Vector3 position, Quaternion rotation, Transform? parent) {
                var result = GameObject.Instantiate<Gun>( Prefabs.GetValues().GetRandom(), position, rotation, parent );
                return result;
            }

        }
    }
    public partial class Gun : MonoBehaviour, IWeapon {

        private GunBody Body { get; set; } = default!;
        private GunView View { get; set; } = default!;

        // Awake
        protected void Awake() {
            Body = new GunBody( gameObject );
            View = new GunView( gameObject );
        }
        protected void OnDestroy() {
            View.Dispose();
            Body.Dispose();
        }

        // Fire
        public void Fire(IDamager damager) {
            if (View.Fire( out var position, out var rotation )) {
                var bullet = Bullet.Factory.Create( position.Value, rotation.Value, null, this, damager, 5 );
                Physics.IgnoreCollision( gameObject.RequireComponentInChildren<Collider>(), bullet.gameObject.RequireComponentInChildren<Collider>() );
            }
        }

    }
    public class GunBody : BodyBase {

        public GunBody(GameObject gameObject) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class GunView : ViewBase {

        private Delay FireDelay { get; } = new Delay( 0.25f );
        private FirePoint FirePoint { get; }

        public GunView(GameObject gameObject) {
            FirePoint = gameObject.RequireComponentInChildren<FirePoint>();
        }
        public override void Dispose() {
            base.Dispose();
        }

        public bool Fire([NotNullWhen( true )] out Vector3? position, [NotNullWhen( true )] out Quaternion? rotation) {
            if (FireDelay.IsCompleted) {
                FireDelay.Start();
                position = FirePoint.transform.position;
                rotation = FirePoint.transform.rotation;
                return true;
            }
            position = null;
            rotation = null;
            return false;
        }

    }
}
