#nullable enable
namespace UnityEditor.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    public class PugPostprocessor : AssetPostprocessor {

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
            if (IsPug( path ) && IsSupported( path )) {
                CompilePug( path, Path.ChangeExtension( path, ".uxml" ) );
                AssetDatabase.ImportAsset( Path.ChangeExtension( path, ".uxml" ) );
            }
        }
        private static void OnAssetDeleted(string path) {
            if (IsPug( path )) {
                AssetDatabase.DeleteAsset( Path.ChangeExtension( path, ".uxml" ) );
            }
        }
        private static void OnAssetMoved(string newPath, string oldPath) {
            if (IsPug( oldPath )) {
                AssetDatabase.MoveAsset( Path.ChangeExtension( oldPath, ".uxml" ), Path.ChangeExtension( newPath, ".uxml" ) );
            }
        }

        // Helpers
        private static void CompilePug(string src, string dist) {
            NodeJS.Run( "Packages/com.denis535.uitoolkit-theme-style-sheet/Editor/UIToolkit.ThemeStyleSheet.Editor/PugCompiler.js", src, dist );
        }
        private static bool IsPug(string path) {
            return Path.GetExtension( path ) == ".pug";
        }
        private static bool IsSupported(string path) {
            return !Path.GetFileName( path ).StartsWith( "_" );
        }

    }
}
