#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class GameObjectExtensions {

        public static void SetLayer(this GameObject gameObject, int layer) {
            gameObject.layer = layer;
        }
        public static void SetLayerRecursively(this GameObject gameObject, int layer) {
            gameObject.layer = layer;
            for (var i = 0; i < gameObject.transform.childCount; i++) {
                gameObject.transform.GetChild( i ).gameObject.SetLayerRecursively( layer );
            }
        }
        public static void SetLayerRecursively(this GameObject gameObject, int layer, int layer2) {
            gameObject.layer = layer;
            for (var i = 0; i < gameObject.transform.childCount; i++) {
                gameObject.transform.GetChild( i ).gameObject.SetLayerRecursively( layer2 );
            }
        }

    }
    public static class Tags {

        public static readonly string Entity = "Entity";

    }
    public static class Layers {

        public static readonly int Entity = GetLayer( "Entity" );
        public static readonly int Entity_Approximate = GetLayer( "Entity-Approximate" );
        public static readonly int Entity_Exact = GetLayer( "Entity-Exact" );
        public static readonly int Trivial = GetLayer( "Trivial" );

        public static int GetLayer(string name) {
            var layer = LayerMask.NameToLayer( name );
            Assert.Operation.Message( $"Can not find {name} layer" ).Valid( layer != -1 );
            return layer;
        }
        public static string GetName(int layer) {
            var name = LayerMask.LayerToName( layer );
            Assert.Operation.Message( $"Can not find {layer} layer name" ).Valid( name != null );
            return name;
        }

    }
    public static class Masks {

        public static readonly int Entity = GetMask( "Entity" );
        public static readonly int Entity_Approximate = GetMask( "Entity-Approximate" );
        public static readonly int Entity_Exact = GetMask( "Entity-Exact" );
        public static readonly int Trivial = GetMask( "Trivial" );

        public static int GetMask(string name) {
            var layer = Layers.GetLayer( name );
            Assert.Operation.Message( $"Can not find {name} layer" ).Valid( layer != -1 );
            return 1 << layer;
        }

    }
}
