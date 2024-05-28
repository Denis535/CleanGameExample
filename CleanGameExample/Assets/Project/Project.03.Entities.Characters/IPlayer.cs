﻿#nullable enable
namespace Project.Entities.Characters {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public interface IPlayer {
        string Name { get; }
        Vector3 GetLookTarget();
        bool IsMovePressed(out Vector3 moveVector);
        bool IsJumpPressed();
        bool IsCrouchPressed();
        bool IsAcceleratePressed();
        bool IsFirePressed();
        bool IsAimPressed();
        bool IsInteractPressed(out EntityBase? interactable);
    }
    public enum PlayerCharacterEnum {
        Gray,
        Red,
        Green,
        Blue
    }
}