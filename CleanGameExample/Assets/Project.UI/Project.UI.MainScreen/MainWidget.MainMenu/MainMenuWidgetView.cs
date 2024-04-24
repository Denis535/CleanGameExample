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
        public ViewStackSlotWrapper<UIViewBase> ContentSlot { get; }

        // Constructor
        public MainMenuWidgetView() {
            VisualElement = UIFactory.Main.MainMenuWidget( out var widget, out var title, out var contentSlot );
            Widget = widget.Wrap();
            Title = title.Wrap();
            ContentSlot = contentSlot.AsViewStackSlot<UIViewBase>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // MainMenuView
    public class MainMenuWidgetView_MainMenuView : UIViewBase {

        // View
        public ElementWrapper Root { get; }
        public ButtonWrapper StartGame { get; }
        public ButtonWrapper Settings { get; }
        public ButtonWrapper Quit { get; }

        // Constructor
        public MainMenuWidgetView_MainMenuView() {
            VisualElement = UIFactory.Main.MainMenuWidget_MainMenuView( out var root, out var startGame, out var settings, out var quit );
            Root = root.Wrap();
            StartGame = startGame.Wrap();
            Settings = settings.Wrap();
            Quit = quit.Wrap();
        }

    }
    // StartGameView
    public class MainMenuWidgetView_StartGameView : UIViewBase {

        // View
        public ElementWrapper Root { get; }
        public ButtonWrapper NewGame { get; }
        public ButtonWrapper Continue { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public MainMenuWidgetView_StartGameView() {
            VisualElement = UIFactory.Main.MainMenuWidget_StartGameView( out var root, out var newGame, out var @continue, out var back );
            Root = root.Wrap();
            NewGame = newGame.Wrap();
            Continue = @continue.Wrap();
            Back = back.Wrap();
        }

    }
    // SelectLevelView
    public class MainMenuWidgetView_SelectLevelView : UIViewBase {

        // View
        public ElementWrapper Root { get; }
        public ButtonWrapper Level1 { get; }
        public ButtonWrapper Level2 { get; }
        public ButtonWrapper Level3 { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public MainMenuWidgetView_SelectLevelView() {
            VisualElement = UIFactory.Main.MainMenuWidget_SelectLevelView( out var root, out var level1, out var level2, out var level3, out var back );
            Root = root.Wrap();
            Level1 = level1.Wrap();
            Level2 = level2.Wrap();
            Level3 = level3.Wrap();
            Back = back.Wrap();
        }

    }
    // SelectYourCharacterView
    public class MainMenuWidgetView_SelectYourCharacterView : UIViewBase {

        // View
        public ElementWrapper Root { get; }
        public ButtonWrapper Gray { get; }
        public ButtonWrapper Red { get; }
        public ButtonWrapper Green { get; }
        public ButtonWrapper Blue { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public MainMenuWidgetView_SelectYourCharacterView() {
            VisualElement = UIFactory.Main.MainMenuWidget_SelectYourCharacterView( out var root, out var gray, out var red, out var green, out var blue, out var back );
            Root = root.Wrap();
            Gray = gray.Wrap();
            Red = red.Wrap();
            Green = green.Wrap();
            Blue = blue.Wrap();
            Back = back.Wrap();
        }

    }
}
