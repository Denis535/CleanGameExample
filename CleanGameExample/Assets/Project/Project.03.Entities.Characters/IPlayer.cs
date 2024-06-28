#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;

    public interface IPlayer {
        string Name { get; }
        bool IsMovePressed(out Vector3 moveVector);
        Vector3? GetLookTarget();
        Vector3? GetHeadTarget();
        Vector3? GetAimTarget();
        bool IsJumpPressed();
        bool IsCrouchPressed();
        bool IsAcceleratePressed();
        bool IsFirePressed();
        bool IsAimPressed();
        bool IsInteractPressed(out MonoBehaviour? interactable);
    }
}
