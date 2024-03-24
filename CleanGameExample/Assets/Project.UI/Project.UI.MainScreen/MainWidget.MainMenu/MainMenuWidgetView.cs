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
        // MainPage
        public SlotWrapper MainPageSlot { get; }
        public ButtonWrapper MainPage_StartGame { get; }
        public ButtonWrapper MainPage_Settings { get; }
        public ButtonWrapper MainPage_Quit { get; }
        // StartGamePage
        public SlotWrapper StartGamePageSlot { get; }
        public ButtonWrapper StartGamePage_NewGame { get; }
        public ButtonWrapper StartGamePage_Continue { get; }
        public ButtonWrapper StartGamePage_Back { get; }

        // Constructor
        public MainMenuWidgetView(UIFactory factory) {
            VisualElement = factory.MainMenuWidget( out var widget, out var title, out var mainPageSlot, out var startGamePageSlot );
            Widget = widget.Wrap();
            Title = title.Wrap();
            // MainPage
            MainPageSlot = mainPageSlot.AsSlot();
            MainPageSlot.Add( factory.MainMenuWidget_MainPage( out _, out var startGame, out var settings, out var quit ) );
            MainPage_StartGame = startGame.Wrap();
            MainPage_Settings = settings.Wrap();
            MainPage_Quit = quit.Wrap();
            // StartGamePage
            StartGamePageSlot = startGamePageSlot.AsSlot();
            StartGamePageSlot.Add( factory.MainMenuWidget_StartGamePage( out _, out var newGame, out var @continue, out var back ) );
            StartGamePage_NewGame = newGame.Wrap();
            StartGamePage_Continue = @continue.Wrap();
            StartGamePage_Back = back.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
