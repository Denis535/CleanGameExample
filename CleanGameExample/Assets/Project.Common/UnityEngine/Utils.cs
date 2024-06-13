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

        // AsElement
        public static LabelElement AsLabel(this Label visualElement, Action<Label>? onChange) {
            return new LabelElement( visualElement, onChange );
        }
        public static FieldElement<T> AsField<T>(this BaseField<T> visualElement, Action<BaseField<T>>? onChange) where T : notnull {
            return new FieldElement<T>( visualElement, onChange );
        }
        public static SliderFieldElement<T> AsField<T>(this BaseSlider<T> visualElement, Action<BaseSlider<T>>? onChange) where T : notnull, IComparable<T> {
            return new SliderFieldElement<T>( visualElement, onChange );
        }
        public static PopupFieldElement<T> AsField<T>(this PopupField<T> visualElement, Action<PopupField<T>>? onChange) where T : notnull {
            return new PopupFieldElement<T>( visualElement, onChange );
        }
        public static SlotElement AsSlot(this VisualElement visualElement, Action<VisualElement>? onChange) {
            return new SlotElement( visualElement, onChange );
        }

        // AsObservable
        public static Observable<T> AsObservable<T>(this VisualElement element) where T : notnull, EventBase<T>, new() {
            return new Observable<T>( element );
        }

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
    public readonly struct LabelElement {

        private readonly Label visualElement;
        private readonly Action<Label>? onChange;

        public readonly string Text {
            get => visualElement.text;
            set {
                visualElement.text = value;
                onChange?.Invoke( visualElement );
            }
        }

        public LabelElement(Label visualElement, Action<Label>? onChange) {
            this.visualElement = visualElement;
            this.onChange = onChange;
        }

    }
    public readonly struct FieldElement<T> where T : notnull {

        private readonly BaseField<T> visualElement;
        private readonly Action<BaseField<T>>? onChange;

        public readonly T Value {
            get => visualElement.value;
            set {
                visualElement.value = value;
                onChange?.Invoke( visualElement );
            }
        }

        public FieldElement(BaseField<T> visualElement, Action<BaseField<T>>? onChange) {
            this.visualElement = visualElement;
            this.onChange = onChange;
        }

    }
    public readonly struct SliderFieldElement<T> where T : notnull, IComparable<T> {

        private readonly BaseSlider<T> visualElement;
        private readonly Action<BaseSlider<T>>? onChange;

        public readonly T Value {
            get => visualElement.value;
            set {
                visualElement.value = value;
                onChange?.Invoke( visualElement );
            }
        }
        public readonly (T Min, T Max) MinMax {
            get => (visualElement.lowValue, visualElement.highValue);
            set {
                (visualElement.lowValue, visualElement.highValue) = (value.Min, value.Max);
                onChange?.Invoke( visualElement );
            }
        }

        public SliderFieldElement(BaseSlider<T> visualElement, Action<BaseSlider<T>>? onChange) {
            this.visualElement = visualElement;
            this.onChange = onChange;
        }

    }
    public readonly struct PopupFieldElement<T> where T : notnull {

        private readonly PopupField<T> visualElement;
        private readonly Action<PopupField<T>>? onChange;

        public readonly T Value {
            get => visualElement.value;
            set {
                visualElement.value = value;
                onChange?.Invoke( visualElement );
            }
        }
        public readonly List<T> Choices {
            get => visualElement.choices;
            set {
                visualElement.choices = value;
                onChange?.Invoke( visualElement );
            }
        }

        public PopupFieldElement(PopupField<T> visualElement, Action<PopupField<T>>? onChange) {
            this.visualElement = visualElement;
            this.onChange = onChange;
        }

    }
    public readonly struct SlotElement {

        private readonly VisualElement visualElement;
        private readonly Action<VisualElement>? onChange;

        public IEnumerable<UIViewBase> Children => visualElement.Children().Select( i => i.GetView() );

        public SlotElement(VisualElement visualElement, Action<VisualElement>? onChange) {
            this.visualElement = visualElement;
            this.onChange = onChange;
        }

        public void Add(UIViewBase view) {
            visualElement.Add( view );
            onChange?.Invoke( visualElement );
        }
        public void Remove(UIViewBase view) {
            visualElement.Remove( view );
            onChange?.Invoke( visualElement );
        }
        public void Contains(UIViewBase view) {
            visualElement.Contains( view );
        }

    }
    public readonly struct Observable<T> where T : notnull, EventBase<T>, new() {

        private readonly VisualElement visualElement;

        public Observable(VisualElement visualElement) {
            this.visualElement = visualElement;
        }

        public readonly void Register(EventCallback<T> callback) {
            visualElement.RegisterCallback( callback );
        }
        public readonly void RegisterOnce(EventCallback<T> callback) {
            visualElement.RegisterCallbackOnce( callback );
        }
        public readonly void Unregister(EventCallback<T> callback) {
            visualElement.UnregisterCallback( callback );
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
