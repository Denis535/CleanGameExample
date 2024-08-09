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

        protected override VisualElement VisualElement => Widget;
        public Widget Widget { get; }
        public Label Title { get; }
        public VisualElement Content { get; }

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
            Content.Children().ToViews().DisposeAll();
            base.Dispose();
        }

        public void AddView(UIViewBase view) {
            Content.Add( view );
            Recalculate( Content.Children().ToViews().ToArray() );
            Title.text = GetTitle( Content.Children().ToViews().Last() );
        }
        public void RemoveView(UIViewBase view) {
            Content.Remove( view );
            Recalculate( Content.Children().ToViews().ToArray() );
            Title.text = GetTitle( Content.Children().ToViews().Last() );
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
    public class MenuWidgetView_Menu : UIViewBase {

        protected override VisualElement VisualElement => Scope;
        public ColumnScope Scope { get; }
        public Button StartGame { get; }
        public Button Settings { get; }
        public Button Quit { get; }

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
    public class MenuWidgetView_StartGame : UIViewBase {

        protected override VisualElement VisualElement => Scope;
        public ColumnScope Scope { get; }
        public Button NewGame { get; }
        public Button Continue { get; }
        public Button Back { get; }

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
    public class MenuWidgetView_SelectLevel : UIViewBase {

        protected override VisualElement VisualElement => Scope;
        public ColumnScope Scope { get; }
        public Button Level1 { get; }
        public Button Level2 { get; }
        public Button Level3 { get; }
        public Button Back { get; }

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
    public class MenuWidgetView_SelectCharacter : UIViewBase {

        protected override VisualElement VisualElement => Scope;
        public ColumnScope Scope { get; }
        public Button Gray { get; }
        public Button Red { get; }
        public Button Green { get; }
        public Button Blue { get; }
        public Button Back { get; }

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
