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
        public static bool IsDisplayedInHierarchy(this UIViewBase view) {
            return view.__GetVisualElement__().IsDisplayedInHierarchy();
        }

        // IsContentValid
        public static bool IsContentValid(this ElementWrapper element) {
            return element.__GetVisualElement__().IsContentValid();
        }

        // SetBackgroundEffect
        public static void SetBackgroundEffect(this ElementWrapper element, Color color, Vector2 translate, float rotate, float scale) {
            element.GetStyle().unityBackgroundImageTintColor = color;
            element.GetStyle().translate = new Translate( translate.x, translate.y );
            element.GetStyle().rotate = new Rotate( Angle.Degrees( rotate ) );
            element.GetStyle().scale = new Scale( new Vector3( scale, scale, 1 ) );
        }

        // SetTargetEffect
        public static void SetTargetEffect(this ElementWrapper element, TargetEffect value) {
            switch (value) {
                case TargetEffect.Normal:
                    element.GetStyle().color = Color.white;
                    break;
                case TargetEffect.Loot:
                    element.GetStyle().color = Color.yellow;
                    break;
                case TargetEffect.Enemy:
                    element.GetStyle().color = Color.red;
                    break;
                default:
                    Exceptions.Internal.NotSupported( $"Value {value} is supported" );
                    break;
            }
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
    public enum TargetEffect {
        Normal,
        Loot,
        Enemy,
    }
}
