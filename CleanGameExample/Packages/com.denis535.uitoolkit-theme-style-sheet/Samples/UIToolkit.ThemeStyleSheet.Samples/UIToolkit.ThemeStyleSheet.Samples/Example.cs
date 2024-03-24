#nullable enable
namespace UIToolkit.ThemeStyleSheet.Samples {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class Example : MonoBehaviour {

        private int toolbarIndex = 0;

        // Document
        private UIDocument Document { get; set; } = default!;
        // Factory
        private VisualElementFactory Factory { get; set; } = default!;

        // Awake
        public void Awake() {
            Document = GetComponent<UIDocument>();
            Factory = GetComponent<VisualElementFactory>();
        }
        public void OnDestroy() {
        }

        // Start
        public void Start() {
        }
        public void Update() {
        }

        // OnGUI
        public void OnGUI() {
            var prevToolbarIndex = toolbarIndex;
            toolbarIndex = GUILayout.Toolbar(
                toolbarIndex,
                new[] {
                    // None
                    "None",
                    // Widget
                    "Widget",
                    "Left Widget",
                    "Small Widget",
                    "Medium Widget",
                    "Large Widget",
                    // Widget
                    "Dialog Widget",
                    "Info Dialog Widget",
                    "Warning Dialog Widget",
                    "Error Dialog Widget",
                    // View
                    "Tab View",
                    "Scroll View",
                    // Scope
                    "Scope",
                    "Group",
                    "Box",
                    // Misc
                    "Misc"
                },
                null,
                GUI.ToolbarButtonSize.FitToContents );
            // None
            if (toolbarIndex == 0 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
            }
            // Widget
            if (toolbarIndex == 1 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( Widget( Factory ) );
            }
            if (toolbarIndex == 2 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( LeftWidget( Factory ) );
            }
            if (toolbarIndex == 3 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( SmallWidget( Factory ) );
            }
            if (toolbarIndex == 4 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( MediumWidget( Factory ) );
            }
            if (toolbarIndex == 5 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( LargeWidget( Factory ) );
            }
            // Widget
            if (toolbarIndex == 6 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( DialogWidget( Factory ) );
            }
            if (toolbarIndex == 7 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( InfoDialogWidget( Factory ) );
            }
            if (toolbarIndex == 8 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( WarningDialogWidget( Factory ) );
            }
            if (toolbarIndex == 9 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( ErrorDialogWidget( Factory ) );
            }
            // TabView
            if (toolbarIndex == 10 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( TabView( Factory ) );
            }
            // ScrollView
            if (toolbarIndex == 11 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( ScrollView( Factory ) );
            }
            // Scope
            if (toolbarIndex == 12 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( Scope( Factory ) );
            }
            if (toolbarIndex == 13 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( Group( Factory ) );
            }
            if (toolbarIndex == 14 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( Box( Factory ) );
            }
            // Misc
            if (toolbarIndex == 15 && toolbarIndex != prevToolbarIndex) {
                Document.rootVisualElement.Clear();
                Document.rootVisualElement.Add( Misc( Factory ) );
            }
        }

        // Helpers
        private static VisualElement Widget(VisualElementFactory factory) {
            using (var root = factory.Widget().AsScope()) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Widget" );
                    }
                    using (factory.Content().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Widget" );
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        private static VisualElement LeftWidget(VisualElementFactory factory) {
            using (var root = factory.LeftWidget().AsScope()) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Left Widget" );
                    }
                    using (factory.Content().AsScope()) {
                        _ = VisualElementScope.Current + factory.Select( "Item 1" );
                        _ = VisualElementScope.Current + factory.Select( "Item 2" );
                        _ = VisualElementScope.Current + factory.Select( "Item 3" );
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        private static VisualElement SmallWidget(VisualElementFactory factory) {
            using (var root = factory.SmallWidget().AsScope()) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Small Widget" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        private static VisualElement MediumWidget(VisualElementFactory factory) {
            using (var root = factory.MediumWidget().AsScope()) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Medium Widget" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        private static VisualElement LargeWidget(VisualElementFactory factory) {
            using (var root = factory.LargeWidget().AsScope()) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Large Widget" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        // Helpers
        private static VisualElement DialogWidget(VisualElementFactory factory) {
            using (var root = factory.DialogWidget().AsScope()) {
                using (factory.DialogCard().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Dialog Widget" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        private static VisualElement InfoDialogWidget(VisualElementFactory factory) {
            using (var root = factory.InfoDialogWidget().AsScope()) {
                using (factory.InfoDialogCard().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Info Dialog Widget" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        private static VisualElement WarningDialogWidget(VisualElementFactory factory) {
            using (var root = factory.WarningDialogWidget().AsScope()) {
                using (factory.WarningDialogCard().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Warning Dialog Widget" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        private static VisualElement ErrorDialogWidget(VisualElementFactory factory) {
            using (var root = factory.ErrorDialogWidget().AsScope()) {
                using (factory.ErrorDialogCard().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Error Dialog Widget" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                            _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        // Helpers
        private static VisualElement TabView(VisualElementFactory factory) {
            using (var root = factory.LargeWidget().AsScope()) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Tab View" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.TabView().Classes( "no-outline", "grow-1" ).AsScope()) {
                            using (factory.Tab( "light10" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "light10", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "light9" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "light9", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "light8" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "light8", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "light7" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "light7", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "light6" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "light6", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "light5" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "light5", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "light4" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "light4", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "light3" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "light3", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "light2" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "light2", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "light" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "light", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "gray" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "gray", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "dark" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "dark", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "dark2" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "dark2", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "dark3" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "dark3", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "dark4" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "dark4", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "dark5" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "dark5", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "dark6" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "dark6", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "dark7" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "dark7", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "dark8" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "dark8", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "dark9" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "dark9", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                            using (factory.Tab( "dark10" ).AsScope()) {
                                using (factory.ColumnGroup().Classes( "dark10", "medium", "margin-0px", "grow-1", "justify-content-center", "align-items-center" ).AsScope()) {
                                    _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                                }
                            }
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        private static VisualElement ScrollView(VisualElementFactory factory) {
            using (var root = factory.LargeWidget().AsScope()) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Scroll View" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.ScrollView().Classes( "gray", "medium", "grow-1" ).AsScope()) {
                            using (factory.ColumnGroup().Classes( "width-150pc", "height-150pc", "medium", "margin-0px", "shrink-0", "justify-content-center", "align-items-center" ).AsScope()) {
                                _ = VisualElementScope.Current + factory.Label( "Hello World !!!" );
                            }
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        // Helpers
        private static VisualElement Scope(VisualElementFactory factory) {
            using (var root = factory.LargeWidget().AsScope()) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Scope" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.RowScope().Classes( "grow-1", "align-items-stretch" ).AsScope()) {
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "light10", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "light9", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "light8", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "light7", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "light6", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "light5", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "light4", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "light3", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "light2", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "light", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "gray", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "dark", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "dark2", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "dark3", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "dark4", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "dark5", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "dark6", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "dark7", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "dark8", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "dark9", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnScope().Classes( "dark10", "medium", "grow-1" );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        private static VisualElement Group(VisualElementFactory factory) {
            using (var root = factory.LargeWidget().AsScope()) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Group" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.RowScope().Classes( "grow-1", "align-items-stretch" ).AsScope()) {
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "light10", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "light9", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "light8", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "light7", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "light6", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "light5", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "light4", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "light3", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "light2", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "light", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "gray", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "dark", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "dark2", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "dark3", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "dark4", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "dark5", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "dark6", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "dark7", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "dark8", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "dark9", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.ColumnGroup().Classes( "dark10", "medium", "grow-1" );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        private static VisualElement Box(VisualElementFactory factory) {
            using (var root = factory.LargeWidget().AsScope()) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Box" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.RowScope().Classes( "grow-1", "align-items-stretch" ).AsScope()) {
                            _ = VisualElementScope.Current + factory.Box().Classes( "light10", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "light9", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "light8", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "light7", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "light6", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "light5", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "light4", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "light3", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "light2", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "light", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "gray", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "dark", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "dark2", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "dark3", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "dark4", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "dark5", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "dark6", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "dark7", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "dark8", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "dark9", "medium", "grow-1" );
                            _ = VisualElementScope.Current + factory.Box().Classes( "dark10", "medium", "grow-1" );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }
        // Helpers
        private static VisualElement Misc(VisualElementFactory factory) {
            using (var root = factory.LargeWidget().AsScope()) {
                using (factory.Card().AsScope()) {
                    using (factory.Header().AsScope()) {
                        _ = VisualElementScope.Current + factory.Label( "Misc" );
                    }
                    using (factory.Content().AsScope()) {
                        using (factory.ColumnGroup().Classes( "gray", "medium", "grow-1" ).AsScope()) {
                            _ = VisualElementScope.Current + factory.Label( "Label" ).Classes( "medium" );
                            _ = VisualElementScope.Current + factory.Button( "Button" );
                            _ = VisualElementScope.Current + factory.RepeatButton( "Repeat Button" );
                            _ = VisualElementScope.Current + factory.TextField( "Text Field", "Text", 64, false ).Classes( "label-width-150px", "invalid" );
                            _ = VisualElementScope.Current + factory.PopupField( "Popup Field", null, new string[] { } ).Classes( "label-width-150px" );
                            _ = VisualElementScope.Current + factory.DropdownField( "Dropdown Field", "Item 1", new string[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10", "Item 11", "Item 12", "Item 13", "Item 14", "Item 15", "Item 16", "Item 17", "Item 18", "Item 19", "Item 20" } ).Classes( "label-width-150px" );
                            _ = VisualElementScope.Current + factory.SliderField( "Slider Field", 1, 0, 2 ).Classes( "label-width-150px" );
                            _ = VisualElementScope.Current + factory.IntSliderField( "Int Slider Field", 1, 0, 2 ).Classes( "label-width-150px" );
                            _ = VisualElementScope.Current + factory.ToggleField( "Toggle Field", true ).Classes( "label-width-150px" );
                        }
                    }
                    using (factory.Footer().AsScope()) {
                        _ = VisualElementScope.Current + factory.Submit( "Submit" ).Classes( "invalid" );
                        _ = VisualElementScope.Current + factory.Cancel( "Cancel" );
                    }
                }
                return root.VisualElement;
            }
        }

    }
}
