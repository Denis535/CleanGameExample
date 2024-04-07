#nullable enable
namespace UnityEngine.Framework.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class VisualElementFactory {

        public static Func<object?, string?>? StringSelector { get; set; }
        // OnVisualElementCreated
        public static event Action<VisualElement>? OnVisualElementCreated;
        // OnWidgetAttach
        public static event Action<VisualElement>? OnWidgetAttach;
        public static event Action<VisualElement>? OnWidgetDetach;
        // OnViewAttach
        public static event Action<VisualElement>? OnViewAttach;
        public static event Action<VisualElement>? OnViewDetach;
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

        // Widget
        public static Widget Widget() {
            var result = Create<Widget>( "widget", "widget" );
            result.OnAttachToPanel( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.OnDetachFromPanel( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget LeftWidget() {
            var result = Create<Widget>( "left-widget", "left-widget" );
            result.OnAttachToPanel( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.OnDetachFromPanel( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget SmallWidget() {
            var result = Create<Widget>( "small-widget", "small-widget" );
            result.OnAttachToPanel( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.OnDetachFromPanel( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget MediumWidget() {
            var result = Create<Widget>( "medium-widget", "medium-widget" );
            result.OnAttachToPanel( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.OnDetachFromPanel( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget LargeWidget() {
            var result = Create<Widget>( "large-widget", "large-widget" );
            result.OnAttachToPanel( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.OnDetachFromPanel( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }

        // Widget
        public static Widget DialogWidget() {
            var result = Create<Widget>( "dialog-widget", "dialog-widget" );
            result.OnAttachToPanel( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.OnAttachToPanel( evt => OnPlayDialog?.Invoke( evt ) );
            result.OnDetachFromPanel( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget InfoDialogWidget() {
            var result = Create<Widget>( "info-dialog-widget", "info-dialog-widget" );
            result.OnAttachToPanel( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.OnAttachToPanel( evt => OnPlayInfoDialog?.Invoke( evt ) );
            result.OnDetachFromPanel( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget WarningDialogWidget() {
            var result = Create<Widget>( "warning-dialog-widget", "warning-dialog-widget" );
            result.OnAttachToPanel( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.OnAttachToPanel( evt => OnPlayWarningDialog?.Invoke( evt ) );
            result.OnDetachFromPanel( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }
        public static Widget ErrorDialogWidget() {
            var result = Create<Widget>( "error-dialog-widget", "error-dialog-widget" );
            result.OnAttachToPanel( evt => OnWidgetAttach?.Invoke( (VisualElement) evt.target ) );
            result.OnAttachToPanel( evt => OnPlayErrorDialog?.Invoke( evt ) );
            result.OnDetachFromPanel( evt => OnWidgetDetach?.Invoke( (VisualElement) evt.target ) );
            return result;
        }

        // View
        public static VisualElement View() {
            var result = Create<VisualElement>( "view", "view" );
            result.OnAttachToPanel( evt => OnViewAttach?.Invoke( (VisualElement) evt.target ) );
            result.OnDetachFromPanel( evt => OnViewDetach?.Invoke( (VisualElement) evt.target ) );
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
            result.tabHeader.OnMouseDown( evt => OnPlayClick?.Invoke( evt ) );
            return result;
        }

        // ScrollView
        public static ScrollView ScrollView() {
            var result = Create<ScrollView>( "scroll-view", null );
            result.horizontalScroller.highButton.BringToFront();
            result.verticalScroller.highButton.BringToFront();
            result.horizontalScroller.lowButton.OnClick( evt => OnPlayClick?.Invoke( evt ) );
            result.horizontalScroller.highButton.OnClick( evt => OnPlayClick?.Invoke( evt ) );
            result.verticalScroller.lowButton.OnClick( evt => OnPlayClick?.Invoke( evt ) );
            result.verticalScroller.highButton.OnClick( evt => OnPlayClick?.Invoke( evt ) );
            result.horizontalScroller.slider.OnChange( evt => OnPlayChange?.Invoke( evt ) );
            result.verticalScroller.slider.OnChange( evt => OnPlayChange?.Invoke( evt ) );
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
            result.OnFocus( evt => OnPlayFocus?.Invoke( evt ) );
            result.OnClick( evt => OnPlayClick?.Invoke( evt ) );
            return result;
        }
        public static RepeatButton RepeatButton(string? text) {
            var result = Create<RepeatButton>( null, null );
            result.text = text;
            result.OnFocus( evt => OnPlayFocus?.Invoke( evt ) );
            result.OnClick( evt => OnPlayClick?.Invoke( evt ) );
            return result;
        }

        // Select
        public static Button Select(string? text) {
            var result = Create<Button>( null, null );
            result.text = text;
            result.OnFocus( evt => OnPlayFocus?.Invoke( evt ) );
            result.OnClick( evt => OnPlaySelect?.Invoke( evt ) );
            return result;
        }
        public static Button Submit(string? text) {
            var result = Create<Button>( null, null );
            result.text = text;
            result.OnFocus( evt => OnPlayFocus?.Invoke( evt ) );
            result.OnClick( evt => OnPlaySubmit?.Invoke( evt ) );
            return result;
        }
        public static Button Cancel(string? text) {
            var result = Create<Button>( null, null );
            result.text = text;
            result.OnFocus( evt => OnPlayFocus?.Invoke( evt ) );
            result.OnClick( evt => OnPlayCancel?.Invoke( evt ) );
            return result;
        }

        // Field
        public static TextField TextField(string? label, string? value, int maxLength, bool isMultiline = false, bool isReadOnly = false) {
            var result = Create<TextField>( null, null );
            result.label = label;
            result.value = value;
            result.maxLength = maxLength;
            result.multiline = isMultiline;
            result.isReadOnly = isReadOnly;
            result.OnFocus( evt => OnPlayFocus?.Invoke( evt ) );
            result.OnChange( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }
        public static PopupField<object?> PopupField(string? label, object? value, params object?[] choices) {
            var result = Create<PopupField<object?>>( null, null );
            result.formatSelectedValueCallback = StringSelector;
            result.formatListItemCallback = StringSelector;
            result.label = label;
            result.value = value;
            result.choices = choices.ToList();
            result.OnFocus( evt => OnPlayFocus?.Invoke( evt ) );
            result.OnChange( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }
        public static DropdownField DropdownField(string? label, string? value, params string?[] choices) {
            var result = Create<DropdownField>( null, null );
            result.formatSelectedValueCallback = StringSelector;
            result.formatListItemCallback = StringSelector;
            result.label = label;
            result.value = value;
            result.choices = choices.ToList();
            result.OnFocus( evt => OnPlayFocus?.Invoke( evt ) );
            result.OnChange( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }
        public static Slider SliderField(string? label, float value, float min, float max) {
            var result = Create<Slider>( null, null );
            result.label = label;
            result.value = value;
            result.lowValue = min;
            result.highValue = max;
            result.OnFocus( evt => OnPlayFocus?.Invoke( evt ) );
            result.OnChange( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }
        public static SliderInt IntSliderField(string? label, int value, int min, int max) {
            var result = Create<SliderInt>( null, null );
            result.label = label;
            result.value = value;
            result.lowValue = min;
            result.highValue = max;
            result.OnFocus( evt => OnPlayFocus?.Invoke( evt ) );
            result.OnChange( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }
        public static Toggle ToggleField(string? label, bool value) {
            var result = Create<Toggle>( null, null );
            result.label = label;
            result.value = value;
            result.OnFocus( evt => OnPlayFocus?.Invoke( evt ) );
            result.OnChange( evt => OnPlayChange?.Invoke( evt ) );
            return result;
        }

        // Helpers
        private static T Create<T>(string? name, string? @class) where T : VisualElement, new() {
            var result = new T();
            result.name = name;
            result.AddToClassList( "visual-element" );
            result.AddToClassList( @class );
            OnVisualElementCreated?.Invoke( result );
            return result;
        }

    }
}
