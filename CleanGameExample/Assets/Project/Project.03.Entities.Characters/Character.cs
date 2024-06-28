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

        // MoveableBody
        protected MoveableBody MoveableBody { get; set; } = default!;
        // Rigidbody
        private Rigidbody Rigidbody { get; set; } = default!;
        // Game
        public IGame Game { get; set; } = default!;
        // IsAlive
        public bool IsAlive => MoveableBody.enabled;
        // Weapon
        public IWeapon? Weapon {
            get => View.Weapon?.GetComponent<IWeapon>();
            protected set {
                var prevWeapon = Weapon;
                if (prevWeapon != null) {
                    //SetPhysical( (IThing) prevWeapon, null );
                }
                if (value != null) {
                    //SetPhysical( (IThing) value, slot.transform );
                }
                View.Weapon = ((MonoBehaviour?) value)?.gameObject;
            }
        }
        // OnDamageEvent
        public event Action<DamageInfo>? OnDamageEvent;

        // Awake
        protected override void Awake() {
            Body = new CharacterBody( gameObject );
            View = new CharacterView( gameObject );
            MoveableBody = gameObject.RequireComponent<MoveableBody>();
            Rigidbody = gameObject.RequireComponent<Rigidbody>();
        }
        protected override void OnDestroy() {
            View.Dispose();
            Body.Dispose();
        }

        // Start
        protected virtual void Start() {
        }
        protected virtual void FixedUpdate() {
            MoveableBody.FixedUpdate2();
        }
        protected virtual void Update() {
            MoveableBody.Update2();
        }

        // OnDamage
        void IDamageable.OnDamage(DamageInfo info) {
            OnDamage( info );
        }
        protected virtual void OnDamage(DamageInfo info) {
            if (IsAlive) {
                Weapon = null;
                //SetPhysical( this, true );
                //Rigidbody.AddForceAtPosition( info.Direction * 5, info.Point, ForceMode.Impulse );
                OnDamageEvent?.Invoke( info );
            }
        }

        // Helpers
        //private static void SetPhysical(Character character, bool value) {
        //    if (value) {
        //        character.gameObject.SetLayerRecursively( Layers.Entity );
        //        character.MoveableBody.enabled = false;
        //        character.Rigidbody.isKinematic = false;
        //    } else {
        //        character.gameObject.SetLayerRecursively( Layers.CharacterEntity, Layers.CharacterEntityInternal );
        //        character.MoveableBody.enabled = true;
        //        character.Rigidbody.isKinematic = true;
        //    }
        //}
        //private static void SetPhysical(IThing thing, Transform? parent) {
        //    if (parent != null) {
        //        thing.gameObject.SetLayerRecursively( Layers.CharacterEntityInternal );
        //        thing.transform.localPosition = Vector3.zero;
        //        thing.transform.localRotation = Quaternion.identity;
        //        thing.transform.SetParent( parent, false );
        //        thing.gameObject.RequireComponent<Rigidbody>().isKinematic = true;
        //    } else {
        //        thing.gameObject.SetLayerRecursively( Layers.Entity );
        //        thing.transform.SetParent( null, true );
        //        thing.gameObject.RequireComponent<Rigidbody>().isKinematic = false;
        //    }
        //}

    }
    public class CharacterBody : EntityBodyBase {

        public CharacterBody(GameObject gameObject) : base( gameObject ) {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class CharacterView : EntityViewBase {

        private Transform Body { get; }
        private Transform Head { get; }
        private Slot WeaponSlot { get; }
        public GameObject? Weapon {
            get => WeaponSlot.transform.childCount > 0 ? WeaponSlot.transform.GetChild( 0 ).gameObject : null;
            set {
                WeaponSlot.transform.DetachChildren();
                if (value != null) {
                    value.transform.parent = WeaponSlot.transform;
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
