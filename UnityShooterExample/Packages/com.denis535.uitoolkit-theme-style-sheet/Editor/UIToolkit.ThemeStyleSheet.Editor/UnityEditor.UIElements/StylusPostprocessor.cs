#nullable enable
namespace UnityEditor.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    public class StylusPostprocessor : AssetPostprocessor {

        // OnPostprocessAllAssets
        public static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFrom) {
            foreach (var imported_ in imported) {
                OnAssetImported( imported_ );
            }
            foreach (var deleted_ in deleted) {
                OnAssetDeleted( deleted_ );
            }
            foreach (var (moved_, movedFrom_) in moved.Zip( movedFrom, (a, b) => (a, b) )) {
                OnAssetMoved( moved_, movedFrom_ );
            }
        }
        private static void OnAssetImported(string path) {
            if (IsStylus( path ) && IsSupported( path )) {
                CompileStylus( path, Path.ChangeExtension( path, ".uss" ) );
                AssetDatabase.ImportAsset( Path.ChangeExtension( path, ".uss" ) );
            }
        }
        private static void OnAssetDeleted(string path) {
            if (IsStylus( path )) {
                AssetDatabase.DeleteAsset( Path.ChangeExtension( path, ".uss" ) );
            }
        }
        private static void OnAssetMoved(string newPath, string oldPath) {
            if (IsStylus( oldPath )) {
                AssetDatabase.MoveAsset( Path.ChangeExtension( oldPath, ".uss" ), Path.ChangeExtension( newPath, ".uss" ) );
            }
        }

        // Helpers
        private static void CompileStylus(string src, string dist) {
            NodeJS.Run( "Packages/com.denis535.uitoolkit-theme-style-sheet/Editor/UIToolkit.ThemeStyleSheet.Editor/StylusCompiler.js", src, dist );
        }
        private static bool IsStylus(string path) {
            return Path.GetExtension( path ) == ".styl";
        }
        private static bool IsSupported(string path) {
            return !Path.GetFileName( path ).StartsWith( "_" );
        }

    }
}
