#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;

    public class TotalsGameWidgetView : UIViewBase {

        // Props
        public override int Layer => -1000;

        // Constructor
        public TotalsGameWidgetView() {
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
