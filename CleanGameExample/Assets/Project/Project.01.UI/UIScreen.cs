#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.UI.Common;
    using Project.UI.GameScreen;
    using Project.UI.MainScreen;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class UIScreen : UIScreenBase2 {

        // Constructor
        public UIScreen(IDependencyContainer container) : base( container, container.RequireDependency<UIDocument>(), container.RequireDependency<AudioSource>( "SfxAudioSource" ) ) {
            AddWidget( new RootWidget( container ) );
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
        }
        public override void Dispose() {
            Widget.RemoveSelf();
            base.Dispose();
        }

        // ShowWidget
        public void ShowMainWidget() {
            Widget.RemoveChildren( i => i is not DialogWidgetBase );
            Widget.AddChild( new MainWidget( Container ) );
        }
        public void ShowGameWidget() {
            Widget.RemoveChildren( i => i is not DialogWidgetBase );
            Widget.AddChild( new GameWidget( Container ) );
        }
        public void ShowLoadingWidget() {
            Widget.RemoveChildren( i => i is not DialogWidgetBase );
            Widget.AddChild( new LoadingWidget( Container ) );
        }
        public void ShowUnloadingWidget() {
            Widget.RemoveChildren( i => i is not DialogWidgetBase );
            Widget.AddChild( new UnloadingWidget( Container ) );
        }
        public void HideWidget() {
            Widget.RemoveChildren( i => i is not DialogWidgetBase );
        }

        // Update
        public void Update() {
            foreach (var child in Widget.Children) {
                (child as MainWidget)?.Update();
                (child as GameWidget)?.Update();
            }
        }
        public void LateUpdate() {
            foreach (var child in Widget.Children) {
                (child as MainWidget)?.LateUpdate();
                (child as GameWidget)?.LateUpdate();
            }
        }

    }
    public class RootWidget : UIRootWidget<RootWidgetView> {

        // View
        public override RootWidgetView View { get; }

        // Constructor
        public RootWidget(IDependencyContainer container) : base( container ) {
            View = CreateView();
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Helpers
        protected static RootWidgetView CreateView() {
            var view = new RootWidgetView();
            view.OnSubmitEvent += OnSubmit;
            view.OnCancelEvent += OnCancel;
            return view;
        }
        // Helpers
        protected static new void OnSubmit(NavigationSubmitEvent evt) {
            var button = evt.target as Button;
            if (button != null) {
                Click( button );
                evt.StopPropagation();
            }
        }
        protected static new void OnCancel(NavigationCancelEvent evt) {
            var widget = ((VisualElement) evt.target).GetAncestorsAndSelf().OfType<Widget>().Where( i => i.enabledInHierarchy && i.IsDisplayedInHierarchy() ).FirstOrDefault();
            var button = widget?.Query<Button>().Where( i => i.enabledInHierarchy && i.IsDisplayedInHierarchy() ).Where( IsCancel ).First();
            if (button != null) {
                Click( button );
                evt.StopPropagation();
            }
        }
        // Helpers
        protected static new bool IsCancel(Button button) {
            return button.ClassListContains( "resume" ) ||
                button.ClassListContains( "cancel" ) ||
                button.ClassListContains( "back" ) ||
                button.ClassListContains( "no" ) ||
                button.ClassListContains( "exit" ) ||
                button.ClassListContains( "quit" );
        }
        protected static new void Click(Button button) {
            using (var evt = ClickEvent.GetPooled()) {
                evt.target = button;
                button.SendEvent( evt );
            }
        }

    }
    public class RootWidgetView : UIRootWidgetView {

        // Constructor
        public RootWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Recalculate
        protected override void Recalculate(VisualElement widget) {
            base.Recalculate( widget );
        }
        protected override void Recalculate(UIViewBase2[] views) {
            base.Recalculate( views );
        }
        protected override void Recalculate(UIViewBase2 view, UIViewBase2 next) {
            base.Recalculate( view, next );
        }
        protected override void Recalculate(UIViewBase2 view) {
            base.Recalculate( view );
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
