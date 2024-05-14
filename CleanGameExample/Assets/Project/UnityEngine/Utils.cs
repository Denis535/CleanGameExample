#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public static class Utils {

        // Container
        public static IDependencyContainer Container { get; set; } = default!;

        // IsAttached
        public static bool IsAttached(this UIViewBase view) {
            return view.__GetVisualElement__().IsAttached();
        }

        // IsEnabledSelf
        public static bool IsEnabledSelf(this UIViewBase view) {
            return view.__GetVisualElement__().enabledSelf;
        }
        public static bool IsEnabledInHierarchy(this UIViewBase view) {
            return view.__GetVisualElement__().enabledInHierarchy;
        }
        public static void SetEnabled(this UIViewBase view, bool value) {
            view.__GetVisualElement__().SetEnabled( value );
        }

        // IsDisplayedSelf
        public static bool IsDisplayedSelf(this UIViewBase view) {
            return view.__GetVisualElement__().IsDisplayedSelf();
        }
        public static bool IsDisplayedInHierarchy(this UIViewBase view) {
            return view.__GetVisualElement__().IsDisplayedInHierarchy();
        }
        public static void SetDisplayed(this UIViewBase view, bool value) {
            view.__GetVisualElement__().SetDisplayed( value );
        }

        // GetMinMax
        public static (T Value, T Min, T Max) GetValueMinMax<T>(this BaseSlider<T> field) where T : IComparable<T> {
            return (field.value, field.lowValue, field.highValue);
        }
        public static void GetValueMinMax<T>(this BaseSlider<T> field, (T Value, T Min, T Max) value) where T : IComparable<T> {
            (field.value, field.lowValue, field.highValue) = value;
        }

        // GetValueChoices
        public static (T Value, List<T> Choices) GetValueChoices<T>(this PopupField<T> field) {
            return (field.value, field.choices);
        }
        public static void SetValueChoices<T>(this PopupField<T> field, (T Value, List<T> Choices) value) {
            (field.value, field.choices) = value;
        }

        // PlayAnimation
        public static async void PlayAnimation<T>(T @object, float from, float to, float duration, Action<T, float> onUpdate, Action<T>? onComplete, Action<T>? onCancel, CancellationToken cancellationToken) {
            await PlayAnimationAsync( @object, from, to, duration, onUpdate, onComplete, onCancel, cancellationToken );
        }
        public static async Task PlayAnimationAsync<T>(T @object, float from, float to, float duration, Action<T, float> onUpdate, Action<T>? onComplete, Action<T>? onCancel, CancellationToken cancellationToken) {
            var time = 0f;
            while (!cancellationToken.IsCancellationRequested) {
                var time01 = Mathf.InverseLerp( 0, duration, time );
                var value = Mathf.Lerp( from, to, time01 );
                onUpdate.Invoke( @object, value );
                if (time < duration) {
                    await Task.Yield();
                    time += Time.unscaledDeltaTime;
                } else {
                    break;
                }
            }
            if (!cancellationToken.IsCancellationRequested) {
                onComplete?.Invoke( @object );
            } else {
                onCancel?.Invoke( @object );
            }
        }

    }
}
