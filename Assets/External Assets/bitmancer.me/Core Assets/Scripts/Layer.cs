using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bitmancer.Core {

    /// <summary>
    /// Built-in layer name strings, and layer utility methods.
    /// </summary>
    public static class Layer {

        // Built-ins:
        public const string Default = "Default";
        public const string TransparentFX = "TransparentFX";
        public const string IgnoreRaycast = "Ignore Raycast";
        public const string Water = "Water";
        public const string UI = "UI";

        /// <summary>
        /// Gets the mask for the specified layers.
        /// </summary>
        /// <returns>Layer mask appropriate for use with raycasting, etc.</returns>
        /// <param name="layerNames">Layer names.</param>
        /// <remarks>
        /// Use this instead of the Unity built-in LayerMask.GetMask(), which does not handle the Default layer correctly (treats as 0 rather than 1).
        /// </remarks>
        public static int getMask( params string[] layerNames ) {
            int mask = 0;

            for ( int i = 0; i < layerNames.Length; i++ ) {
                int layerID = LayerMask.NameToLayer( layerNames[ i ] );
                if ( layerID < 0 ) {
                    throw new ArgumentOutOfRangeException( "layerNames", layerNames[ i ], "Unknown layer name" );
                } else {
                    mask |= 1 << layerID;
                }
            }

            return mask;
        }
    }
}
