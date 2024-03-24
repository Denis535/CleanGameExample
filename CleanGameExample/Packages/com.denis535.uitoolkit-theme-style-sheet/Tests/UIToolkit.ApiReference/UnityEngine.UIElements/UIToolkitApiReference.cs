#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using UnityEngine;

    public class UIToolkitApiReference : ApiReferenceBase {

        // Test
        [Test]
        public void Test() {
            var actual = GetActualTypes().OfType<Type>().ToArray();
            var expected = GetExpectedTypes().OfType<Type>().ToArray();
            AssertThatTypesAreEqual( actual, expected );
        }

        // GetActualTypes
        public override object[] GetActualTypes() {
            return new object[] {
                "UnityEngine.UIElements",
                // Base
                typeof( VisualElement ),
                typeof( BindableElement ),
                typeof( ImmediateModeElement ),

                // Text
                typeof( TextElement ),
                typeof( Label ),
                typeof( Button ),
                typeof( RepeatButton ),
                typeof( PopupWindow ),
                // Image
                typeof( Image ),
                // Field
                typeof( BaseField<> ),
                typeof( BoundsField ),
                typeof( BoundsIntField ),
                typeof( EnumField ),
                typeof( RadioButtonGroup ),
                typeof( ToggleButtonGroup ),
                // Field/Text
                typeof( TextInputBaseField<> ),
                typeof( TextField ),
                typeof( Hash128Field ),
                // Field/Text/Value
                typeof( TextValueField<> ),
                typeof( IntegerField ),
                typeof( UnsignedIntegerField ),
                typeof( LongField ),
                typeof( UnsignedLongField ),
                typeof( FloatField ),
                typeof( DoubleField ),
                // Field/Popup
                typeof( BasePopupField<,> ),
                typeof( PopupField<> ),
                typeof( DropdownField ),
                // Field/Slider
                typeof( BaseSlider<> ),
                typeof( Slider ),
                typeof( SliderInt ),
                typeof( MinMaxSlider ),
                // Field/Bool
                typeof( BaseBoolField ),
                typeof( Toggle ),
                typeof( RadioButton ),
                // Field/Composite
                typeof( BaseCompositeField<,,> ),
                typeof( Vector2Field ),
                typeof( Vector2IntField ),
                typeof( Vector3Field ),
                typeof( Vector3IntField ),
                typeof( Vector4Field ),
                //typeof( Vector4IntField ),
                typeof( RectField ),
                typeof( RectIntField ),
                // ProgressBar
                typeof( AbstractProgressBar ),
                typeof( ProgressBar ),

                // Container
                typeof( TemplateContainer ),
                typeof( IMGUIContainer ),
                // View/Tab
                typeof( TabView ),
                typeof( Tab ),
                // View/Scroll
                typeof( ScrollView ),
                typeof( Scroller ),
                // View/Split
                typeof( TwoPaneSplitView ),
                // View/Collection
                typeof( BaseVerticalCollectionView ),
                // View/Collection/List
                typeof( BaseListView ),
                typeof( ListView ),
                typeof( MultiColumnListView ),
                // View/Collection/Tree
                typeof( BaseTreeView ),
                typeof( TreeView ),
                typeof( MultiColumnTreeView ),
                // Foldout
                typeof( Foldout ),
                // Box
                typeof( Box ),
                typeof( GroupBox ),
                typeof( HelpBox ),
            };
        }

        // GetExpectedTypes
        public override object[] GetExpectedTypes() {
            return typeof( VisualElement ).Assembly.GetExportedTypes().Where( i => typeof( VisualElement ).IsAssignableFrom( i ) ).ToArray();
        }

    }
}
