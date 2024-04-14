#nullable enable
namespace Project.Entities.Characters.Primary {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public class CharacterBody : EntityBodyBase {

        // Input
        public Vector3 MoveDirectionInput { get; set; }
        public bool JumpInput { get; set; }
        public bool CrouchInput { get; set; }
        public bool AccelerationInput { get; set; }

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
        }
        public void FixedUpdate() {
            if (MoveDirectionInput != default) {
                if (AccelerationInput) {
                    transform.localPosition += MoveDirectionInput * 13 * Time.fixedDeltaTime;
                } else {
                    transform.localPosition += MoveDirectionInput * 8 * Time.fixedDeltaTime;
                }
            }
            if (JumpInput) {
                if (AccelerationInput) {
                    transform.localPosition += Vector3.up * 13 * Time.fixedDeltaTime;
                } else {
                    transform.localPosition += Vector3.up * 8 * Time.fixedDeltaTime;
                }
            }
            if (CrouchInput) {
                if (AccelerationInput) {
                    transform.localPosition -= Vector3.up * 13 * Time.fixedDeltaTime;
                } else {
                    transform.localPosition -= Vector3.up * 8 * Time.fixedDeltaTime;
                }
            }
        }

    }
}
