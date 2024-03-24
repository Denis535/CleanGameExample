#nullable enable
namespace UIToolkit.ThemeStyleSheet {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    public static class Toolbar {

        // CreateAsset
        [MenuItem( "Assets/Create/UI Toolkit/Pug" )]
        public static void CreateAsset_Pug() {
            ProjectWindowUtil.CreateAssetWithContent( "New Pug.pug", "" );
        }
        //[MenuItem( "Assets/Create/UI Toolkit/Css" )]
        //public static void CreateAsset_Css() {
        //    ProjectWindowUtil.CreateAssetWithContent( "New Css.css", "" );
        //}
        //[MenuItem( "Assets/Create/UI Toolkit/Sass" )]
        //public static void CreateAsset_Sass() {
        //    ProjectWindowUtil.CreateAssetWithContent( "New Sass.sass", "" );
        //}
        [MenuItem( "Assets/Create/UI Toolkit/Stylus" )]
        public static void CreateAsset_Stylus() {
            ProjectWindowUtil.CreateAssetWithContent( "New Stylus.styl", "" );
        }

        // TakeScreenshot
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/Take Screenshot (Game) _F12", priority = 0 )]
        internal static void TakeScreenshot_Game() {
            var path = $"Screenshots/{Application.productName}-{DateTime.UtcNow.Ticks}.png";
            ScreenCapture.CaptureScreenshot( path, 1 );
            EditorApplication.Beep();
            EditorUtility.RevealInFinder( path );
        }
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/Take Screenshot (Editor) &F12", priority = 1 )]
        internal static void TakeScreenshot_Editor() {
            var position = EditorGUIUtility.GetMainWindowPosition();
            var texture = new Texture2D( (int) position.width, (int) position.height );
            texture.SetPixels( InternalEditorUtility.ReadScreenPixel( position.position, (int) position.width, (int) position.height ) );
            var bytes = texture.EncodeToPNG();
            UnityEngine.Object.DestroyImmediate( texture );

            var path = $"Screenshots/{Application.productName}-{DateTime.UtcNow.Ticks}.png";
            Directory.CreateDirectory( Path.GetDirectoryName( path ) );
            File.WriteAllBytes( path, bytes );
            EditorApplication.Beep();
            EditorUtility.RevealInFinder( path );
        }

        // OpenAll
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/Open All (UXML)", priority = 100 )]
        public static void OpenAll_UXML() {
            OpenAssets( "Assets/(*.uxml)" );
            OpenAssets( "Packages/(*.uxml)" );
        }
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/Open All (Pug)", priority = 101 )]
        public static void OpenAll_Pug() {
            OpenAssets( "Assets/(*.pug)" );
            OpenAssets( "Packages/(*.pug)" );
        }

        // OpenAll
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/Open All (USS)", priority = 200 )]
        public static void OpenAll_USS() {
            OpenAssets( "Assets/(*.uss)" );
            OpenAssets( "Packages/(*.uss)" );
        }
        //[MenuItem( "Tools/UIToolkit Theme Style Sheet/Open All (CSS)", priority = 201 )]
        //public static void OpenAll_CSS() {
        //    OpenAssets( "Assets/(*.css)" );
        //    OpenAssets( "Packages/(*.css)" );
        //}
        //[MenuItem( "Tools/UIToolkit Theme Style Sheet/Open All (Sass)", priority = 202 )]
        //public static void OpenAll_Sass() {
        //    OpenAssets( "Assets/(*.sass)" );
        //    OpenAssets( "Packages/(*.sass)" );
        //}
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/Open All (Stylus)", priority = 203 )]
        public static void OpenAll_Stylus() {
            OpenAssets( "Assets/(*.stylus|*.styl)" );
            OpenAssets( "Packages/(*.stylus|*.styl)" );
        }

        // EmbedPackage
        [MenuItem( "Tools/UIToolkit Theme Style Sheet/Embed Package (com.denis535.uitoolkit-theme-style-sheet)", priority = 300 )]
        public static void EmbedPackage() {
            UnityEditor.PackageManager.Client.Embed( "com.denis535.uitoolkit-theme-style-sheet" );
        }

        // Helpers
        private static void OpenAssets(params string[] patterns) {
            foreach (var path in GetPaths().GetMatches( patterns )) {
                if (!Path.GetFileName( path ).StartsWith( "_" )) {
                    AssetDatabase.OpenAsset( AssetDatabase.LoadAssetAtPath<UnityEngine.Object>( path ) );
                    Thread.Sleep( 500 );
                }
            }
        }
        private static void OpenAssetsReverse(params string[] patterns) {
            foreach (var path in GetPaths().GetMatches( patterns ).Reverse()) {
                if (!Path.GetFileName( path ).StartsWith( "_" )) {
                    AssetDatabase.OpenAsset( AssetDatabase.LoadAssetAtPath<UnityEngine.Object>( path ) );
                    Thread.Sleep( 500 );
                }
            }
        }
        // Helpers
        private static IEnumerable<string> GetPaths() {
            var path = Directory.GetCurrentDirectory();
            return GetPaths( path )
                .Select( i => Path.GetRelativePath( path, i ) )
                .Select( i => i.Replace( '\\', '/' ) );
        }
        private static IEnumerable<string> GetPaths(string path) {
            foreach (var file in Directory.EnumerateFiles( path ).OrderBy( i => i )) {
                yield return file;
            }
            foreach (var dir in Directory.EnumerateDirectories( path ).OrderBy( i => i )) {
                yield return dir;
                foreach (var i in GetPaths( dir )) yield return i;
            }
        }
        // Helpers
        private static IEnumerable<string> GetMatches(this IEnumerable<string> paths, string[] patterns) {
            paths = paths.ToList();
            foreach (var pattern in patterns) {
                var regex = new Regex( "^" + pattern.Replace( " ", "" ).Replace( "*", "(.*?)" ) + "$", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace );
                foreach (var path in paths.Where( path => regex.IsMatch( path ) )) {
                    yield return path;
                }
            }
        }

    }
}
