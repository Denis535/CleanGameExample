#if UNITY_EDITOR
#nullable enable
namespace Project.Toolbar {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    public static class ProjectToolbar {

        // LoadScene
        [MenuItem( "Project/Launcher", priority = 0 )]
        public static void LoadLauncher() {
            EditorSceneManager.OpenScene( "Assets/Project/Assets.Project/Launcher.unity" );
        }

        // LoadScene
        [MenuItem( "Project/Program", priority = 1 )]
        public static void LoadProgram() {
            EditorSceneManager.OpenScene( "Assets/Project/Assets.Project.Scenes/Program.unity" );
        }
        [MenuItem( "Project/Main Scene", priority = 2 )]
        public static void LoadMainScene() {
            EditorSceneManager.OpenScene( "Assets/Project/Assets.Project.Scenes/MainScene.unity" );
        }
        [MenuItem( "Project/Game Scene", priority = 3 )]
        public static void LoadGameScene() {
            EditorSceneManager.OpenScene( "Assets/Project/Assets.Project.Scenes/GameScene.unity" );
        }

        // LoadScene
        [MenuItem( "Project/World 01", priority = 100 )]
        public static void LoadLevel1() {
            EditorSceneManager.OpenScene( "Assets/Project.Entities/Assets.Project.Entities.Worlds/World_01.unity" );
        }
        [MenuItem( "Project/World 02", priority = 101 )]
        public static void LoadLevel2() {
            EditorSceneManager.OpenScene( "Assets/Project.Entities/Assets.Project.Entities.Worlds/World_02.unity" );
        }
        [MenuItem( "Project/World 03", priority = 101 )]
        public static void LoadLevel3() {
            EditorSceneManager.OpenScene( "Assets/Project.Entities/Assets.Project.Entities.Worlds/World_03.unity" );
        }

        // Build
        [MenuItem( "Project/Pre Build", priority = 200 )]
        public static void PreBuild() {
            ProjectBuilder.PreBuild();
        }
        [MenuItem( "Project/Build Development", priority = 201 )]
        public static void BuildDevelopment() {
            var path = $"Build/Development/{PlayerSettings.productName}.exe";
            ProjectBuilder.BuildDevelopment( path );
            EditorApplication.Beep();
            EditorUtility.RevealInFinder( path );
        }
        [MenuItem( "Project/Build Production", priority = 202 )]
        public static void BuildProduction() {
            var path = $"Build/Production/{PlayerSettings.productName}.exe";
            ProjectBuilder.BuildProduction( path );
            EditorApplication.Beep();
            EditorUtility.RevealInFinder( path );
        }

        // EmbedPackage
        [MenuItem( "Project/Embed Package (com.denis535.uitoolkit-theme-style-sheet)", priority = 300 )]
        public static void EmbedPackage_UIToolkitThemeStyleSheet() {
            UnityEditor.PackageManager.Client.Embed( "com.denis535.uitoolkit-theme-style-sheet" );
        }

    }
}
#endif
