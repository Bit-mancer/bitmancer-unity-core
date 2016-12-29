using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

namespace Bitmancer.Core {

    /// <summary>
    /// Maintains a reference to a specific generation of an <see cref="Entity"/>. The reference is considered "expired" if the Entity generation changes (i.e. when it is recycled).
    /// </summary>
    /// <remarks>
    /// <c>Handle</c> will function correctly if the reference is actually destroyed via GameObject.Destroy (directly destroying pooled objects, e.g. <c>Entity</c>, as a consequence 
    /// of gameplay is not typically recommended).
    /// 
    /// Entity-based objects should expire any <c>Handles</c> they hold upon deactivation to prevent circular references.
    /// </remarks>
    public class Handle {

        private Bitmancer.Core.Entity _ref;
        private ushort _generation;


        /// <summary>
        /// Gets the target <see cref="Entity"/> reference.
        /// </summary>
        /// <value>The target, or null if the target has expired (or if no target was ever set).</value>
        public Entity Target {
            get {
                if ( _generation == Bitmancer.Core.Entity.kExpiredGeneration ) {
                    return null;
                }

                // We can check if a GameObject (or the parent transform of a Component) has been destroyed by using the == operator (which is overriden) to compare 
                // against null (which the operator will evaluate to true if the GO has been destroyed)
                if ( _ref == null || _ref.Generation != _generation ) {
                    _generation = Bitmancer.Core.Entity.kExpiredGeneration; // faster check next time, and helps avoid the (likely very rare) case of cycling all the way back
                    _ref = null;
                    return null;
                }

                return _ref;
            }

            set {
                if ( value == null ) {
                    _ref = null;
                    _generation = Bitmancer.Core.Entity.kExpiredGeneration;
                } else {
                    _ref = value;
                    _generation = value.Generation;
                }
            }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="Bitmancer.Core.Handle"/> class. The Handle is "empty" (expired). A reference may be later assigned to <see cref="Target"/>.
        /// </summary>
        public Handle() {
            _ref = null;
            _generation = Bitmancer.Core.Entity.kExpiredGeneration;
        }
        

        /// <summary>
        /// Initializes a new instance of the <see cref="Bitmancer.Core.Handle"/> class associated with the provided <see cref="Entity"/>.
        /// </summary>
        /// <param name="entity">Entity, or null.</param>
        public Handle( Entity entity ) {

            _ref = entity;

            // We can check if a GameObject (or the parent transform of a Component) has been destroyed by using the == operator (which is overriden) to compare 
            // against null (which the operator will evaluate to true if the GO has been destroyed)
            if ( entity == null ) {
                _generation = Bitmancer.Core.Entity.kExpiredGeneration;
            } else {
                _generation = entity.Generation;
            }
        }



        /// <summary>
        /// Expires the reference to the current <see cref="Entity"/>.
        /// </summary>
        public void expire() {
            _ref = null;
            _generation = Bitmancer.Core.Entity.kExpiredGeneration;
        }
    }
}
