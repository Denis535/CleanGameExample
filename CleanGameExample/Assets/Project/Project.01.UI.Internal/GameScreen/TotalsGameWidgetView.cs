#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public abstract class TotalsGameWidgetView : UIViewBase {

        // Layer
        public override int Layer => -1000;

        // Constructor
        public TotalsGameWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class WinTotalsGameWidgetView : TotalsGameWidgetView {

        private Widget widget;
        private Label title;
        private Label message;
        private Button nextLevel;
        private Button back;

        // Props
        public override int Layer => -1000;

        // Constructor
        public WinTotalsGameWidgetView() {
            VisualElement = VisualElementFactory_Game.WinTotalsWidget( out widget, out title, out message, out nextLevel, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public class LossTotalsGameWidgetView : TotalsGameWidgetView {

        private Widget widget;
        private Label title;
        private Label message;
        private Button tryAgain;
        private Button back;

        // Props
        public override int Layer => -1000;

        // Constructor
        public LossTotalsGameWidgetView() {
            VisualElement = VisualElementFactory_Game.LossTotalsWidget( out widget, out title, out message, out tryAgain, out back );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
