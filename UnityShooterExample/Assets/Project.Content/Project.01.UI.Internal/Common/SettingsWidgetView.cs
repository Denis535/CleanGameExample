#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public class SettingsWidgetView : MediumWidgetView {

        public Card Card { get; }
        public Header Header { get; }
        public Label Title { get; }
        public Content Content { get; }
        public TabView TabView { get; }
        public Tab ProfileSettingsTab { get; }
        public Tab VideoSettingsTab { get; }
        public Tab AudioSettingsTab { get; }
        public Footer Footer { get; }
        public Button Okey { get; }
        public Button Back { get; }

        public SettingsWidgetView() : base( "settings-widget-view" ) {
            Add(
                Card = VisualElementFactory.Card().Children(
                    Header = VisualElementFactory.Header().Children(
                        Title = VisualElementFactory.Label( "Settings" )
                    ),
                    Content = VisualElementFactory.Content().Children(
                        TabView = VisualElementFactory.TabView().Classes( "no-outline", "grow-1" ).Children(
                            ProfileSettingsTab = VisualElementFactory.Tab( "Profile Settings" ),
                            VideoSettingsTab = VisualElementFactory.Tab( "Video Settings" ),
                            AudioSettingsTab = VisualElementFactory.Tab( "Audio Settings" )
                        )
                    ),
                    Footer = VisualElementFactory.Footer().Children(
                        Okey = VisualElementFactory.Submit( "Ok" ),
                        Back = VisualElementFactory.Cancel( "Back" )
                    )
                )
            );
            this.OnValidate( evt => {
                Okey.SetValid(
                    ProfileSettingsTab.Descendants().All( i => i.IsValidSelf() ) &&
                    VideoSettingsTab.Descendants().All( i => i.IsValidSelf() ) &&
                    AudioSettingsTab.Descendants().All( i => i.IsValidSelf() ) );
            } );
        }
        public override void Dispose() {
            base.Dispose();
        }

        protected override bool AddView(UIViewBase view) {
            if (view is ProfileSettingsWidgetView profileSettings) {
                ProfileSettingsTab.Add( profileSettings );
                return true;
            }
            if (view is VideoSettingsWidgetView videoSettings) {
                VideoSettingsTab.Add( videoSettings );
                return true;
            }
            if (view is AudioSettingsWidgetView audioSettings) {
                AudioSettingsTab.Add( audioSettings );
                return true;
            }
            return false;
        }
        protected override bool RemoveView(UIViewBase view) {
            if (view is ProfileSettingsWidgetView profileSettings) {
                ProfileSettingsTab.Clear();
                return true;
            }
            if (view is VideoSettingsWidgetView videoSettings) {
                VideoSettingsTab.Clear();
                return true;
            }
            if (view is AudioSettingsWidgetView audioSettings) {
                AudioSettingsTab.Clear();
                return true;
            }
            return false;
        }

    }
}
