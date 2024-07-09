#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Project.UI.Common;
    using Project.UI.GameScreen;
    using Project.UI.MainScreen;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class UIScreen : UIScreenBase2 {

        private new RootWidget Widget => (RootWidget?) base.Widget!;

        public UIScreen(IDependencyContainer container) : base( container, container.RequireDependency<UIDocument>(), container.RequireDependency<AudioSource>( "SfxAudioSource" ) ) {
            VisualElementFactory.OnPlayClick += evt => { };
            VisualElementFactory.OnPlaySelect += evt => { };
            VisualElementFactory.OnPlaySubmit += evt => { };
            VisualElementFactory.OnPlayCancel += evt => { };
            VisualElementFactory.OnPlayChange += evt => { };
            VisualElementFactory.OnPlayFocus += evt => { };
            VisualElementFactory.OnPlayDialog += evt => { };
            VisualElementFactory.OnPlayInfoDialog += evt => { };
            VisualElementFactory.OnPlayWarningDialog += evt => { };
            VisualElementFactory.OnPlayErrorDialog += evt => { };
            AddWidget( new RootWidget( container ) );
        }
        public override void Dispose() {
            Widget.RemoveSelf();
            base.Dispose();
        }

        public void OnFixedUpdate() {
        }
        public void OnUpdate() {
            foreach (var child in Widget.Children) {
                (child as MainWidget)?.OnUpdate();
                (child as GameWidget)?.OnUpdate();
            }
        }

        public void ShowMainScreen() {
            HideScreen();
            Widget.AddChild( new MainWidget( Container ) );
        }
        public void ShowGameScreen() {
            HideScreen();
            Widget.AddChild( new GameWidget( Container ) );
        }
        public void ShowLoadingScreen() {
            HideScreen();
            Widget.AddChild( new LoadingWidget( Container ) );
        }
        public void ShowUnloadingScreen() {
            HideScreen();
            Widget.AddChild( new UnloadingWidget( Container ) );
        }
        public void HideScreen() {
            Widget.RemoveChildren( i => i is not DialogWidgetBase );
        }

    }
    public class RootWidget : UIRootWidgetBase {

        // Constructor
        public RootWidget(IDependencyContainer container) : base( container ) {
            View = new RootWidgetView();
            View.OnSubmitEvent += OnSubmit;
            View.OnCancelEvent += OnCancel;
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

    }
    public class RootWidgetView : UIRootWidgetView {

        // Constructor
        public RootWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // GetPriority
        protected override int GetPriority(UIViewBase view) {
            return GetLayer( view );
        }

        // GetLayer
        protected override int GetLayer(UIViewBase view) {
            return view switch {
                // MainScreen
                MainScreen.MainWidgetView => 0,
                MainScreen.MenuWidgetView => 500,
                // GameScreen
                GameScreen.GameWidgetView => 0,
                GameScreen.TotalsWidgetView => 0,
                GameScreen.MenuWidgetView => 500,
                // Common
                Common.SettingsWidgetView => 500,
                Common.ProfileSettingsWidgetView => 500,
                Common.VideoSettingsWidgetView => 500,
                Common.AudioSettingsWidgetView => 500,
                Common.LoadingWidgetView => 999,
                Common.DialogWidgetViewBase => 1000,
                _ => 0
            };
        }

    }
}
