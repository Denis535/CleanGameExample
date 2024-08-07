﻿#nullable enable
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
        public static void SetEnabled(this InputActions_Player input, bool value) {
            if (value) input.Enable(); else input.Disable();
        }
        public static void SetEnabled(this InputActions_Player.CameraActions input, bool value) {
            if (value) input.Enable(); else input.Disable();
        }
        public static void SetEnabled(this InputActions_Player.CharacterActions input, bool value) {
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

        public static bool IsAttached(this UIViewBase view) {
            return view.__GetVisualElement__().panel != null;
        }

        public static bool IsEnabledSelf(this UIViewBase view) {
            return view.__GetVisualElement__().enabledSelf;
        }
        public static bool IsEnabledInHierarchy(this UIViewBase view) {
            return view.__GetVisualElement__().enabledInHierarchy;
        }
        public static void SetEnabled(this UIViewBase view, bool value) {
            view.__GetVisualElement__().SetEnabled( value );
        }

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
    public class FireDelay {

        private float Interval { get; }
        private float? FireTime { get; set; }
        public bool CanFire => FireTime.HasValue ? (FireTime.Value + Interval - Time.time) <= 0 : true;

        public FireDelay(float interval) {
            Interval = interval;
        }

        public void Fire() {
            FireTime = Time.time;
        }

    }
}
