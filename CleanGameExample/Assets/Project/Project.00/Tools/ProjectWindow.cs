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
        public ProjectWindow() : base( GetPackagePaths(), GetModulePaths() ) {
        }

        // OnGUI
        protected override void OnGUI(Rect rect, string path) {
            base.OnGUI( rect, path );
        }

        // DrawPackage
        protected override void DrawPackage(Rect rect, string path, string package, string content) {
            base.DrawPackage( rect, path, package, content );
        }

        // DrawAssembly
        protected override void DrawAssembly(Rect rect, string path, string assembly, string content) {
            if (Path.GetExtension( path ) is not (".asmdef" or ".asmref")) {
                base.DrawAssembly( rect, path, assembly, content );
            }
        }

        // Helpers
        private static string[] GetPackagePaths() {
            return Enumerable.Empty<string>()
                .Append( "Packages/com.denis535.clean-architecture-game-framework" )
                .Append( "Packages/com.denis535.addressables-extensions" )
                .Append( "Packages/com.denis535.addressables-source-generator" )
                .Append( "Packages/com.denis535.colorful-project-window" )
                .Append( "Packages/com.denis535.project-infrastructure" )
                .Append( "Packages/com.denis535.infrastructure" )
                .ToArray();
        }
        private static string[] GetModulePaths() {
            return Enumerable.Empty<string>()
                .Append( "Assets/Project" )
                .Concat( AssetDatabase.GetAllAssetPaths()
                    .Where( i => Path.GetExtension( i ) is ".asmdef" or ".asmref" )
                    .Where( i => i.StartsWith( "Packages/" ) )
                    .Select( Path.GetDirectoryName )
                    .Select( i => i.Replace( '\\', '/' ) )
                    .Distinct() ).ToArray();
        }

    }
}
#endif
