#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public static partial class UIFactory {
        public static class Common {

            // SettingsWidget
            public static Widget SettingsWidget(UIViewBase view, out Widget widget, out Label title, out TabView tabView, out Tab profileSettingsSlot, out Tab videoSettingsSlot, out Tab audioSettingsSlot, out Button okey, out Button back) {
                using (VisualElementFactory.MediumWidget( view ).AsScope( out widget )) {
                    using (VisualElementFactory.Card().AsScope()) {
                        using (VisualElementFactory.Header().AsScope()) {
                            VisualElementFactory.Label( "Settings" ).AddToScope( out title );
                        }
                        using (VisualElementFactory.Content().AsScope()) {
                            using (VisualElementFactory.TabView().Classes( "no-outline", "grow-1" ).AsScope( out tabView )) {
                                VisualElementFactory.Tab( "Profile Settings" ).AddToScope( out profileSettingsSlot );
                                VisualElementFactory.Tab( "Video Settings" ).AddToScope( out videoSettingsSlot );
                                VisualElementFactory.Tab( "Audio Settings" ).AddToScope( out audioSettingsSlot );
                            }
                        }
                        using (VisualElementFactory.Footer().AsScope()) {
                            VisualElementFactory.Submit( "Ok" ).AddToScope( out okey );
                            VisualElementFactory.Cancel( "Back" ).AddToScope( out back );
                        }
                    }
                }
                return widget;
            }
            public static VisualElement ProfileSettingsWidget(UIViewBase view, out VisualElement root, out TextField name) {
                using (VisualElementFactory.View( view ).Classes( "grow-1" ).AsScope( out root )) {
                    using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                        VisualElementFactory.TextField( "Name", null, 16 ).Classes( "label-width-25pc" ).AddToScope( out name );
                    }
                }
                return root;
            }
            public static VisualElement VideoSettingsWidget(UIViewBase view, out VisualElement root, out Toggle isFullScreen, out PopupField<object?> screenResolution, out Toggle isVSync) {
                using (VisualElementFactory.View( view ).Classes( "grow-1" ).AsScope( out root )) {
                    using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                        VisualElementFactory.ToggleField( "Full Screen", false ).Classes( "label-width-25pc" ).AddToScope( out isFullScreen );
                        VisualElementFactory.PopupField( "Screen Resolution", null ).Classes( "label-width-25pc" ).AddToScope( out screenResolution );
                        VisualElementFactory.ToggleField( "V-Sync", false ).Classes( "label-width-25pc" ).AddToScope( out isVSync );
                    }
                }
                return root;
            }
            public static VisualElement AudioSettingsWidget(UIViewBase view, out VisualElement root, out Slider masterVolume, out Slider musicVolume, out Slider sfxVolume, out Slider gameVolume) {
                using (VisualElementFactory.View( view ).Classes( "grow-1" ).AsScope( out root )) {
                    using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                        VisualElementFactory.SliderField( "Master Volume", 0, 0, 1 ).Classes( "label-width-25pc" ).AddToScope( out masterVolume );
                        VisualElementFactory.SliderField( "Music Volume", 0, 0, 1 ).Classes( "label-width-25pc" ).AddToScope( out musicVolume );
                        VisualElementFactory.SliderField( "Sfx Volume", 0, 0, 1 ).Classes( "label-width-25pc" ).AddToScope( out sfxVolume );
                        VisualElementFactory.SliderField( "Game Volume", 0, 0, 1 ).Classes( "label-width-25pc" ).AddToScope( out gameVolume );
                    }
                }
                return root;
            }

        }
    }
}
