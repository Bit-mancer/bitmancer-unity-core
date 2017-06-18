using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;
using Bitmancer.Core;

namespace Bitmancer.Core.Util {

    public static class Singleton {

        /// <summary>
        /// Gets the 'T' Component instance that is anchored to a GameObject with the provided name. You should cache the returned value.
        /// </summary>
        /// <returns>The 'T' Component instance; you should cache this value.</returns>
        /// <param name="anchoringGameObjectName">Name of the <c>GameObject</c> that anchors the component.</param>
        /// <typeparam name="T">The Component type.</typeparam>
        public static T getComponent<T>( string anchoringGameObjectName ) where T: BaseBehavior {

            var anchor = GameObject.Find( anchoringGameObjectName );
            if ( ! anchor ) {
                var tuple = GameObjectUtils.createHierarchy( anchoringGameObjectName );
                GameObject.DontDestroyOnLoad( tuple.First ); // mark the root as don't-destroy

                anchor = tuple.Second;

                GameObjectUtils.walkUpHierarchy( anchor, go => {
                    go.isStatic = true;
                    go.layer = LayerMask.NameToLayer( Layer.IgnoreRaycast );
                });
            }

            return getComponent<T>( anchor );
        }


        /// <summary>
        /// Gets the Component instance that is anchored to the provided GameObject. You should cache the returned value.
        /// </summary>
        /// <returns>The 'T' Component instance; you should cache this value.</returns>
        /// <param name="anchor">The GameObject that anchors the component.</param>
        /// <typeparam name="T">The Component type.</typeparam>
        public static T getComponent<T>( GameObject anchor ) where T: BaseBehavior {

            Assert.IsNotNull( anchor );

            T target = anchor.GetComponentInChildren<T>();
            if ( ! target ) {
                target = anchor.AddComponent<T>();
            }

            return target;
        }
    }
}
