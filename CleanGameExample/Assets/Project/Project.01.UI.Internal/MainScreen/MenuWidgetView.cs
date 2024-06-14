#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MenuWidgetView : UIViewBase {

        private readonly Widget widget;
        private readonly Label title;
        private readonly VisualElement views;

        // Layer
        public override int Layer => 0;
        // Props
        public UIViewBase[] Views => views.Children2().ToArray();

        // Constructor
        public MenuWidgetView() {
            VisualElement = VisualElementFactory_Main.Menu( out widget, out title, out views );
        }
        public override void Dispose() {
            views.Children2().DisposeAll();
            base.Dispose();
        }

        // AddView
        public void AddView(UIViewBase view) {
            views.Add( view );
            Recalculate( views.Children2().ToArray() );
            title.text = GetTitle( views.Children2().Last() );
        }
        public void RemoveView(UIViewBase view) {
            views.Remove( view );
            Recalculate( views.Children2().ToArray() );
            title.text = GetTitle( views.Children2().Last() );
        }

        // Helpers
        private static void Recalculate(UIViewBase[] views) {
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
            if (view is MenuMainWidgetView_MenuView) {
                return "Menu";
            }
            if (view is MenuMainWidgetView_StartGameView) {
                return "Start Game";
            }
            if (view is MenuMainWidgetView_SelectLevelView) {
                return "Select Level";
            }
            if (view is MenuMainWidgetView_SelectCharacterView) {
                return "Select Your Character";
            }
            throw Exceptions.Internal.NotSupported( $"View {view} is not supported" );
        }

    }
    // MenuView
    public class MenuMainWidgetView_MenuView : UIViewBase {

        private readonly VisualElement view;
        private readonly Button startGame;
        private readonly Button settings;
        private readonly Button quit;

        // Layer
        public override int Layer => throw Exceptions.Internal.NotImplemented( $"Property 'Layer' is not implemented" );
        // Props
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

        // Constructor
        public MenuMainWidgetView_MenuView() {
            VisualElement = VisualElementFactory_Main.Menu_Menu( out view, out startGame, out settings, out quit );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // StartGameView
    public class MenuMainWidgetView_StartGameView : UIViewBase {

        private readonly VisualElement view;
        private readonly Button newGame;
        private readonly Button @continue;
        private readonly Button back;

        // Layer
        public override int Layer => throw Exceptions.Internal.NotImplemented( $"Property 'Layer' is not implemented" );
        // Props
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

        // Constructor
        public MenuMainWidgetView_StartGameView() {
            VisualElement = VisualElementFactory_Main.Menu_StartGame( out view, out newGame, out @continue, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // SelectLevelView
    public class MenuMainWidgetView_SelectLevelView : UIViewBase {

        private readonly VisualElement view;
        private readonly Button level1;
        private readonly Button level2;
        private readonly Button level3;
        private readonly Button back;

        // Layer
        public override int Layer => throw Exceptions.Internal.NotImplemented( $"Property 'Layer' is not implemented" );
        // Props
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

        // Constructor
        public MenuMainWidgetView_SelectLevelView() {
            VisualElement = VisualElementFactory_Main.Menu_SelectLevel( out view, out level1, out level2, out level3, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // SelectCharacterView
    public class MenuMainWidgetView_SelectCharacterView : UIViewBase {

        private readonly VisualElement view;
        private readonly Button gray;
        private readonly Button red;
        private readonly Button green;
        private readonly Button blue;
        private readonly Button back;

        // Layer
        public override int Layer => throw Exceptions.Internal.NotImplemented( $"Property 'Layer' is not implemented" );
        // Props
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

        // Constructor
        public MenuMainWidgetView_SelectCharacterView() {
            VisualElement = VisualElementFactory_Main.Menu_SelectCharacter( out view, out gray, out red, out green, out blue, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
