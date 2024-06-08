#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainMenuWidgetView : UIViewBase {

        private readonly Widget widget;
        private readonly Label title;
        private readonly VisualElement content;

        // Props
        public override int Priority => 0;
        public override bool IsAlwaysVisible => false;
        public override bool IsModal => false;
        // Props
        public string Title => title.text;

        // Constructor
        public MainMenuWidgetView() {
            VisualElement = VisualElementFactory_Main.MainMenuWidget( out widget, out title, out content );
        }
        public override void Dispose() {
            this.GetChildren().DisposeAll();
            base.Dispose();
        }

        // AddView
        public void AddView(UIViewBase view) {
            content.Add( view );
            Recalculate( content.Children().ToArray() );
            title.text = GetTitle( content.Children().Last().GetView() );
        }

        // RemoveView
        public void RemoveView(UIViewBase view) {
            content.Remove( view );
            Recalculate( content.Children().ToArray() );
            title.text = GetTitle( content.Children().Last().GetView() );
        }

        // Helpers
        private static void Recalculate(VisualElement[] children) {
            for (var i = 0; i < children.Length; i++) {
                var child = children[ i ];
                var next = children.ElementAtOrDefault( i + 1 );
                if (next == null) {
                    Show( child );
                } else {
                    Hide( child );
                }
            }
        }
        private static void Show(VisualElement element) {
            var view = element.GetView();
            view.SetDisplayed( true );
            if (!view.HasFocusedElement()) {
                if (!view.LoadFocus()) view.Focus();
            }
        }
        public static void Hide(VisualElement element) {
            var view = element.GetView();
            if (view.HasFocusedElement()) {
                view.SaveFocus();
            }
            view.SetDisplayed( false );
        }
        // Helpers
        private static string GetTitle(UIViewBase view) {
            if (view is MainMenuWidgetView_MainMenuView) {
                return "Main Menu";
            }
            if (view is MainMenuWidgetView_StartGameView) {
                return "Start Game";
            }
            if (view is MainMenuWidgetView_SelectLevelView) {
                return "Select Level";
            }
            if (view is MainMenuWidgetView_SelectCharacterView) {
                return "Select Your Character";
            }
            throw Exceptions.Internal.NotSupported( $"View {view} is not supported" );
        }

    }
    // MainMenuView
    public class MainMenuWidgetView_MainMenuView : UIViewBase {

        private readonly VisualElement view;
        private readonly Button startGame;
        private readonly Button settings;
        private readonly Button quit;

        // Props
        public override int Priority => 0;
        public override bool IsAlwaysVisible => false;
        public override bool IsModal => false;

        // Constructor
        public MainMenuWidgetView_MainMenuView() {
            VisualElement = VisualElementFactory_Main.MainMenuWidget_MainMenuView( out view, out startGame, out settings, out quit );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnEvent
        public void OnStartGame(EventCallback<ClickEvent> callback) {
            startGame.OnClick( callback );
        }
        public void OnSettings(EventCallback<ClickEvent> callback) {
            settings.OnClick( callback );
        }
        public void OnQuit(EventCallback<ClickEvent> callback) {
            quit.OnClick( callback );
        }

    }
    // StartGameView
    public class MainMenuWidgetView_StartGameView : UIViewBase {

        private readonly VisualElement view;
        private readonly Button newGame;
        private readonly Button @continue;
        private readonly Button back;

        // Props
        public override int Priority => 0;
        public override bool IsAlwaysVisible => false;
        public override bool IsModal => false;

        // Constructor
        public MainMenuWidgetView_StartGameView() {
            VisualElement = VisualElementFactory_Main.MainMenuWidget_StartGameView( out view, out newGame, out @continue, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnEvent
        public void OnNewGame(EventCallback<ClickEvent> callback) {
            newGame.OnClick( callback );
        }
        public void OnContinue(EventCallback<ClickEvent> callback) {
            @continue.OnClick( callback );
        }
        public void OnBack(EventCallback<ClickEvent> callback) {
            back.OnClick( callback );
        }

    }
    // SelectLevelView
    public class MainMenuWidgetView_SelectLevelView : UIViewBase {

        private readonly VisualElement view;
        private readonly Button level1;
        private readonly Button level2;
        private readonly Button level3;
        private readonly Button back;

        // Props
        public override int Priority => 0;
        public override bool IsAlwaysVisible => false;
        public override bool IsModal => false;

        // Constructor
        public MainMenuWidgetView_SelectLevelView() {
            VisualElement = VisualElementFactory_Main.MainMenuWidget_SelectLevelView( out view, out level1, out level2, out level3, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnEvent
        public void OnLevel1(EventCallback<ClickEvent> callback) {
            level1.OnClick( callback );
        }
        public void OnLevel2(EventCallback<ClickEvent> callback) {
            level2.OnClick( callback );
        }
        public void OnLevel3(EventCallback<ClickEvent> callback) {
            level3.OnClick( callback );
        }
        public void OnBack(EventCallback<ClickEvent> callback) {
            back.OnClick( callback );
        }

    }
    // SelectCharacterView
    public class MainMenuWidgetView_SelectCharacterView : UIViewBase {

        private readonly VisualElement view;
        private readonly Button gray;
        private readonly Button red;
        private readonly Button green;
        private readonly Button blue;
        private readonly Button back;

        // Props
        public override int Priority => 0;
        public override bool IsAlwaysVisible => false;
        public override bool IsModal => false;

        // Constructor
        public MainMenuWidgetView_SelectCharacterView() {
            VisualElement = VisualElementFactory_Main.MainMenuWidget_SelectCharacterView( out view, out gray, out red, out green, out blue, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnEvent
        public void OnGray(EventCallback<ClickEvent> callback) {
            gray.OnClick( callback );
        }
        public void OnRed(EventCallback<ClickEvent> callback) {
            red.OnClick( callback );
        }
        public void OnGreen(EventCallback<ClickEvent> callback) {
            green.OnClick( callback );
        }
        public void OnBlue(EventCallback<ClickEvent> callback) {
            blue.OnClick( callback );
        }
        public void OnBack(EventCallback<ClickEvent> callback) {
            back.OnClick( callback );
        }

    }
}
