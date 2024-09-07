#nullable enable
namespace Project.Entities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Framework.Entities;

    public abstract class GameBase3 : GameBase2 {

        public GameInfo Info { get; }

        public GameBase3(IDependencyContainer container, GameInfo info) : base( container ) {
            Info = info;
        }
        public override void Dispose() {
            base.Dispose();
        }

    }
    public record GameInfo(string Name, GameInfo.Mode_ Mode, GameInfo.Level_ Level) {
        public enum Mode_ {
            None
        }
        public enum Level_ {
            Level1,
            Level2,
            Level3
        }
    }
    public static class GameInfoExtensions {
        public static bool IsLast(this GameInfo.Level_ level) {
            return level == GameInfo.Level_.Level3;
        }
        public static GameInfo.Level_ GetNext(this GameInfo.Level_ level) {
            Assert.Operation.Message( $"Level {level} must be non-last" ).Valid( level != GameInfo.Level_.Level3 );
            return level + 1;
        }
    }
}
