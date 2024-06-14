#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class VisualElementFactory_Common {

        // Dialog
        public static Widget Dialog(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.DialogWidget().AsScope( out widget )) {
                using (VisualElementFactory.DialogCard().AsScope( out card )) {
                    using (VisualElementFactory.Header().AsScope( out header )) {
                        title = VisualElementFactory.Label( null );
                    }
                    using (VisualElementFactory.Content().AsScope( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label( null );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope( out footer )) {
                    }
                }
                return widget;
            }
        }
        public static Widget InfoDialog(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.InfoDialogWidget().AsScope( out widget )) {
                using (VisualElementFactory.InfoDialogCard().AsScope( out card )) {
                    using (VisualElementFactory.Header().AsScope( out header )) {
                        title = VisualElementFactory.Label( null );
                    }
                    using (VisualElementFactory.Content().AsScope( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label( null );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope( out footer )) {
                    }
                }
                return widget;
            }
        }
        public static Widget WarningDialog(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.WarningDialogWidget().AsScope( out widget )) {
                using (VisualElementFactory.WarningDialogCard().AsScope( out card )) {
                    using (VisualElementFactory.Header().AsScope( out header )) {
                        title = VisualElementFactory.Label( null );
                    }
                    using (VisualElementFactory.Content().AsScope( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label( null );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope( out footer )) {
                    }
                }
                return widget;
            }
        }
        public static Widget ErrorDialog(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.ErrorDialogWidget().AsScope( out widget )) {
                using (VisualElementFactory.ErrorDialogCard().AsScope( out card )) {
                    using (VisualElementFactory.Header().AsScope( out header )) {
                        title = VisualElementFactory.Label( null );
                    }
                    using (VisualElementFactory.Content().AsScope( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label( null );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope( out footer )) {
                    }
                }
                return widget;
            }
        }

        // Settings
        public static Widget Settings(out Widget widget, out Label title, out TabView view, out Tab profileSettings, out Tab videoSettings, out Tab audioSettings, out Button okey, out Button back) {
            using (VisualElementFactory.MediumWidget().AsScope( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "Settings" );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        using (VisualElementFactory.TabView().Classes( "no-outline", "grow-1" ).AsScope( out view )) {
                            profileSettings = VisualElementFactory.Tab( "Profile Settings" );
                            videoSettings = VisualElementFactory.Tab( "Video Settings" );
                            audioSettings = VisualElementFactory.Tab( "Audio Settings" );
                        }
                    }
                    using (VisualElementFactory.Footer().AsScope()) {
                        okey = VisualElementFactory.Submit( "Ok" );
                        back = VisualElementFactory.Cancel( "Back" );
                    }
                }
                return widget;
            }
        }
        public static Widget ProfileSettings(out Widget widget, out TextField name) {
            using (VisualElementFactory.Widget().Classes( "grow-1" ).AsScope( out widget )) {
                using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    name = VisualElementFactory.TextField( "Name", 16 ).Classes( "label-width-25pc" );
                }
                return widget;
            }
        }
        public static Widget VideoSettings(out Widget widget, out Toggle isFullScreen, out PopupField<object?> screenResolution, out Toggle isVSync) {
            using (VisualElementFactory.Widget().Classes( "grow-1" ).AsScope( out widget )) {
                using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    isFullScreen = VisualElementFactory.ToggleField( "Full Screen" ).Classes( "label-width-25pc" );
                    screenResolution = VisualElementFactory.PopupField( "Screen Resolution" ).Classes( "label-width-25pc" );
                    isVSync = VisualElementFactory.ToggleField( "V-Sync" ).Classes( "label-width-25pc" );
                }
                return widget;
            }
        }
        public static Widget AudioSettings(out Widget widget, out Slider masterVolume, out Slider musicVolume, out Slider sfxVolume, out Slider gameVolume) {
            using (VisualElementFactory.Widget().Classes( "grow-1" ).AsScope( out widget )) {
                using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    masterVolume = VisualElementFactory.SliderField( "Master Volume", 0, 1 ).Classes( "label-width-25pc" );
                    musicVolume = VisualElementFactory.SliderField( "Music Volume", 0, 1 ).Classes( "label-width-25pc" );
                    sfxVolume = VisualElementFactory.SliderField( "Sfx Volume", 0, 1 ).Classes( "label-width-25pc" );
                    gameVolume = VisualElementFactory.SliderField( "Game Volume", 0, 1 ).Classes( "label-width-25pc" );
                }
                return widget;
            }
        }

    }
}
