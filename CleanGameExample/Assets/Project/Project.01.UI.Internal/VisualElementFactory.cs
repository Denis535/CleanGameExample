#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static partial class VisualElementFactory {

        // StringSelector
        public static Func<object?, string?>? StringSelector { get; set; }
        // OnWidgetAttach
        public static event Action<VisualElement>? OnWidgetAttach;
        public static event Action<VisualElement>? OnWidgetDetach;
        // OnPlaySfx
        public static event EventCallback<EventBase>? OnPlayClick;
        public static event EventCallback<ClickEvent>? OnPlaySelect;
        public static event EventCallback<ClickEvent>? OnPlaySubmit;
        public static event EventCallback<ClickEvent>? OnPlayCancel;
        public static event EventCallback<IChangeEvent>? OnPlayChange;
        // OnPlaySfx
        public static event EventCallback<FocusEvent>? OnPlayFocus;
        // OnPlaySfx
        public static event EventCallback<AttachToPanelEvent>? OnPlayDialog;
        public static event EventCallback<AttachToPanelEvent>? OnPlayInfoDialog;
        public static event EventCallback<AttachToPanelEvent>? OnPlayWarningDialog;
        public static event EventCallback<AttachToPanelEvent>? OnPlayErrorDialog;

        // VisualElement
        public static VisualElement VisualElement() {
            var result = Create<VisualElement>( null, null );
            return result;
        }

        // Label
        public static Label Label(string? text) {
            var result = Create<Label>( null, null );
            result.text = text;
            return result;
        }

        // Button
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

        // Button
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

        // Button
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

        // Field
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
            Scope.Peek?.VisualElement.Add( result );
            return result;
        }

    }
    public static partial class VisualElementFactory {

        // Widget
        public static Widget Widget() {
            var result = Create<Widget>( "widget", "widget" );
            result.RegisterCallback<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallback<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget LeftWidget() {
            var result = Create<Widget>( "left-widget", "left-widget" );
            result.RegisterCallback<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallback<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget SmallWidget() {
            var result = Create<Widget>( "small-widget", "small-widget" );
            result.RegisterCallback<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallback<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget MediumWidget() {
            var result = Create<Widget>( "medium-widget", "medium-widget" );
            result.RegisterCallback<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallback<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget LargeWidget() {
            var result = Create<Widget>( "large-widget", "large-widget" );
            result.RegisterCallback<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallback<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }

        // Widget
        public static Widget DialogWidget() {
            var result = Create<Widget>( "dialog-widget", "dialog-widget" );
            result.RegisterCallback<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnPlayDialog?.Invoke( evt ) );
            result.RegisterCallback<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget InfoDialogWidget() {
            var result = Create<Widget>( "info-dialog-widget", "info-dialog-widget" );
            result.RegisterCallback<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnPlayInfoDialog?.Invoke( evt ) );
            result.RegisterCallback<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget WarningDialogWidget() {
            var result = Create<Widget>( "warning-dialog-widget", "warning-dialog-widget" );
            result.RegisterCallback<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnPlayWarningDialog?.Invoke( evt ) );
            result.RegisterCallback<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget ErrorDialogWidget() {
            var result = Create<Widget>( "error-dialog-widget", "error-dialog-widget" );
            result.RegisterCallback<AttachToPanelEvent>( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.RegisterCallbackOnce<AttachToPanelEvent>( evt => OnPlayErrorDialog?.Invoke( evt ) );
            result.RegisterCallback<DetachFromPanelEvent>( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }

        // Card
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

        // Card
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

        // TabView
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

        // ScrollView
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

        // Scope
        public static ColumnScope ColumnScope() {
            var result = Create<ColumnScope>( "scope", null );
            return result;
        }
        public static RowScope RowScope() {
            var result = Create<RowScope>( "scope", null );
            return result;
        }

        // Group
        public static ColumnGroup ColumnGroup() {
            var result = Create<ColumnGroup>( "group", null );
            return result;
        }
        public static RowGroup RowGroup() {
            var result = Create<RowGroup>( "group", null );
            return result;
        }

        // Box
        public static Box Box() {
            var result = Create<Box>( "box", null );
            return result;
        }

    }
    class Scope : IDisposable {

        private static Stack<Scope> Stack { get; } = new Stack<Scope>();
        public static Scope? Peek => Stack.Any() ? Stack.Peek() : null;

        public VisualElement VisualElement { get; }

        public Scope(VisualElement visualElement) {
            VisualElement = visualElement;
            Stack.Push( this );
        }
        public void Dispose() {
            Stack.Pop();
        }

    }
    static class ScopeExtensions {

        public static T Out<T>(this T element, out T @out) where T : VisualElement {
            @out = element;
            return element;
        }

        public static Scope AsScope(this VisualElement element) {
            return new Scope( element );
        }

    }
}
