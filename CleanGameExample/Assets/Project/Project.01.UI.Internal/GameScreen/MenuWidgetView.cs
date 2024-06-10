#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MenuWidgetView : UIViewBase {

        private readonly Widget widget;
        private readonly Label title;
        private readonly Button resume;
        private readonly Button settings;
        private readonly Button back;

        // Props
        public override int Layer => 0;
        // Props
        public string Title => title.text;

        // Constructor
        public MenuWidgetView() {
            VisualElement = VisualElementFactory_Game.GameMenuWidget( out widget, out title, out resume, out settings, out back );
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
