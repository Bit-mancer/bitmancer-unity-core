using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bitmancer.Core.Extensions {

    /// <summary>
    /// Represents errors that occur when a Unity Component that is expected to be attached to a GameObject cannot be found.
    /// </summary>
    public class RequiredComponentException : Exception {

        public RequiredComponentException( string message ) : base( message ) {
        }

        public RequiredComponentException( string message, Exception inner ) : base( message, inner ) {
        }
    }
}
