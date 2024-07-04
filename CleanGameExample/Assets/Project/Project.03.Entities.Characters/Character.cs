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
        protected CharacterBody Body { get; private set; } = default!;
        protected CharacterHead Head { get; private set; } = default!;
        protected CharacterWeaponSlot WeaponSlot { get; private set; } = default!;

        protected virtual void Awake() {
            gameObject.SetLayerRecursively( Layers.Entity_Approximate, Layers.Entity_Exact );
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
        private Transform Transform => GameObject.transform;
        private MoveableBody MoveableBody { get; }
        private Rigidbody Rigidbody { get; }
        public bool IsRagdoll {
            get => !MoveableBody.enabled;
            set {
                if (value) {
                    MoveableBody.enabled = false;
                    Rigidbody.isKinematic = false;
                } else {
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

        private Transform Transform { get; }

        public CharacterHead(GameObject gameObject) {
            Transform = gameObject.transform.Require( "Head" );
        }
        public override void Dispose() {
            base.Dispose();
        }

        public bool LookAt(Vector3? target) {
            var rotation = Transform.localRotation;
            if (target != null) {
                Transform.localRotation = Quaternion.identity;
                var direction = Transform.InverseTransformPoint( target.Value );
                var rotation2 = GetRotation( direction );
                if (rotation2 != null) {
                    Transform.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                    return true;
                } else {
                    Transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
            } else {
                Transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
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

        private Slot Slot { get; }
        public Weapon? Weapon {
            get => Slot.transform.childCount > 0 ? Slot.transform.GetChild( 0 ).gameObject.RequireComponent<Weapon>() : null;
            set {
                var prevWeapon = Weapon;
                if (prevWeapon != null) {
                    prevWeapon.gameObject.SetLayerRecursively( Layers.Entity );
                    prevWeapon.transform.SetParent( null, true );
                    prevWeapon.IsRigidbody = true;
                }
                if (value != null) {
                    value.transform.SetParent( Slot.transform, true );
                    value.transform.localPosition = Vector3.zero;
                    value.transform.localRotation = Quaternion.identity;
                    value.IsRigidbody = false;
                }
            }
        }

        public CharacterWeaponSlot(GameObject gameObject) {
            Slot = gameObject.RequireComponentInChildren<Slot>();
        }
        public override void Dispose() {
            base.Dispose();
        }

        public bool LookAt(Vector3? target) {
            var rotation = Slot.transform.localRotation;
            if (target != null) {
                Slot.transform.localRotation = Quaternion.identity;
                var direction = Slot.transform.InverseTransformPoint( target.Value );
                var rotation2 = GetRotation( direction );
                if (rotation2 != null) {
                    Slot.transform.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                    return true;
                } else {
                    Slot.transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
            } else {
                Slot.transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
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
