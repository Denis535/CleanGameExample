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

        protected override VisualElement VisualElement => Widget;
        private Widget Widget { get; }
        private Label Title { get; }
        private VisualElement Content { get; }

        public MenuWidgetView() {
            Widget = VisualElementFactory.LeftWidget( "menu-widget" ).UserData( this ).Children(
                VisualElementFactory.Card().Children(
                    VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Menu" )
                    ),
                    Content = VisualElementFactory.Content()
                )
            );
        }
        public override void Dispose() {
            Content.GetViews().DisposeAll();
            base.Dispose();
        }

        public void AddView(UIViewBase2 view) {
            Content.AddView( view );
            Recalculate( Content.GetViews().ToArray() );
            Title.text = GetTitle( Content.GetViews().Last() );
        }
        public void RemoveView(UIViewBase2 view) {
            Content.RemoveView( view );
            Recalculate( Content.GetViews().ToArray() );
            Title.text = GetTitle( Content.GetViews().Last() );
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

        protected override VisualElement VisualElement => Scope;
        private ColumnScope Scope { get; }
        private Button StartGame { get; }
        private Button Settings { get; }
        private Button Quit { get; }

        public event EventCallback<ClickEvent> OnStartGame {
            add => StartGame.RegisterCallback( value );
            remove => StartGame.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnSettings {
            add => Settings.RegisterCallback( value );
            remove => Settings.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnQuit {
            add => Quit.RegisterCallback( value );
            remove => Quit.UnregisterCallback( value );
        }

        public MenuWidgetView_Menu() {
            Scope = VisualElementFactory.ColumnScope().UserData( this ).Children(
                StartGame = VisualElementFactory.Select( "Start Game" ),
                Settings = VisualElementFactory.Select( "Settings" ),
                Quit = VisualElementFactory.Quit( "Quit" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MenuWidgetView_StartGame : UIViewBase2 {

        protected override VisualElement VisualElement => Scope;
        private ColumnScope Scope { get; }
        private Button NewGame { get; }
        private Button Continue { get; }
        private Button Back { get; }

        public event EventCallback<ClickEvent> OnNewGame {
            add => NewGame.RegisterCallback( value );
            remove => NewGame.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnContinue {
            add => Continue.RegisterCallback( value );
            remove => Continue.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBack {
            add => Back.RegisterCallback( value );
            remove => Back.UnregisterCallback( value );
        }

        public MenuWidgetView_StartGame() {
            Scope = VisualElementFactory.ColumnScope().UserData( this ).Children(
                NewGame = VisualElementFactory.Select( "New Game" ),
                Continue = VisualElementFactory.Select( "Continue" ),
                Back = VisualElementFactory.Back( "Back" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MenuWidgetView_SelectLevel : UIViewBase2 {

        protected override VisualElement VisualElement => Scope;
        private ColumnScope Scope { get; }
        private Button Level1 { get; }
        private Button Level2 { get; }
        private Button Level3 { get; }
        private Button Back { get; }

        public event EventCallback<ClickEvent> OnLevel1 {
            add => Level1.RegisterCallback( value );
            remove => Level1.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnLevel2 {
            add => Level2.RegisterCallback( value );
            remove => Level2.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnLevel3 {
            add => Level3.RegisterCallback( value );
            remove => Level3.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBack {
            add => Back.RegisterCallback( value );
            remove => Back.UnregisterCallback( value );
        }

        public MenuWidgetView_SelectLevel() {
            Scope = VisualElementFactory.ColumnScope().UserData( this ).Children(
                VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).Children(
                    Level1 = VisualElementFactory.Select( "Level 1" ),
                    Level2 = VisualElementFactory.Select( "Level 2" ),
                    Level3 = VisualElementFactory.Select( "Level 3" )
                ),
                Back = VisualElementFactory.Back( "Back" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class MenuWidgetView_SelectCharacter : UIViewBase2 {

        protected override VisualElement VisualElement => Scope;
        private ColumnScope Scope { get; }
        private Button Gray { get; }
        private Button Red { get; }
        private Button Green { get; }
        private Button Blue { get; }
        private Button Back { get; }

        public event EventCallback<ClickEvent> OnGray {
            add => Gray.RegisterCallback( value );
            remove => Gray.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnRed {
            add => Red.RegisterCallback( value );
            remove => Red.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnGreen {
            add => Green.RegisterCallback( value );
            remove => Green.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBlue {
            add => Blue.RegisterCallback( value );
            remove => Blue.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBack {
            add => Back.RegisterCallback( value );
            remove => Back.UnregisterCallback( value );
        }

        public MenuWidgetView_SelectCharacter() {
            Scope = VisualElementFactory.ColumnScope().UserData( this ).Children(
                VisualElementFactory.ColumnScope().Classes( "margin-bottom-4px" ).Children(
                    Gray = VisualElementFactory.Select( "Gray" ),
                    Red = VisualElementFactory.Select( "Red" ),
                    Green = VisualElementFactory.Select( "Green" ),
                    Blue = VisualElementFactory.Select( "Blue" )
                ),
                Back = VisualElementFactory.Back( "Back" )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
