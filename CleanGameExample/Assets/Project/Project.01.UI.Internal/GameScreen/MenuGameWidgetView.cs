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
        // Props
        public Observable<ClickEvent> OnResume => resume.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnSettings => settings.AsObservable<ClickEvent>();
        public Observable<ClickEvent> OnBack => back.AsObservable<ClickEvent>();

        // Constructor
        public MenuGameWidgetView() {
            VisualElement = VisualElementFactory_Game.Menu( out widget, out title, out resume, out settings, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
