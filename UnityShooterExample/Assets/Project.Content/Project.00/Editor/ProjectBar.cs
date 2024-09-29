#if UNITY_EDITOR
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    public static class ProjectBar {

        [MenuItem( "Project/Launcher", priority = 0 )]
        public static void LoadLauncher() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "Launcher.unity" );
            EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/Startup", priority = 1 )]
        public static void LoadStartup() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "Startup.unity" );
            EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/Main Scene", priority = 2 )]
        public static void LoadMainScene() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "MainScene.unity" );
            EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/Game Scene", priority = 3 )]
        public static void LoadGameScene() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "GameScene.unity" );
            EditorSceneManager.OpenScene( path );
        }

        [MenuItem( "Project/World 01", priority = 100 )]
        public static void LoadLevel1() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "World_01.unity" );
            EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/World 02", priority = 101 )]
        public static void LoadLevel2() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "World_02.unity" );
            EditorSceneManager.OpenScene( path );
        }
        [MenuItem( "Project/World 03", priority = 101 )]
        public static void LoadLevel3() {
            var path = AssetDatabase.GetAllAssetPaths().Single( i => Path.GetFileName( i ) == "World_03.unity" );
            EditorSceneManager.OpenScene( path );
        }

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

        [MenuItem( "Project/Open All", priority = 300 )]
        public static void OpenAll() {
            var paths = AssetDatabase.GetAllAssetPaths()
                .Where( i => i.StartsWith( "Assets/Project.Content/" ) && Path.GetExtension( i ) == ".cs" )
                .Select( i => new {
                    path = i,
                    dir = Path.GetDirectoryName( i ).Replace( '\\', '/' ) + '/',
                    name = Path.GetFileName( i )
                } )
                .OrderByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.00/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.01.UI/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.01.UI/MainScreen/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.01.UI/GameScreen/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.01.UI/Common/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.01.UI.Internal/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.01.UI.Internal/MainScreen/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.01.UI.Internal/GameScreen/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.01.UI.Internal/Common/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.02.App/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.03.Entities/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.03.Entities.Actors/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.03.Entities.Things/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project.Content/Project.03.Entities.Worlds/" ) )
                // UI/
                .ThenByDescending( i => i.path.EndsWith( ".UI/UITheme.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI/UIScreen.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI/UIRouter.cs" ) )
                // UI/MainScreen/
                .ThenByDescending( i => i.path.EndsWith( ".UI/MainScreen/MainWidget.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI/MainScreen/MenuWidget.cs" ) )
                // UI/GameScreen/
                .ThenByDescending( i => i.path.EndsWith( ".UI/GameScreen/GameWidget.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI/GameScreen/TotalsWidget.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI/GameScreen/MenuWidget.cs" ) )
                // UI/Common/
                .ThenByDescending( i => i.path.EndsWith( ".UI/Common/DialogWidget.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI/Common/LoadingWidget.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI/Common/UnloadingWidget.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI/Common/SettingsWidget.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI/Common/ProfileSettingsWidget.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI/Common/VideoSettingsWidget.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI/Common/AudioSettingsWidget.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( "Widget.cs" ) )
                // UI.Internal/
                // UI.Internal/MainScreen/
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/MainScreen/MainWidgetView.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/MainScreen/MenuWidgetView.cs" ) )
                // UI.Internal/GameScreen/
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/GameScreen/GameWidgetView.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/GameScreen/TotalsWidgetView.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/GameScreen/MenuWidgetView.cs" ) )
                // UI.Internal/Common/
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/Common/DialogWidgetView.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/Common/LoadingWidgetView.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/Common/UnloadingWidgetView.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/Common/SettingsWidgetView.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/Common/ProfileSettingsWidgetView.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/Common/VideoSettingsWidgetView.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( ".UI.Internal/Common/AudioSettingsWidgetView.cs" ) )
                .ThenByDescending( i => i.path.EndsWith( "View.cs" ) )
                // App/
                .ThenByDescending( i => i.name.Equals( "Application2.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Storage.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Storage.ProfileSettings.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Storage.VideoSettings.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Storage.AudioSettings.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Storage.Preferences.cs" ) )
                // Entities/
                .ThenByDescending( i => i.name.Equals( "Game.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Player.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Camera.cs" ) )
                // Entities.Actors/
                .ThenByDescending( i => i.name.Equals( "ActorBase.cs" ) )
                .ThenByDescending( i => i.name.Equals( "CharacterBase.cs" ) )
                .ThenByDescending( i => i.name.Equals( "PlayableCharacterBase.cs" ) )
                .ThenByDescending( i => i.name.Equals( "NonPlayableCharacterBase.cs" ) )
                .ThenByDescending( i => i.name.Equals( "PlayerCharacter.cs" ) )
                .ThenByDescending( i => i.name.Equals( "EnemyCharacter.cs" ) )
                // Entities.Things/
                .ThenByDescending( i => i.name.Equals( "ThingBase.cs" ) )
                .ThenByDescending( i => i.name.Equals( "WeaponBase.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Gun.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Bullet.cs" ) )
                // Entities.Worlds/
                .ThenByDescending( i => i.name.Equals( "World.cs" ) )
                // Misc
                .ThenBy( i => i.path )
                .Select( i => i.path )
                .ToArray();

            //Debug.Log( string.Join( "\n", paths ) );
            foreach (var path in paths.Reverse()) {
                AssetDatabase.OpenAsset( AssetDatabase.LoadAssetAtPath<UnityEngine.Object>( path ) );
                Thread.Sleep( 100 );
            }
        }

        [MenuItem( "Project/Player Point", priority = 500 )]
        public static void PlayerPoint() {
            var ray = HandleUtility.GUIPointToWorldRay( GUIUtility.ScreenToGUIPoint( SceneView.lastActiveSceneView.cameraViewport.center ) );
            if (Physics.Raycast( ray, out var hit, 512, ~0, QueryTriggerInteraction.Ignore )) {
                var go = Selection.activeGameObject = new GameObject( "PlayerPoint", typeof( PlayerPoint ) );
                go.transform.position = hit.point;
            }
        }
        [MenuItem( "Project/Enemy Point", priority = 501 )]
        public static void EnemyPoint() {
            var ray = HandleUtility.GUIPointToWorldRay( GUIUtility.ScreenToGUIPoint( SceneView.lastActiveSceneView.cameraViewport.center ) );
            if (Physics.Raycast( ray, out var hit, 512, ~0, QueryTriggerInteraction.Ignore )) {
                var go = Selection.activeGameObject = new GameObject( "EnemyPoint", typeof( EnemyPoint ) );
                go.transform.position = hit.point;
            }
        }
        [MenuItem( "Project/Thing Point", priority = 502 )]
        public static void ThingPoint() {
            var ray = HandleUtility.GUIPointToWorldRay( GUIUtility.ScreenToGUIPoint( SceneView.lastActiveSceneView.cameraViewport.center ) );
            if (Physics.Raycast( ray, out var hit, 512, ~0, QueryTriggerInteraction.Ignore )) {
                var go = Selection.activeGameObject = new GameObject( "ThingPoint", typeof( ThingPoint ) );
                go.transform.position = hit.point;
            }
        }

    }
}
#endif
