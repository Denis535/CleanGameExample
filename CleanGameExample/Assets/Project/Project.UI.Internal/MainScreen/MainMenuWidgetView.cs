#nullable enable
namespace Project.UI.MainScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MainMenuWidgetView : UIViewBase {

        private readonly Label title;
        private readonly VisualElement content;

        // Values
        public string Title { get => title.text; set => title.text = value; }

        // Constructor
        public MainMenuWidgetView() {
            VisualElement = ViewFactory.MainMenuWidget( out _, out title, out content );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // Push
        public void Push(MainMenuWidgetView_InitialView view) {
            content.Children().LastOrDefault()?.SetDisplayed( false );
            content.Add( view.__GetVisualElement__() );
        }
        public void Push(MainMenuWidgetView_StartGameView view) {
            content.Children().LastOrDefault()?.SetDisplayed( false );
            content.Add( view.__GetVisualElement__() );
        }
        public void Push(MainMenuWidgetView_SelectLevelView view) {
            content.Children().LastOrDefault()?.SetDisplayed( false );
            content.Add( view.__GetVisualElement__() );
        }
        public void Push(MainMenuWidgetView_SelectCharacterView view) {
            content.Children().LastOrDefault()?.SetDisplayed( false );
            content.Add( view.__GetVisualElement__() );
        }
        public void Pop() {
            content.Remove( content.Children().Last() );
            content.Children().LastOrDefault()?.SetDisplayed( true );
        }

    }
    // InitialView
    public class MainMenuWidgetView_InitialView : UIViewBase {

        private readonly Button startGame;
        private readonly Button settings;
        private readonly Button quit;

        // Constructor
        public MainMenuWidgetView_InitialView() {
            VisualElement = ViewFactory.MainMenuWidget_InitialView( out _, out startGame, out settings, out quit );
        }

        // OnEvent
        public void OnAttachToPanel(EventCallback<AttachToPanelEvent> callback) {
            VisualElement.OnAttachToPanel( callback );
        }
        public void OnStartGame(EventCallback<ClickEvent> callback) {
            startGame.OnClick( callback );
        }
        public void OnSettings(EventCallback<ClickEvent> callback) {
            settings.OnClick( callback );
        }
        public void OnQuit(EventCallback<ClickEvent> callback) {
            quit.OnClick( callback );
        }

    }
    // StartGameView
    public class MainMenuWidgetView_StartGameView : UIViewBase {

        private readonly Button newGame;
        private readonly Button @continue;
        private readonly Button back;

        // Constructor
        public MainMenuWidgetView_StartGameView() {
            VisualElement = ViewFactory.MainMenuWidget_StartGameView( out _, out newGame, out @continue, out back );
        }

        // OnEvent
        public void OnAttachToPanel(EventCallback<AttachToPanelEvent> callback) {
            VisualElement.OnAttachToPanel( callback );
        }
        public void OnNewGame(EventCallback<ClickEvent> callback) {
            newGame.OnClick( callback );
        }
        public void OnContinue(EventCallback<ClickEvent> callback) {
            @continue.OnClick( callback );
        }
        public void OnBack(EventCallback<ClickEvent> callback) {
            back.OnClick( callback );
        }

    }
    // SelectLevelView
    public class MainMenuWidgetView_SelectLevelView : UIViewBase {

        private readonly Button level1;
        private readonly Button level2;
        private readonly Button level3;
        private readonly Button back;

        // Constructor
        public MainMenuWidgetView_SelectLevelView() {
            VisualElement = ViewFactory.MainMenuWidget_SelectLevelView( out _, out level1, out level2, out level3, out back );
        }

        // OnEvent
        public void OnAttachToPanel(EventCallback<AttachToPanelEvent> callback) {
            VisualElement.OnAttachToPanel( callback );
        }
        public void OnLevel1(EventCallback<ClickEvent> callback) {
            level1.OnClick( callback );
        }
        public void OnLevel2(EventCallback<ClickEvent> callback) {
            level2.OnClick( callback );
        }
        public void OnLevel3(EventCallback<ClickEvent> callback) {
            level3.OnClick( callback );
        }
        public void OnBack(EventCallback<ClickEvent> callback) {
            back.OnClick( callback );
        }

    }
    // SelectCharacterView
    public class MainMenuWidgetView_SelectCharacterView : UIViewBase {

        private readonly Button gray;
        private readonly Button red;
        private readonly Button green;
        private readonly Button blue;
        private readonly Button back;

        // Constructor
        public MainMenuWidgetView_SelectCharacterView() {
            VisualElement = ViewFactory.MainMenuWidget_SelectCharacterView( out _, out gray, out red, out green, out blue, out back );
        }

        // OnEvent
        public void OnAttachToPanel(EventCallback<AttachToPanelEvent> callback) {
            VisualElement.OnAttachToPanel( callback );
        }
        public void OnGray(EventCallback<ClickEvent> callback) {
            gray.OnClick( callback );
        }
        public void OnRed(EventCallback<ClickEvent> callback) {
            red.OnClick( callback );
        }
        public void OnGreen(EventCallback<ClickEvent> callback) {
            green.OnClick( callback );
        }
        public void OnBlue(EventCallback<ClickEvent> callback) {
            blue.OnClick( callback );
        }
        public void OnBack(EventCallback<ClickEvent> callback) {
            back.OnClick( callback );
        }

    }
}
