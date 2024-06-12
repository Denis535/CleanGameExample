#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;

    public static class Utils {

        public static readonly RaycastHit[] RaycastHitBuffer = new RaycastHit[ 256 ];
        public static readonly Collider[] ColliderBuffer = new Collider[ 256 ];

        // SetEnabled
        public static void SetEnabled(this InputActions_UI input, bool value) {
            if (value) input.Enable(); else input.Disable();
        }
        public static void SetEnabled(this InputActions_Game input, bool value) {
            if (value) input.Enable(); else input.Disable();
        }
        public static void SetEnabled(this InputActions_Player input, bool value) {
            if (value) input.Enable(); else input.Disable();
        }

        // RaycastAll
        public static IEnumerable<RaycastHit> RaycastAll(Vector3 position, Vector3 direction, float maxDistance) {
            var mask = ~0 & ~Masks.CharacterEntity & ~Masks.Trivial; // excludeCharacterEntity and Trivial
            var count = Physics.RaycastNonAlloc( position, direction, RaycastHitBuffer, maxDistance, mask, QueryTriggerInteraction.Ignore );
            return RaycastHitBuffer.Take( count );
        }

        // OverlapSphere
        public static IEnumerable<Collider> OverlapSphere(Vector3 position, float radius) {
            var mask = ~0 & ~Masks.CharacterEntityInternal & ~Masks.Trivial; // exclude CharacterEntityInternal and Trivial
            var count = Physics.OverlapSphereNonAlloc( position, radius, ColliderBuffer, mask, QueryTriggerInteraction.Ignore );
            return ColliderBuffer.Take( count );
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

    }
    public static class GameObjectExtensions {

        // SetLayer
        public static void SetLayer(this GameObject gameObject, int layer) {
            gameObject.layer = layer;
        }

        // SetLayerRecursively
        public static void SetLayerRecursively(this GameObject gameObject, int layer) {
            gameObject.layer = layer;
            for (var i = 0; i < gameObject.transform.childCount; i++) {
                gameObject.transform.GetChild( i ).gameObject.SetLayerRecursively( layer );
            }
        }

        // SetLayerRecursively
        public static void SetLayerRecursively(this GameObject gameObject, int layer, int layer2) {
            gameObject.layer = layer;
            for (var i = 0; i < gameObject.transform.childCount; i++) {
                gameObject.transform.GetChild( i ).gameObject.SetLayerRecursively( layer2 );
            }
        }

    }
    public static class UIViewExtensions {

        // IsAttached
        public static bool IsAttached(this UIViewBase view) {
            return view.__GetVisualElement__().panel != null;
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

        // OnValidate
        public static void OnValidate(this VisualElement element, EventCallback<EventBase> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) {
            // todo: how to handle any event?
            //element.RegisterCallback<EventBase>( callback, useTrickleDown );
            element.RegisterCallback<AttachToPanelEvent>( callback, useTrickleDown );
            element.RegisterCallback<ChangeEvent<object>>( callback, useTrickleDown );
            element.RegisterCallback<ChangeEvent<string>>( callback, useTrickleDown );
            element.RegisterCallback<ChangeEvent<int>>( callback, useTrickleDown );
            element.RegisterCallback<ChangeEvent<float>>( callback, useTrickleDown );
            element.RegisterCallback<ChangeEvent<bool>>( callback, useTrickleDown );
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
