#nullable enable
namespace Project.Entities.Actors {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PlayableCharacter : Character {

        public IPlayableCharacterInput? Input { get; set; }

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
