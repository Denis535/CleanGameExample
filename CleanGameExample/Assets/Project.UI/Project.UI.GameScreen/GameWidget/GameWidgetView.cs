﻿#nullable enable
namespace Project.UI.GameScreen {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class GameWidgetView : UIViewBase {
        public class TargetWrapper : ElementWrapper {
            public enum Mode {
                Normal,
                Interactable,
                Enemy,
            }
            public TargetWrapper(VisualElement visualElement) : base( visualElement ) {
            }
            public void SetMode(Mode value) {
                switch (value) {
                    case Mode.Normal:
                        VisualElement.style.color = Color.white;
                        break;
                    case Mode.Interactable:
                        VisualElement.style.color = Color.yellow;
                        break;
                    case Mode.Enemy:
                        VisualElement.style.color = Color.red;
                        break;
                    default:
                        Exceptions.Internal.NotSupported( $"Value {value} is supported" );
                        break;
                }
            }
        }

        // View
        public ElementWrapper Widget { get; }
        public TargetWrapper Target { get; }

        // Constructor
        public GameWidgetView() {
            VisualElement = UIFactory.Game.GameWidget( out var widget, out var target );
            Widget = widget.Wrap();
            Target = target.Wrap<TargetWrapper>();
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
