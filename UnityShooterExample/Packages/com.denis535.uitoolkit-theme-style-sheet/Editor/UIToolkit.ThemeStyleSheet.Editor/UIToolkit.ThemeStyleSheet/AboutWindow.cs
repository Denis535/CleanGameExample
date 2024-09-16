#nullable enable
namespace UIToolkit.ThemeStyleSheet {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class AboutWindow : EditorWindow {

        [MenuItem( "Tools/UIToolkit Theme Style Sheet/About UIToolkit Theme Style Sheet", priority = 10000 )]
        public new static void Show() {
            var window = GetWindow<AboutWindow>( true, "About UIToolkit Theme Style Sheet", true );
            window.minSize = window.maxSize = new Vector2( 800, 600 );
        }

        public void OnGUI() {
            using (new GUILayout.VerticalScope( EditorStyles.helpBox )) {
                EditorGUILayout.LabelField( "Overview", EditorStyles.boldLabel );
                EditorGUILayout.LabelField( "The UIToolkit theme style sheet." );
                EditorGUILayout.Separator();

                EditorGUILayout.LabelField( "Links", EditorStyles.boldLabel );
                if (EditorGUILayout.LinkButton( "denis535.github.io" )) {
                    Application.OpenURL( "https://denis535.github.io" );
                }
                if (EditorGUILayout.LinkButton( "Unity Asset Store" )) {
                    Application.OpenURL( "https://assetstore.unity.com/publishers/90787" );
                }
                if (EditorGUILayout.LinkButton( "itch.io" )) {
                    Application.OpenURL( "https://denis535.itch.io/" );
                }
                if (EditorGUILayout.LinkButton( "Unity Package Registry" )) {
                    Application.OpenURL( "https://openupm.com/packages/?sort=downloads&q=denis535" );
                }
                if (EditorGUILayout.LinkButton( "YouTube" )) {
                    Application.OpenURL( "https://www.youtube.com/channel/UCLFdZl0pFkCkHpDWmodBUFg" );
                }
                if (EditorGUILayout.LinkButton( "GitHub" )) {
                    Application.OpenURL( "https://github.com/Denis535/UIToolkitThemeStyleSheet" );
                }
                EditorGUILayout.Separator();

                EditorGUILayout.LabelField( "If you want to support me", EditorStyles.boldLabel );
                EditorGUILayout.LabelField( "If you want to support me, please rate my packages, subscribe to my YouTube channel and like my videos." );
            }
        }

    }
}
