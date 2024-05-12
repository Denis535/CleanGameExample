#if UNITY_EDITOR
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
        public ProjectWindow() : base( GetModulePaths() ) {
        }

        // OnGUI
        protected override void OnGUI(string guid, Rect rect) {
            base.OnGUI( guid, rect );
        }

        // Helpers
        private static string[] GetModulePaths() {
            return AssetDatabase.GetAllAssetPaths()
                .Where( i => Path.GetExtension( i ) is ".asmdef" or ".asmref" )
                .Select( Path.GetDirectoryName )
                .Select( i => i.Replace( '\\', '/' ) )
                .Where( i => i.StartsWith( "Packages/" ) )
                .Distinct()
                .Prepend( "Assets/Project" )
                .ToArray();
        }

    }
}
#endif
