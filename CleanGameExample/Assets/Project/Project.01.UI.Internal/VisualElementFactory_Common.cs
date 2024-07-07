#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public static class VisualElementFactory_Common {

        public static Widget Dialog(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.DialogWidget().AsScope().Out( out widget )) {
                using (VisualElementFactory.DialogCard().AsScope().Out( out card )) {
                    using (VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out header )) {
                        title = VisualElementFactory.Label( null );
                    }
                    using (VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label( null );
                        }
                    }
                    using (VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out footer )) {
                    }
                }
                return widget;
            }
        }
        public static Widget InfoDialog(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.InfoDialogWidget().AsScope().Out( out widget )) {
                using (VisualElementFactory.InfoDialogCard().AsScope().Out( out card )) {
                    using (VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out header )) {
                        title = VisualElementFactory.Label( null );
                    }
                    using (VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label( null );
                        }
                    }
                    using (VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out footer )) {
                    }
                }
                return widget;
            }
        }
        public static Widget WarningDialog(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.WarningDialogWidget().AsScope().Out( out widget )) {
                using (VisualElementFactory.WarningDialogCard().AsScope().Out( out card )) {
                    using (VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out header )) {
                        title = VisualElementFactory.Label( null );
                    }
                    using (VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label( null );
                        }
                    }
                    using (VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out footer )) {
                    }
                }
                return widget;
            }
        }
        public static Widget ErrorDialog(out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.ErrorDialogWidget().AsScope().Out( out widget )) {
                using (VisualElementFactory.ErrorDialogCard().AsScope().Out( out card )) {
                    using (VisualElementFactory.Header().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out header )) {
                        title = VisualElementFactory.Label( null );
                    }
                    using (VisualElementFactory.Content().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out content )) {
                        using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            message = VisualElementFactory.Label( null );
                        }
                    }
                    using (VisualElementFactory.Footer().Pipe( i => i.SetDisplayed( false ) ).AsScope().Out( out footer )) {
                    }
                }
                return widget;
            }
        }

        public static Widget Settings(out Widget widget, out Label title, out VisualElement profileSettings, out VisualElement videoSettings, out VisualElement audioSettings, out Button okey, out Button back) {
            using (VisualElementFactory.MediumWidget( "settings-widget" ).AsScope().Out( out widget )) {
                using (VisualElementFactory.Card().AsScope()) {
                    using (VisualElementFactory.Header().AsScope()) {
                        title = VisualElementFactory.Label( "Settings" );
                    }
                    using (VisualElementFactory.Content().AsScope()) {
                        using (VisualElementFactory.TabView().Classes( "no-outline", "grow-1" ).AsScope()) {
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
            using (VisualElementFactory.Widget( "profile-settings-widget" ).Classes( "grow-1" ).AsScope().Out( out widget )) {
                using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    name = VisualElementFactory.TextField( "Name", 16 ).Classes( "label-width-25pc" );
                }
                return widget;
            }
        }
        public static Widget VideoSettings(out Widget widget, out Toggle isFullScreen, out PopupField<object?> screenResolution, out Toggle isVSync) {
            using (VisualElementFactory.Widget( "video-settings-widget" ).Classes( "grow-1" ).AsScope().Out( out widget )) {
                using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    isFullScreen = VisualElementFactory.ToggleField( "Full Screen" ).Classes( "label-width-25pc" );
                    screenResolution = VisualElementFactory.PopupField( "Screen Resolution" ).Classes( "label-width-25pc" );
                    isVSync = VisualElementFactory.ToggleField( "V-Sync" ).Classes( "label-width-25pc" );
                }
                return widget;
            }
        }
        public static Widget AudioSettings(out Widget widget, out Slider masterVolume, out Slider musicVolume, out Slider sfxVolume, out Slider gameVolume) {
            using (VisualElementFactory.Widget( "audio-settings-widget" ).Classes( "grow-1" ).AsScope().Out( out widget )) {
                using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    masterVolume = VisualElementFactory.SliderField( "Master Volume", 0, 1 ).Classes( "label-width-25pc" );
                    musicVolume = VisualElementFactory.SliderField( "Music Volume", 0, 1 ).Classes( "label-width-25pc" );
                    sfxVolume = VisualElementFactory.SliderField( "Sfx Volume", 0, 1 ).Classes( "label-width-25pc" );
                    gameVolume = VisualElementFactory.SliderField( "Game Volume", 0, 1 ).Classes( "label-width-25pc" );
                }
                return widget;
            }
        }

        public static Widget Loading(out Widget widget, out VisualElement background, out Label loading) {
            using (VisualElementFactory.Widget( "loading-widget" ).AsScope().Out( out widget )) {
                background = VisualElementFactory.VisualElement().Classes( "loading-widget-background", "width-100pc", "height-100pc" );
                loading = VisualElementFactory.Label( "Loading..." ).Classes( "color-light", "font-size-200pc", "font-style-bold", "position-absolute", "left-50pc", "bottom-2pc", "translate-x-n50pc" );
                return widget;
            }
        }

        public static Widget Unloading(out Widget widget, out VisualElement background) {
            using (VisualElementFactory.Widget( "unloading-widget" ).AsScope().Out( out widget )) {
                background = VisualElementFactory.VisualElement().Classes( "unloading-widget-background", "width-100pc", "height-100pc" );
                return widget;
            }
        }

    }
}
