using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bitmancer.Core.Physics {

    /// <summary>
    /// Represents a gravity well (a singularity).
    /// </summary>
    /// <remarks>
    /// Gravity is applied only to Rigidbodies with an attached <c>GravityReceptor</c>.
    /// </remarks>
    [RequireComponent( typeof(SphereCollider) )]
    [DisallowMultipleComponent]
    public sealed class GravityWell : BaseBehavior {

        /// <summary>
        /// The gravity scalar (similar to how you would provide per-project gravity via Unity's Edit->Project Settings->Physics->Gravity, but in this case, as the magnitude of the vector).
        /// </summary>
        /// <remarks>The gravity vector at a given update depends on the relative positions of the receptor and the well.</remarks>
        [Tooltip( "The gravity scalar (similar to how you would provide per-project gravity via Unity's Edit->Project Settings->Physics->Gravity, but in this case, as the magnitude of the vector)." )]
        public float gravityScalar;


        private SphereCollider _collider;



        /// <summary>
        /// Gets the singularity position (takes the attached collider <c>center</c> into account).
        /// </summary>
        /// <value>The singularity position.</value>
        public Vector3 SingularityPosition {
            get {
                return transform.position + _collider.center;
            }
        }


        /**
         * Execution Order:
         *
         *  Awake()
         *  OnEnable() / OnDisable()
         *  Start()
         *  FixedUpdate() [0..n times]
         *  Update()
         *  LateUpdate()
         *  OnDestroy()
         * */

        void Awake() {
            _collider = GetRequiredComponent<SphereCollider>();
//            Bitmancer.Core.Util.DebugUtils.assert( _collider.isTrigger, string.Format( "The attached collider must be a trigger!  (is the {0} attached to the main transform, rather than a child transform with a trigger collider?)", this.GetType().Name ) );
            Assert.IsTrue( _collider.isTrigger, string.Format( "The attached collider must be a trigger collider!  (is the {0} attached to the main transform, rather than a child transform with a trigger collider?)", this.GetType().Name ) );
        }


        void OnDrawGizmos() {

            // _collider may be null in the editor, so let's look it up

            var collider = _collider;
            if ( collider == null ) {
                collider = this.GetComponent<SphereCollider>();
                if ( collider == null ) {
                    return;
                }
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere( transform.position + collider.center, collider.radius * Bitmancer.Core.Extensions.Vector3Extensions.GetLargestAxisMagnitude( transform.lossyScale ) );
        }


        void OnTriggerEnter( Collider other ) {

            // TODO cache
            var receptor = other.GetComponent<GravityReceptor>();
            if ( receptor != null ) {
                receptor.addWell( this ); // TODO make this Entity/Handle based?
            }
        }


        void OnTriggerExit( Collider other ) {

            // TODO cache
            var receptor = other.GetComponent<GravityReceptor>();
            if ( receptor != null ) {
                receptor.addWell( this ); // TODO make this Entity/Handle based?
            }
        }
    }
}
