using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bitmancer.Core.Util {

    /// <summary>
    /// API for a pool of <c>T</c> objects.
    /// </summary>
    /// <remarks>
    /// The pool is a contribution-type pool: the caller, with knowledge of how to construct the type <c>T</c>, "releases" instances into the pool for later consumption.
    /// Releasing objects back is technically optional, but the pool will never have instances to offer up without "old" instances added back to the pool.
    /// 
    /// The caller is responsible for any freezing/thawing required to safely store and later reconstitute the objects.
    /// </remarks>
    public interface IObjectPool<T> where T: class {

        /// <summary>
        /// Gets the number of objects in the pool.
        /// </summary>
        /// <value>The number of objects in the pool.</value>
        int Count {
            get;
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
        T claim();

        /// <summary>
        /// Release the provided instance.
        /// </summary>
        /// <param name="obj">Object instance to release.</param>
        /// <remarks>The caller should ensure that the object is no longer used following a call to release() (i.e. setting the variable to null would be a best practice).</remarks>
        void release( T obj );

        /// <summary>
        /// Removes all items in the pool.
        /// </summary>
        void clear();
    }
}
