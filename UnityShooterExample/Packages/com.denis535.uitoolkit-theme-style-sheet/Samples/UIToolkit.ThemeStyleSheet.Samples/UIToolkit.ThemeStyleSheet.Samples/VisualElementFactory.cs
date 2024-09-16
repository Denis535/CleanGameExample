#nullable enable
namespace UIToolkit.ThemeStyleSheet.Samples {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEngine.UIElements.Experimental;

    public class VisualElementFactory : MonoBehaviour {

        // Assets
        [SerializeField] private AudioClip? click = default;
        [SerializeField] private AudioClip? selectClick = default;
        [SerializeField] private AudioClip? submitClick = default;
        [SerializeField] private AudioClip? cancelClick = default;
        [SerializeField] private AudioClip? invalidClick = default;
        [SerializeField] private AudioClip? tik = default;
        [SerializeField] private AudioClip? focus = default;
        [SerializeField] private AudioClip? dialog = default;
        [SerializeField] private AudioClip? infoDialog = default;
        [SerializeField] private AudioClip? warningDialog = default;
        [SerializeField] private AudioClip? errorDialog = default;

        // AudioSource
        private AudioSource AudioSource { get; set; } = default!;

        // Awake
        public void Awake() {
            AudioSource = GetComponent<AudioSource>();
        }
        public void OnDestroy() {
        }

        // VisualElement
        public VisualElement VisualElement() {
            var result = Create<VisualElement>( null );
            return result;
        }

        // Widget
        public Widget Widget() {
            var result = Create<Widget>( "widget" );
            return result;
        }
        public Widget LeftWidget() {
            var result = Create<Widget>( "left-widget", "left-widget" );
            return result;
        }
        public Widget SmallWidget() {
            var result = Create<Widget>( "small-widget", "small-widget" );
            return result;
        }
        public Widget MediumWidget() {
            var result = Create<Widget>( "medium-widget", "medium-widget" );
            return result;
        }
        public Widget LargeWidget() {
            var result = Create<Widget>( "large-widget", "large-widget" );
            return result;
        }

        // Widget
        public Widget DialogWidget() {
            var result = Create<Widget>( "dialog-widget", "dialog-widget" );
            result.OnAttachToPanel( PlayDialog );
            return result;
        }
        public Widget InfoDialogWidget() {
            var result = Create<Widget>( "info-dialog-widget", "info-dialog-widget" );
            result.OnAttachToPanel( PlayInfoDialog );
            return result;
        }
        public Widget WarningDialogWidget() {
            var result = Create<Widget>( "warning-dialog-widget", "warning-dialog-widget" );
            result.OnAttachToPanel( PlayWarningDialog );
            return result;
        }
        public Widget ErrorDialogWidget() {
            var result = Create<Widget>( "error-dialog-widget", "error-dialog-widget" );
            result.OnAttachToPanel( PlayErrorDialog );
            return result;
        }

        // Card
        public Card Card() {
            var result = Create<Card>( "card" );
            return result;
        }
        public Header Header() {
            var result = Create<Header>( "header" );
            return result;
        }
        public Content Content() {
            var result = Create<Content>( "content" );
            return result;
        }
        public Footer Footer() {
            var result = Create<Footer>( "footer" );
            return result;
        }

        // Card
        public Card DialogCard() {
            var result = Create<Card>( "dialog-card", "dialog-card" );
            return result;
        }
        public Card InfoDialogCard() {
            var result = Create<Card>( "info-dialog-card", "info-dialog-card" );
            return result;
        }
        public Card WarningDialogCard() {
            var result = Create<Card>( "warning-dialog-card", "warning-dialog-card" );
            return result;
        }
        public Card ErrorDialogCard() {
            var result = Create<Card>( "error-dialog-card", "error-dialog-card" );
            return result;
        }

        // TabView
        public TabView TabView() {
            var result = Create<TabView>( "tab-view" );
            return result;
        }
        public Tab Tab(string label) {
            var result = Create<Tab>( "tab" );
            result.label = label;
            result.tabHeader.OnMouseDown( PlayClick );
            return result;
        }

        // ScrollView
        public ScrollView ScrollView() {
            var result = Create<ScrollView>( "scroll-view" );
            result.horizontalScroller.lowButton.OnClick( PlayClick );
            result.horizontalScroller.highButton.OnClick( PlayClick );
            result.horizontalScroller.highButton.BringToFront();
            result.horizontalScroller.slider.OnChange( PlayChange );
            result.verticalScroller.lowButton.OnClick( PlayClick );
            result.verticalScroller.highButton.OnClick( PlayClick );
            result.verticalScroller.highButton.BringToFront();
            result.verticalScroller.slider.OnChange( PlayChange );
            return result;
        }

        // Scope
        public ColumnScope ColumnScope() {
            var result = Create<ColumnScope>( "scope" );
            return result;
        }
        public RowScope RowScope() {
            var result = Create<RowScope>( "scope" );
            return result;
        }

        // Group
        public ColumnGroup ColumnGroup() {
            var result = Create<ColumnGroup>( "group" );
            return result;
        }
        public RowGroup RowGroup() {
            var result = Create<RowGroup>( "group" );
            return result;
        }

        // Box
        public Box Box() {
            var result = Create<Box>( "box" );
            return result;
        }

        // Label
        public Label Label(string? text) {
            var result = Create<Label>( null );
            result.text = text;
            return result;
        }

        // Button
        public Button Button(string? text) {
            var result = Create<Button>( null );
            result.text = text;
            result.OnFocus( PlayFocus );
            result.OnClick( PlayClick );
            return result;
        }
        public RepeatButton RepeatButton(string? text) {
            var result = Create<RepeatButton>( null );
            result.text = text;
            result.OnFocus( PlayFocus );
            result.OnClick( PlayClick );
            return result;
        }

        // Select
        public Button Select(string? text) {
            var result = Create<Button>( null );
            result.text = text;
            result.OnFocus( PlayFocus );
            result.OnClick( PlaySelect );
            return result;
        }

        // Submit
        public Button Submit(string? text) {
            var result = Create<Button>( null );
            result.text = text;
            result.OnFocus( PlayFocus );
            result.OnClick( PlaySubmit );
            return result;
        }

        // Cancel
        public Button Cancel(string? text) {
            var result = Create<Button>( null );
            result.text = text;
            result.OnFocus( PlayFocus );
            result.OnClick( PlayCancel );
            return result;
        }

        // Field
        public TextField TextField(string? label, string? value, int maxLength, bool isMultiline = false, bool isReadOnly = false) {
            var result = Create<TextField>( null );
            result.label = label;
            result.value = value;
            result.maxLength = maxLength;
            result.multiline = isMultiline;
            result.isReadOnly = isReadOnly;
            result.OnFocus( PlayFocus );
            result.OnChange( PlayChange );
            return result;
        }
        public PopupField<object?> PopupField(string? label, object? value, object?[]? choices) {
            var result = Create<PopupField<object?>>( null );
            //result.formatSelectedValueCallback = StringSelector;
            //result.formatListItemCallback = StringSelector;
            result.label = label;
            result.value = value;
            result.choices = (choices ?? new object[ 0 ])?.ToList();
            result.OnFocus( PlayFocus );
            result.OnChange( PlayChange );
            return result;
        }
        public DropdownField DropdownField(string? label, string? value, string?[]? choices) {
            var result = Create<DropdownField>( null );
            //result.formatSelectedValueCallback = StringSelector;
            //result.formatListItemCallback = StringSelector;
            result.label = label;
            result.value = value;
            result.choices = (choices ?? new string[ 0 ])?.ToList();
            result.OnFocus( PlayFocus );
            result.OnChange( PlayChange );
            return result;
        }
        public Slider SliderField(string? label, float value, float min, float max) {
            var result = Create<Slider>( null );
            result.label = label;
            result.value = value;
            result.lowValue = min;
            result.highValue = max;
            result.OnFocus( PlayFocus );
            result.OnChange( PlayChange );
            return result;
        }
        public SliderInt IntSliderField(string? label, int value, int min, int max) {
            var result = Create<SliderInt>( null );
            result.label = label;
            result.value = value;
            result.lowValue = min;
            result.highValue = max;
            result.OnFocus( PlayFocus );
            result.OnChange( PlayChange );
            return result;
        }
        public Toggle ToggleField(string? label, bool value) {
            var result = Create<Toggle>( null );
            result.label = label;
            result.value = value;
            result.OnFocus( PlayFocus );
            result.OnChange( PlayChange );
            return result;
        }

        // Helpers
        private void PlayClick(MouseDownEvent evt) {
            var target = (VisualElement) evt.target;
            PlaySound( target.IsValid() ? click : invalidClick, true );
        }
        private void PlayClick(ClickEvent evt) {
            var target = (VisualElement) evt.target;
            PlaySound( target.IsValid() ? click : invalidClick, true );
        }
        private void PlaySelect(ClickEvent evt) {
            var target = (VisualElement) evt.target;
            PlaySound( target.IsValid() ? selectClick : invalidClick, true );
        }
        private void PlaySubmit(ClickEvent evt) {
            var target = (VisualElement) evt.target;
            PlaySound( target.IsValid() ? submitClick : invalidClick, true );
        }
        private void PlayCancel(ClickEvent evt) {
            var target = (VisualElement) evt.target;
            PlaySound( target.IsValid() ? cancelClick : invalidClick, true );
        }
        private void PlayChange(ChangeEvent<object?> evt) {
            if (evt.newValue != evt.previousValue) {
                PlaySound( tik );
            }
        }
        private void PlayChange(ChangeEvent<string?> evt) {
            if (evt.newValue != evt.previousValue) {
                PlaySound( tik );
            }
        }
        private void PlayChange(ChangeEvent<float> evt) {
            if (Mathf.FloorToInt( evt.newValue * 100 ) != Mathf.FloorToInt( evt.previousValue * 100 )) {
                PlaySound( tik );
            }
        }
        private void PlayChange(ChangeEvent<int> evt) {
            if (evt.newValue != evt.previousValue) {
                PlaySound( tik );
            }
        }
        private void PlayChange(ChangeEvent<bool> evt) {
            if (evt.newValue != evt.previousValue) {
                PlaySound( tik );
            }
        }
        // Helpers
        private void PlayFocus(FocusEvent evt) {
            if (evt.direction != FocusChangeDirection.none && evt.direction != FocusChangeDirection.unspecified) {
                PlaySound( focus );
            }
        }
        // Helpers
        private void PlayDialog(AttachToPanelEvent evt) {
            PlaySound( dialog, true );
            PlayAppearance( (VisualElement) evt.target );
        }
        private void PlayInfoDialog(AttachToPanelEvent evt) {
            PlaySound( infoDialog, true );
            PlayAppearance( (VisualElement) evt.target );
        }
        private void PlayWarningDialog(AttachToPanelEvent evt) {
            PlaySound( warningDialog, true );
            PlayAppearance( (VisualElement) evt.target );
        }
        private void PlayErrorDialog(AttachToPanelEvent evt) {
            PlaySound( errorDialog, true );
            PlayAppearance( (VisualElement) evt.target );
        }
        // Helpers
        private void PlaySound(AudioClip? clip, bool isPriority = false) {
            if (isPriority) {
                AudioSource.clip = clip;
                AudioSource.Play();
            } else {
                if (!AudioSource.isPlaying || AudioSource.clip == clip) {
                    AudioSource.clip = clip;
                    AudioSource.Play();
                }
            }
        }
        private static void PlayAppearance(VisualElement element) {
            var animation = ValueAnimation<float>.Create( element, Mathf.LerpUnclamped );
            animation.valueUpdated = (view, t) => {
                var tx = Easing.OutBack( Easing.InPower( t, 2 ), 4 );
                var ty = Easing.OutBack( Easing.OutPower( t, 2 ), 4 );
                var x = Mathf.LerpUnclamped( 0.8f, 1f, tx );
                var y = Mathf.LerpUnclamped( 0.8f, 1f, ty );
                view.transform.scale = new Vector3( x, y, 1 );
            };
            animation.from = 0;
            animation.to = 1;
            animation.durationMs = 500;
            animation.Start();
        }
        // Helpers
        private static T Create<T>(string? name, string? @class = null) where T : VisualElement, new() {
            var result = new T();
            result.name = name;
            result.AddToClassList( "visual-element" );
            result.AddToClassList( @class );
            return result;
        }

    }
}
