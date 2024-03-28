#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public static class UIFactoryExtensions_Common {

        // DialogWidget
        public static Widget DialogWidget(this UIFactory factory, UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (factory.DialogWidget( view ).AsScope( out widget )) {
                using (factory.DialogCard().AsScope( out card )) {
                    using (factory.Header().AsScope( out header )) {
                        factory.Label( null ).AddToScope( out title );
                    }
                    using (factory.Content().AsScope( out content )) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            factory.Label( null ).AddToScope( out message );
                        }
                    }
                    using (factory.Footer().AsScope( out footer )) {
                    }
                }
            }
            return widget;
        }
        public static Widget InfoDialogWidget(this UIFactory factory, UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (factory.InfoDialogWidget( view ).AsScope( out widget )) {
                using (factory.InfoDialogCard().AsScope( out card )) {
                    using (factory.Header().AsScope( out header )) {
                        factory.Label( null ).AddToScope( out title );
                    }
                    using (factory.Content().AsScope( out content )) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            factory.Label( null ).AddToScope( out message );
                        }
                    }
                    using (factory.Footer().AsScope( out footer )) {
                    }
                }
            }
            return widget;
        }
        public static Widget WarningDialogWidget(this UIFactory factory, UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (factory.WarningDialogWidget( view ).AsScope( out widget )) {
                using (factory.WarningDialogCard().AsScope( out card )) {
                    using (factory.Header().AsScope( out header )) {
                        factory.Label( null ).AddToScope( out title );
                    }
                    using (factory.Content().AsScope( out content )) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            factory.Label( null ).AddToScope( out message );
                        }
                    }
                    using (factory.Footer().AsScope( out footer )) {
                    }
                }
            }
            return widget;
        }
        public static Widget ErrorDialogWidget(this UIFactory factory, UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (factory.ErrorDialogWidget( view ).AsScope( out widget )) {
                using (factory.ErrorDialogCard().AsScope( out card )) {
                    using (factory.Header().AsScope( out header )) {
                        factory.Label( null ).AddToScope( out title );
                    }
                    using (factory.Content().AsScope( out content )) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            factory.Label( null ).AddToScope( out message );
                        }
                    }
                    using (factory.Footer().AsScope( out footer )) {
                    }
                }
            }
            return widget;
        }

        // SettingsWidget
        public static Widget SettingsWidget(this UIFactory factory, UIViewBase view, out Widget widget, out Label title, out TabView tabView, out Tab profileSettingsSlot, out Tab videoSettingsSlot, out Tab audioSettingsSlot, out Button okey, out Button back) {
            using (factory.MediumWidget( view ).AsScope( out widget )) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        factory.Label( "Settings" ).AddToScope( out title );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.TabView().Classes( "no-outline", "grow-1" ).AsScope( out tabView )) {
                            factory.Tab( "Profile Settings" ).AddToScope( out profileSettingsSlot );
                            factory.Tab( "Video Settings" ).AddToScope( out videoSettingsSlot );
                            factory.Tab( "Audio Settings" ).AddToScope( out audioSettingsSlot );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        factory.Submit( "Ok" ).AddToScope( out okey );
                        factory.Cancel( "Back" ).AddToScope( out back );
                    }
                }
            }
            return widget;
        }
        public static VisualElement ProfileSettingsWidget(this UIFactory factory, UIViewBase view, out VisualElement root, out TextField name) {
            using (factory.View( view ).Classes( "grow-1" ).AsScope( out root )) {
                using (factory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    factory.TextField( "Name", null, 16 ).Classes( "label-width-25pc" ).AddToScope( out name );
                }
            }
            return root;
        }
        public static VisualElement VideoSettingsWidget(this UIFactory factory, UIViewBase view, out VisualElement root, out Toggle isFullScreen, out PopupField<object?> screenResolution, out Toggle isVSync) {
            using (factory.View( view ).Classes( "grow-1" ).AsScope( out root )) {
                using (factory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    factory.ToggleField( "Full Screen", false ).Classes( "label-width-25pc" ).AddToScope( out isFullScreen );
                    factory.PopupField( "Screen Resolution", null ).Classes( "label-width-25pc" ).AddToScope( out screenResolution );
                    factory.ToggleField( "V-Sync", false ).Classes( "label-width-25pc" ).AddToScope( out isVSync );
                }
            }
            return root;
        }
        public static VisualElement AudioSettingsWidget(this UIFactory factory, UIViewBase view, out VisualElement root, out Slider masterVolume, out Slider musicVolume, out Slider sfxVolume, out Slider gameVolume) {
            using (factory.View( view ).Classes( "grow-1" ).AsScope( out root )) {
                using (factory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    factory.SliderField( "Master Volume", 0, 0, 1 ).Classes( "label-width-25pc" ).AddToScope( out masterVolume );
                    factory.SliderField( "Music Volume", 0, 0, 1 ).Classes( "label-width-25pc" ).AddToScope( out musicVolume );
                    factory.SliderField( "Sfx Volume", 0, 0, 1 ).Classes( "label-width-25pc" ).AddToScope( out sfxVolume );
                    factory.SliderField( "Game Volume", 0, 0, 1 ).Classes( "label-width-25pc" ).AddToScope( out gameVolume );
                }
            }
            return root;
        }

    }
}
