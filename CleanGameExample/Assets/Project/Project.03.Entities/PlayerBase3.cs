#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class PlayerBase3 : PlayerBase2 {

        public PlayerInfo Info { get; }

        public PlayerBase3(IDependencyContainer container, PlayerInfo info) : base( container ) {
            Info = info;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public record PlayerInfo(string Name, PlayerInfo.CharacterType_ CharacterType) {
        public enum CharacterType_ {
            Gray,
            Red,
            Green,
            Blue
        }
    }
}
