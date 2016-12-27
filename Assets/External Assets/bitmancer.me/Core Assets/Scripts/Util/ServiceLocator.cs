using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bitmancer.Core.Util {

    /// <summary>
    /// A simple service locator pattern implementation, with the object type as the key.
    /// Services in this context are things like your game controllers, etc.
    /// </summary>
    public static class ServiceLocator {

        private static Dictionary<Type, System.Object> _cache = new Dictionary<Type, System.Object>();


        /// <summary>
        /// Registers a service with the locator.
        /// </summary>
        /// <param name="service">Service instance.</param>
        public static void registerService<T>( T service ) where T: class {
            Assert.IsNotNull( service );
            _cache.Add( typeof(T), service );
        }


        /// <summary>
        /// Unregisters a service with the locator.
        /// </summary>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void unregisterService<T>() where T: class {
            _cache.Remove( typeof(T) );
        }


        /// <summary>
        /// Gets the service of the provided type.
        /// </summary>
        /// <returns>The service instance.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if a service for the specified type is not registered.</exception>
        /// <remarks>
        /// You should cache the returned service instance.
        /// </remarks>
        public static T getService<T>() where T: class {
            return (T)_cache[ typeof(T) ];
        }
    }
}
