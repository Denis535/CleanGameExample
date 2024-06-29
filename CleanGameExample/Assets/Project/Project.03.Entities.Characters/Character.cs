#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    [RequireComponent( typeof( Rigidbody ) )]
    [RequireComponent( typeof( MoveableBody ) )]
    public abstract class Character : EntityBase<CharacterBody, CharacterView>, IDamager, IDamageable {

        // Game
        public IGame Game { get; set; } = default!;
        // IsAlive
        public bool IsAlive { get; private set; } = true;
        // Weapon
        public IWeapon? Weapon {
            get => View.Weapon?.RequireComponent<IWeapon>();
            protected set => View.Weapon = ((MonoBehaviour?) value)?.gameObject;
        }
        // OnDamageEvent
        public event Action<DamageInfo>? OnDamageEvent;

        // Awake
        protected override void Awake() {
            Body = new CharacterBody( gameObject );
            View = new CharacterView( gameObject );
        }
        protected override void OnDestroy() {
            View.Dispose();
            Body.Dispose();
        }

        // Start
        protected virtual void Start() {
        }
        protected virtual void FixedUpdate() {
        }
        protected virtual void Update() {
        }

        // OnDamage
        void IDamageable.OnDamage(DamageInfo info) {
            OnDamage( info );
        }
        protected virtual void OnDamage(DamageInfo info) {
            if (IsAlive) {
                IsAlive = false;
                Body.IsRagdoll = true;
                Body.AddImpulse( info.Direction * 5, info.Point );
                Weapon = null;
                OnDamageEvent?.Invoke( info );
            }
        }

    }
    public class CharacterBody : EntityBodyBase {

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

        public CharacterBody(GameObject gameObject) : base( gameObject ) {
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
    public class CharacterView : EntityViewBase {

        private Transform Body { get; }
        private Transform Head { get; }
        private Slot WeaponSlot { get; }
        public GameObject? Weapon {
            get => WeaponSlot.transform.childCount > 0 ? WeaponSlot.transform.GetChild( 0 ).gameObject : null;
            set {
                var prevWeapon = Weapon;
                if (prevWeapon != null) {
                    prevWeapon.SetLayerRecursively( Layers.Entity );
                    prevWeapon.RequireComponent<Rigidbody>().isKinematic = false;
                    prevWeapon.transform.SetParent( null, true );
                }
                if (value != null) {
                    value.SetLayerRecursively( Layers.CharacterEntityInternal );
                    value.RequireComponent<Rigidbody>().isKinematic = true;
                    value.transform.SetParent( WeaponSlot.transform, true );
                    value.transform.localPosition = Vector3.zero;
                    value.transform.localRotation = Quaternion.identity;
                }
            }
        }

        public CharacterView(GameObject gameObject) : base( gameObject ) {
            Body = gameObject.transform.Require( "Body" );
            Head = gameObject.transform.Require( "Head" );
            WeaponSlot = gameObject.RequireComponentInChildren<Slot>();
        }
        public override void Dispose() {
            base.Dispose();
        }

        public bool HeadAt(Vector3? target) {
            return HeadAt( Head, target );
        }

        public bool WeaponAt(Vector3? target) {
            return WeaponAt( WeaponSlot.transform, target );
        }

        // Helpers
        private static bool HeadAt(Transform transform, Vector3? target) {
            var rotation = transform.localRotation;
            if (target != null) {
                transform.localRotation = Quaternion.identity;
                var direction = transform.InverseTransformPoint( target.Value );
                var rotation2 = GetHeadRotation( direction );
                if (rotation2 != null) {
                    transform.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                    return true;
                } else {
                    transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
            } else {
                transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                return false;
            }
        }
        private static bool WeaponAt(Transform transform, Vector3? target) {
            var rotation = transform.localRotation;
            if (target != null) {
                transform.localRotation = Quaternion.identity;
                var direction = transform.InverseTransformPoint( target.Value );
                var rotation2 = GetWeaponRotation( direction );
                if (rotation2 != null) {
                    transform.localRotation = Quaternion.RotateTowards( rotation, rotation2.Value, 2 * 360 * Time.deltaTime );
                    return true;
                } else {
                    transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
            } else {
                transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                return false;
            }
        }
        // Helpers
        private static Quaternion? GetHeadRotation(Vector3 direction) {
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
        private static Quaternion? GetWeaponRotation(Vector3 direction) {
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
