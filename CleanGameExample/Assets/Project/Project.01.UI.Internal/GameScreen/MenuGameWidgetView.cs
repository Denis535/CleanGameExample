#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MenuGameWidgetView : UIViewBase {

        private readonly Widget widget;
        private readonly Label title;
        private readonly Button resume;
        private readonly Button settings;
        private readonly Button back;

        // Layer
        public override int Layer => 0;

        // Constructor
        public MenuGameWidgetView() {
            VisualElement = VisualElementFactory_Game.Menu( out widget, out title, out resume, out settings, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnEvent
        public void OnResume(EventCallback<ClickEvent> callback) {
            resume.OnClick( callback );
        }
        public void OnSettings(EventCallback<ClickEvent> callback) {
            settings.OnClick( callback );
        }
        public void OnBack(EventCallback<ClickEvent> callback) {
            back.OnClick( callback );
        }

    }
}
