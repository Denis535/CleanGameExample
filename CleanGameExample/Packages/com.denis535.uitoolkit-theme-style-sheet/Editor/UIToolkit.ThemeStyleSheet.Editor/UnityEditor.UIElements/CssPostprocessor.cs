//#nullable enable
//namespace UnityEditor.UIElements {
//    using System;
//    using System.Collections;
//    using System.Collections.Generic;
//    using System.IO;
//    using System.Linq;
//    using UnityEditor;
//    using UnityEngine;

//    public class CssPostprocessor : AssetPostprocessor {

//        // OnPostprocessAllAssets
//        public static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFrom) {
//            foreach (var imported_ in imported) {
//                OnAssetImported( imported_ );
//            }
//            foreach (var deleted_ in deleted) {
//                OnAssetDeleted( deleted_ );
//            }
//            foreach (var (moved_, movedFrom_) in moved.Zip( movedFrom, (a, b) => (a, b) )) {
//                OnAssetMoved( moved_, movedFrom_ );
//            }
//        }
//        private static void OnAssetImported(string path) {
//            if (IsCss( path ) && IsSupported( path )) {
//                CompilePostCss( path, Path.ChangeExtension( path, ".uss" ) );
//                AssetDatabase.ImportAsset( Path.ChangeExtension( path, ".uss" ) );
//            }
//        }
//        private static void OnAssetDeleted(string path) {
//            if (IsCss( path )) {
//                AssetDatabase.DeleteAsset( Path.ChangeExtension( path, ".uss" ) );
//            }
//        }
//        private static void OnAssetMoved(string newPath, string oldPath) {
//            if (IsCss( oldPath )) {
//                AssetDatabase.MoveAsset( Path.ChangeExtension( oldPath, ".uss" ), Path.ChangeExtension( newPath, ".uss" ) );
//            }
//        }

//        // Helpers
//        private static void CompilePostCss(string src, string dist) {
//        }

//        // Helpers
//        private static bool IsCss(string path) {
//            return Path.GetExtension( path ) == ".css";
//        }
//        private static bool IsSupported(string path) {
//            return !Path.GetFileName( path ).StartsWith( "_" );
//        }

//    }
//}
