#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.App;

    public partial class Storage {
        public class Preferences : StorageBase {

            // Constructor
            internal Preferences() {
                Load();
            }

            // Load
            public void Load() {
            }
            public void Save() {
            }

        }
    }
}
