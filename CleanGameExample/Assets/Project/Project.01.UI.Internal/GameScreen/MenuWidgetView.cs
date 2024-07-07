#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class MenuWidgetView : UIViewBase2 {

        private readonly Widget widget;
        private readonly Label title;
        private readonly Button resume;
        private readonly Button settings;
        private readonly Button back;

        public event EventCallback<ClickEvent> OnResumeEvent {
            add => resume.RegisterCallback( value );
            remove => resume.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnSettingsEvent {
            add => settings.RegisterCallback( value );
            remove => settings.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBackEvent {
            add => back.RegisterCallback( value );
            remove => back.UnregisterCallback( value );
        }

        public MenuWidgetView() {
            VisualElement = VisualElementFactory_Game.Menu( out widget, out title, out resume, out settings, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
