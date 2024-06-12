#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class Layers {

        public static int Entity { get; } = GetLayer( "Entity" );
        public static int CharacterEntity { get; } = GetLayer( "Character-Entity" );
        public static int CharacterEntityInternal { get; } = GetLayer( "Character-Entity-Internal" );
        public static int Trivial { get; } = GetLayer( "Trivial" );

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
}
