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

        // SetWidgetEffect
        public void SetWidgetEffect(UIViewBase view) {
            // Menu
            if (view is MenuWidgetView menu) {
                view = menu.Views.FirstOrDefault( i => i.IsDisplayedInHierarchy() );
                if (view is MenuMainWidgetView_MenuView) {
                    SetWidgetEffect( Color.white, default, 0, 1.0f );
                    return;
                }
                if (view is MenuMainWidgetView_StartGameView) {
                    SetWidgetEffect( Color.white, default, 1, 1.1f );
                    return;
                }
                if (view is MenuMainWidgetView_SelectLevelView) {
                    SetWidgetEffect( Color.white, default, 2, 1.2f );
                    return;
                }
                if (view is MenuMainWidgetView_SelectCharacterView) {
                    SetWidgetEffect( Color.white, default, 3, 1.3f );
                    return;
                }
                return;
            }
            // Settings
            if (view is SettingsWidgetView settings) {
                if (settings.ProfileSettings!.IsDisplayedInHierarchy()) {
                    SetWidgetEffect( Color.white, default, 1, 1.1f );
                    return;
                }
                if (settings.AudioSettings!.IsDisplayedInHierarchy()) {
                    SetWidgetEffect( Color.white, default, 1, 1.1f );
                    return;
                }
                if (settings.VideoSettings!.IsDisplayedInHierarchy()) {
                    SetWidgetEffect( Color.white, default, 1, 1.1f );
                    return;
                }
                return;
            }
            // Loading
            if (view is LoadingWidgetView loading) {
                SetWidgetEffect( Color.gray, default, 45, 2.5f );
                return;
            }
        }
        private void SetWidgetEffect(Color color, Vector2 translate, float rotate, float scale) {
            widget.style.unityBackgroundImageTintColor = color;
            widget.style.translate = new Translate( translate.x, translate.y );
            widget.style.rotate = new Rotate( Angle.Degrees( rotate ) );
            widget.style.scale = new Scale( new Vector3( scale, scale, 1 ) );
        }

    }
}
