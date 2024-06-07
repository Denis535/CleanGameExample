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

        // Props
        public override int Priority => 0;
        public override bool IsAlwaysVisible => true;
        public override bool IsModal => false;

        // Constructor
        public MainWidgetView() {
            VisualElement = VisualElementFactory_Main.MainWidget( out widget );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // SetEffect
        public void SetEffect(UIViewBase view) {
            // MainMenuWidgetView
            if (view is MainMenuWidgetView mainMenuWidgetView) {
                view = mainMenuWidgetView.GetChildren().FirstOrDefault( i => i.IsActive() && i.IsDisplayedInHierarchy() );
                if (view is MainMenuWidgetView_MainMenuView) {
                    SetEffect( widget, Color.white, default, 0, 1.0f );
                    return;
                }
                if (view is MainMenuWidgetView_StartGameView) {
                    SetEffect( widget, Color.white, default, 1, 1.1f );
                    return;
                }
                if (view is MainMenuWidgetView_SelectLevelView) {
                    SetEffect( widget, Color.white, default, 2, 1.2f );
                    return;
                }
                if (view is MainMenuWidgetView_SelectCharacterView) {
                    SetEffect( widget, Color.white, default, 3, 1.3f );
                    return;
                }
                return;
            }
            // SettingsWidgetView
            if (view is SettingsWidgetView settingsWidgetView) {
                view = settingsWidgetView.GetChildren().FirstOrDefault( i => i.IsActive() && i.IsDisplayedInHierarchy() );
                if (view is ProfileSettingsWidgetView) {
                    SetEffect( widget, Color.white, default, 1, 1.1f );
                    return;
                }
                if (view is AudioSettingsWidgetView) {
                    SetEffect( widget, Color.white, default, 1, 1.1f );
                    return;
                }
                if (view is VideoSettingsWidgetView) {
                    SetEffect( widget, Color.white, default, 1, 1.1f );
                    return;
                }
                return;
            }
            // LoadingWidgetView
            if (view is LoadingWidgetView loadingWidgetView) {
                SetEffect( widget, Color.gray, default, 45, 2.5f );
                return;
            }
        }

        // Helpers
        private static void SetEffect(VisualElement element, Color color, Vector2 translate, float rotate, float scale) {
            element.style.unityBackgroundImageTintColor = color;
            element.style.translate = new Translate( translate.x, translate.y );
            element.style.rotate = new Rotate( Angle.Degrees( rotate ) );
            element.style.scale = new Scale( new Vector3( scale, scale, 1 ) );
        }

    }
}
