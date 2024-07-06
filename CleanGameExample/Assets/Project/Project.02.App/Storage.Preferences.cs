#nullable enable
namespace Project.App {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.App;

    public partial class Storage {
        public class Preferences : StorageBase {

            internal Preferences() {
                Load();
            }
            public override void Dispose() {
                base.Dispose();
            }

            public void Load() {
            }
            public void Save() {
            }

        }
    }
}
