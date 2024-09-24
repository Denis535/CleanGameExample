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
            SetWidget( new RootWidget( container ) );
        }
        public override void Dispose() {
            SetWidget( null );
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
            Widget.Clear();
        }

    }
    public class RootWidget : UIRootWidgetBase<RootWidgetView> {

        public RootWidget(IDependencyContainer container) : base( container ) {
            View = new RootWidgetView();
            View.OnSubmitEvent += OnSubmit;
            View.OnCancelEvent += OnCancel;
        }
        public override void Dispose() {
            View.Dispose();
            base.Dispose();
        }

        internal void AddChild(MainWidget widget) {
            base.AddChild( widget );
        }
        internal void AddChild(GameWidget widget) {
            base.AddChild( widget );
        }
        internal void AddChild(LoadingWidget widget) {
            base.AddChild( widget );
        }
        internal void AddChild(UnloadingWidget widget) {
            base.AddChild( widget );
        }
        internal void AddChild(WarningDialogWidget widget) {
            base.AddChild( widget );
        }
        internal void AddChild(ErrorDialogWidget widget) {
            base.AddChild( widget );
        }
        internal void Clear() {
            RemoveChildren( i => i is not (DialogWidget or InfoDialogWidget or WarningDialogWidget or ErrorDialogWidget) );
        }

        protected override void Sort(List<UIWidgetBase> children) {
            children.Sort( (a, b) => Comparer<int>.Default.Compare( GetOrderOf( a ), GetOrderOf( b ) ) );
        }
        private static int GetOrderOf(UIWidgetBase widget) {
            return widget switch {
                MainWidget or GameWidget or LoadingWidget or UnloadingWidget => 0,
                _ => 1,
            };
        }

    }
    public class RootWidgetView : UIRootWidgetViewBase {

        public RootWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override int GetOrderOf(UIViewBase view) {
            return GetLayerOf( view );
        }

        protected override int GetLayerOf(UIViewBase view) {
            return view switch {
                // MainScreen
                MainScreen.MainWidgetView => 0,
                MainScreen.MenuWidgetView => 100,
                // GameScreen
                GameScreen.GameWidgetView => 0,
                GameScreen.TotalsWidgetView => 0,
                GameScreen.MenuWidgetView => 100,
                // Common
                Common.LoadingWidgetView => 100,
                Common.UnloadingWidgetView => 100,
                // Common
                Common.SettingsWidgetView => 100,
                Common.ProfileSettingsWidgetView => 100,
                Common.VideoSettingsWidgetView => 100,
                Common.AudioSettingsWidgetView => 100,
                // Common
                Common.DialogWidgetView => 1000,
                Common.InfoDialogWidgetView => 1001,
                Common.WarningDialogWidgetView => 1002,
                Common.ErrorDialogWidgetView => 1003,
                _ => 0
            };
        }

    }
}
