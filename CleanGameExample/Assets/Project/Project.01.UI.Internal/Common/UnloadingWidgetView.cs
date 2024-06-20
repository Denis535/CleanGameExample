#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class UnloadingWidgetView : UIViewBase {

        private readonly Widget widget;
        private readonly VisualElement background;
        private readonly Label loading;

        // Constructor
        public UnloadingWidgetView() {
            VisualElement = VisualElementFactory_Common.Unloading( out widget, out background, out loading );
            background.RegisterCallbackOnce<AttachToPanelEvent>( async evt => {
                background.style.unityBackgroundImageTintColor = Color.gray;
                background.style.translate = new Translate( 0, 0 );
                background.style.rotate = new Rotate( Angle.Degrees( 15 ) );
                background.style.scale = new Scale( new Vector3( 2, 2, 1 ) );
                await Awaitable.NextFrameAsync( DisposeCancellationToken );
                background.style.unityBackgroundImageTintColor = Color.white;
                background.style.translate = new Translate( 0, 0 );
                background.style.rotate = new Rotate( Angle.Degrees( 0 ) );
                background.style.scale = new Scale( new Vector3( 1, 1, 1 ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
