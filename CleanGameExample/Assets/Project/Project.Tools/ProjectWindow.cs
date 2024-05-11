#if UNITY_EDITOR
#nullable enable
namespace Project.Tools {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEditor;
    using UnityEditor.ColorfulProjectWindow;
    using UnityEngine;

    [InitializeOnLoad]
    public class ProjectWindow : ColorfulProjectWindow {

        // Constructor
        static ProjectWindow() {
            new ProjectWindow();
        }

        // Constructor
        public ProjectWindow() : base( new[] { "Assets/Project" } ) {
        }

        // OnGUI
        protected override void OnGUI(string guid, Rect rect) {
            base.OnGUI( guid, rect );
        }
        protected override void OnGUI(Rect rect, string path, string module, string content) {
            base.OnGUI( rect, path, module, content );
        }

    }
}
#endif
