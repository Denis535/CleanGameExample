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

        protected GameObjectFacade Facade { get; private set; } = default!;
        public bool IsAlive { get; private set; } = true;
        public event Action<DamageInfo>? OnDamageEvent;

        protected virtual void Awake() {
            Facade = new GameObjectFacade( gameObject );
        }
        protected virtual void OnDestroy() {
            Facade.Dispose();
        }

        protected virtual void Start() {
        }
        protected virtual void FixedUpdate() {
        }
        protected virtual void Update() {
        }

        public virtual void OnDamage(DamageInfo info) {
            if (IsAlive) {
                IsAlive = false;
                Facade.Weapon = null;
                if (info is BulletDamageInfo bulletDamageInfo) {
                    Facade.Kill( bulletDamageInfo.Direction * 5, bulletDamageInfo.Point );
                } else {
                    Facade.Kill();
                }
                OnDamageEvent?.Invoke( info );
            }
        }

    }
    public abstract partial class Character {
        protected class GameObjectFacade : Disposable {

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

            public GameObjectFacade(GameObject gameObject) {
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

            public void Kill() {
                GameObject.SetLayerRecursively( Layers.Entity );
                MoveableBody.enabled = false;
                Rigidbody.isKinematic = false;
            }
            public void Kill(Vector3 force, Vector3 position) {
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
