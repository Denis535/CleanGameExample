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

        public static IEnumerable<RaycastHit> RaycastAll(Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction) {
            var count = Physics.RaycastNonAlloc( ray, RaycastHitBuffer, maxDistance, mask, QueryTriggerInteraction.Ignore );
            return RaycastHitBuffer.Take( count );
        }

        public static IEnumerable<Collider> OverlapSphere(Vector3 position, float radius, int mask, QueryTriggerInteraction queryTriggerInteraction) {
            var count = Physics.OverlapSphereNonAlloc( position, radius, ColliderBuffer, mask, QueryTriggerInteraction.Ignore );
            return ColliderBuffer.Take( count );
        }

        public static void SetEnabled(this InputActions_UI input, bool value) {
            if (value) input.Enable(); else input.Disable();
        }
        public static void SetEnabled(this InputActions_Game input, bool value) {
            if (value) input.Enable(); else input.Disable();
        }
        public static void SetEnabled(this InputActions_Player input, bool value) {
            if (value) input.Enable(); else input.Disable();
        }

        public static T GetRandom<T>(this T[] values) {
            return values[ Random.Range( 0, values.Length ) ];
        }
        public static T GetRandom<T>(this IList<T> values) {
            return values[ Random.Range( 0, values.Count ) ];
        }
        public static T GetRandom<T>(this IReadOnlyList<T> values) {
            return values[ Random.Range( 0, values.Count ) ];
        }

    }
    public static class UIViewExtensions {

        public static bool IsAttached(this UIViewBase2 view) {
            return view.__GetVisualElement__().panel != null;
        }

        public static bool IsEnabledSelf(this UIViewBase2 view) {
            return view.__GetVisualElement__().enabledSelf;
        }
        public static bool IsEnabledInHierarchy(this UIViewBase2 view) {
            return view.__GetVisualElement__().enabledInHierarchy;
        }
        public static void SetEnabled(this UIViewBase2 view, bool value) {
            view.__GetVisualElement__().SetEnabled( value );
        }

        public static bool IsDisplayedSelf(this UIViewBase2 view) {
            return view.__GetVisualElement__().IsDisplayedSelf();
        }
        public static bool IsDisplayedInHierarchy(this UIViewBase2 view) {
            return view.__GetVisualElement__().IsDisplayedInHierarchy();
        }
        public static void SetDisplayed(this UIViewBase2 view, bool value) {
            view.__GetVisualElement__().SetDisplayed( value );
        }

    }
    public static class VisualElementExtensions {

        public static (T Min, T Max) GetMinMax<T>(this BaseSlider<T> element) where T : IComparable<T> {
            return (element.lowValue, element.highValue);
        }
        public static void SetMinMax<T>(this BaseSlider<T> element, (T Min, T Max) value) where T : IComparable<T> {
            (element.lowValue, element.highValue) = value;
        }

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
}
