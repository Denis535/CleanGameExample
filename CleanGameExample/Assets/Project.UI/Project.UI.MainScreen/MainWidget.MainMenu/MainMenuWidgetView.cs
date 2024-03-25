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
        public SlotWrapper PageView { get; }
        public MainMenuWidgetView_MainPage MainPage { get; }
        public MainMenuWidgetView_StartGamePage StartGamePage { get; }

        // Constructor
        public MainMenuWidgetView(UIFactory factory) {
            VisualElement = factory.MainMenuWidget( out var widget, out var title, out var pageView );
            Widget = widget.Wrap();
            Title = title.Wrap();
            PageView = pageView.AsSlot();
            PageView.Add( MainPage = new MainMenuWidgetView_MainPage( factory ) );
            PageView.Add( StartGamePage = new MainMenuWidgetView_StartGamePage( factory ) );
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

        // SetActive
        public void SetActive() {
            foreach (var child in VisualElement.parent.Children()) {
                child.SetEnabled( false );
                child.SetDisplayed( false );
            }
            VisualElement.SetEnabled( true );
            VisualElement.SetDisplayed( true );
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

        // SetActive
        public void SetActive() {
            foreach (var child in VisualElement.parent.Children()) {
                child.SetEnabled( false );
                child.SetDisplayed( false );
            }
            VisualElement.SetEnabled( true );
            VisualElement.SetDisplayed( true );
        }

    }
}
