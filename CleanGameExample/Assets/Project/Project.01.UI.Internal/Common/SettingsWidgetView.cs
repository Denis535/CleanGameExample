#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class SettingsWidgetView : UIViewBase2 {

        protected override VisualElement VisualElement => Widget;
        private Widget Widget { get; }
        private Label Title { get; }
        private TabView TabView { get; }
        private Tab ProfileSettingsTab { get; }
        private Tab VideoSettingsTab { get; }
        private Tab AudioSettingsTab { get; }
        private Button Okey { get; }
        private Button Back { get; }

        public ProfileSettingsWidgetView? ProfileSettingsEvent {
            get => ProfileSettingsTab.GetViews<ProfileSettingsWidgetView>().FirstOrDefault();
            set {
                if (value != null) {
                    ProfileSettingsTab.AddView( value );
                } else {
                    ProfileSettingsTab.Clear();
                }
            }
        }
        public VideoSettingsWidgetView? VideoSettingsEvent {
            get => VideoSettingsTab.GetViews<VideoSettingsWidgetView>().FirstOrDefault();
            set {
                if (value != null) {
                    VideoSettingsTab.AddView( value );
                } else {
                    VideoSettingsTab.Clear();
                }
            }
        }
        public AudioSettingsWidgetView? AudioSettingsEvent {
            get => AudioSettingsTab.GetViews<AudioSettingsWidgetView>().FirstOrDefault();
            set {
                if (value != null) {
                    AudioSettingsTab.AddView( value );
                } else {
                    AudioSettingsTab.Clear();
                }
            }
        }
        public event EventCallback<ClickEvent> OnOkeyEvent {
            add => Okey.RegisterCallback( value );
            remove => Okey.RegisterCallback( value );
        }
        public event EventCallback<ClickEvent> OnBackEvent {
            add => Back.RegisterCallback( value );
            remove => Back.RegisterCallback( value );
        }

        public SettingsWidgetView() {
            Widget = VisualElementFactory.MediumWidget( "settings-widget" ).UserData( this ).Children(
                VisualElementFactory.Card().Children(
                    VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Settings" )
                    ),
                    VisualElementFactory.Content().Children(
                        TabView = VisualElementFactory.TabView().Classes( "no-outline", "grow-1" ).Children(
                            ProfileSettingsTab = VisualElementFactory.Tab( "Profile Settings" ),
                            VideoSettingsTab = VisualElementFactory.Tab( "Video Settings" ),
                            AudioSettingsTab = VisualElementFactory.Tab( "Audio Settings" )
                        )
                    ),
                    VisualElementFactory.Footer().Children(
                        Okey = VisualElementFactory.Submit( "Ok" ),
                        Back = VisualElementFactory.Cancel( "Back" )
                    )
                )
            );
            Widget.OnValidate( evt => {
                Okey.SetValid(
                    ProfileSettingsTab.GetDescendants().All( i => i.IsValidSelf() ) &&
                    VideoSettingsTab.GetDescendants().All( i => i.IsValidSelf() ) &&
                    AudioSettingsTab.GetDescendants().All( i => i.IsValidSelf() ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
}
