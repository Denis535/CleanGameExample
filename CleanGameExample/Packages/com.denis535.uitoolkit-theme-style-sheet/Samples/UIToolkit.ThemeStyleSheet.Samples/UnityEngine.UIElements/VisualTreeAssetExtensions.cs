#nullable enable
namespace UnityEngine.UIElements {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class VisualTreeAssetExtensions {

        public static T Instantiate<T>(this VisualTreeAsset asset) where T : VisualElement, new() {
            var view = asset.Instantiate().Children().OfType<T>().FirstOrDefault();
            return view ?? throw new InvalidOperationException( $"VisualElement {typeof( T )} ({asset.name}) could not be instantiated" );
        }

    }
}
