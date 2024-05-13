#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class GameMenuWidgetView : UIViewBase {

        private readonly Label title;
        private readonly Button resume;
        private readonly Button settings;
        private readonly Button back;

        // Values
        public string Title => title.text;

        // Constructor
        public GameMenuWidgetView() {
            VisualElement = ViewFactory.GameMenuWidget( out _, out title, out resume, out settings, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnEvent
        public void OnResumeClick(EventCallback<ClickEvent> callback) {
            resume.OnClick( callback );
        }
        public void OnSettingsClick(EventCallback<ClickEvent> callback) {
            settings.OnClick( callback );
        }
        public void OnBackClick(EventCallback<ClickEvent> callback) {
            back.OnClick( callback );
        }

    }
}
