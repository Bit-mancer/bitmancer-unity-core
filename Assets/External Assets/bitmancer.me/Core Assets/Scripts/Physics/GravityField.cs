using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bitmancer.Core.Physics {

    /// <summary>
    /// Represents a gravity field (gravity applied along a vector).
    /// </summary>
    /// <remarks>
    /// Gravity is applied only to Rigidbodies with an attached <c>GravityReceptor</c>.
    /// </remarks>
    [RequireComponent( typeof(Collider) )]
    [DisallowMultipleComponent]
    public sealed class GravityField : BaseBehavior {

        /// <summary>
        /// The gravity vector (similar to how you would provide per-project gravity via Unity's Edit->Project Settings->Physics->Gravity).
        /// </summary>
        [Tooltip( "The gravity vector (similar to how you would provide per-project gravity via Unity's Edit->Project Settings->Physics->Gravity)." )]
        public Vector3 gravity;


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
            Assert.IsTrue( this.GetComponent<Collider>().isTrigger, "The attached collider must be a trigger collider!" );
        }


        void OnDrawGizmos() {

            var collider = this.GetComponent<Collider>();
            if ( collider == null ) {
                return;
            }



            float length = collider.bounds.extents.y * 0.8f;

            Vector3 end = transform.position + (gravity.normalized * length);

            Gizmos.color = Color.green;
            Gizmos.DrawLine( transform.position, end );
            Gizmos.DrawWireSphere( transform.position, 0.2f );
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere( end, 0.1f );


            Gizmos.color = Color.green;

            Type colliderType = collider.GetType();
            if ( colliderType == typeof(SphereCollider) ) {
                SphereCollider sphere = (SphereCollider)collider;
                Gizmos.DrawWireSphere( transform.position + sphere.center, sphere.radius * Bitmancer.Core.Extensions.Vector3Extensions.GetLargestAxisMagnitude( transform.lossyScale ) );
            } else if ( colliderType == typeof(BoxCollider) ) {
                BoxCollider box = (BoxCollider)collider;
                Gizmos.DrawWireCube( transform.position + box.center, Vector3.Scale( box.size, transform.lossyScale ) );
            } else {
                Bitmancer.Core.Util.Log.error( "Support for collider type {0} has not been implemented.", colliderType.Name );
            }
        }


        void OnTriggerEnter( Collider other ) {

            // TODO cache?
            var receptor = other.GetComponent<GravityReceptor>();
            if ( receptor != null ) {
                receptor.addField( this ); // TODO make this Entity/Handle based?
            }
        }


        void OnTriggerExit( Collider other ) {

            // TODO cache?
            var receptor = other.GetComponent<GravityReceptor>();
            if ( receptor != null ) {
                receptor.removeField( this ); // TODO make this Entity/Handle based?
            }
        }
    }
}
