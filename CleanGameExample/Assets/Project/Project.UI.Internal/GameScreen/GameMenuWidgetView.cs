#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class GameMenuWidgetView : UIViewBase {

        // Root
        public ElementWrapper Root { get; }
        public LabelWrapper Title { get; }
        public ButtonWrapper Resume { get; }
        public ButtonWrapper Settings { get; }
        public ButtonWrapper Back { get; }

        // Constructor
        public GameMenuWidgetView() {
            VisualElement = ViewFactory.GameMenuWidget( out var root, out var title, out var resume, out var settings, out var back );
            Root = root.Wrap();
            Title = title.Wrap();
            Resume = resume.Wrap();
            Settings = settings.Wrap();
            Back = back.Wrap();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
