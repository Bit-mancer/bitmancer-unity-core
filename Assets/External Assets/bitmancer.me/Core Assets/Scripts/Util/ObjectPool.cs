using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bitmancer.Core.Util {

    /// <summary>
    /// Pool for objects of type <c>T</c>.
    /// </summary>
    /// <remarks>
    /// The pool is a contribution-type pool: the caller, with knowledge of how to construct the type <c>T</c>, "releases" instances into the pool for later consumption.
    /// Releasing objects back is technically optional, but the pool will never have instances to offer up without "old" instances added back to the pool.
    /// 
    /// The caller is responsible for any freezing/thawing required to safely store and later reconstitute the objects.
    /// </remarks>
    public class ObjectPool<T> : IObjectPool<T> where T: class {

        /// <summary>
        /// A shared instance of the thread pool -- typically you would use this unless you have a need for a local pool.
        /// </summary>
        public static readonly ObjectPool<T> Shared = new ObjectPool<T>();


        private List<T> _pool = new List<T>();


        #region IObjectPool implementation

        /// <summary>
        /// Gets the number of objects in the pool.
        /// </summary>
        /// <value>The number of objects in the pool.</value>
        public int Count {
            get {
                return _pool.Count;
            }
        }



        /// <summary>
        /// Returns an instance from the pool, or null if no instances are remaining.
        /// </summary>
        /// <remarks>
        /// If null is returned, the caller, which knowlege of how to construct the type <c>T</c>, should allocate a new instance and plan to add it "back" to the pool via release() 
        /// once the instance is no longer needed.
        /// 
        /// No guarantee is made as to the order of objects returned by claim (e.g. do not depend on releasing objects and reclaiming those same objects).
        /// </remarks>
        public T claim() {
            if ( _pool.Count > 0 ) {
                T obj = _pool[ _pool.Count - 1 ];
                _pool.RemoveAt( _pool.Count - 1 );
                return obj;
            } else {
                return null;
            }
        }


        /// <summary>
        /// Release the provided instance.
        /// </summary>
        /// <param name="obj">Object instance to release.</param>
        /// <remarks>The caller should ensure that the object is no longer used following a call to release() (i.e. setting the variable to null would be a best practice).</remarks>
        public void release( T obj ) {
            
            Assert.IsNotNull( obj );

#if DEVELOPMENT_BUILD
            for ( int i = 0; i < _pool.Count; i++ ) {
                if ( _pool[ i ] == obj ) {
                    throw new InvalidOperationException( string.Format( "Target object ({0}) has already been released (is it being released twice?).", obj.ToString() ) );
                }
            }
#endif

            _pool.Add( obj );
        }


        /// <summary>
        /// Removes all items in the pool.
        /// </summary>
        public void clear() {
            _pool.Clear();
        }

        #endregion
    }

}
