#if UNITY_EDITOR
#nullable enable
namespace Project.Tools {
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
            EditorSceneManager.OpenScene( "Assets/Project.00/Assets.Project/Launcher.unity" );
        }
        [MenuItem( "Project/Startup", priority = 1 )]
        public static void LoadStartup() {
            EditorSceneManager.OpenScene( "Assets/Project.00/Assets.Project.Scenes/Startup.unity" );
        }
        [MenuItem( "Project/Main Scene", priority = 2 )]
        public static void LoadMainScene() {
            EditorSceneManager.OpenScene( "Assets/Project.00/Assets.Project.Scenes/MainScene.unity" );
        }
        [MenuItem( "Project/Game Scene", priority = 3 )]
        public static void LoadGameScene() {
            EditorSceneManager.OpenScene( "Assets/Project.00/Assets.Project.Scenes/GameScene.unity" );
        }

        // LoadScene
        [MenuItem( "Project/World 01", priority = 100 )]
        public static void LoadLevel1() {
            EditorSceneManager.OpenScene( "Assets/Project.04.Worlds/Assets.Project.Worlds/World_01.unity" );
        }
        [MenuItem( "Project/World 02", priority = 101 )]
        public static void LoadLevel2() {
            EditorSceneManager.OpenScene( "Assets/Project.04.Worlds/Assets.Project.Worlds/World_02.unity" );
        }
        [MenuItem( "Project/World 03", priority = 101 )]
        public static void LoadLevel3() {
            EditorSceneManager.OpenScene( "Assets/Project.04.Worlds/Assets.Project.Worlds/World_03.unity" );
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

        // SpawnPoint
        [MenuItem( "Project/Player Spawn Point", priority = 300 )]
        public static void PlayerSpawnPoint() {
            var ray = HandleUtility.GUIPointToWorldRay( GUIUtility.ScreenToGUIPoint( SceneView.lastActiveSceneView.cameraViewport.center ) );
            if (Physics.Raycast( ray, out var hit, 512, ~0, QueryTriggerInteraction.Ignore )) {
                var go = Selection.activeGameObject = new GameObject( "PlayerSpawnPoint", typeof( PlayerSpawnPoint ) );
                go.transform.position = hit.point;
            }
        }
        [MenuItem( "Project/Enemy Spawn Point", priority = 301 )]
        public static void EnemySpawnPoint() {
            var ray = HandleUtility.GUIPointToWorldRay( GUIUtility.ScreenToGUIPoint( SceneView.lastActiveSceneView.cameraViewport.center ) );
            if (Physics.Raycast( ray, out var hit, 512, ~0, QueryTriggerInteraction.Ignore )) {
                var go = Selection.activeGameObject = new GameObject( "EnemySpawnPoint", typeof( EnemySpawnPoint ) );
                go.transform.position = hit.point;
            }
        }
        [MenuItem( "Project/Loot Spawn Point", priority = 302 )]
        public static void LootSpawnPoint() {
            var ray = HandleUtility.GUIPointToWorldRay( GUIUtility.ScreenToGUIPoint( SceneView.lastActiveSceneView.cameraViewport.center ) );
            if (Physics.Raycast( ray, out var hit, 512, ~0, QueryTriggerInteraction.Ignore )) {
                var go = Selection.activeGameObject = new GameObject( "LootSpawnPoint", typeof( LootSpawnPoint ) );
                go.transform.position = hit.point;
            }
        }

        //// ShowAssets
        //[MenuItem( "Project/Show Assets", priority = 400 )]
        //public static void ShowAssets() {
        //    foreach (var path in AssetDatabase.GetAllAssetPaths()) {
        //        if (path.EndsWith( "/csc.rsp" )) {
        //            var asset = AssetDatabase.LoadAssetAtPath<DefaultAsset>( path );
        //            var importer = AssetImporter.GetAtPath( path );
        //        }
        //    }
        //}

        //// HideAssets
        //[MenuItem( "Project/Hide Assets", priority = 401 )]
        //public static void HideAssets() {
        //    foreach (var path in AssetDatabase.GetAllAssetPaths()) {
        //        if (path.EndsWith( "/csc.rsp" )) {
        //            var asset = AssetDatabase.LoadAssetAtPath<DefaultAsset>( path );
        //            var importer = AssetImporter.GetAtPath( path );
        //        }
        //    }
        //}

    }
}
#endif
