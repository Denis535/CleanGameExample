#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;

    [RequireComponent( typeof( Rigidbody ) )]
    [RequireComponent( typeof( MoveableBody ) )]
    public abstract class Character : MonoBehaviour, ICharacter, IDamageable {

        public bool IsAlive { get; private set; } = true;
        public event Action<DamageInfo>? OnDamageEvent;
        protected CharacterBody Body { get; set; } = default!;
        protected CharacterHead Head { get; set; } = default!;
        protected CharacterWeaponSlot WeaponSlot { get; set; } = default!;

        protected virtual void Awake() {
            Body = new CharacterBody( gameObject );
            Head = new CharacterHead( gameObject );
            WeaponSlot = new CharacterWeaponSlot( gameObject );
        }
        protected virtual void OnDestroy() {
            WeaponSlot.Dispose();
            Head.Dispose();
            Body.Dispose();
        }

        protected virtual void Start() {
        }
        protected virtual void FixedUpdate() {
        }
        protected virtual void Update() {
        }

        void IDamageable.OnDamage(DamageInfo info) {
            if (IsAlive) {
                if (info is BulletDamageInfo bulletDamageInfo) {
                    IsAlive = false;
                    WeaponSlot.Weapon = null;
                    Body.IsRagdoll = true;
                    Body.AddImpulse( bulletDamageInfo.Direction * 5, bulletDamageInfo.Point );
                    OnDamageEvent?.Invoke( bulletDamageInfo );
                }
            }
        }

    }
    public class CharacterBody : Disposable {

        private GameObject GameObject { get; }
        private MoveableBody MoveableBody { get; }
        private Rigidbody Rigidbody { get; }
        public bool IsRagdoll {
            get => !MoveableBody.enabled;
            set {
                if (value) {
                    GameObject.SetLayerRecursively( Layers.Entity );
                    MoveableBody.enabled = false;
                    Rigidbody.isKinematic = false;
                } else {
                    GameObject.SetLayerRecursively( Layers.CharacterEntity, Layers.CharacterEntityInternal );
                    MoveableBody.enabled = true;
                    Rigidbody.isKinematic = true;
                }
            }
        }

        public CharacterBody(GameObject gameObject) {
            GameObject = gameObject;
            MoveableBody = gameObject.RequireComponent<MoveableBody>();
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        public override void Dispose() {
            base.Dispose();
        }

        public void Move(Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            MoveableBody.Move( moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
        }
        public void LookAt(Vector3? target) {
            MoveableBody.LookAt( target );
        }

        public void AddImpulse(Vector3 force, Vector3 position) {
            Rigidbody.AddForceAtPosition( force, position, ForceMode.Impulse );
        }

    }
    public class CharacterHead : Disposable {

        private Transform Head { get; }

        public CharacterHead(GameObject gameObject) {
            Head = gameObject.transform.Require( "Head" );
        }
        public override void Dispose() {
            base.Dispose();
        }

        public bool LookAt(Vector3? target) {
            var rotation = Head.localRotation;
            if (target != null) {
                Head.localRotation = Quaternion.identity;
                var direction = Head.InverseTransformPoint( target.Value );
                var rotation2 = GetRotation( direction );
                if (rotation2 != null) {
                    Head.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                    return true;
                } else {
                    Head.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
            } else {
                Head.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                return false;
            }
        }

        private static Quaternion? GetRotation(Vector3 direction) {
            var rotation = Quaternion.LookRotation( direction );
            var angles = rotation.eulerAngles;
            if (angles.x > 180) angles.x -= 360;
            if (angles.y > 180) angles.y -= 360;
            if (angles.y >= -80 && angles.y <= 80) {
                angles.x = Mathf.Clamp( angles.x, -80, 80 );
                angles.y = Mathf.Clamp( angles.y, -80, 80 );
                return Quaternion.Euler( angles );
            }
            return null;
        }

    }
    public class CharacterWeaponSlot : Disposable {

        private Slot WeaponSlot { get; }
        public IWeapon? Weapon {
            get => WeaponSlot.transform.childCount > 0 ? WeaponSlot.transform.GetChild( 0 ).gameObject.RequireComponent<IWeapon>() : null;
            set {
                var prevWeapon = Weapon;
                if (prevWeapon != null) {
                    prevWeapon.gameObject.SetLayerRecursively( Layers.Entity );
                    prevWeapon.gameObject.RequireComponent<Rigidbody>().isKinematic = false;
                    prevWeapon.transform.SetParent( null, true );
                }
                if (value != null) {
                    value.gameObject.SetLayerRecursively( Layers.CharacterEntityInternal );
                    value.gameObject.RequireComponent<Rigidbody>().isKinematic = true;
                    value.transform.SetParent( WeaponSlot.transform, true );
                    value.transform.localPosition = Vector3.zero;
                    value.transform.localRotation = Quaternion.identity;
                }
            }
        }

        public CharacterWeaponSlot(GameObject gameObject) {
            WeaponSlot = gameObject.RequireComponentInChildren<Slot>();
        }
        public override void Dispose() {
            base.Dispose();
        }

        public bool LookAt(Vector3? target) {
            var rotation = WeaponSlot.transform.localRotation;
            if (target != null) {
                WeaponSlot.transform.localRotation = Quaternion.identity;
                var direction = WeaponSlot.transform.InverseTransformPoint( target.Value );
                var rotation2 = GetRotation( direction );
                if (rotation2 != null) {
                    WeaponSlot.transform.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                    return true;
                } else {
                    WeaponSlot.transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
            } else {
                WeaponSlot.transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                return false;
            }
        }

        private static Quaternion? GetRotation(Vector3 direction) {
            var rotation = Quaternion.LookRotation( direction );
            var angles = rotation.eulerAngles;
            if (angles.x > 180) angles.x -= 360;
            if (angles.y > 180) angles.y -= 360;
            if (angles.y >= -80 && angles.y <= 80) {
                angles.y = Mathf.Clamp( angles.y, -80, 80 );
                return Quaternion.Euler( angles );
            }
            return null;
        }

    }
}
