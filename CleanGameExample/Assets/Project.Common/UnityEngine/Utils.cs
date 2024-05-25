#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public static class Utils {

        // Container
        public static IDependencyContainer Container { get; set; } = default!;

        // SetLayerRecursively
        public static void SetLayerRecursively(this GameObject gameObject, int layer) {
            gameObject.layer = layer;
            for (var i = 0; i < gameObject.transform.childCount; i++) {
                gameObject.transform.GetChild( i ).gameObject.SetLayerRecursively( layer );
            }
        }

        // Instantiate
        public static T Instantiate<T>(this GameObject prefab) where T : MonoBehaviour {
            return Object.Instantiate( prefab.RequireComponent<T>() );
        }
        public static T Instantiate<T>(this GameObject prefab, Vector3 position, Quaternion rotation) where T : MonoBehaviour {
            return Object.Instantiate( prefab.RequireComponent<T>(), position, rotation );
        }
        public static T Instantiate<T>(this GameObject prefab, Transform? parent) where T : MonoBehaviour {
            return Object.Instantiate( prefab.RequireComponent<T>(), parent );
        }
        public static T Instantiate<T>(this GameObject prefab, Transform? parent, bool worldPositionStays) where T : MonoBehaviour {
            return Object.Instantiate( prefab.RequireComponent<T>(), parent, worldPositionStays );
        }
        public static T Instantiate<T>(this GameObject prefab, Vector3 position, Quaternion rotation, Transform? parent) where T : MonoBehaviour {
            return Object.Instantiate( prefab.RequireComponent<T>(), position, rotation, parent );
        }

        // GetRandomValue
        public static T GetRandomValue<T>(this T[] values) {
            return values[ Random.Range( 0, values.Length ) ];
        }
        public static T GetRandomValue<T>(this IList<T> values) {
            return values[ Random.Range( 0, values.Count ) ];
        }
        public static T GetRandomValue<T>(this IReadOnlyList<T> values) {
            return values[ Random.Range( 0, values.Count ) ];
        }

        // PlayAnimation
        //public static async void PlayAnimation<T>(T @object, float from, float to, float duration, Action<T, float> onUpdate, Action<T>? onComplete, Action<T>? onCancel, CancellationToken cancellationToken) {
        //    await PlayAnimationAsync( @object, from, to, duration, onUpdate, onComplete, onCancel, cancellationToken );
        //}
        //public static async Task PlayAnimationAsync<T>(T @object, float from, float to, float duration, Action<T, float> onUpdate, Action<T>? onComplete, Action<T>? onCancel, CancellationToken cancellationToken) {
        //    var time = 0f;
        //    while (!cancellationToken.IsCancellationRequested) {
        //        var time01 = Mathf.InverseLerp( 0, duration, time );
        //        var value = Mathf.Lerp( from, to, time01 );
        //        onUpdate.Invoke( @object, value );
        //        if (time < duration) {
        //            await Task.Yield();
        //            time += Time.unscaledDeltaTime;
        //        } else {
        //            break;
        //        }
        //    }
        //    if (!cancellationToken.IsCancellationRequested) {
        //        onComplete?.Invoke( @object );
        //    } else {
        //        onCancel?.Invoke( @object );
        //    }
        //}

    }
    public static class UIViewExtensions {

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

    }
    public static class VisualElementExtensions {

        public static (T Value, T Min, T Max) GetValueMinMax<T>(this BaseSlider<T> field) where T : IComparable<T> {
            return (field.value, field.lowValue, field.highValue);
        }
        public static void GetValueMinMax<T>(this BaseSlider<T> field, (T Value, T Min, T Max) value) where T : IComparable<T> {
            (field.value, field.lowValue, field.highValue) = value;
        }

        public static (T Value, List<T> Choices) GetValueChoices<T>(this PopupField<T> field) {
            return (field.value, field.choices);
        }
        public static void SetValueChoices<T>(this PopupField<T> field, (T Value, List<T> Choices) value) {
            (field.value, field.choices) = value;
        }

    }
    public static class Physics2 {

        public static readonly RaycastHit[] RaycastHitBuffer = new RaycastHit[ 256 ];
        public static readonly Collider[] ColliderBuffer = new Collider[ 256 ];

        public static IEnumerable<RaycastHit> RaycastAll(Vector3 position, Vector3 direction, float maxDistance) {
            var mask = ~0 & ~Masks.CharacterEntity & ~Masks.Trivial; // excludeCharacterEntity and Trivial
            var count = Physics.RaycastNonAlloc( position, direction, RaycastHitBuffer, maxDistance, mask, QueryTriggerInteraction.Ignore );
            return RaycastHitBuffer.Take( count );
        }

        public static IEnumerable<Collider> OverlapSphere(Vector3 position, float radius) {
            var mask = ~0 & ~Masks.CharacterEntityInternal & ~Masks.Trivial; // exclude CharacterEntityInternal and Trivial
            var count = Physics.OverlapSphereNonAlloc( position, radius, ColliderBuffer, mask, QueryTriggerInteraction.Ignore );
            return ColliderBuffer.Take( count );
        }

    }
    public static class Tags {

        public static string Entity { get; } = "Entity";

    }
    public static class Layers {

        public static int Entity { get; } = GetLayer( "Entity" );
        public static int CharacterEntity { get; } = GetLayer( "Character-Entity" );
        public static int CharacterEntityInternal { get; } = GetLayer( "Character-Entity-Internal" );
        public static int Trivial { get; } = GetLayer( "Trivial" );

        public static int GetLayer(string name) {
            var layer = LayerMask.NameToLayer( name );
            Assert.Operation.Message( $"Can not find {name} layer" ).Valid( layer != -1 );
            return layer;
        }
        public static string GetName(int layer) {
            var name = LayerMask.LayerToName( layer );
            Assert.Operation.Message( $"Can not find {layer} layer name" ).Valid( name != null );
            return name;
        }

    }
    public static class Masks {

        public static int Entity { get; } = GetMask( "Entity" );
        public static int CharacterEntity { get; } = GetMask( "Character-Entity" );
        public static int CharacterEntityInternal { get; } = GetMask( "Character-Entity-Internal" );
        public static int Trivial { get; } = GetMask( "Trivial" );

        public static int GetMask(string name) {
            return 1 << Layers.GetLayer( name );
        }

    }
    public class Delay {

        public float Interval { get; }
        public float? StartTime { get; private set; }
        public float? EndTime => StartTime.HasValue ? StartTime.Value + Interval : null;
        public float? Left => StartTime.HasValue ? Math.Max( StartTime.Value + Interval - Time.time, 0 ) : null;
        public bool IsCompleted => StartTime.HasValue ? (StartTime.Value + Interval - Time.time) <= 0 : true;

        public Delay(float interval) {
            Interval = interval;
        }

        public void Start() {
            StartTime = Time.time;
        }

        public override string ToString() {
            return "Delay: " + Interval;
        }

    }
}
