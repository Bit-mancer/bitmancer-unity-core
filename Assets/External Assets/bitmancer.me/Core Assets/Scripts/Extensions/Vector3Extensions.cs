using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// To use this extension in a file, add "using Extensions;" at the top

namespace Bitmancer.Core.Extensions {

    /// <summary>
    /// Vector3 extensions.
    /// </summary>
    public static class Vector3Extensions {

        /// <summary>
        /// Returns the magnitude (the absolute value) of the axis with the greatest magnitude.
        /// </summary>
        /// <returns>The axis magnitude.</returns>
        /// <param name="self">Self.</param>
        public static float GetLargestAxisMagnitude( this Vector3 self ) {
            return Mathf.Max( Mathf.Abs( self.x ), Mathf.Abs( self.y ), Mathf.Abs( self.z ) );
        }
    }
}
