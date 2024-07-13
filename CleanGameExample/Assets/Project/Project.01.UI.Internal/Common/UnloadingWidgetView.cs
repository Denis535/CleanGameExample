#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class UnloadingWidgetView : UIViewBase2 {

        protected override VisualElement VisualElement => Widget;
        public Widget Widget { get; }
        public VisualElement Background { get; }

        public UnloadingWidgetView() {
            Widget = VisualElementFactory.Widget( "unloading-widget" ).UserData( this ).Children(
                Background = VisualElementFactory.VisualElement().Classes( "unloading-widget-background", "width-100pc", "height-100pc" )
            );
            Background.RegisterCallbackOnce<AttachToPanelEvent>( async evt => {
                Background.style.unityBackgroundImageTintColor = Color.gray;
                Background.style.translate = new Translate( 0, 0 );
                Background.style.rotate = new Rotate( Angle.Degrees( 15 ) );
                Background.style.scale = new Scale( new Vector3( 2, 2, 1 ) );
                await Awaitable.NextFrameAsync( DisposeCancellationToken );
                Background.style.unityBackgroundImageTintColor = Color.white;
                Background.style.translate = new Translate( 0, 0 );
                Background.style.rotate = new Rotate( Angle.Degrees( 0 ) );
                Background.style.scale = new Scale( new Vector3( 1, 1, 1 ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
