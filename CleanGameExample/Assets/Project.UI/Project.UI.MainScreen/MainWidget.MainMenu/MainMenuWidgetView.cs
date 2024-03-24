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
        private SlotWrapper PageView { get; }
        // MainPage
        public ElementWrapper MainPage { get; }
        public ButtonWrapper MainPage_StartGame { get; }
        public ButtonWrapper MainPage_Settings { get; }
        public ButtonWrapper MainPage_Quit { get; }
        // StartGamePage
        public ElementWrapper StartGamePage { get; }
        public ButtonWrapper StartGamePage_NewGame { get; }
        public ButtonWrapper StartGamePage_Continue { get; }
        public ButtonWrapper StartGamePage_Back { get; }

        // Constructor
        public MainMenuWidgetView(UIFactory factory) {
            VisualElement = factory.MainMenuWidget( out var widget, out var title, out var pageView );
            Widget = widget.Wrap();
            Title = title.Wrap();
            PageView = pageView.AsSlot();
            // MainPage
            PageView.Add( factory.MainMenuWidget_MainPage( out var mainPage, out var startGame, out var settings, out var quit ) );
            MainPage = mainPage.Wrap();
            MainPage_StartGame = startGame.Wrap();
            MainPage_Settings = settings.Wrap();
            MainPage_Quit = quit.Wrap();
            // StartGamePage
            PageView.Add( factory.MainMenuWidget_StartGamePage( out var startGamePage, out var newGame, out var @continue, out var back ) );
            StartGamePage = startGamePage.Wrap();
            StartGamePage_NewGame = newGame.Wrap();
            StartGamePage_Continue = @continue.Wrap();
            StartGamePage_Back = back.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
