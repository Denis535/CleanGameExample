#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public static class Utils {

        // Container
        public static IDependencyContainer Container { get; set; } = default!;
        // DeltaTime
        public static float DeltaTime => Time.inFixedTimeStep ? Time.fixedDeltaTime : Time.deltaTime;

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

        // AddWidget
        public static void AddWidget(this UIDocument document, UIWidgetBase widget) {
            Assert.Argument.Message( $"Argument 'document' must be non-null" ).NotNull( document is not null );
            Assert.Argument.Message( $"Argument 'document' {document} must be awakened" ).Valid( document.didAwake );
            Assert.Argument.Message( $"Argument 'document' {document} must be alive" ).Valid( document );
            Assert.Argument.Message( $"Argument 'document' {document} must have rootVisualElement" ).Valid( document.rootVisualElement != null );
            Assert.Argument.Message( $"Argument 'widget' must be non-null" ).NotNull( widget != null );
            document.rootVisualElement.Add( widget.__GetView__()!.__GetVisualElement__() );
        }
        public static void AddWidgetIfNeeded(this UIDocument document, UIWidgetBase widget) {
            Assert.Argument.Message( $"Argument 'document' must be non-null" ).NotNull( document is not null );
            Assert.Argument.Message( $"Argument 'document' {document} must be awakened" ).Valid( document.didAwake );
            Assert.Argument.Message( $"Argument 'document' {document} must be alive" ).Valid( document );
            Assert.Argument.Message( $"Argument 'document' {document} must have rootVisualElement" ).Valid( document.rootVisualElement != null );
            Assert.Argument.Message( $"Argument 'widget' must be non-null" ).NotNull( widget != null );
            if (!document.rootVisualElement.Contains( widget.__GetView__()!.__GetVisualElement__() )) {
                document.rootVisualElement.Add( widget.__GetView__()!.__GetVisualElement__()! );
            }
        }
        public static void RemoveWidget(this UIDocument document, UIWidgetBase widget) {
            Assert.Argument.Message( $"Argument 'document' must be non-null" ).NotNull( document is not null );
            Assert.Argument.Message( $"Argument 'document' {document} must be awakened" ).Valid( document.didAwake );
            Assert.Argument.Message( $"Argument 'document' {document} must be alive" ).Valid( document );
            Assert.Argument.Message( $"Argument 'document' {document} must have rootVisualElement" ).Valid( document.rootVisualElement != null );
            Assert.Argument.Message( $"Argument 'widget' must be non-null" ).NotNull( widget != null );
            document.rootVisualElement.Remove( widget.__GetView__()!.__GetVisualElement__() );
        }

        // IsViewAttached
        public static bool IsViewAttached(this UIWidgetBase widget) {
            return widget.__GetView__()?.__GetVisualElement__().IsAttached() ?? false;
        }

        // SaveFocus
        public static void SaveFocus(this UIWidgetBase widget) {
            widget.__GetView__()!.__GetVisualElement__().SaveFocus();
        }
        public static bool LoadFocus(this UIWidgetBase widget) {
            return widget.__GetView__()!.__GetVisualElement__().LoadFocus();
        }

        // IsViewValid
        public static bool IsViewValid(this ElementWrapper element) {
            return element.__GetVisualElement__().GetDescendantsAndSelf().All( i => i.IsValid() );
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

        // Push
        public static void Push(this WidgetListSlotWrapper<UIWidgetBase> slot, UIWidgetBase widget, Func<UIWidgetBase, bool> isVisibleAlways) {
            var last = slot.Children.LastOrDefault();
            if (last != null && !isVisibleAlways( last )) {
                slot.__GetVisualElement__().Remove( last.__GetView__()!.__GetVisualElement__() );
            }
            slot.Add( widget );
        }
        public static void Pop(this WidgetListSlotWrapper<UIWidgetBase> slot, UIWidgetBase widget, Func<UIWidgetBase, bool> isVisibleAlways) {
            Assert.Operation.Message( $"Widget {widget} must be last" ).Valid( widget == slot.Children.LastOrDefault() );
            slot.Remove( widget );
            var last = slot.Children.LastOrDefault();
            if (last != null && !isVisibleAlways( last )) {
                slot.__GetVisualElement__().Add( last.__GetView__()!.__GetVisualElement__() );
            }
        }

    }
    public enum TargetEffect {
        Normal,
        Loot,
        Enemy,
    }
}
