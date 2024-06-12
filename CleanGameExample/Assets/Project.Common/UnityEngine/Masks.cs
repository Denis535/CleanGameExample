#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class Masks {

        public static int Entity { get; } = GetMask( "Entity" );
        public static int CharacterEntity { get; } = GetMask( "Character-Entity" );
        public static int CharacterEntityInternal { get; } = GetMask( "Character-Entity-Internal" );
        public static int Trivial { get; } = GetMask( "Trivial" );

        public static int GetMask(string name) {
            return 1 << Layers.GetLayer( name );
        }

    }
}
