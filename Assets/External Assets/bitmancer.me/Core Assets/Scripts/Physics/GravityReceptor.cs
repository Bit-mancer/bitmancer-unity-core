using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Bitmancer.Core.Physics {

    /// <summary>
    /// Receives gravity from gravity components such as <c>GravityField</c> and <c>GravityWell</c>.
    /// </summary>
    [RequireComponent( typeof(Rigidbody) )]
    [DisallowMultipleComponent]
    public sealed class GravityReceptor : BaseBehavior {

        [Tooltip( "Whether to use the project's gravity vector (Project Settings -> Physics -> Gravity)." )]
        public bool useProjectGravityVector = true;

        [Tooltip( "Whether the rigidbody should be rotated such that the \"feet\" are always pointed towards the strongest source of gravity." )]
        public bool plantFeet;


        private Rigidbody _rigidbody;

    //    [SerializeField] // TODO how does trigger enter/exit function when loading a scene?? make sure we don't add a duplicate field (despite check here)
        private List<GravityField> _fields;
        private List<GravityWell> _wells;



        /// <summary>
        /// Adds the provided <c>GravityField</c> to this receptor as a gravity effector.
        /// </summary>
        /// <param name="field">Field.</param>
        public void addField( GravityField field ) {

            if ( ! _fields.Contains( field ) ) {
                _fields.Add( field );
            }
        }


        /// <summary>
        /// Removes the provided <c>GravityField</c> from this receptor.
        /// </summary>
        /// <param name="field">Field.</param>
        public void removeField( GravityField field ) {
            _fields.Remove( field );
        }


        /// <summary>
        /// Adds the provided <c>GravityWell</c> to this receptor as a gravity effector.
        /// </summary>
        /// <param name="well">Well.</param>
        public void addWell( GravityWell well ) {

            if ( ! _wells.Contains( well ) ) {
                _wells.Add( well );
            }
        }


        /// <summary>
        /// Removes the provided <c>GravityWell</c> from this receptor.
        /// </summary>
        /// <param name="well">Well.</param>
        public void removeWell( GravityWell well ) {
            _wells.Remove( well );
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
            _rigidbody = GetRequiredComponent<Rigidbody>();

            _fields = new List<GravityField>();
            _wells = new List<GravityWell>();
        }


        // Make these checks in OnEnable to allow for objects that are initially kinematic (with disabled receptors), 
        // that are then made non-kinematic (followed by enabling the receptor).
        void OnEnable() {
            Assert.IsFalse( _rigidbody.isKinematic, "The rigidbody must NOT be kinematic." );
            Assert.IsFalse( _rigidbody.useGravity, "The rigidbody must NOT use built-in gravity." );
        }


        void FixedUpdate() {

            Vector3 gravitySum = Vector3.zero;
            if ( useProjectGravityVector ) {
                gravitySum = UnityEngine.Physics.gravity;
            }

            for ( int i = 0; i < _fields.Count; i++ ) {
                gravitySum += _fields[ i ].gravity;
            }

            for ( int i = 0; i < _wells.Count; i++ ) {
                Vector3 gravity = (_wells[ i ].SingularityPosition - transform.position).normalized * _wells[ i ].gravityScalar;
                gravitySum += gravity;
            }

            _rigidbody.AddForce( gravitySum, ForceMode.Acceleration );


            if ( plantFeet && gravitySum != Vector3.zero ) {

                // TODO likely want to revisit this as I had a specific game/behavior in mind when I wrote it...

                // Rotate feet to point in the direction of the dominant force due to gravity

                Vector3 gravityDirection = gravitySum.normalized;

                Vector3 rotationDirection = gravityDirection;
                float largestDot = Vector3.Dot( gravityDirection, UnityEngine.Physics.gravity.normalized );

                for ( int i = 0; i < _fields.Count; i++ ) {
                    Vector3 dir = _fields[ i ].gravity.normalized;
                    float dot = Vector3.Dot( gravityDirection, dir );
                    if ( dot > largestDot ) {
                        largestDot = dot;
                        rotationDirection = dir;
                    }
                }

                for ( int i = 0; i < _wells.Count; i++ ) {
                    Vector3 gravity = (_wells[ i ].SingularityPosition - transform.position).normalized * _wells[ i ].gravityScalar;
                    Vector3 dir = gravity.normalized;
                    float dot = Vector3.Dot( gravityDirection, dir );
                    if ( dot > largestDot ) {
                        largestDot = dot;
                        rotationDirection = dir;
                    }
                }

                Quaternion rotationAdjustment = Quaternion.FromToRotation( -transform.up, rotationDirection );
                _rigidbody.MoveRotation( rotationAdjustment * _rigidbody.rotation ); // quaternions are non-commutative
            }
        }


        void OnDrawGizmos() {

            const float kLineScale = 0.5f;

            Vector3 gravitySum = Vector3.zero;

            if ( useProjectGravityVector && UnityEngine.Physics.gravity != Vector3.zero ) {
                gravitySum += UnityEngine.Physics.gravity;
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere( transform.position, 0.25f );
                Gizmos.DrawLine( transform.position, transform.position + (UnityEngine.Physics.gravity * kLineScale) );
            }

            if ( _fields != null && _fields.Count > 0 ) {
                Gizmos.color = Color.yellow;
                for ( int i = 0; i < _fields.Count; i++ ) {
                    gravitySum += _fields[ i ].gravity;
                    Gizmos.DrawLine( transform.position, transform.position + (_fields[ i ].gravity * kLineScale) );
                }
            }

            if ( _wells != null && _wells.Count > 0 ) {
                Gizmos.color = Color.black;
                for ( int i = 0; i < _wells.Count; i++ ) {
                    Vector3 gravity = (_wells[ i ].SingularityPosition - transform.position).normalized * _wells[ i ].gravityScalar;
                    gravitySum += gravity;
                    Gizmos.DrawLine( transform.position, transform.position + (gravity * kLineScale) );
                }
            }

            Gizmos.color = Color.magenta;
            Gizmos.DrawLine( transform.position, transform.position + (gravitySum.normalized * 3) );
        }
    }
}
