#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainMenuWidgetView : UIViewBase {

        // VisualElement
        protected override VisualElement VisualElement { get; }
        public ElementWrapper Widget { get; }
        public LabelWrapper Title { get; }
        public ViewStackSlotWrapper<UIViewBase> PagesSlot { get; }

        // Constructor
        public MainMenuWidgetView(UIFactory factory) {
            VisualElement = factory.MainMenuWidget( out var widget, out var title, out var pagesSlot );
            Widget = widget.Wrap();
            Title = title.Wrap();
            PagesSlot = pagesSlot.AsViewStackSlot<UIViewBase>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    // MainMenuPage
    public class MainMenuWidgetView_MainMenuPage : UIViewBase {

        // VisualElement
        protected override VisualElement VisualElement { get; }
        public ElementWrapper Scope { get; }
        public ButtonWrapper StartGame { get; }
        public ButtonWrapper Settings { get; }
        public ButtonWrapper Quit { get; }

        // Constructor
        public MainMenuWidgetView_MainMenuPage(UIFactory factory) {
            VisualElement = factory.MainMenuWidget_MainMenuPage( out var scope, out var startGame, out var settings, out var quit );
            Scope = scope.Wrap();
            StartGame = startGame.Wrap();
            Settings = settings.Wrap();
            Quit = quit.Wrap();
        }

    }
    // StartGamePage
    public class MainMenuWidgetView_StartGamePage : UIViewBase {

        // VisualElement
        protected override VisualElement VisualElement { get; }
        public ElementWrapper Scope { get; }
        public ButtonWrapper NewGame { get; }
        public ButtonWrapper Continue { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public MainMenuWidgetView_StartGamePage(UIFactory factory) {
            VisualElement = factory.MainMenuWidget_StartGamePage( out var scope, out var newGame, out var @continue, out var back );
            Scope = scope.Wrap();
            NewGame = newGame.Wrap();
            Continue = @continue.Wrap();
            Back = back.Wrap();
        }

    }
    // SelectLevelPage
    public class MainMenuWidgetView_SelectLevelPage : UIViewBase {

        // VisualElement
        protected override VisualElement VisualElement { get; }
        public ElementWrapper Scope { get; }
        public ButtonWrapper Level1 { get; }
        public ButtonWrapper Level2 { get; }
        public ButtonWrapper Level3 { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public MainMenuWidgetView_SelectLevelPage(UIFactory factory) {
            VisualElement = factory.MainMenuWidget_SelectLevelPage( out var scope, out var level1, out var level2, out var level3, out var back );
            Scope = scope.Wrap();
            Level1 = level1.Wrap();
            Level2 = level2.Wrap();
            Level3 = level3.Wrap();
            Back = back.Wrap();
        }

    }
    // SelectYourCharacter
    public class MainMenuWidgetView_SelectYourCharacter : UIViewBase {

        // VisualElement
        protected override VisualElement VisualElement { get; }
        public ElementWrapper Scope { get; }
        public ButtonWrapper White { get; }
        public ButtonWrapper Red { get; }
        public ButtonWrapper Green { get; }
        public ButtonWrapper Blue { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public MainMenuWidgetView_SelectYourCharacter(UIFactory factory) {
            VisualElement = factory.MainMenuWidgetView_SelectYourCharacter( out var scope, out var white, out var red, out var green, out var blue, out var back );
            Scope = scope.Wrap();
            White = white.Wrap();
            Red = red.Wrap();
            Green = green.Wrap();
            Blue = blue.Wrap();
            Back = back.Wrap();
        }

    }
}
