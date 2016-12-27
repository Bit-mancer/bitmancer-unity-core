using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bitmancer.Core.Util {

    /// <summary>
    /// Logging helpers.
    /// </summary>
    /// <remarks>
    /// The various debug() methods are conditionally compiled into debug editor builds only (via "DEBUG" and "UNITY_EDITOR" #defs).
    /// </remarks>
    public static class Log {

        private static string formatLogMessage( string message ) {
            return string.Format( "{0:0.00} - {1}", Time.unscaledTime, message );
        }


        // Debug:

        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void debug( string message, object arg0  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0 ) ) );
        }


        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void debug( UnityEngine.Object context, string message, object arg0  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0 ) ) );
        }


        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void debug( string message, object arg0, object arg1  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0, arg1 ) ) );
        }


        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void debug( UnityEngine.Object context, string message, object arg0, object arg1  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0, arg1 ) ), context );
        }


        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void debug( string message, object arg0, object arg1, object arg2  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0, arg1, arg2 ) ) );
        }


        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void debug( UnityEngine.Object context, string message, object arg0, object arg1, object arg2  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0, arg1, arg2 ) ), context );
        }


        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void debug( string message, params object[] args  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, args ) ) );
        }


        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void debug( UnityEngine.Object context, string message, params object[] args  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, args ) ), context );
        }


        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void debug( object message ) {
            UnityEngine.Debug.Log( message );
        }


        [Conditional( "DEBUG" )]
        [Conditional( "UNITY_EDITOR" )]
        public static void debug( UnityEngine.Object context, object message ) {
            UnityEngine.Debug.Log( message, context );
        }




        // Info:

        public static void info( string message, object arg0  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0 ) ) );
        }

        public static void info( UnityEngine.Object context, string message, object arg0  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0 ) ), context );
        }

        public static void info( string message, object arg0, object arg1  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0, arg1 ) ) );
        }

        public static void info( UnityEngine.Object context, string message, object arg0, object arg1  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0, arg1 ) ), context );
        }

        public static void info( string message, object arg0, object arg1, object arg2  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0, arg1, arg2 ) ) );
        }

        public static void info( UnityEngine.Object context, string message, object arg0, object arg1, object arg2  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, arg0, arg1, arg2 ) ), context );
        }

        public static void info( string message, params object[] args  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, args ) ) );
        }

        public static void info( UnityEngine.Object context, string message, params object[] args  ) {
            UnityEngine.Debug.Log( formatLogMessage( string.Format( message, args ) ), context );
        }

        public static void info( object message ) {
            UnityEngine.Debug.Log( message );
        }

        public static void info( UnityEngine.Object context, object message ) {
            UnityEngine.Debug.Log( message, context );
        }




        // Warn:

        public static void warn( string message, object arg0  ) {
            UnityEngine.Debug.LogWarning( formatLogMessage( string.Format( message, arg0 ) ) );
        }

        public static void warn( UnityEngine.Object context, string message, object arg0  ) {
            UnityEngine.Debug.LogWarning( formatLogMessage( string.Format( message, arg0 ) ), context );
        }

        public static void warn( string message, object arg0, object arg1  ) {
            UnityEngine.Debug.LogWarning( formatLogMessage( string.Format( message, arg0, arg1 ) ) );
        }

        public static void warn( UnityEngine.Object context, string message, object arg0, object arg1  ) {
            UnityEngine.Debug.LogWarning( formatLogMessage( string.Format( message, arg0, arg1 ) ), context );
        }

        public static void warn( string message, object arg0, object arg1, object arg2  ) {
            UnityEngine.Debug.LogWarning( formatLogMessage( string.Format( message, arg0, arg1, arg2 ) ) );
        }

        public static void warn( UnityEngine.Object context, string message, object arg0, object arg1, object arg2  ) {
            UnityEngine.Debug.LogWarning( formatLogMessage( string.Format( message, arg0, arg1, arg2 ) ), context );
        }

        public static void warn( string message, params object[] args  ) {
            UnityEngine.Debug.LogWarning( formatLogMessage( string.Format( message, args ) ) );
        }

        public static void warn( UnityEngine.Object context, string message, params object[] args  ) {
            UnityEngine.Debug.LogWarning( formatLogMessage( string.Format( message, args ) ), context );
        }

        public static void warn( object message ) {
            UnityEngine.Debug.LogWarning( message );
        }

        public static void warn( UnityEngine.Object context, object message ) {
            UnityEngine.Debug.LogWarning( message, context );
        }




        // Error:

        public static void error( string message, object arg0  ) {
            UnityEngine.Debug.LogError( formatLogMessage( string.Format( message, arg0 ) ) );
        }

        public static void error( UnityEngine.Object context, string message, object arg0  ) {
            UnityEngine.Debug.LogError( formatLogMessage( string.Format( message, arg0 ) ), context );
        }

        public static void error( string message, object arg0, object arg1  ) {
            UnityEngine.Debug.LogError( formatLogMessage( string.Format( message, arg0, arg1 ) ) );
        }

        public static void error( UnityEngine.Object context, string message, object arg0, object arg1  ) {
            UnityEngine.Debug.LogError( formatLogMessage( string.Format( message, arg0, arg1 ) ), context );
        }

        public static void error( string message, object arg0, object arg1, object arg2  ) {
            UnityEngine.Debug.LogError( formatLogMessage( string.Format( message, arg0, arg1, arg2 ) ) );
        }

        public static void error( UnityEngine.Object context, string message, object arg0, object arg1, object arg2  ) {
            UnityEngine.Debug.LogError( formatLogMessage( string.Format( message, arg0, arg1, arg2 ) ), context );
        }

        public static void error( string message, params object[] args  ) {
            UnityEngine.Debug.LogError( formatLogMessage( string.Format( message, args ) ) );
        }

        public static void error( UnityEngine.Object context, string message, params object[] args  ) {
            UnityEngine.Debug.LogError( formatLogMessage( string.Format( message, args ) ), context );
        }

        public static void error( object message ) {
            UnityEngine.Debug.LogError( message );
        }

        public static void error( UnityEngine.Object context, object message ) {
            UnityEngine.Debug.LogError( message, context );
        }
    }
}
