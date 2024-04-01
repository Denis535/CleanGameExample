#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework;

    [DefaultExecutionOrder( ScriptExecutionOrders.Application - 1 )]
    public class Camera2 : MonoBehaviour {

        public Vector3? Target { get; set; }
        public Vector2 LookDelta { get; set; }
        public float ScrollDelta { get; set; }

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
        public void LateUpdate() {
            //Debug.Log( LookDelta + " / " + ScrollDelta );
        }

    }
}
