#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Delay {

        public float Interval { get; }
        public float? Time { get; private set; }
        public float Left => Time.HasValue ? Math.Max( Time.Value + Interval - UnityEngine.Time.time, 0 ) : 0;
        public bool IsCompleted => Time.HasValue ? (Time.Value + Interval - UnityEngine.Time.time) <= 0 : true;

        public Delay(float interval) {
            Interval = interval;
        }

        public void Start() {
            Time = UnityEngine.Time.time;
        }

        public override string ToString() {
            return "Delay: " + Interval;
        }

    }
}
