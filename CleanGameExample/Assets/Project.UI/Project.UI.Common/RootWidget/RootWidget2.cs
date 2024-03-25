#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Project.UI.GameScreen;
    using Project.UI.MainScreen;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class RootWidget2 : RootWidget {

        // Constructor
        public RootWidget2() {
        }
        public override void Dispose() {
            base.Dispose();
        }

        // OnAttach
        public override void OnAttach(object? argument) {
            base.OnAttach( argument );
        }
        public override void OnDetach(object? argument) {
            base.OnDetach( argument );
        }

        // ShowWidget
        protected override void ShowWidget(UIWidgetBase widget) {
            base.ShowWidget( widget );
        }
        protected override void HideWidget(UIWidgetBase widget) {
            base.HideWidget( widget );
        }

        // RecalcVisibility
        protected override void RecalcVisibility() {
            base.RecalcVisibility();
        }
        protected override void RecalcWidgetVisibility(UIWidgetBase widget, bool isLast) {
            if (!isLast) {
                // hide covered widgets
                widget.SetEnabled( true );
                if (widget is not MainWidget and not GameWidget) {
                    widget.SetDisplayed( false );
                }
            } else {
                // show new widget or unhide uncovered widget
                widget.SetEnabled( !ModalWidgets.Any() );
                widget.SetDisplayed( true );
            }
        }
        protected override void RecalcModalWidgetVisibility(UIWidgetBase widget, bool isLast) {
            base.RecalcModalWidgetVisibility( widget, isLast );
        }

    }
}
