using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bitmancer.Core.Util {

    /// <summary>
    /// Various debugging-related utilities.
    /// </summary>
    /// <remarks>
    /// All methods of this class are conditionally compiled into debug editor builds only (via "DEBUG" and "UNITY_EDITOR" #defs).
    /// There should be no runtime performance penalty to calling these in release builds.
    /// </remarks>
    public static class DebugUtils {

        // Obviated by UnityEngine.Assertions

//        /// <summary>
//        /// Represents runtime assertion errors.
//        /// </summary>
//        private class AssertionException : Exception {
//
//            public AssertionException( string message ) : base( message ) {
//            }
//
//            public AssertionException( string message, Exception inner ) : base( message, inner ) {
//            }
//        }



        /// <summary>
        /// Draws a playmode "cross" marker.
        /// </summary>
        /// <remarks>
        /// Don't use this for drawing editor gizmos; instead, use drawGizmoMarker.
        /// </remarks>
        /// <param name="position">Position.</param>
        /// <param name="size">Size.</param>
        /// <param name="color">Color.</param>
        /// <param name="duration">Duration.</param>
        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void drawMarker( Vector3 position, float size, Color color, float duration = 0f ) {

            Vector3 xStart = position + (Vector3.left * size * 0.5f);
            Vector3 xEnd = position + (Vector3.right * size * 0.5f);

            Vector3 yStart = position + (Vector3.down * size * 0.5f);
            Vector3 yEnd = position + (Vector3.up * size * 0.5f);

            Vector3 zStart = position + (Vector3.back * size * 0.5f);
            Vector3 zEnd = position + (Vector3.forward * size * 0.5f);

            UnityEngine.Debug.DrawLine( xStart, xEnd, color, duration );
            UnityEngine.Debug.DrawLine( yStart, yEnd, color, duration );
            UnityEngine.Debug.DrawLine( zStart, zEnd, color, duration );
        }


        /// <summary>
        /// Draws an editor gizmo "cross" marker.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="size">Size.</param>
        /// <param name="color">Color.</param>
        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void drawGizmoMarker( Vector3 position, float size, Color color ) {

            Vector3 xStart = position + (Vector3.left * size * 0.5f);
            Vector3 xEnd = position + (Vector3.right * size * 0.5f);

            Vector3 yStart = position + (Vector3.down * size * 0.5f);
            Vector3 yEnd = position + (Vector3.up * size * 0.5f);

            Vector3 zStart = position + (Vector3.back * size * 0.5f);
            Vector3 zEnd = position + (Vector3.forward * size * 0.5f);

            Gizmos.color = color;

            Gizmos.DrawLine( xStart, xEnd );
            Gizmos.DrawLine( yStart, yEnd );
            Gizmos.DrawLine( zStart, zEnd );
        }



        // Obviated by UnityEngine.Assertions

//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assert( bool condition ) {
//            if ( ! condition ) {
//                throw new AssertionException( "An assertion failed." );
//            }
//        }
//
//
//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assert( bool condition, string message ) {
//            if ( ! condition ) {
//                throw new AssertionException( string.Format( "An assertion failed: {0}", message ) );
//            }
//        }
//
//
//        /// <remarks>
//        /// Use this instead of assertNotNull when you need to check whether Component-derived variables are not-null.
//        /// tl;dr Unity Components have a custom == operator, and unassigned Components are populated with a "fake null" object when running in the Unity Editor \m/
//        /// https://blogs.unity3d.com/2014/05/16/custom-operator-should-we-keep-it/
//        /// https://forum.unity3d.com/threads/null-check-inconsistency-c.220649/
//        /// 
//        /// Note that more recent versions of Unity have considerably better error reporting when a Component is null during an editor run (by inserting a "null" zombie 
//        /// object that has appropriate error handling). Using assertComponentNotNull in Awake() is still useful when you want to know as soon as possible that a 
//        /// configuration error has occurred, rather than waiting until the component is actually accessed, which could potentially be at a significantly later time in 
//        /// execution.
//        /// </remarks>
//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assertComponentNotNull<T>( T obj ) where T: Component {
//            if ( object.ReferenceEquals( obj, null ) ) {
//                throw new AssertionException( "Component is null/unassigned." );
//            }
//        }
//
//        /// <remarks>
//        /// Use this instead of assertNotNull when you need to check whether Component-derived variables are not-null.
//        /// tl;dr Unity Components have a custom == operator, and unassigned Components are populated with a "fake null" object when running in the Unity Editor \m/
//        /// https://blogs.unity3d.com/2014/05/16/custom-operator-should-we-keep-it/
//        /// https://forum.unity3d.com/threads/null-check-inconsistency-c.220649/
//        /// 
//        /// Note that more recent versions of Unity have considerably better error reporting when a Component is null during an editor run (by inserting a "null" zombie 
//        /// object that has appropriate error handling). Using assertComponentNotNull in Awake() is still useful when you want to know as soon as possible that a 
//        /// configuration error has occurred, rather than waiting until the component is actually accessed, which could potentially be at a significantly later time in 
//        /// execution.
//        /// </remarks>
//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assertComponentNotNull<T>( T obj, string message ) where T: Component {
//            if ( object.ReferenceEquals( obj, null ) ) {
//                throw new AssertionException( string.Format( "Component is null/unassigned: {0}", message ) );
//            }
//        }
//
//
//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assertNull<T>( T obj ) where T: class {
//            if ( ! object.ReferenceEquals( obj, null ) ) {
//                throw new AssertionException( "Object reference is NOT null." );
//            }
//        }
//
//
//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assertNull<T>( T obj, string message ) where T: class {
//            if ( ! object.ReferenceEquals( obj, null ) ) {
//                throw new AssertionException( string.Format( "Object reference is NOT null: {0}", message ) );
//            }
//        }
//
//
//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assertNotNull<T>( T obj ) where T: class {
//            if ( object.ReferenceEquals( obj, null ) ) {
//                throw new AssertionException( "Object reference is null." );
//            }
//        }
//
//
//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assertNotNull<T>( T obj, string message ) where T: class {
//            if ( object.ReferenceEquals( obj, null ) ) {
//                throw new AssertionException( string.Format( "Object reference is null: {0}", message ) );
//            }
//        }
//
//
//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assertEqual<A, B>( A a, B b ) {
//            if ( ! object.Equals( a, b ) ) {
//                throw new AssertionException( string.Format( "Objects are not equal ('a': {0}, 'b': {1})", a, b ) );
//            }
//        }
//
//
//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assertEqual<A, B>( A a, B b, string message ) {
//            if ( ! object.Equals( a, b ) ) {
//                throw new AssertionException( string.Format( "Objects are not equal: {0} ('a': {1}, 'b': {2})", message, a, b ) );
//            }
//        }
//
//
//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assertNotEqual<A, B>( A a, B b ) {
//            if ( object.Equals( a, b ) ) {
//                throw new AssertionException( string.Format( "Objects are equal ('a': {0}, 'b': {1})", a, b ) );
//            }
//        }
//
//
//        [Conditional( "DEBUG" )]
//        [Conditional( "UNITY_EDITOR" )]
//        public static void assertNotEqual<A, B>( A a, B b, string message ) {
//            if ( object.Equals( a, b ) ) {
//                throw new AssertionException( string.Format( "Objects are equal: {0} ('a': {1}, 'b': {2})", message, a, b ) );
//            }
//        }
    }
}
