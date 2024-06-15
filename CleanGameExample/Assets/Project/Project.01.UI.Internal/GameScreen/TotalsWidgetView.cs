#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public abstract class TotalsWidgetView : UIViewBase {

        // Constructor
        public TotalsWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class LevelCompletedWidgetView : TotalsWidgetView {

        private Widget widget;
        private Label title;
        private Label message;
        private Button @continue;
        private Button back;

        // Layer
        public override int Layer => -1000;
        // Events
        public event EventCallback<ClickEvent> OnContinue {
            add => @continue.RegisterCallback( value );
            remove => @continue.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBack {
            add => back.RegisterCallback( value );
            remove => back.UnregisterCallback( value );
        }

        // Constructor
        public LevelCompletedWidgetView() {
            VisualElement = VisualElementFactory_Game.LevelCompleted( out widget, out title, out message, out @continue, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class GameCompletedWidgetView : TotalsWidgetView {

        private Widget widget;
        private Label title;
        private Label message;
        private Button okey;

        // Layer
        public override int Layer => -1000;
        // Events
        public event EventCallback<ClickEvent> OnOkey {
            add => okey.RegisterCallback( value );
            remove => okey.UnregisterCallback( value );
        }

        // Constructor
        public GameCompletedWidgetView() {
            VisualElement = VisualElementFactory_Game.GameCompleted( out widget, out title, out message, out okey );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class LevelFailedWidgetView : TotalsWidgetView {

        private Widget widget;
        private Label title;
        private Label message;
        private Button retry;
        private Button back;

        // Layer
        public override int Layer => -1000;
        // Events
        public event EventCallback<ClickEvent> OnRetry {
            add => retry.RegisterCallback( value );
            remove => retry.UnregisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBack {
            add => back.RegisterCallback( value );
            remove => back.UnregisterCallback( value );
        }

        // Constructor
        public LevelFailedWidgetView() {
            VisualElement = VisualElementFactory_Game.LevelFailed( out widget, out title, out message, out retry, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
