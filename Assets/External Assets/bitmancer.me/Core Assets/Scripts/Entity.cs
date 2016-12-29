using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bitmancer.Core {

    /// <summary>
    /// Represents a pooled object in the game world (i.e. a GameObject/Transform instance).
    /// </summary>
    /// <remarks>
    /// It is safe to derive from <c>Entity</c> (in fact, it is recommended that you do this for the core/parent class of the game world object).
    /// 
    /// Entity-based objects should expire any <c>Handles</c> they hold upon deactivation to prevent circular references.
    /// </remarks>
    [DisallowMultipleComponent]
    public class Entity : Bitmancer.Core.BaseBehavior {

        /// <summary>
        /// Indicates the expired generation.
        /// </summary>
        /// <remarks>
        /// An <c>Entity</c>, no matter the state it's in, should never have a <c>Generation</c> that matches this value (therefore, this value can be used by external code 
        /// to determine whether a reference to an <c>Entity</c> is in a good, non-expired state, without having to store a separate "is-valid" boolean value).
        /// </remarks>
        public const ushort kExpiredGeneration = ushort.MaxValue;


        private ushort _generation; // TODO will need to figure saving out...
        private Bitmancer.Core.Util.IObjectPool<Bitmancer.Core.Entity> _pool;


        /// <summary>
        /// Gets the generation of the current <c>Entity</c>.
        /// </summary>
        /// <value>The generation value.</value>
        public ushort Generation {
            get {
                return _generation;
            }

            private set {
                _generation = value;
            }
        }



        /// <summary>
        /// Spawns the entity (this is the pooled equivalent of instantiating a GameObject).
        /// </summary>
        /// <param name="pool">The <see cref="Bitmancer.Core.Util.ObjectPool"/> the <c>Entity</c> should be released to when <see cref="recycleEntity()"/> is called.</param>
        /// <remarks>The gameObject will be activated (via <c>gameObject.SetActive(true)</c>) as part of spawning.</remarks>
        public void spawnEntity( Bitmancer.Core.Util.ObjectPool<Bitmancer.Core.Entity> pool ) {
            _pool = pool;
            this.gameObject.SetActive( true );
        }


        /// <summary>
        /// Returns the object to the pool provided via <see cref="spawnEntity()"/>.
        /// </summary>
        /// <remarks>
        /// The recycled entity must not be referenced by the caller after this method is called.
        /// 
        /// The gameObject will be deactivated (via <c>gameObject.SetActive(false)</c>) just before the object is added to the pool.
        /// </remarks>
        public void recycleEntity() {
  
            this.gameObject.SetActive( false );


            // advance the generation (this will cause extant Handles to expire)
            _generation++;

            // Check for wrap
            if ( _generation == kExpiredGeneration ) {
                _generation++;

#if DEVELOPMENT_BUILD
                Bitmancer.Core.Util.Log.warn( this, "Generation wrapped for Entity \"{0}\" ({1})", transform.name, this.GetInstanceID() );
#endif
            }


            if ( _pool != null ) {
                _pool.release( this );
                _pool = null;
            } else {
                // We warn and continue to give some latitude towards testing entities by dropping them directly into a scene, etc.
                // TODO will we ever want to intentionally do this in a production build? If so, consider adding a DEVELOPMENT_BUILD check here...
                Bitmancer.Core.Util.Log.warn( this, "Recyling an Entity which was not configured for recycling; deactivating \"{0}\" ({1})...", transform.name, this.GetInstanceID() );
            }
        }
    }
}
