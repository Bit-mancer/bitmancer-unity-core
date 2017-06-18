using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;
using Bitmancer.Core;

namespace Bitmancer.Core.Util {

    public static class GameObjectUtils {

        /// <summary>
        /// Creates an empty <c>GameObject</c> at the origin.
        /// </summary>
        /// <param name="name">Name of the object.</param>
        /// <param name="parent">Optional parent <c>GameObject</c>; pass null for no parent.</param>
        /// <returns>The new <c>GameObject</c>.</returns>
        public static GameObject createEmpty( string name, GameObject parent ) {

            var go = new GameObject( name );

            if ( parent ) {
                go.transform.parent = parent.transform;
            }

            go.transform.position = Vector3.zero;
            go.transform.rotation = Quaternion.identity;

            return go;
        }


        /// <summary>
        /// Creates a hierachy of GameObjects from the provided path.
        /// </summary>
        /// <param name="path">Path in the form "/root/child/grandchild".</param>
        /// <returns>A 2-<c>Tuple</c> containing the root <c>GameObject</c> as the First value, and the descendent <c>GameObject</c> (the last part of the path) as the Second value. If only one object exists in the path, then both values will be the same.</returns>
        public static Tuple<GameObject, GameObject> createHierarchy( string path ) {

            Assert.IsTrue( path.Length > 0 );

            if ( path.IndexOf( '/' ) < 0 ) {
                // Just a single name
                var go = createEmpty( path, null );
                return new Tuple<GameObject, GameObject>( go, go );
            }


            GameObject root = null;
            GameObject parent = null;

            var names = path.Split( '/' );

            for ( var i = 0; i < names.Length; i++ ) {
                if ( names[i].Length == 0 ) {
                    continue;
                }

                parent = createEmpty( names[i], parent );

                if ( root == null ) {
                    root = parent;
                }
            }

            return new Tuple<GameObject, GameObject>( root, parent );
        }


        /// <summary>
        /// Visits a hierarchy of GameObjects by walking up from the specified child.
        /// </summary>
        /// <param name="child">The <c>GameObject</c> to begin visiting.</param>
        /// <param name="callback">Callback.</param>
        public static void walkUpHierarchy( GameObject child, Action<GameObject> callback ) {

            while ( child ) {
                callback( child );

                if ( ! child.transform.parent ) {
                    break;
                }

                child = child.transform.parent.gameObject;
            }
        }


        /// <summary>
        /// Gets the root <c>GameObject</c> for the specified child <c>GameObject</c>.
        /// </summary>
        /// <param name="child">The <c>GameObject</c> at which to start the search.</param>
        /// <returns>
        /// The root (top-level) <c>GameObject</c> in the parent hierarchy. If <c>child</c> has no parent,
        /// then it itself will be returned.
        /// </returns>
        public static GameObject getRootGameObject( GameObject child ) {

            while ( child ) {
                if ( ! child.transform.parent ) {
                    break;
                }

                child = child.transform.parent.gameObject;
            }

            return child;
        }
    }
}
