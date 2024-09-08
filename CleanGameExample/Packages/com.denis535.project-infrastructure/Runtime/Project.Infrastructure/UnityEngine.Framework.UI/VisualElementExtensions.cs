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
    public static partial class VisualElementFactory {

        public static Func<object?, string?>? StringSelector { get; set; }

        public static event EventCallback<EventBase>? OnPlayClick;
        public static event EventCallback<ClickEvent>? OnPlaySelect;
        public static event EventCallback<ClickEvent>? OnPlaySubmit;
        public static event EventCallback<ClickEvent>? OnPlayCancel;
        public static event EventCallback<IChangeEvent>? OnPlayChange;
        public static event EventCallback<FocusEvent>? OnPlayFocus;
        public static EventCallback<AttachToPanelEvent>? OnPlayDialog;
        public static EventCallback<AttachToPanelEvent>? OnPlayInfoDialog;
        public static EventCallback<AttachToPanelEvent>? OnPlayWarningDialog;
        public static EventCallback<AttachToPanelEvent>? OnPlayErrorDialog;

        public static VisualElement VisualElement() {
            var result = Create<VisualElement>( null, null );
            return result;
        }

        public static Label Label(string? text) {
            var result = Create<Label>( null, null );
            result.text = text;
            return result;
        }

        public static Button Button(string? text) {
            var result = Create<Button>( null, null );
            result.text = text;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ClickEvent>( evt => OnPlayClick?.Invoke( evt ) );
            return result;
        }
        public static RepeatButton RepeatButton(string? text) {
            var result = Create<RepeatButton>( null, null );
            result.text = text;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ClickEvent>( evt => OnPlayClick?.Invoke( evt ) );
            return result;
        }

        public static Button Select(string? text) {
            var result = Create<Button>( null, "select" );
            result.text = text;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ClickEvent>( evt => OnPlaySelect?.Invoke( evt ) );
            return result;
        }
        public static Button Resume(string? text) {
            var result = Create<Button>( null, "resume" );
            result.text = text;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ClickEvent>( evt => OnPlaySelect?.Invoke( evt ) );
            return result;
        }
        public static Button Back(string? text) {
            var result = Create<Button>( null, "back" );
            result.text = text;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ClickEvent>( evt => OnPlaySelect?.Invoke( evt ) );
            return result;
        }
        public static Button Exit(string? text) {
            var result = Create<Button>( null, "exit" );
            result.text = text;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ClickEvent>( evt => OnPlaySelect?.Invoke( evt ) );
            return result;
        }
        public static Button Quit(string? text) {
            var result = Create<Button>( null, "quit" );
            result.text = text;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ClickEvent>( evt => OnPlaySelect?.Invoke( evt ) );
            return result;
        }

        public static Button Submit(string? text) {
            var result = Create<Button>( null, "submit" );
            result.text = text;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ClickEvent>( evt => OnPlaySubmit?.Invoke( evt ) );
            return result;
        }
        public static Button Cancel(string? text) {
            var result = Create<Button>( null, "cancel" );
            result.text = text;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ClickEvent>( evt => OnPlayCancel?.Invoke( evt ) );
            return result;
        }

        public static TextField TextField(string? label, int maxLength, bool isMultiline = false, bool isReadOnly = false) {
            var result = Create<TextField>( null, null );
            result.label = label;
            result.maxLength = maxLength;
            result.multiline = isMultiline;
            result.isReadOnly = isReadOnly;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ChangeEvent<float>>( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }
        public static PopupField<object?> PopupField(string? label, params object?[] choices) {
            var result = Create<PopupField<object?>>( null, null );
            result.formatSelectedValueCallback = StringSelector;
            result.formatListItemCallback = StringSelector;
            result.label = label;
            result.choices = choices.ToList();
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ChangeEvent<object?>>( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }
        public static DropdownField DropdownField(string? label, params string?[] choices) {
            var result = Create<DropdownField>( null, null );
            result.formatSelectedValueCallback = StringSelector;
            result.formatListItemCallback = StringSelector;
            result.label = label;
            result.choices = choices.ToList();
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ChangeEvent<string?>>( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }
        public static Slider SliderField(string? label, float min, float max) {
            var result = Create<Slider>( null, null );
            result.label = label;
            result.lowValue = min;
            result.highValue = max;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ChangeEvent<float>>( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }
        public static SliderInt IntSliderField(string? label, int min, int max) {
            var result = Create<SliderInt>( null, null );
            result.label = label;
            result.lowValue = min;
            result.highValue = max;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ChangeEvent<int>>( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }
        public static Toggle ToggleField(string? label) {
            var result = Create<Toggle>( null, null );
            result.label = label;
            result.RegisterCallback<FocusEvent>( evt => OnPlayFocus?.Invoke( evt ) );
            result.RegisterCallback<ChangeEvent<bool>>( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }

        // Helpers
        private static T Create<T>(string? name, string? @class) where T : VisualElement, new() {
            var result = new T();
            result.name = name;
            result.AddToClassList( "visual-element" );
            result.AddToClassList( @class );
            return result;
        }

    }
    public static partial class VisualElementFactory {

        public static Card Card() {
            var result = Create<Card>( "card", "card" );
            return result;
        }
        public static Header Header() {
            var result = Create<Header>( "header", "header" );
            return result;
        }
        public static Content Content() {
            var result = Create<Content>( "content", "content" );
            return result;
        }
        public static Footer Footer() {
            var result = Create<Footer>( "footer", "footer" );
            return result;
        }

        public static Card DialogCard() {
            var result = Create<Card>( "dialog-card", "dialog-card" );
            return result;
        }
        public static Card InfoDialogCard() {
            var result = Create<Card>( "info-dialog-card", "info-dialog-card" );
            return result;
        }
        public static Card WarningDialogCard() {
            var result = Create<Card>( "warning-dialog-card", "warning-dialog-card" );
            return result;
        }
        public static Card ErrorDialogCard() {
            var result = Create<Card>( "error-dialog-card", "error-dialog-card" );
            return result;
        }

        public static TabView TabView() {
            var result = Create<TabView>( "tab-view", null );
            return result;
        }
        public static Tab Tab(string label) {
            var result = Create<Tab>( "tab", null );
            result.label = label;
            result.tabHeader.RegisterCallback<MouseDownEvent>( evt => OnPlayClick?.Invoke( evt ) );
            return result;
        }

        public static ScrollView ScrollView() {
            var result = Create<ScrollView>( "scroll-view", null );
            result.horizontalScroller.highButton.BringToFront();
            result.verticalScroller.highButton.BringToFront();
            result.horizontalScroller.lowButton.RegisterCallback<ClickEvent>( evt => OnPlayClick?.Invoke( evt ) );
            result.horizontalScroller.highButton.RegisterCallback<ClickEvent>( evt => OnPlayClick?.Invoke( evt ) );
            result.verticalScroller.lowButton.RegisterCallback<ClickEvent>( evt => OnPlayClick?.Invoke( evt ) );
            result.verticalScroller.highButton.RegisterCallback<ClickEvent>( evt => OnPlayClick?.Invoke( evt ) );
            result.horizontalScroller.slider.RegisterCallback<ChangeEvent<float>>( evt => OnPlayChange?.Invoke( evt ) );
            result.verticalScroller.slider.RegisterCallback<ChangeEvent<float>>( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }

        public static ColumnScope ColumnScope() {
            var result = Create<ColumnScope>( "scope", null );
            return result;
        }
        public static RowScope RowScope() {
            var result = Create<RowScope>( "scope", null );
            return result;
        }

        public static ColumnGroup ColumnGroup() {
            var result = Create<ColumnGroup>( "group", null );
            return result;
        }
        public static RowGroup RowGroup() {
            var result = Create<RowGroup>( "group", null );
            return result;
        }

        public static Box Box() {
            var result = Create<Box>( "box", null );
            return result;
        }

    }
    public abstract class View : UIViewBase {

        public View(string? name) {
            base.name = name;
            AddToClassList( "view" );
        }

    }
    public abstract class WidgetView : UIViewBase {

        public WidgetView(string? name) {
            base.name = name;
            AddToClassList( "widget-view" );
        }

    }
    public abstract class LeftWidgetView : UIViewBase {

        public LeftWidgetView(string? name) {
            base.name = name;
            AddToClassList( "left-widget-view" );
        }

    }
    public abstract class SmallWidgetView : UIViewBase {

        public SmallWidgetView(string? name) {
            base.name = name;
            AddToClassList( "small-widget-view" );
        }

    }
    public abstract class MediumWidgetView : UIViewBase {

        public MediumWidgetView(string? name) {
            base.name = name;
            AddToClassList( "medium-widget-view" );
        }

    }
    public abstract class LargeWidgetView : UIViewBase {

        public LargeWidgetView(string? name) {
            base.name = name;
            AddToClassList( "large-widget-view" );
        }

    }
    public abstract class DialogWidgetViewBase : UIViewBase {

        public DialogWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "dialog-widget-view" );
            RegisterCallbackOnce<AttachToPanelEvent>( evt => VisualElementFactory.OnPlayDialog?.Invoke( evt ) );
        }

    }
    public abstract class InfoDialogWidgetViewBase : UIViewBase {

        public InfoDialogWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "info-dialog-widget-view" );
            RegisterCallbackOnce<AttachToPanelEvent>( evt => VisualElementFactory.OnPlayInfoDialog?.Invoke( evt ) );
        }

    }
    public abstract class WarningDialogWidgetViewBase : UIViewBase {

        public WarningDialogWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "warning-dialog-widget-view" );
            RegisterCallbackOnce<AttachToPanelEvent>( evt => VisualElementFactory.OnPlayWarningDialog?.Invoke( evt ) );
        }

    }
    public abstract class ErrorDialogWidgetViewBase : UIViewBase {

        public ErrorDialogWidgetViewBase(string? name) {
            base.name = name;
            AddToClassList( "error-dialog-widget-view" );
            RegisterCallbackOnce<AttachToPanelEvent>( evt => VisualElementFactory.OnPlayErrorDialog?.Invoke( evt ) );
        }

    }
}
