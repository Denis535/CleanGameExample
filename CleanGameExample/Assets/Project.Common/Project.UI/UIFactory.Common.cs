#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static partial class UIFactory {
        public static class Common {

            public static Widget DialogWidget(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
                using (VisualElementFactory.DialogWidget().AsScope( out widget )) {
                    using (VisualElementFactory.DialogCard().AsScope( out card )) {
                        using (VisualElementFactory.Header().AsScope( out header )) {
                            VisualElementFactory.Label( null ).AddToScope( out title );
                        }
                        using (VisualElementFactory.Content().AsScope( out content )) {
                            using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                VisualElementFactory.Label( null ).AddToScope( out message );
                            }
                        }
                        using (VisualElementFactory.Footer().AsScope( out footer )) {
                        }
                    }
                }
                return widget;
            }
            public static Widget InfoDialogWidget(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
                using (VisualElementFactory.InfoDialogWidget().AsScope( out widget )) {
                    using (VisualElementFactory.InfoDialogCard().AsScope( out card )) {
                        using (VisualElementFactory.Header().AsScope( out header )) {
                            VisualElementFactory.Label( null ).AddToScope( out title );
                        }
                        using (VisualElementFactory.Content().AsScope( out content )) {
                            using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                VisualElementFactory.Label( null ).AddToScope( out message );
                            }
                        }
                        using (VisualElementFactory.Footer().AsScope( out footer )) {
                        }
                    }
                }
                return widget;
            }
            public static Widget WarningDialogWidget(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
                using (VisualElementFactory.WarningDialogWidget().AsScope( out widget )) {
                    using (VisualElementFactory.WarningDialogCard().AsScope( out card )) {
                        using (VisualElementFactory.Header().AsScope( out header )) {
                            VisualElementFactory.Label( null ).AddToScope( out title );
                        }
                        using (VisualElementFactory.Content().AsScope( out content )) {
                            using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                VisualElementFactory.Label( null ).AddToScope( out message );
                            }
                        }
                        using (VisualElementFactory.Footer().AsScope( out footer )) {
                        }
                    }
                }
                return widget;
            }
            public static Widget ErrorDialogWidget(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
                using (VisualElementFactory.ErrorDialogWidget().AsScope( out widget )) {
                    using (VisualElementFactory.ErrorDialogCard().AsScope( out card )) {
                        using (VisualElementFactory.Header().AsScope( out header )) {
                            VisualElementFactory.Label( null ).AddToScope( out title );
                        }
                        using (VisualElementFactory.Content().AsScope( out content )) {
                            using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                VisualElementFactory.Label( null ).AddToScope( out message );
                            }
                        }
                        using (VisualElementFactory.Footer().AsScope( out footer )) {
                        }
                    }
                }
                return widget;
            }

            // SettingsWidget
            public static Widget SettingsWidget(out Widget widget, out Label title, out TabView tabView, out Tab profileSettingsSlot, out Tab videoSettingsSlot, out Tab audioSettingsSlot, out Button okey, out Button back) {
                using (VisualElementFactory.MediumWidget().AsScope( out widget )) {
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
            public static VisualElement ProfileSettingsWidget(out VisualElement root, out TextField name) {
                using (VisualElementFactory.View().Classes( "grow-1" ).AsScope( out root )) {
                    using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                        VisualElementFactory.TextField( "Name", null, 16 ).Classes( "label-width-25pc" ).AddToScope( out name );
                    }
                }
                return root;
            }
            public static VisualElement VideoSettingsWidget(out VisualElement root, out Toggle isFullScreen, out PopupField<object?> screenResolution, out Toggle isVSync) {
                using (VisualElementFactory.View().Classes( "grow-1" ).AsScope( out root )) {
                    using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                        VisualElementFactory.ToggleField( "Full Screen", false ).Classes( "label-width-25pc" ).AddToScope( out isFullScreen );
                        VisualElementFactory.PopupField( "Screen Resolution", null ).Classes( "label-width-25pc" ).AddToScope( out screenResolution );
                        VisualElementFactory.ToggleField( "V-Sync", false ).Classes( "label-width-25pc" ).AddToScope( out isVSync );
                    }
                }
                return root;
            }
            public static VisualElement AudioSettingsWidget(out VisualElement root, out Slider masterVolume, out Slider musicVolume, out Slider sfxVolume, out Slider gameVolume) {
                using (VisualElementFactory.View().Classes( "grow-1" ).AsScope( out root )) {
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
