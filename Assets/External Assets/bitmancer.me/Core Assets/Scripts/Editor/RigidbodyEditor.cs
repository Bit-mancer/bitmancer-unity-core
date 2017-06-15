using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;

// TODO probably want to provide a #def around this (that defaults to off) since not everyone will want it...

namespace Bitmancer.Core.Editor {

    /// <summary>
    /// Custom editor for Rigidbody to expose some additional debug information while in play mode.
    /// </summary>
    [CanEditMultipleObjects, CustomEditor( typeof(Rigidbody) )]
    public class RigidbodyEditor : Bitmancer.Core.Editor.BaseEditor<Rigidbody> {

        // internal UnityEditor.EditorGUILayout.kLabelFloatMaxW
        static float kLabelFloatMaxW {
            get {
                return EditorGUIUtility.labelWidth + EditorGUIUtility.fieldWidth + 5f;
            }
        }


        private bool _contraintsExpanded = true;


        /// <summary>
        /// Draws a rigidbody constraint toggle.
        /// </summary>
        /// <returns>The updated constraints.</returns>
        /// <param name="r">Where to draw the toggle.</param>
        /// <param name="label">Label.</param>
        /// <param name="constraints">The current set of (all) contraints values.</param>
        /// <param name="value">The contraint value for this toggle.</param>
        /// <remarks>
        /// This is needed because there isn't a built-in to handle all of this, so we have to re-create it all :(
        /// </remarks>
        private RigidbodyConstraints constraintsToggle( Rect r, string label, RigidbodyConstraints constraints, RigidbodyConstraints value ) {
            
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            bool isSet = (constraints & value) != 0;

            bool newValue = EditorGUI.ToggleLeft( r, label, isSet );
            if ( newValue != isSet ) {
                if ( newValue ) {
                    constraints |= value;
                } else {
                    constraints &= ~value;
                }
            }

            EditorGUI.indentLevel = indent;

            return constraints;
        }


        /// <summary>
        /// Create the GUI for a set of contraints toggles.
        /// </summary>
        /// <returns>The updated contraints.</returns>
        /// <param name="label">Label.</param>
        /// <param name="constraints">Constraints.</param>
        /// <param name="x">The type of X coordinate constraint (position or rotation for X).</param>
        /// <param name="y">The type of X coordinate constraint (position or rotation for Y).</param>
        /// <param name="z">The type of X coordinate constraint (position or rotation for Z).</param>
        /// <remarks>
        /// This is needed because there isn't a built-in to handle all of this, so we have to re-create it all :(
        /// </remarks>
        private RigidbodyConstraints constraintsToggleBlock( string label, RigidbodyConstraints constraints, RigidbodyConstraints x, RigidbodyConstraints y, RigidbodyConstraints z ) {
            
            using ( new EditorGUILayout.HorizontalScope() ) {
                Rect rect = GUILayoutUtility.GetRect( EditorGUIUtility.fieldWidth, kLabelFloatMaxW, 16f, 16f, EditorStyles.numberField);
                int controlID = GUIUtility.GetControlID( 7231, FocusType.Keyboard, rect );
                rect = EditorGUI.PrefixLabel( rect, controlID, new GUIContent( label ) );

                rect.width = 30f;
                constraints = constraintsToggle( rect, "X", constraints, x );

                rect.x += 30f;
                constraints =constraintsToggle( rect, "Y", constraints, y );

                rect.x += 30f;
                constraints = constraintsToggle( rect, "Z", constraints, z );
            }

            return constraints;
        }


        public override void OnInspectorGUI() {

            base.OnInspectorGUI();

            DrawDefaultInspector();

            _contraintsExpanded = EditorGUILayout.Foldout( _contraintsExpanded, "Contraints" );
            if ( _contraintsExpanded ) {
                EditorGUI.indentLevel++;
                target.constraints = constraintsToggleBlock( "Freeze Position", target.constraints, RigidbodyConstraints.FreezePositionX, RigidbodyConstraints.FreezePositionY, RigidbodyConstraints.FreezePositionZ );
                target.constraints = constraintsToggleBlock( "Freeze Rotation", target.constraints, RigidbodyConstraints.FreezeRotationX, RigidbodyConstraints.FreezeRotationY, RigidbodyConstraints.FreezeRotationZ );
                EditorGUI.indentLevel--;
            }

            if ( Application.isPlaying ) {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField( "Debug", CustomStyles.Bold );
                EditorGUILayout.LabelField( "Velocity", string.Format( "{0}  Mag: {1}", target.velocity.ToString(), target.velocity.magnitude.ToString( "0.00" ) ) );
                EditorGUILayout.LabelField( "Angular Velocity", string.Format( "{0}  Mag: {1}", target.angularVelocity.ToString(), target.angularVelocity.magnitude.ToString( "0.00" ) ) );
                EditorGUILayout.LabelField( "Is Sleeping", target.IsSleeping() ? bool.TrueString : bool.FalseString );
            }
        }

        void OnSceneGUI() {
            Handles.color = Color.yellow;
            Handles.SphereHandleCap( 1, target.transform.TransformPoint( target.centerOfMass ), target.rotation, 0.2f, Event.current.type );
        }
    }
}
