#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MenuWidgetView : UIViewBase2 {

        private readonly Widget widget;
        private readonly Label title;
        private readonly VisualElement views;

        public UIViewBase[] Views => views.GetViews().ToArray();

        public MenuWidgetView() {
            VisualElement = VisualElementFactory_Main.Menu( out widget, out title, out views );
        }
        public override void Dispose() {
            Views.DisposeAll();
            base.Dispose();
        }

        public void AddView(UIViewBase2 view) {
            views.AddView( view );
            Recalculate( views.GetViews().ToArray() );
            title.text = GetTitle( views.GetViews().Last() );
        }
        public void RemoveView(UIViewBase2 view) {
            views.RemoveView( view );
            Recalculate( views.GetViews().ToArray() );
            title.text = GetTitle( views.GetViews().Last() );
        }

        // Helpers
        private static void Recalculate(UIViewBase2[] views) {
            foreach (var view in views) {
                if (view.HasFocusedElement()) {
                    view.SaveFocus();
                }
            }
            for (var i = 0; i < views.Length; i++) {
                var view = views[ i ];
                var next = views.ElementAtOrDefault( i + 1 );
                if (next == null) {
                    view.SetDisplayed( true );
                } else {
                    view.SetDisplayed( false );
                }
            }
            if (views.Any()) {
                var view = views.Last();
                if (!view.HasFocusedElement()) {
                    if (!view.LoadFocus()) {
                        view.Focus();
                    }
                }
            }
        }
        // Helpers
        private static string GetTitle(UIViewBase view) {
            if (view is MenuWidgetView_Menu) {
                return "Menu";
            }
            if (view is MenuWidgetView_StartGame) {
                return "Start Game";
            }
            if (view is MenuWidgetView_SelectLevel) {
                return "Select Level";
            }
            if (view is MenuWidgetView_SelectCharacter) {
                return "Select Your Character";
            }
            throw Exceptions.Internal.NotSupported( $"View {view} is not supported" );
        }

    }
    public class MenuWidgetView_Menu : UIViewBase2 {

        private readonly ColumnScope scope;
        private readonly Button startGame;
        private readonly Button settings;
        private readonly Button quit;

        public event EventCallback<ClickEvent> OnStartGame {
            add => startGame.RegisterCallback( value );
            remove => startGame.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnSettings {
            add => settings.RegisterCallback( value );
            remove => settings.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnQuit {
            add => quit.RegisterCallback( value );
            remove => quit.UnregisterCallback( value );
        }

        public MenuWidgetView_Menu() {
            VisualElement = VisualElementFactory_Main.Menu_Menu( out scope, out startGame, out settings, out quit );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MenuWidgetView_StartGame : UIViewBase2 {

        private readonly ColumnScope scope;
        private readonly Button newGame;
        private readonly Button @continue;
        private readonly Button back;

        public event EventCallback<ClickEvent> OnNewGame {
            add => newGame.RegisterCallback( value );
            remove => newGame.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnContinue {
            add => @continue.RegisterCallback( value );
            remove => @continue.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBack {
            add => back.RegisterCallback( value );
            remove => back.UnregisterCallback( value );
        }

        public MenuWidgetView_StartGame() {
            VisualElement = VisualElementFactory_Main.Menu_StartGame( out scope, out newGame, out @continue, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MenuWidgetView_SelectLevel : UIViewBase2 {

        private readonly ColumnScope scope;
        private readonly Button level1;
        private readonly Button level2;
        private readonly Button level3;
        private readonly Button back;

        public event EventCallback<ClickEvent> OnLevel1 {
            add => level1.RegisterCallback( value );
            remove => level1.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnLevel2 {
            add => level2.RegisterCallback( value );
            remove => level2.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnLevel3 {
            add => level3.RegisterCallback( value );
            remove => level3.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBack {
            add => back.RegisterCallback( value );
            remove => back.UnregisterCallback( value );
        }

        public MenuWidgetView_SelectLevel() {
            VisualElement = VisualElementFactory_Main.Menu_SelectLevel( out scope, out level1, out level2, out level3, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MenuWidgetView_SelectCharacter : UIViewBase2 {

        private readonly ColumnScope scope;
        private readonly Button gray;
        private readonly Button red;
        private readonly Button green;
        private readonly Button blue;
        private readonly Button back;

        public event EventCallback<ClickEvent> OnGray {
            add => gray.RegisterCallback( value );
            remove => gray.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnRed {
            add => red.RegisterCallback( value );
            remove => red.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnGreen {
            add => green.RegisterCallback( value );
            remove => green.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBlue {
            add => blue.RegisterCallback( value );
            remove => blue.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBack {
            add => back.RegisterCallback( value );
            remove => back.UnregisterCallback( value );
        }

        public MenuWidgetView_SelectCharacter() {
            VisualElement = VisualElementFactory_Main.Menu_SelectCharacter( out scope, out gray, out red, out green, out blue, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
