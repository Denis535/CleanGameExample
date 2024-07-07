#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public abstract class TotalsWidgetView : UIViewBase2 {

        public TotalsWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class TotalsWidgetView_LevelCompleted : TotalsWidgetView {

        private Widget widget;
        private Label title;
        private Label message;
        private Button @continue;
        private Button back;

        public event EventCallback<ClickEvent> OnContinueEvent {
            add => @continue.RegisterCallback( value );
            remove => @continue.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBackEvent {
            add => back.RegisterCallback( value );
            remove => back.UnregisterCallback( value );
        }

        public TotalsWidgetView_LevelCompleted() {
            VisualElement = VisualElementFactory_Game.Totals_LevelCompleted( out widget, out title, out message, out @continue, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class TotalsWidgetView_GameCompleted : TotalsWidgetView {

        private Widget widget;
        private Label title;
        private Label message;
        private Button okey;

        public event EventCallback<ClickEvent> OnOkeyEvent {
            add => okey.RegisterCallback( value );
            remove => okey.UnregisterCallback( value );
        }

        public TotalsWidgetView_GameCompleted() {
            VisualElement = VisualElementFactory_Game.Totals_GameCompleted( out widget, out title, out message, out okey );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class TotalsWidgetView_LevelFailed : TotalsWidgetView {

        private Widget widget;
        private Label title;
        private Label message;
        private Button retry;
        private Button back;

        public event EventCallback<ClickEvent> OnRetryEvent {
            add => retry.RegisterCallback( value );
            remove => retry.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBackEvent {
            add => back.RegisterCallback( value );
            remove => back.UnregisterCallback( value );
        }

        public TotalsWidgetView_LevelFailed() {
            VisualElement = VisualElementFactory_Game.Totals_LevelFailed( out widget, out title, out message, out retry, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
