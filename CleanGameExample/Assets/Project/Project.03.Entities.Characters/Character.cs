#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.Entities.Things;
    using UnityEngine;

    [RequireComponent( typeof( Rigidbody ) )]
    [RequireComponent( typeof( MoveableBody ) )]
    public abstract partial class Character : MonoBehaviour, ICharacter, IDamageable {

        protected MoveableBody MoveableBody { get; private set; } = default!;
        protected Rigidbody Rigidbody { get; private set; } = default!;
        public bool IsAlive { get; private set; } = true;
        public event Action<DamageInfo>? OnDamageEvent;
        public bool IsRagdoll {
            get => !MoveableBody.enabled;
            private set {
                if (value) {
                    MoveableBody.enabled = false;
                    Rigidbody.isKinematic = false;
                } else {
                    MoveableBody.enabled = true;
                    Rigidbody.isKinematic = true;
                }
            }
        }
        protected Head_ Head { get; private set; } = default!;
        protected WeaponSlot_ WeaponSlot { get; private set; } = default!;

        protected virtual void Awake() {
            MoveableBody = gameObject.RequireComponent<MoveableBody>();
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
            Head = new Head_( gameObject.transform.Require( "Head" ).gameObject );
            WeaponSlot = new WeaponSlot_( gameObject.RequireComponentInChildren<WeaponSlot>() );
        }
        protected virtual void OnDestroy() {
            WeaponSlot.Dispose();
            Head.Dispose();
        }

        protected virtual void Start() {
        }
        protected virtual void FixedUpdate() {
        }
        protected virtual void Update() {
        }

        void IDamageable.OnDamage(DamageInfo info) {
            if (IsAlive) {
                gameObject.SetLayerRecursively( Layers.Entity );
                IsAlive = false;
                IsRagdoll = true;
                WeaponSlot.Weapon = null;
                if (info is BulletDamageInfo bulletDamageInfo) {
                    Rigidbody.AddForceAtPosition( bulletDamageInfo.Direction * 5, bulletDamageInfo.Point, ForceMode.Impulse );
                }
                OnDamageEvent?.Invoke( info );
            }
        }

    }
    public abstract partial class Character {
        protected class Head_ : Disposable {

            private GameObject GameObject { get; }
            private Transform Transform => GameObject.transform;

            public Head_(GameObject gameObject) {
                GameObject = gameObject;
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

            // Helpers
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
        protected class WeaponSlot_ : Disposable {

            private WeaponSlot Slot { get; }
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
                        value.gameObject.SetLayerRecursively( Layers.Entity_Exact );
                        value.transform.SetParent( Slot.transform, true );
                        value.transform.localPosition = Vector3.zero;
                        value.transform.localRotation = Quaternion.identity;
                        value.IsRigidbody = false;
                    }
                }
            }

            public WeaponSlot_(WeaponSlot slot) {
                Slot = slot;
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

            // Helpers
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
    public abstract class PlayableCharacter : Character {

        public IPlayableCharacterInput? Input { get; set; }

        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

    }
    public abstract class NonPlayableCharacter : Character {

        protected override void Awake() {
            base.Awake();
        }
        protected override void OnDestroy() {
            base.OnDestroy();
        }

    }
    public interface IPlayableCharacterInput {
        Vector3 GetMoveVector();
        Vector3? GetBodyTarget();
        Vector3? GetHeadTarget();
        Vector3? GetWeaponTarget();
        bool IsJumpPressed();
        bool IsCrouchPressed();
        bool IsAcceleratePressed();
        bool IsFirePressed();
        bool IsAimPressed();
        bool IsInteractPressed(out MonoBehaviour? interactable);
    }
}
