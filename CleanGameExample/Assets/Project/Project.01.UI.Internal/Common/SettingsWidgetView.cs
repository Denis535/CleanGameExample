#nullable enable
namespace Project.UI.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class SettingsWidgetView : MediumWidgetView {

        public Label Title { get; }
        public TabView TabView { get; }
        public Tab ProfileSettingsTab { get; }
        public Tab VideoSettingsTab { get; }
        public Tab AudioSettingsTab { get; }
        public Button Okey { get; }
        public Button Back { get; }

        public SettingsWidgetView() : base( "settings-widget-view" ) {
            Add(
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
            this.OnValidate( evt => {
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
