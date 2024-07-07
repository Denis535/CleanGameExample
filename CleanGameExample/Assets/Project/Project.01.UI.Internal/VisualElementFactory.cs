#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static partial class VisualElementFactory {

        public static Func<object?, string?>? StringSelector { get; set; }

        public static event Action<VisualElement>? OnWidgetAttach;
        public static event Action<VisualElement>? OnWidgetDetach;

        public static event EventCallback<EventBase>? OnPlayClick;
        public static event EventCallback<ClickEvent>? OnPlaySelect;
        public static event EventCallback<ClickEvent>? OnPlaySubmit;
        public static event EventCallback<ClickEvent>? OnPlayCancel;
        public static event EventCallback<IChangeEvent>? OnPlayChange;

        public static event EventCallback<FocusEvent>? OnPlayFocus;

        public static event EventCallback<AttachToPanelEvent>? OnPlayDialog;
        public static event EventCallback<AttachToPanelEvent>? OnPlayInfoDialog;
        public static event EventCallback<AttachToPanelEvent>? OnPlayWarningDialog;
        public static event EventCallback<AttachToPanelEvent>? OnPlayErrorDialog;

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
            VisualElementScope.Peek?.VisualElement.Add( result );
            return result;
        }

    }
    public static partial class VisualElementFactory {

        public static Widget Widget(string name) {
            var result = Create<Widget>( name, "widget" );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget LeftWidget(string name) {
            var result = Create<Widget>( name, "left-widget" );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget SmallWidget(string name) {
            var result = Create<Widget>( name, "small-widget" );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget MediumWidget(string name) {
            var result = Create<Widget>( name, "medium-widget" );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget LargeWidget(string name) {
            var result = Create<Widget>( name, "large-widget" );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }

        public static Widget DialogWidget() {
            var result = Create<Widget>( "dialog-widget", "dialog-widget" );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnPlayDialog?.Invoke( evt ) );
            result.RegisterCallbackOnce<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget InfoDialogWidget() {
            var result = Create<Widget>( "info-dialog-widget", "info-dialog-widget" );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnPlayInfoDialog?.Invoke( evt ) );
            result.RegisterCallbackOnce<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget WarningDialogWidget() {
            var result = Create<Widget>( "warning-dialog-widget", "warning-dialog-widget" );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnPlayWarningDialog?.Invoke( evt ) );
            result.RegisterCallbackOnce<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget ErrorDialogWidget() {
            var result = Create<Widget>( "error-dialog-widget", "error-dialog-widget" );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnPlayErrorDialog?.Invoke( evt ) );
            result.RegisterCallbackOnce<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }

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
    internal abstract class VisualElementScope : IDisposable {

        private static Stack<VisualElementScope> Stack { get; } = new Stack<VisualElementScope>();
        public static VisualElementScope? Peek => Stack.Any() ? Stack.Peek() : null;

        public VisualElement VisualElement { get; }

        public VisualElementScope(VisualElement visualElement) {
            VisualElement = visualElement;
            Stack.Push( this );
        }
        public void Dispose() {
            Stack.Pop();
        }

    }
    internal class VisualElementScope<T> : VisualElementScope where T : VisualElement {

        public new T VisualElement => (T) base.VisualElement;

        public VisualElementScope(T visualElement) : base( visualElement ) {
        }

        public VisualElementScope<T> Out(out T @out) {
            @out = VisualElement;
            return this;
        }

    }
    internal static class VisualElementScopeExtensions {

        public static VisualElementScope<T> AsScope<T>(this T element) where T : VisualElement {
            return new VisualElementScope<T>( element );
        }

    }
}
