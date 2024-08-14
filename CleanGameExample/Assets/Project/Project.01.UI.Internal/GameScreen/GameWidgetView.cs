#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEngine.Framework.UI;

    public class GameWidgetView : WidgetView {

        public VisualElement Target { get; }

        public GameWidgetView() : base( "game-widget-view" ) {
            focusable = true;
            Add(
                Target = VisualElementFactory.Label( "+" )
                    .Classes( "font-size-400pc", "color-light", "margin-0pc", "border-0pc", "position-absolute", "left-50pc", "top-50pc" )
                    .Style( i => i.translate = new Translate( new Length( -50, LengthUnit.Percent ), new Length( -50, LengthUnit.Percent ) ) )
            );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
