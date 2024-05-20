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
        protected override void OnGUI(Rect rect, string path) {
            base.OnGUI( rect, path );
        }

        // DrawModule
        protected override void DrawModule(Rect rect, string path, string module) {
            base.DrawModule( rect, path, module );
        }
        protected override void DrawContent(Rect rect, string path, string module, string content) {
            if (Path.GetExtension( path ) is ".asmdef" or ".asmref") {
                return;
            }
            base.DrawContent( rect, path, module, content );
        }

        // Helpers
        private static string[] GetModulePaths() {
            return Enumerable.Empty<string>()
                .Append( "Assets/Project" )
                .Append( "Assets/Project.Common" )
                .Concat(
                    AssetDatabase.GetAllAssetPaths()
                    .Where( i => Path.GetExtension( i ) is ".asmdef" or ".asmref" )
                    .Select( Path.GetDirectoryName )
                    .Select( i => i.Replace( '\\', '/' ) )
                    .Where( i => i.StartsWith( "Packages/" ) )
                    .Distinct()
                )
                .ToArray();
        }

    }
}
#endif
