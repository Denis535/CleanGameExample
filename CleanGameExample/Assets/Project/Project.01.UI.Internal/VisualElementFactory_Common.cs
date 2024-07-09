#nullable enable
namespace Project.UI {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.UI;
    using UnityEngine.UIElements;

    public static class VisualElementFactory_Common {

        public static void Dialog(UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.DialogWidget().UserData( view ).AsScope().Out( out widget )) {
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
            }
        }
        public static void InfoDialog(UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.InfoDialogWidget().UserData( view ).AsScope().Out( out widget )) {
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
            }
        }
        public static void WarningDialog(UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.WarningDialogWidget().UserData( view ).AsScope().Out( out widget )) {
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
            }
        }
        public static void ErrorDialog(UIViewBase view, out Widget widget, out Card card, out Header header, out Content content, out Footer footer, out Label title, out Label message) {
            using (VisualElementFactory.ErrorDialogWidget().UserData( view ).AsScope().Out( out widget )) {
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
            }
        }

        public static void Settings(UIViewBase view, out Widget widget, out Label title, out VisualElement profileSettings, out VisualElement videoSettings, out VisualElement audioSettings, out Button okey, out Button back) {
            using (VisualElementFactory.MediumWidget( "settings-widget" ).UserData( view ).AsScope().Out( out widget )) {
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
            }
        }
        public static void ProfileSettings(UIViewBase view, out Widget widget, out TextField name) {
            using (VisualElementFactory.Widget( "profile-settings-widget" ).Classes( "grow-1" ).UserData( view ).AsScope().Out( out widget )) {
                using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    name = VisualElementFactory.TextField( "Name", 16 ).Classes( "label-width-25pc" );
                }
            }
        }
        public static void VideoSettings(UIViewBase view, out Widget widget, out Toggle isFullScreen, out PopupField<object?> screenResolution, out Toggle isVSync) {
            using (VisualElementFactory.Widget( "video-settings-widget" ).Classes( "grow-1" ).UserData( view ).AsScope().Out( out widget )) {
                using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    isFullScreen = VisualElementFactory.ToggleField( "Full Screen" ).Classes( "label-width-25pc" );
                    screenResolution = VisualElementFactory.PopupField( "Screen Resolution" ).Classes( "label-width-25pc" );
                    isVSync = VisualElementFactory.ToggleField( "V-Sync" ).Classes( "label-width-25pc" );
                }
            }
        }
        public static void AudioSettings(UIViewBase view, out Widget widget, out Slider masterVolume, out Slider musicVolume, out Slider sfxVolume, out Slider gameVolume) {
            using (VisualElementFactory.Widget( "audio-settings-widget" ).Classes( "grow-1" ).UserData( view ).AsScope().Out( out widget )) {
                using (VisualElementFactory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1" ).AsScope()) {
                    masterVolume = VisualElementFactory.SliderField( "Master Volume", 0, 1 ).Classes( "label-width-25pc" );
                    musicVolume = VisualElementFactory.SliderField( "Music Volume", 0, 1 ).Classes( "label-width-25pc" );
                    sfxVolume = VisualElementFactory.SliderField( "Sfx Volume", 0, 1 ).Classes( "label-width-25pc" );
                    gameVolume = VisualElementFactory.SliderField( "Game Volume", 0, 1 ).Classes( "label-width-25pc" );
                }
            }
        }

        public static void Loading(UIViewBase view, out Widget widget, out VisualElement background, out Label loading) {
            using (VisualElementFactory.Widget( "loading-widget" ).UserData( view ).AsScope().Out( out widget )) {
                background = VisualElementFactory.VisualElement().Classes( "loading-widget-background", "width-100pc", "height-100pc" );
                loading = VisualElementFactory.Label( "Loading..." ).Classes( "color-light", "font-size-200pc", "font-style-bold", "position-absolute", "left-50pc", "bottom-2pc", "translate-x-n50pc" );
            }
        }

        public static void Unloading(UIViewBase view, out Widget widget, out VisualElement background) {
            using (VisualElementFactory.Widget( "unloading-widget" ).UserData( view ).AsScope().Out( out widget )) {
                background = VisualElementFactory.VisualElement().Classes( "unloading-widget-background", "width-100pc", "height-100pc" );
            }
        }

    }
}
