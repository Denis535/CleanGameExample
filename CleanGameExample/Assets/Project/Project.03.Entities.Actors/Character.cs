#nullable enable
namespace Project.Entities.Actors {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent( typeof( Rigidbody ) )]
    [RequireComponent( typeof( MoveableBody ) )]
    public abstract partial class Character : Actor {

        private Facade_ Facade { get; set; } = default!;
        public Weapon? Weapon { get => Facade.Weapon; protected set => Facade.Weapon = value; }

        protected override void Awake() {
            base.Awake();
            Facade = new Facade_( gameObject );
        }
        protected override void OnDestroy() {
            Facade.Dispose();
            base.OnDestroy();
        }

        protected virtual void Start() {
        }
        protected virtual void FixedUpdate() {
        }
        protected virtual void Update() {
        }
        protected virtual void LateUpdate() {
        }

        protected override void OnDamage(DamageInfo info) {
            base.OnDamage( info );
        }
        protected override void OnDeath(DamageInfo info) {
            base.OnDeath( info );
            Facade.Weapon = null;
            if (info is BulletDamageInfo bulletDamageInfo) {
                Facade.Die( bulletDamageInfo.Direction * 5, bulletDamageInfo.Point );
            } else {
                Facade.Die();
            }
        }

        protected void Move(Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
            Facade.Move( moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
        }

        protected void BodyAt(Vector3? target) {
            Facade.BodyAt( target );
        }

        protected bool HeadAt(Vector3? target) {
            return Facade.HeadAt( target );
        }

        protected bool AimAt(Vector3? target) {
            return Facade.AimAt( target );
        }

        protected void Die() {
            Facade.Die();
        }
        protected void Die(Vector3 force, Vector3 position) {
            Facade.Die( force, position );
        }

    }
    public abstract partial class Character {
        protected class Facade_ : Disposable {

            private GameObject GameObject { get; }
            private Transform Transform => GameObject.transform;
            private MoveableBody MoveableBody { get; }
            private Rigidbody Rigidbody { get; }
            private GameObject Head { get; }
            private WeaponSlot WeaponSlot { get; }
            public Weapon? Weapon {
                get => WeaponSlot.transform.childCount > 0 ? WeaponSlot.transform.GetChild( 0 ).gameObject.RequireComponent<Weapon>() : null;
                set {
                    var prevWeapon = Weapon;
                    if (prevWeapon != null) {
                        prevWeapon.gameObject.SetLayerRecursively( Layers.Entity );
                        prevWeapon.transform.SetParent( null, true );
                        prevWeapon.IsRigidbody = true;
                    }
                    if (value != null) {
                        value.gameObject.SetLayerRecursively( Layers.Entity_Exact );
                        value.transform.SetParent( WeaponSlot.transform, true );
                        value.transform.localPosition = Vector3.zero;
                        value.transform.localRotation = Quaternion.identity;
                        value.IsRigidbody = false;
                    }
                }
            }

            public Facade_(GameObject gameObject) {
                GameObject = gameObject;
                MoveableBody = gameObject.RequireComponent<MoveableBody>();
                Rigidbody = gameObject.RequireComponent<Rigidbody>();
                Head = gameObject.transform.Require( "Head" ).gameObject;
                WeaponSlot = gameObject.RequireComponentInChildren<WeaponSlot>();
            }
            public override void Dispose() {
                base.Dispose();
            }

            public void Move(Vector3 moveVector, bool isJumpPressed, bool isCrouchPressed, bool isAcceleratePressed) {
                MoveableBody.Move( moveVector, isJumpPressed, isCrouchPressed, isAcceleratePressed );
            }

            public void BodyAt(Vector3? target) {
                MoveableBody.LookAt( target );
            }

            public bool HeadAt(Vector3? target) {
                return LookAt( Head.transform, target );
                static bool LookAt(Transform transform, Vector3? target) {
                    var rotation = transform.localRotation;
                    if (target != null) {
                        transform.LookAt( target.Value );
                        if (Check( transform.localRotation )) {
                            transform.localRotation = Quaternion.RotateTowards( rotation, transform.localRotation, 2 * 360 * Time.deltaTime );
                            return true;
                        }
                    }
                    transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
                static bool Check(Quaternion rotation) {
                    var angles = rotation.eulerAngles;
                    if (angles.x > 180) angles.x -= 360;
                    if (angles.y > 180) angles.y -= 360;
                    if (angles.x >= -100 && angles.x <= 100) {
                        if (angles.y >= -80 && angles.y <= 80) {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public bool AimAt(Vector3? target) {
                return LookAt( WeaponSlot.transform, target );
                static bool LookAt(Transform transform, Vector3? target) {
                    var rotation = transform.localRotation;
                    if (target != null) {
                        transform.LookAt( target.Value );
                        if (Check( transform.localRotation )) {
                            transform.localRotation = Quaternion.RotateTowards( rotation, transform.localRotation, 2 * 360 * Time.deltaTime );
                            return true;
                        }
                    }
                    transform.localRotation = Quaternion.RotateTowards( rotation, Quaternion.identity, 2 * 360 * Time.deltaTime );
                    return false;
                }
                static bool Check(Quaternion rotation) {
                    var angles = rotation.eulerAngles;
                    if (angles.x > 180) angles.x -= 360;
                    if (angles.y > 180) angles.y -= 360;
                    if (angles.x >= -100 && angles.x <= 100) {
                        if (angles.y >= -80 && angles.y <= 80) {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public void Die() {
                GameObject.SetLayerRecursively( Layers.Entity );
                MoveableBody.enabled = false;
                Rigidbody.isKinematic = false;
            }
            public void Die(Vector3 force, Vector3 position) {
                GameObject.SetLayerRecursively( Layers.Entity );
                MoveableBody.enabled = false;
                Rigidbody.isKinematic = false;
                Rigidbody.AddForceAtPosition( force, position, ForceMode.Impulse );
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
