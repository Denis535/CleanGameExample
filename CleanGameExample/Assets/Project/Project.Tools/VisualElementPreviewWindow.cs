#if UNITY_EDITOR
#nullable enable
namespace Project.Tools {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Project.UI;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class VisualElementPreviewWindow : EditorWindow, ISerializationCallbackReceiver {
        private record VisualElementOption(string Name, Func<VisualElement> Provider);

        [SerializeField]
        private string? option_Serialized;

        // Option
        private VisualElementOption? Option { get; set; }
        private VisualElementOption[] Options { get; } = GetOptions().ToArray();

        // Show
        [MenuItem( "Project/Visual Element Preview", priority = 1000 )]
        public new static void Show() {
            var window = GetWindow<VisualElementPreviewWindow>( false, "Visual Element Preview", true );
            window.minSize = new Vector2( 800, 600 );
            window.maxSize = new Vector2( 1200, 800 );
        }

        // Awake
        public void Awake() {
        }
        public void OnDestroy() {
        }

        // OnBeforeSerialize
        public void OnBeforeSerialize() {
            option_Serialized = Option?.Name;
        }
        public void OnAfterDeserialize() {
            Option = Array.Find( Options, i => i.Name == option_Serialized );
        }

        // CreateGUI
        public void CreateGUI() {
            var container = (VisualElement?) null;
            rootVisualElement.Add( new IMGUIContainer( () => {
                using (new GUILayout.VerticalScope( GUI.skin.label )) {
                    using (var scope = new EditorGUI.ChangeCheckScope()) {
                        var index = EditorGUILayout.Popup( "Visual Element", Array.IndexOf( Options, Option ), Options.ConvertAll( i => i.Name ) );
                        Option = (VisualElementOption?) Options.ElementAtOrDefault( index );
                        if (scope.changed || container!.childCount == 0) {
                            container!.Clear();
                            container.Add( Option!.Provider() );
                        }
                    }
                }
            } ) );
            rootVisualElement.Add( container = new VisualElement().Style( i => i.backgroundColor = Color.black ).Style( i => i.marginLeft = i.marginRight = i.marginTop = i.marginBottom = new Length( 4, LengthUnit.Pixel ) ).Style( i => i.flexGrow = 1 ) );
        }

        // Helpers
        private static IEnumerable<VisualElementOption> GetOptions() {
            foreach (var method in typeof( UIFactory.Main ).GetMethods( BindingFlags.Public | BindingFlags.Static )) {
                var name = "Main/" + method.Name;
                Func<VisualElement> provider = () => (VisualElement) method.Invoke( null, new object?[ method.GetParameters().Length ] );
                yield return new VisualElementOption( name, provider );
            }
            foreach (var method in typeof( UIFactory.Game ).GetMethods( BindingFlags.Public | BindingFlags.Static )) {
                var name = "Game/" + method.Name;
                Func<VisualElement> provider = () => (VisualElement) method.Invoke( null, new object?[ method.GetParameters().Length ] );
                yield return new VisualElementOption( name, provider );
            }
            foreach (var method in typeof( UIFactory.Common ).GetMethods( BindingFlags.Public | BindingFlags.Static )) {
                var name = "Common/" + method.Name;
                Func<VisualElement> provider = () => (VisualElement) method.Invoke( null, new object?[ method.GetParameters().Length ] );
                yield return new VisualElementOption( name, provider );
            }
        }

    }
}
#endif
