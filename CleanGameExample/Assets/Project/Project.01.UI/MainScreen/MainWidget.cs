#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.UI.Common;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class MainWidget : UIWidgetBase2<MainWidgetView> {

        // View
        public override MainWidgetView View { get; }

        // Constructor
        public MainWidget(IDependencyContainer container) : base( container ) {
            View = CreateView( this );
            AddChild( new MenuWidget( Container ) );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnActivate
        protected override void OnActivate(object? argument) {
            try {
                ShowSelf();
            } catch (OperationCanceledException) {
            }
        }
        protected override void OnDeactivate(object? argument) {
            HideSelf();
        }

        // OnDescendantActivate
        protected override void OnBeforeDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantActivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnBeforeDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }
        protected override void OnAfterDescendantDeactivate(UIWidgetBase descendant, object? argument) {
        }

        // Update
        public void Update() {
            View.Effect = GetEffect( this );
        }
        public void LateUpdate() {
        }

        // Helpers
        private static MainWidgetView CreateView(MainWidget widget) {
            var view = new MainWidgetView();
            return view;
        }

        // Helpers
        private static (Color Color, Vector2 Translate, float Rotate, float Scale) GetEffect(MainWidget widget) {
            var view = widget.Descendants.Where( i => i.IsViewable ).Select( i => i.View! ).FirstOrDefault( i => i.IsAttached() && i.IsDisplayedInHierarchy() );
            // Menu
            if (view is MenuWidgetView menu) {
                view = menu.Views.FirstOrDefault( i => i.IsDisplayedInHierarchy() );
                if (view is MenuMainWidgetView_MenuView) {
                    return (Color.white, default, 0, 1.0f);
                }
                if (view is MenuMainWidgetView_StartGameView) {
                    return (Color.white, default, 1, 1.1f);
                }
                if (view is MenuMainWidgetView_SelectLevelView) {
                    return (Color.white, default, 2, 1.2f);
                }
                if (view is MenuMainWidgetView_SelectCharacterView) {
                    return (Color.white, default, 3, 1.3f);
                }
                return (Color.white, default, 3, 1.3f);
            }
            // Settings
            if (view is SettingsWidgetView settings) {
                if (settings.ProfileSettings!.IsDisplayedInHierarchy()) {
                    return (Color.white, default, 1, 1.1f);
                }
                if (settings.AudioSettings!.IsDisplayedInHierarchy()) {
                    return (Color.white, default, 1, 1.1f);
                }
                if (settings.VideoSettings!.IsDisplayedInHierarchy()) {
                    return (Color.white, default, 1, 1.1f);
                }
                return (Color.white, default, 1, 1.1f);
            }
            // Loading
            if (view is LoadingWidgetView loading) {
                return (Color.gray, default, 45, 2.5f);
            }
            return (Color.white, default, 0, 1.0f);
        }

    }
}
