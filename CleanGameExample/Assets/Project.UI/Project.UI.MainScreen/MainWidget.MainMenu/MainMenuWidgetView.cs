#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class MainMenuWidgetView : UIViewBase {

        // View
        public ElementWrapper Widget { get; }
        public LabelWrapper Title { get; }
        public ViewStackSlotWrapper<UIViewBase> ViewsSlot { get; }

        // Constructor
        public MainMenuWidgetView(UIFactory factory) {
            VisualElement = factory.MainMenuWidget( out var widget, out var title, out var viewsSlot );
            Widget = widget.Wrap();
            Title = title.Wrap();
            ViewsSlot = viewsSlot.AsViewStackSlot<UIViewBase>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // MainMenuView
    public class MainMenuWidgetView_MainMenuView : UIViewBase {

        // View
        public ElementWrapper Scope { get; }
        public ButtonWrapper StartGame { get; }
        public ButtonWrapper Settings { get; }
        public ButtonWrapper Quit { get; }

        // Constructor
        public MainMenuWidgetView_MainMenuView(UIFactory factory) {
            VisualElement = factory.MainMenuWidget_MainMenuView( out var scope, out var startGame, out var settings, out var quit );
            Scope = scope.Wrap();
            StartGame = startGame.Wrap();
            Settings = settings.Wrap();
            Quit = quit.Wrap();
        }

    }
    // StartGameView
    public class MainMenuWidgetView_StartGameView : UIViewBase {

        // View
        public ElementWrapper Scope { get; }
        public ButtonWrapper NewGame { get; }
        public ButtonWrapper Continue { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public MainMenuWidgetView_StartGameView(UIFactory factory) {
            VisualElement = factory.MainMenuWidget_StartGameView( out var scope, out var newGame, out var @continue, out var back );
            Scope = scope.Wrap();
            NewGame = newGame.Wrap();
            Continue = @continue.Wrap();
            Back = back.Wrap();
        }

    }
    // SelectLevelView
    public class MainMenuWidgetView_SelectLevelView : UIViewBase {

        // View
        public ElementWrapper Scope { get; }
        public ButtonWrapper Level1 { get; }
        public ButtonWrapper Level2 { get; }
        public ButtonWrapper Level3 { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public MainMenuWidgetView_SelectLevelView(UIFactory factory) {
            VisualElement = factory.MainMenuWidget_SelectLevelView( out var scope, out var level1, out var level2, out var level3, out var back );
            Scope = scope.Wrap();
            Level1 = level1.Wrap();
            Level2 = level2.Wrap();
            Level3 = level3.Wrap();
            Back = back.Wrap();
        }

    }
    // SelectYourCharacterView
    public class MainMenuWidgetView_SelectYourCharacterView : UIViewBase {

        // View
        public ElementWrapper Scope { get; }
        public ButtonWrapper White { get; }
        public ButtonWrapper Red { get; }
        public ButtonWrapper Green { get; }
        public ButtonWrapper Blue { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public MainMenuWidgetView_SelectYourCharacterView(UIFactory factory) {
            VisualElement = factory.MainMenuWidgetView_SelectYourCharacterView( out var scope, out var white, out var red, out var green, out var blue, out var back );
            Scope = scope.Wrap();
            White = white.Wrap();
            Red = red.Wrap();
            Green = green.Wrap();
            Blue = blue.Wrap();
            Back = back.Wrap();
        }

    }
}
