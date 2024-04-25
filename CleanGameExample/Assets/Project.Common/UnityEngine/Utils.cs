#nullable enable
namespace UnityEngine {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;

    public static class Utils {

        public static IDependencyContainer Container { get; set; } = default!;
        public static float DeltaTime => Time.inFixedTimeStep ? Time.fixedDeltaTime : Time.deltaTime;

        static Utils() {
        }

        public static async void PlayAnimation<T>(T @object, float from, float to, float duration, Action<T, float> onUpdate, Action<T>? onComplete, Action<T>? onCancel, CancellationToken cancellationToken) {
            await PlayAnimationAsync( @object, from, to, duration, onUpdate, onComplete, onCancel, cancellationToken );
        }
        public static async Task PlayAnimationAsync<T>(T @object, float from, float to, float duration, Action<T, float> onUpdate, Action<T>? onComplete, Action<T>? onCancel, CancellationToken cancellationToken) {
            var time = 0f;
            while (!cancellationToken.IsCancellationRequested) {
                var time01 = Mathf.InverseLerp( 0, duration, time );
                var value = Mathf.Lerp( from, to, time01 );
                onUpdate.Invoke( @object, value );
                if (time < duration) {
                    await Task.Yield();
                    time += Time.unscaledDeltaTime;
                } else {
                    break;
                }
            }
            if (!cancellationToken.IsCancellationRequested) {
                onComplete?.Invoke( @object );
            } else {
                onCancel?.Invoke( @object );
            }
        }

    }
}
