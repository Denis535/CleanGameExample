#if UNITY_EDITOR
#nullable enable
namespace Project.Tools {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.AddressableAssets;
    using UnityEngine;

    public static class ProjectBuilder {

        // Build/Pre
        public static void PreBuild() {
            new ResourcesSourceGenerator().Generate( AddressableAssetSettingsDefaultObject.Settings, "Assets/Project.Common/UnityEngine.AddressableAssets/R.cs", "UnityEngine.AddressableAssets", "R" );
            new LabelsSourceGenerator().Generate( AddressableAssetSettingsDefaultObject.Settings, "Assets/Project.Common/UnityEngine.AddressableAssets/L.cs", "UnityEngine.AddressableAssets", "L" );
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
