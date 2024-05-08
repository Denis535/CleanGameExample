#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public struct TimePoint {

        public static TimePoint Now => new TimePoint( UnityEngine.Time.time );

        public float? Time { get; }
        public float Duration => Time.HasValue ? (UnityEngine.Time.time - Time.Value) : float.MaxValue;

        private TimePoint(float time) {
            Time = time;
        }

        public override string ToString() {
            return Time.ToString();
        }

    }
}
