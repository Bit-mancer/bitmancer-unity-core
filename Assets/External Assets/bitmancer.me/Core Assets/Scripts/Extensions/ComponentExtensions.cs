using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// To use this extension in a file, add "using Extensions;" at the top

namespace Bitmancer.Core.Extensions {

    /// <summary>
    /// Component extensions.
    /// </summary>
    public static class ComponentExtensions {

        /// <summary>
        /// Gets a component (via Component.GetComponent) that is expected to be attached to the GameObject.
        /// </summary>
        /// <returns>The required component.</returns>
        /// <param name="self">Self (C# syntax).</param>
        /// <typeparam name="T">The Component type.</typeparam>
        /// <exception cref="RequiredComponentException">Thrown if no Component of type <c>T</c> is attached to the GameObject.</exception>
        public static T GetRequiredComponent<T>( this Component self ) where T : Component {

            T component = self.GetComponent<T>();
            if ( component != null ) {
                return component;
            } else {
                throw new Bitmancer.Core.Extensions.RequiredComponentException( string.Format( "Required component not found!  Expected component {0} on {1} (name: {2}).", typeof(T).Name, self.GetType().Name, self.gameObject.name ) );
            }
        }


        /// <summary>
        /// Gets a component (via Component.GetComponent) that is expected to be attached to the GameObject.
        /// </summary>
        /// <returns>The required component.</returns>
        /// <param name="self">Self (C# syntax).</param>
        /// <param name="type">Type of the Component.</param>
        /// <exception cref="RequiredComponentException">Thrown if no Component of type <c>T</c> is attached to the GameObject.</exception>
        public static Component GetRequiredComponent( this Component self, Type type ) {

            var component = self.GetComponent( type );
            if ( component != null ) {
                return component;
            } else {
                throw new Bitmancer.Core.Extensions.RequiredComponentException( string.Format( "Required component not found!  Expected component {0} on {1} (name: {2}).", type.Name, self.GetType().Name, self.gameObject.name ) );
            }
        }


        /// <summary>
        /// Gets a component (via Component.GetComponentInChildren) that is expected to be attached to a child of the GameObject.
        /// </summary>
        /// <returns>The required component.</returns>
        /// <param name="self">Self (C# syntax).</param>
        /// <typeparam name="T">The Component type.</typeparam>
        /// <exception cref="RequiredComponentException">Thrown if no Component of type <c>T</c> is attached to a child of the GameObject.</exception>
        public static T GetRequiredComponentInChildren<T>( this Component self ) where T : Component {

            T component = self.GetComponentInChildren<T>();
            if ( component != null ) {
                return component;
            } else {
                throw new Bitmancer.Core.Extensions.RequiredComponentException( string.Format( "Required component not found!  Expected component {0} on child of {1} (name: {2}).", typeof(T).Name, self.GetType().Name, self.gameObject.name ) );
            }
        }


        /// <summary>
        /// Gets a component (via Component.GetComponentInChildren) that is expected to be attached to a child of the GameObject.
        /// </summary>
        /// <returns>The required component.</returns>
        /// <param name="self">Self (C# syntax).</param>
        /// <param name="type">Type of the Component.</param>
        /// <exception cref="RequiredComponentException">Thrown if no Component of type <c>T</c> is attached to a child of the GameObject.</exception>
        public static Component GetRequiredComponentInChildren( this Component self, Type type ) {

            var component = self.GetComponentInChildren( type );
            if ( component != null ) {
                return component;
            } else {
                throw new Bitmancer.Core.Extensions.RequiredComponentException( string.Format( "Required component not found!  Expected component {0} on child of {1} (name: {2}).", type.Name, self.GetType().Name, self.gameObject.name ) );
            }
        }



        /// <summary>
        /// Gets a component (via Component.GetComponentInParent) that is expected to be attached to the parent of the GameObject.
        /// </summary>
        /// <returns>The required component.</returns>
        /// <param name="self">Self (C# syntax).</param>
        /// <typeparam name="T">The Component type.</typeparam>
        /// <exception cref="RequiredComponentException">Thrown if no Component of type <c>T</c> is attached to the parent of the GameObject.</exception>
        public static T GetRequiredComponentInParent<T>( this Component self ) where T : Component {

            T component = self.GetComponentInParent<T>();
            if ( component != null ) {
                return component;
            } else {
                throw new Bitmancer.Core.Extensions.RequiredComponentException( string.Format( "Required component not found!  Expected component {0} on parent of {1} (name: {2}).", typeof(T).Name, self.GetType().Name, self.gameObject.name ) );
            }
        }


        /// <summary>
        /// Gets a component (via Component.GetComponentInParent) that is expected to be attached to the parent of the GameObject.
        /// </summary>
        /// <returns>The required component.</returns>
        /// <param name="self">Self (C# syntax).</param>
        /// <param name="type">Type of the Component.</param>
        /// <exception cref="RequiredComponentException">Thrown if no Component of type <c>T</c> is attached to the parent of the GameObject.</exception>
        public static Component GetRequiredComponentInParent( this Component self, Type type ) {

            var component = self.GetComponentInParent( type );
            if ( component != null ) {
                return component;
            } else {
                throw new Bitmancer.Core.Extensions.RequiredComponentException( string.Format( "Required component not found!  Expected component {0} on parent of {1} (name: {2}).", type.Name, self.GetType().Name, self.gameObject.name ) );
            }
        }

    }
}
