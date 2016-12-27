using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bitmancer.Core {
    
    /// <summary>
    /// A replacement (derivation) for Unity's MonoBehaviour that adds some helpful utilities.
    /// </summary>
    public class BaseBehavior : MonoBehaviour {

        // Synthesize some commonly-used Extensions methods to make them easier to use

        /// <summary>
        /// Gets a component (via Component.GetComponent) that is expected to be attached to the GameObject.
        /// </summary>
        /// <returns>The required component.</returns>
        /// <param name="self">Self (C# syntax).</param>
        /// <typeparam name="T">The Component type.</typeparam>
        /// <exception cref="RequiredComponentException">Thrown if no Component of type <c>T</c> is attached to the GameObject.</exception>
        public T GetRequiredComponent<T>() where T : Component {
            return Bitmancer.Core.Extensions.ComponentExtensions.GetRequiredComponent<T>( this );
        }


        /// <summary>
        /// Gets a component (via Component.GetComponentInChildren) that is expected to be attached to a child of the GameObject.
        /// </summary>
        /// <returns>The required component.</returns>
        /// <param name="self">Self (C# syntax).</param>
        /// <typeparam name="T">The Component type.</typeparam>
        /// <exception cref="RequiredComponentException">Thrown if no Component of type <c>T</c> is attached to a child of the GameObject.</exception>
        public T GetRequiredComponentInChildren<T>() where T : Component {
            return Bitmancer.Core.Extensions.ComponentExtensions.GetRequiredComponentInChildren<T>( this );
        }


        /// <summary>
        /// Gets a component (via Component.GetComponentInParent) that is expected to be attached to the parent of the GameObject.
        /// </summary>
        /// <returns>The required component.</returns>
        /// <param name="self">Self (C# syntax).</param>
        /// <typeparam name="T">The Component type.</typeparam>
        /// <exception cref="RequiredComponentException">Thrown if no Component of type <c>T</c> is attached to the parent of the GameObject.</exception>
        public T GetRequiredComponentInParent<T>() where T : Component {
            return Bitmancer.Core.Extensions.ComponentExtensions.GetRequiredComponentInParent<T>( this );
        }
    }
}
