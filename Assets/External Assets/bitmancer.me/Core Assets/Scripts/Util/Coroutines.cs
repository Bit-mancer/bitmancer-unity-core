using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bitmancer.Core.Util {

    /// <summary>
    /// Coroutine utilities and helpers.
    /// </summary>
    public static class Coroutines {

        /// <summary>
        /// Does a linear lerp in Update().
        /// </summary>
        /// <returns>Coroutine IEnumerator</returns>
        /// <param name="start">Starting value</param>
        /// <param name="end">Ending value</param>
        /// <param name="seconds">Scaled (affected by Time.timeScale) seconds over which to perform the lerp.</param>
        /// <param name="onLerp">Called each frame with the current lerped value.</param>
        public static IEnumerator linearLerpInUpdateCoroutine( float start, float end, float seconds, Action<float> onLerp ) {

            float time = 0;

            while ( time < seconds ) {
                float t = time / seconds;
                float v = Mathf.Lerp( start, end, t );
                onLerp( v );

                time += Time.deltaTime;

                yield return null; // wait for next Update
            }

            onLerp( end );
        }

    
        /// <summary>
        /// Does a lerp with exponential decay in Update().
        /// </summary>
        /// <returns>Coroutine IEnumerator</returns>
        /// <param name="start">Starting value</param>
        /// <param name="end">Ending value</param>
        /// <param name="seconds">Scaled (affected by Time.timeScale) seconds over which to perform the lerp.</param>
        /// <param name="onLerp">Called each frame with the current lerped value.</param>
        public static IEnumerator decayLerpInUpdateCoroutine( float start, float end, float seconds, Action<float> onLerp ) {

            float time = 0;
            float v = start;

            while ( time < seconds ) {
                float t = time / seconds;
                v = Mathf.Lerp( v, end, t );
                onLerp( v );

                time += Time.deltaTime;

                yield return null; // wait for next Update
            }

            onLerp( end );
        }
    }
}
