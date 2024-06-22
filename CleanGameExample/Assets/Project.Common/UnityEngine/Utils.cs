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
        public static bool IsAttached(this UIViewBase2 view) {
            return view.__GetVisualElement__().panel != null;
        }

        // IsEnabledSelf
        public static bool IsEnabledSelf(this UIViewBase2 view) {
            return view.__GetVisualElement__().enabledSelf;
        }
        public static bool IsEnabledInHierarchy(this UIViewBase2 view) {
            return view.__GetVisualElement__().enabledInHierarchy;
        }
        public static void SetEnabled(this UIViewBase2 view, bool value) {
            view.__GetVisualElement__().SetEnabled( value );
        }

        // IsDisplayedSelf
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

        //// AsProperty
        //public static IsEnabledProperty AsIsEnabled(this VisualElement visualElement, Action<VisualElement>? onChange) {
        //    return new IsEnabledProperty( visualElement, onChange );
        //}
        //public static IsDisplayedProperty AsIsDisplayed(this VisualElement visualElement, Action<VisualElement>? onChange) {
        //    return new IsDisplayedProperty( visualElement, onChange );
        //}
        //public static TextProperty AsText(this Label visualElement, Action<Label>? onChange) {
        //    return new TextProperty( visualElement, onChange );
        //}
        //public static ValueProperty<T> AsValue<T>(this BaseField<T> visualElement, Action<BaseField<T>>? onChange) {
        //    return new ValueProperty<T>( visualElement, onChange );
        //}
        //public static ValueMinMaxProperty<T> AsValueMinMax<T>(this BaseSlider<T> visualElement, Action<BaseSlider<T>>? onChange) where T : IComparable<T> {
        //    return new ValueMinMaxProperty<T>( visualElement, onChange );
        //}
        //public static ValueChoicesProperty<T> AsValueChoices<T>(this PopupField<T> visualElement, Action<PopupField<T>>? onChange) {
        //    return new ValueChoicesProperty<T>( visualElement, onChange );
        //}
        //public static ChildrenProperty<T> AsChildren<T>(this VisualElement visualElement, Action<VisualElement>? onChange) where T : UIViewBase {
        //    return new ChildrenProperty<T>( visualElement, onChange );
        //}
        //public static EventObservable<T> AsObservable<T>(this VisualElement element) where T : EventBase<T>, new() {
        //    return new EventObservable<T>( element );
        //}

        // GetMinMax
        public static (T Min, T Max) GetMinMax<T>(this BaseSlider<T> element) where T : IComparable<T> {
            return (element.lowValue, element.highValue);
        }
        public static void SetMinMax<T>(this BaseSlider<T> element, (T Min, T Max) value) where T : IComparable<T> {
            (element.lowValue, element.highValue) = value;
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
    //public readonly struct IsEnabledProperty {

    //    private readonly VisualElement visualElement;
    //    private readonly Action<VisualElement>? onChange;

    //    public readonly bool IsEnabled {
    //        get => visualElement.enabledSelf;
    //        set {
    //            visualElement.SetEnabled( value );
    //            onChange?.Invoke( visualElement );
    //        }
    //    }

    //    public IsEnabledProperty(VisualElement visualElement, Action<VisualElement>? onChange) {
    //        this.visualElement = visualElement;
    //        this.onChange = onChange;
    //    }

    //}
    //public readonly struct IsDisplayedProperty {

    //    private readonly VisualElement visualElement;
    //    private readonly Action<VisualElement>? onChange;

    //    public readonly bool IsDisplayed {
    //        get => visualElement.IsDisplayedSelf();
    //        set {
    //            visualElement.SetDisplayed( value );
    //            onChange?.Invoke( visualElement );
    //        }
    //    }

    //    public IsDisplayedProperty(VisualElement visualElement, Action<VisualElement>? onChange) {
    //        this.visualElement = visualElement;
    //        this.onChange = onChange;
    //    }

    //}
    //public readonly struct TextProperty {

    //    private readonly Label visualElement;
    //    private readonly Action<Label>? onChange;

    //    public readonly string Text {
    //        get => visualElement.text;
    //        set {
    //            visualElement.text = value;
    //            onChange?.Invoke( visualElement );
    //        }
    //    }

    //    public TextProperty(Label visualElement, Action<Label>? onChange) {
    //        this.visualElement = visualElement;
    //        this.onChange = onChange;
    //    }

    //}
    //public readonly struct ValueProperty<T> {

    //    private readonly BaseField<T> visualElement;
    //    private readonly Action<BaseField<T>>? onChange;

    //    public readonly T Value {
    //        get => visualElement.value;
    //        set {
    //            visualElement.value = value;
    //            onChange?.Invoke( visualElement );
    //        }
    //    }

    //    public ValueProperty(BaseField<T> visualElement, Action<BaseField<T>>? onChange) {
    //        this.visualElement = visualElement;
    //        this.onChange = onChange;
    //    }

    //}
    //public readonly struct ValueMinMaxProperty<T> where T : IComparable<T> {

    //    private readonly BaseSlider<T> visualElement;
    //    private readonly Action<BaseSlider<T>>? onChange;

    //    public readonly T Value {
    //        get => visualElement.value;
    //        set {
    //            visualElement.value = value;
    //            onChange?.Invoke( visualElement );
    //        }
    //    }
    //    public readonly (T Min, T Max) MinMax {
    //        get => (visualElement.lowValue, visualElement.highValue);
    //        set {
    //            (visualElement.lowValue, visualElement.highValue) = (value.Min, value.Max);
    //            onChange?.Invoke( visualElement );
    //        }
    //    }
    //    public readonly (T Value, T Min, T Max) ValueMinMax {
    //        get => (visualElement.value, visualElement.lowValue, visualElement.highValue);
    //        set {
    //            (visualElement.value, visualElement.lowValue, visualElement.highValue) = (value.Value, value.Min, value.Max);
    //            onChange?.Invoke( visualElement );
    //        }
    //    }

    //    public ValueMinMaxProperty(BaseSlider<T> visualElement, Action<BaseSlider<T>>? onChange) {
    //        this.visualElement = visualElement;
    //        this.onChange = onChange;
    //    }

    //}
    //public readonly struct ValueChoicesProperty<T> {

    //    private readonly PopupField<T> visualElement;
    //    private readonly Action<PopupField<T>>? onChange;

    //    public readonly T Value {
    //        get => visualElement.value;
    //        set {
    //            visualElement.value = value;
    //            onChange?.Invoke( visualElement );
    //        }
    //    }
    //    public readonly List<T> Choices {
    //        get => visualElement.choices;
    //        set {
    //            visualElement.choices = value;
    //            onChange?.Invoke( visualElement );
    //        }
    //    }
    //    public readonly (T Value, List<T> Choices) ValueChoices {
    //        get => (visualElement.value, visualElement.choices);
    //        set {
    //            (visualElement.value, visualElement.choices) = (value.Value, value.Choices);
    //            onChange?.Invoke( visualElement );
    //        }
    //    }

    //    public ValueChoicesProperty(PopupField<T> visualElement, Action<PopupField<T>>? onChange) {
    //        this.visualElement = visualElement;
    //        this.onChange = onChange;
    //    }

    //}
    //public readonly struct ChildrenProperty<T> where T : UIViewBase {

    //    private readonly VisualElement visualElement;
    //    private readonly Action<VisualElement>? onChange;

    //    public readonly IEnumerable<T> Children => visualElement.Children().Select( i => i.GetView<T>() );

    //    public ChildrenProperty(VisualElement visualElement, Action<VisualElement>? onChange) {
    //        this.visualElement = visualElement;
    //        this.onChange = onChange;
    //    }

    //    public readonly void Add(T child) {
    //        visualElement.Add( child );
    //        onChange?.Invoke( visualElement );
    //    }
    //    public readonly void Remove(T child) {
    //        visualElement.Remove( child );
    //        onChange?.Invoke( visualElement );
    //    }
    //    public readonly void Contains(T child) {
    //        visualElement.Contains( child );
    //    }

    //}
    //public readonly struct EventObservable<T> where T : EventBase<T>, new() {

    //    private readonly VisualElement visualElement;

    //    public EventObservable(VisualElement visualElement) {
    //        this.visualElement = visualElement;
    //    }

    //    public readonly void Register(EventCallback<T> callback, TrickleDown trickleDown = TrickleDown.NoTrickleDown) {
    //        visualElement.RegisterCallback( callback, trickleDown );
    //    }
    //    public readonly void RegisterOnce(EventCallback<T> callback, TrickleDown trickleDown = TrickleDown.NoTrickleDown) {
    //        visualElement.RegisterCallbackOnce( callback, trickleDown );
    //    }
    //    public readonly void Unregister(EventCallback<T> callback, TrickleDown trickleDown = TrickleDown.NoTrickleDown) {
    //        visualElement.UnregisterCallback( callback, trickleDown );
    //    }

    //}
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
