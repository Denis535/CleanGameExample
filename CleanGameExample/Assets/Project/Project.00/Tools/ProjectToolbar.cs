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

    public static class ProjectToolbar {

        // LoadScene
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

        // LoadScene
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

        // OpenAll
        [MenuItem( "Project/Open All", priority = 300 )]
        public static void OpenAll() {
            var paths = AssetDatabase.GetAllAssetPaths()
                .Where( i => i.StartsWith( "Assets/Project/" ) && Path.GetExtension( i ) == ".cs" )
                .Select( i => new {
                    path = i,
                    dir = Path.GetDirectoryName( i ).Replace( '\\', '/' ) + '/',
                    name = Path.GetFileName( i )
                } )

                .OrderByDescending( i => i.dir.StartsWith( "Assets/Project/Project.00/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project/Project.01.UI/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project/Project.01.UI.Internal/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project/Project.02.App/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project/Project.03.Entities/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project/Project.03.Entities.Characters/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project/Project.03.Entities.Things/" ) )
                .ThenByDescending( i => i.dir.StartsWith( "Assets/Project/Project.03.Entities.Worlds/" ) )

                .ThenByDescending( i => i.dir.EndsWith( ".UI/" ) )
                .ThenByDescending( i => i.dir.EndsWith( ".UI/MainScreen/" ) )
                .ThenByDescending( i => i.dir.EndsWith( ".UI/GameScreen/" ) )
                .ThenByDescending( i => i.dir.EndsWith( ".UI/Common/" ) )

                .ThenByDescending( i => i.dir.EndsWith( ".UI.Internal/" ) )
                .ThenByDescending( i => i.dir.EndsWith( ".UI.Internal/MainScreen/" ) )
                .ThenByDescending( i => i.dir.EndsWith( ".UI.Internal/GameScreen/" ) )
                .ThenByDescending( i => i.dir.EndsWith( ".UI.Internal/Common/" ) )

                .ThenByDescending( i => i.name.Equals( "UITheme.cs" ) )
                .ThenByDescending( i => i.name.Equals( "UIScreen.cs" ) )
                .ThenByDescending( i => i.name.Equals( "UIRouter.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Widget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "View.cs" ) )
                .ThenByDescending( i => i.name.Equals( "VisualElementFactory.cs" ) )
                .ThenByDescending( i => i.name.Equals( "VisualElementFactory_Main.cs" ) )
                .ThenByDescending( i => i.name.Equals( "VisualElementFactory_Game.cs" ) )
                .ThenByDescending( i => i.name.Equals( "VisualElementFactory_Common.cs" ) )

                .ThenByDescending( i => i.name.Equals( "MainWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "MenuWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "LoadingWidget.cs" ) )

                .ThenByDescending( i => i.name.Equals( "GameWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "TotalsWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "WinWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "LossWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "MenuWidget.cs" ) )

                .ThenByDescending( i => i.name.Equals( "DialogWidgetBase.cs" ) )
                .ThenByDescending( i => i.name.Equals( "SettingsWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "ProfileSettingsWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "VideoSettingsWidget.cs" ) )
                .ThenByDescending( i => i.name.Equals( "AudioSettingsWidget.cs" ) )

                .ThenByDescending( i => i.name.Equals( "MainWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "MenuWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "LoadingWidgetView.cs" ) )

                .ThenByDescending( i => i.name.Equals( "GameWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "TotalsWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "WinWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "LossWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "MenuWidgetView.cs" ) )

                .ThenByDescending( i => i.name.Equals( "DialogWidgetViewBase.cs" ) )
                .ThenByDescending( i => i.name.Equals( "SettingsWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "ProfileSettingsWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "VideoSettingsWidgetView.cs" ) )
                .ThenByDescending( i => i.name.Equals( "AudioSettingsWidgetView.cs" ) )

                .ThenByDescending( i => i.name.Equals( "Application2.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Storage.cs" ) )

                .ThenByDescending( i => i.name.Equals( "EntityFactory.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Game.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Player.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Camera.cs" ) )

                .ThenByDescending( i => i.name.Equals( "Character.cs" ) )
                .ThenByDescending( i => i.name.Equals( "PlayerCharacter.cs" ) )
                .ThenByDescending( i => i.name.Equals( "EnemyCharacter.cs" ) )

                .ThenByDescending( i => i.name.Equals( "Thing.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Gun.cs" ) )
                .ThenByDescending( i => i.name.Equals( "Bullet.cs" ) )

                .ThenByDescending( i => i.name.Equals( "World.cs" ) )

                .ThenBy( i => i.path )
                .Select( i => i.path )
                .ToArray();

            //Debug.Log( string.Join( "\n", paths ) );
            foreach (var path in paths.Reverse()) {
                AssetDatabase.OpenAsset( AssetDatabase.LoadAssetAtPath<UnityEngine.Object>( path ) );
                Thread.Sleep( 100 );
            }
        }

        // Point
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
