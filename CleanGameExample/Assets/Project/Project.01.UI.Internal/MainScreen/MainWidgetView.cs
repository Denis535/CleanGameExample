#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainWidgetView : UIViewBase {

        private readonly Widget widget;

        // Layer
        public override int Layer => -1000;

        // Constructor
        public MainWidgetView() {
            VisualElement = VisualElementFactory_Main.Main( out widget );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // SetBackgroundEffect
        public void SetBackgroundEffect(UIViewBase view) {
            // Menu
            if (view is MenuWidgetView menu) {
                view = menu.GetChildren().FirstOrDefault( i => i.IsAttached() && i.IsDisplayedInHierarchy() );
                if (view is MenuMainWidgetView_MenuView) {
                    SetBackgroundEffect( widget, Color.white, default, 0, 1.0f );
                    return;
                }
                if (view is MenuMainWidgetView_StartGameView) {
                    SetBackgroundEffect( widget, Color.white, default, 1, 1.1f );
                    return;
                }
                if (view is MenuMainWidgetView_SelectLevelView) {
                    SetBackgroundEffect( widget, Color.white, default, 2, 1.2f );
                    return;
                }
                if (view is MenuMainWidgetView_SelectCharacterView) {
                    SetBackgroundEffect( widget, Color.white, default, 3, 1.3f );
                    return;
                }
                return;
            }
            // Settings
            if (view is SettingsWidgetView settings) {
                view = settings.GetChildren().FirstOrDefault( i => i.IsAttached() && i.IsDisplayedInHierarchy() );
                if (view is ProfileSettingsWidgetView) {
                    SetBackgroundEffect( widget, Color.white, default, 1, 1.1f );
                    return;
                }
                if (view is AudioSettingsWidgetView) {
                    SetBackgroundEffect( widget, Color.white, default, 1, 1.1f );
                    return;
                }
                if (view is VideoSettingsWidgetView) {
                    SetBackgroundEffect( widget, Color.white, default, 1, 1.1f );
                    return;
                }
                return;
            }
            // Loading
            if (view is LoadingWidgetView loading) {
                SetBackgroundEffect( widget, Color.gray, default, 45, 2.5f );
                return;
            }
        }

        // Helpers
        private static void SetBackgroundEffect(VisualElement background, Color color, Vector2 translate, float rotate, float scale) {
            background.style.unityBackgroundImageTintColor = color;
            background.style.translate = new Translate( translate.x, translate.y );
            background.style.rotate = new Rotate( Angle.Degrees( rotate ) );
            background.style.scale = new Scale( new Vector3( scale, scale, 1 ) );
        }

    }
}
