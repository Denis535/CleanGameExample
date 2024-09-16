#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class VisualElementExtensions {

        public static void SetValue<T>(this BaseField<T> element, T value) {
            element.value = value;
        }
        public static void SetValue<T>(this PopupField<T> element, T value, List<T> choices) {
            (element.value, element.choices) = (value, choices.ToList());
        }
        public static void SetValue<T>(this PopupField<T> element, T value, T[] choices) {
            (element.value, element.choices) = (value, choices.ToList());
        }
        public static void SetValue<T>(this BaseSlider<T> element, T value, T min, T max) where T : IComparable<T> {
            (element.value, element.lowValue, element.highValue) = (value, min, max);
        }

        public static void OnValidate(this VisualElement element, EventCallback<EventBase> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) {
            // todo: how to handle any event?
            //element.RegisterCallback<EventBase>( callback, useTrickleDown );
            element.RegisterCallback<AttachToPanelEvent>( callback, useTrickleDown );
            element.RegisterCallback<ChangeEvent<object?>>( callback, useTrickleDown );
            element.RegisterCallback<ChangeEvent<string?>>( callback, useTrickleDown );
            element.RegisterCallback<ChangeEvent<int>>( callback, useTrickleDown );
            element.RegisterCallback<ChangeEvent<float>>( callback, useTrickleDown );
            element.RegisterCallback<ChangeEvent<bool>>( callback, useTrickleDown );
        }

    }
}
