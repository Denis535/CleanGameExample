#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Framework.UI;
    using UnityEngine.ResourceManagement.AsyncOperations;
    using UnityEngine.UIElements;
    using UnityEngine.UIElements.Experimental;

    public class UIFactory : MonoBehaviour {

        // Assets
        private AudioClip click = default!;
        private AudioClip selectClick = default!;
        private AudioClip submitClick = default!;
        private AudioClip cancelClick = default!;
        private AudioClip invalidClick = default!;
        private AudioClip tik = default!;
        private AudioClip focus = default!;
        private AudioClip dialog = default!;
        private AudioClip infoDialog = default!;
        private AudioClip warningDialog = default!;
        private AudioClip errorDialog = default!;

        // System
        public static Func<object?, string?>? StringSelector { get; set; }
        public static event Action<UIViewBase>? OnViewAttach;
        public static event Action<UIViewBase>? OnViewDetach;
        // Globals
        private AudioSource AudioSource { get; set; } = default!;

        // Awake
        public void Awake() {
            // Assets
            //click = Addressables2.LoadAssetAsync<AudioClip>( R.Project.UI.Sounds.Click ).GetResult();
            //selectClick = Addressables2.LoadAssetAsync<AudioClip>( R.Project.UI.Sounds.Select ).GetResult();
            //submitClick = Addressables2.LoadAssetAsync<AudioClip>( R.Project.UI.Sounds.Select ).GetResult();
            //cancelClick = Addressables2.LoadAssetAsync<AudioClip>( R.Project.UI.Sounds.Select ).GetResult();
            //invalidClick = Addressables2.LoadAssetAsync<AudioClip>( R.Project.UI.Sounds.Invalid ).GetResult();
            //tik = Addressables2.LoadAssetAsync<AudioClip>( R.Project.UI.Sounds.Tik ).GetResult();
            //focus = Addressables2.LoadAssetAsync<AudioClip>( R.Project.UI.Sounds.Tik ).GetResult();
            //dialog = Addressables2.LoadAssetAsync<AudioClip>( R.Project.UI.Sounds.InfoDialog ).GetResult();
            //infoDialog = Addressables2.LoadAssetAsync<AudioClip>( R.Project.UI.Sounds.InfoDialog ).GetResult();
            //warningDialog = Addressables2.LoadAssetAsync<AudioClip>( R.Project.UI.Sounds.WarningDialog ).GetResult();
            //errorDialog = Addressables2.LoadAssetAsync<AudioClip>( R.Project.UI.Sounds.ErrorDialog ).GetResult();
            // Globals
            AudioSource = GetComponentInChildren<AudioSource>();
        }
        public void OnDestroy() {
            // Assets
            //Addressables2.Release( dialog );
            //Addressables2.Release( infoDialog );
            //Addressables2.Release( warningDialog );
            //Addressables2.Release( errorDialog );
            //Addressables2.Release( focus );
            //Addressables2.Release( click );
            //Addressables2.Release( selectClick );
            //Addressables2.Release( submitClick );
            //Addressables2.Release( cancelClick );
            //Addressables2.Release( invalidClick );
            //Addressables2.Release( tik );
        }

        // Widget
        public Widget Widget(UIViewBase view) {
            var result = Create<Widget>( view, "widget", "widget" );
            return result;
        }
        public Widget LeftWidget(UIViewBase view) {
            var result = Create<Widget>( view, "left-widget", "left-widget" );
            return result;
        }
        public Widget SmallWidget(UIViewBase view) {
            var result = Create<Widget>( view, "small-widget", "small-widget" );
            return result;
        }
        public Widget MediumWidget(UIViewBase view) {
            var result = Create<Widget>( view, "medium-widget", "medium-widget" );
            return result;
        }
        public Widget LargeWidget(UIViewBase view) {
            var result = Create<Widget>( view, "large-widget", "large-widget" );
            return result;
        }

        // Widget
        public Widget DialogWidget(UIViewBase view) {
            var result = Create<Widget>( view, "dialog-widget", "dialog-widget" );
            result.OnAttachToPanel( PlayDialog );
            return result;
        }
        public Widget InfoDialogWidget(UIViewBase view) {
            var result = Create<Widget>( view, "info-dialog-widget", "info-dialog-widget" );
            result.OnAttachToPanel( PlayInfoDialog );
            return result;
        }
        public Widget WarningDialogWidget(UIViewBase view) {
            var result = Create<Widget>( view, "warning-dialog-widget", "warning-dialog-widget" );
            result.OnAttachToPanel( PlayWarningDialog );
            return result;
        }
        public Widget ErrorDialogWidget(UIViewBase view) {
            var result = Create<Widget>( view, "error-dialog-widget", "error-dialog-widget" );
            result.OnAttachToPanel( PlayErrorDialog );
            return result;
        }

        // View
        public VisualElement View(UIViewBase view) {
            var result = Create<VisualElement>( view, "view", "view" );
            return result;
        }

        // Card
        public Card Card() {
            var result = Create<Card>( "card", "card" );
            return result;
        }
        public Header Header() {
            var result = Create<Header>( "header", "header" );
            return result;
        }
        public Content Content() {
            var result = Create<Content>( "content", "content" );
            return result;
        }
        public Footer Footer() {
            var result = Create<Footer>( "footer", "footer" );
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
            var result = Create<TabView>( "tab-view", null );
            return result;
        }
        public Tab Tab(string label) {
            var result = Create<Tab>( "tab", null );
            result.label = label;
            result.tabHeader.OnMouseDown( PlayClick );
            return result;
        }

        // ScrollView
        public ScrollView ScrollView() {
            var result = Create<ScrollView>( "scroll-view", null );
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
            var result = Create<ColumnScope>( "scope", null );
            return result;
        }
        public RowScope RowScope() {
            var result = Create<RowScope>( "scope", null );
            return result;
        }

        // Group
        public ColumnGroup ColumnGroup() {
            var result = Create<ColumnGroup>( "group", null );
            return result;
        }
        public RowGroup RowGroup() {
            var result = Create<RowGroup>( "group", null );
            return result;
        }

        // Box
        public Box Box() {
            var result = Create<Box>( "box", null );
            return result;
        }

        // Label
        public Label Label(string? text) {
            var result = Create<Label>( null, null );
            result.text = text;
            return result;
        }

        // Button
        public Button Button(string? text) {
            var result = Create<Button>( null, null );
            result.text = text;
            result.OnFocus( PlayFocus );
            result.OnClick( PlayClick );
            return result;
        }
        public RepeatButton RepeatButton(string? text) {
            var result = Create<RepeatButton>( null, null );
            result.text = text;
            result.OnFocus( PlayFocus );
            result.OnClick( PlayClick );
            return result;
        }

        // Select
        public Button Select(string? text) {
            var result = Create<Button>( null, null );
            result.text = text;
            result.OnFocus( PlayFocus );
            result.OnClick( PlaySelect );
            return result;
        }
        public Button Submit(string? text) {
            var result = Create<Button>( null, null );
            result.text = text;
            result.OnFocus( PlayFocus );
            result.OnClick( PlaySubmit );
            return result;
        }
        public Button Cancel(string? text) {
            var result = Create<Button>( null, null );
            result.text = text;
            result.OnFocus( PlayFocus );
            result.OnClick( PlayCancel );
            return result;
        }

        // Field
        public TextField TextField(string? label, string? value, int maxLength, bool isMultiline = false, bool isReadOnly = false) {
            var result = Create<TextField>( null, null );
            result.label = label;
            result.value = value;
            result.maxLength = maxLength;
            result.multiline = isMultiline;
            result.isReadOnly = isReadOnly;
            result.OnFocus( PlayFocus );
            result.OnChange( PlayChange );
            return result;
        }
        public PopupField<object?> PopupField(string? label, object? value, params object?[] choices) {
            var result = Create<PopupField<object?>>( null, null );
            result.formatSelectedValueCallback = StringSelector;
            result.formatListItemCallback = StringSelector;
            result.label = label;
            result.value = value;
            result.choices = choices.ToList();
            result.OnFocus( PlayFocus );
            result.OnChange( PlayChange );
            return result;
        }
        public DropdownField DropdownField(string? label, string? value, params string?[] choices) {
            var result = Create<DropdownField>( null, null );
            result.formatSelectedValueCallback = StringSelector;
            result.formatListItemCallback = StringSelector;
            result.label = label;
            result.value = value;
            result.choices = choices.ToList();
            result.OnFocus( PlayFocus );
            result.OnChange( PlayChange );
            return result;
        }
        public Slider SliderField(string? label, float value, float min, float max) {
            var result = Create<Slider>( null, null );
            result.label = label;
            result.value = value;
            result.lowValue = min;
            result.highValue = max;
            result.OnFocus( PlayFocus );
            result.OnChange( PlayChange );
            return result;
        }
        public SliderInt IntSliderField(string? label, int value, int min, int max) {
            var result = Create<SliderInt>( null, null );
            result.label = label;
            result.value = value;
            result.lowValue = min;
            result.highValue = max;
            result.OnFocus( PlayFocus );
            result.OnChange( PlayChange );
            return result;
        }
        public Toggle ToggleField(string? label, bool value) {
            var result = Create<Toggle>( null, null );
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
        private void PlaySound(AudioClip clip, bool isPriority = false) {
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
        private static T Create<T>(UIViewBase view, string? name, string? @class) where T : VisualElement, new() {
            var result = new T();
            result.name = name;
            result.AddToClassList( "visual-element" );
            result.AddToClassList( @class );
            result.OnAttachToPanel( evt => {
                OnViewAttach?.Invoke( view );
            } );
            result.OnDetachFromPanel( evt => {
                OnViewDetach?.Invoke( view );
            } );
            return result;
        }
        private static T Create<T>(string? name, string? @class) where T : VisualElement, new() {
            var result = new T();
            result.name = name;
            result.AddToClassList( "visual-element" );
            result.AddToClassList( @class );
            return result;
        }

    }
}
