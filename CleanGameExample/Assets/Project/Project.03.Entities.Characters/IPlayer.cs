#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IPlayer {
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
