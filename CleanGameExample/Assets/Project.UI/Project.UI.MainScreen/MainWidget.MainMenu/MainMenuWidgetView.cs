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
    public class MainMenuWidgetView_MainPage : UIViewBase {

        // VisualElement
        protected override VisualElement VisualElement { get; }
        public ElementWrapper Scope { get; }
        public ButtonWrapper StartGame { get; }
        public ButtonWrapper Settings { get; }
        public ButtonWrapper Quit { get; }

        // Constructor
        public MainMenuWidgetView_MainPage(UIFactory factory) {
            VisualElement = factory.MainMenuWidget_MainPage( out var scope, out var startGame, out var settings, out var quit );
            Scope = scope.Wrap();
            StartGame = startGame.Wrap();
            Settings = settings.Wrap();
            Quit = quit.Wrap();
        }

    }
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
}
