#if UNITY_EDITOR
#nullable enable
namespace Project {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public static class ProjectBuilder {

        // Build/Pre
        public static void PreBuild() {
            var generator = AssetDatabase.LoadAssetAtPath<AddressableSourceGenerator>( AssetDatabase.FindAssets( "t:AddressableSourceGenerator" ).Single().Chain( AssetDatabase.GUIDToAssetPath ) );
            generator.Generate();
        }

        // Build/Development
        public static void BuildDevelopment(string path) {
            PreBuild();
            BuildPipeline.BuildPlayer(
                EditorBuildSettings.scenes,
                path,
                BuildTarget.StandaloneWindows64,
                BuildOptions.Development |
                BuildOptions.AllowDebugging |
                BuildOptions.ShowBuiltPlayer
                );
        }

        // Build/Production
        public static void BuildProduction(string path) {
            PreBuild();
            BuildPipeline.BuildPlayer(
                EditorBuildSettings.scenes,
                path,
                BuildTarget.StandaloneWindows64,
                BuildOptions.CleanBuildCache |
                BuildOptions.ShowBuiltPlayer
                );
        }

    }
}
#endif
