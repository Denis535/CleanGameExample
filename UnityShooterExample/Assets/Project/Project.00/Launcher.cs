#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.UI;
    using UnityEngine;

    public class Launcher : MonoBehaviour {

        public void Awake() {
        }
        public void OnDestroy() {
        }

        public void Start() {
            UIRouter.LoadStartup();
        }
        public void Update() {
        }

    }
}
