#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public abstract class TotalsWidgetView : UIViewBase {

        // Layer
        public override int Layer => -1000;

        // Constructor
        public TotalsWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class WinTotalsGameWidgetView : TotalsWidgetView {

        private Widget widget;
        private Label title;
        private Label message;
        private Button nextLevel;
        private Button back;

        // Layer
        public override int Layer => -1000;

        // Constructor
        public WinTotalsGameWidgetView() {
            VisualElement = VisualElementFactory_Game.WinTotals( out widget, out title, out message, out nextLevel, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class LossTotalsGameWidgetView : TotalsWidgetView {

        private Widget widget;
        private Label title;
        private Label message;
        private Button tryAgain;
        private Button back;

        // Layer
        public override int Layer => -1000;

        // Constructor
        public LossTotalsGameWidgetView() {
            VisualElement = VisualElementFactory_Game.LossTotals( out widget, out title, out message, out tryAgain, out back );
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

        // Constructor
        public LevelCompletedWidgetView() {
            //VisualElement = VisualElementFactory_Game.LevelCompleted( out widget, out title, out message, out tryAgain, out back );
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

        // Constructor
        public GameCompletedWidgetView() {
            //VisualElement = VisualElementFactory_Game.GameCompleted( out widget, out title, out message, out tryAgain, out back );
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

        // Constructor
        public LevelFailedWidgetView() {
            //VisualElement = VisualElementFactory_Game.LevelFailed( out widget, out title, out message, out tryAgain, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
