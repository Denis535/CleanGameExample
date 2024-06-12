#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MenuMainWidgetView : UIViewBase {

        private readonly Widget widget;
        private readonly Label title;
        private readonly VisualElement content;

        // Layer
        public override int Layer => 0;

        // Constructor
        public MenuMainWidgetView() {
            VisualElement = VisualElementFactory_Main.Menu( out widget, out title, out content );
        }
        public override void Dispose() {
            this.GetChildren().DisposeAll();
            base.Dispose();
        }

        // AddView
        public void AddView(UIViewBase view) {
            content.Add( view );
            Recalculate( content.Children().Select( i => i.GetView() ).ToArray() );
            title.text = GetTitle( content.Children().Last().GetView() );
        }
        public void RemoveView(UIViewBase view) {
            content.Remove( view );
            Recalculate( content.Children().Select( i => i.GetView() ).ToArray() );
            title.text = GetTitle( content.Children().Last().GetView() );
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
        public Observable<ClickEvent> OnStartGame => startGame.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnSettings => settings.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnQuit => quit.AsObservable<ClickEvent>();

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
        public Observable<ClickEvent> OnNewGame => newGame.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnContinue => @continue.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnBack => back.AsObservable<ClickEvent>();

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
        public Observable<ClickEvent> OnLevel1 => level1.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnLevel2 => level2.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnLevel3 => level3.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnBack => back.AsObservable<ClickEvent>();

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
        public Observable<ClickEvent> OnGray => gray.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnRed => red.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnGreen => green.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnBlue => blue.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnBack => back.AsObservable<ClickEvent>();

        // Constructor
        public MenuMainWidgetView_SelectCharacterView() {
            VisualElement = VisualElementFactory_Main.Menu_SelectCharacter( out view, out gray, out red, out green, out blue, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
