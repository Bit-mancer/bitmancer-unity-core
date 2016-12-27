using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;

namespace Bitmancer.Core.Editor {

    /// <summary>
    /// A replacement (derivation) for Unity's Editor that adds some useful utilities.
    /// </summary>
    /// <typeparam name="T">The Component type that the Editor targets.</typeparam>
    public class BaseEditor<T> : UnityEditor.Editor where T: Component {

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        /// <remarks>This replacement for <c>UnityEditor.Editor.target</c> provides a typed value rather than a generic object.</remarks>
        public new T target {
            get {
                UnityEngine.Object obj = base.target;
                if ( obj != null ) {
                    T val = obj as T;
                    Assert.IsNotNull( val, string.Format( "The target was not the expected Component type ({0}).", typeof(T).Name ) );
                    return val;
                } else {
                    return null;
                }
            }

            set { base.target = value; }
        }


        /// <summary>
        /// Gets the targets.
        /// </summary>
        /// <value>The targets.</value>
        /// <remarks>This replacement for <c>UnityEditor.Editor.targets</c> provides a typed value rather than a generic object.</remarks>
        public new T[] targets {
            get { return base.targets as T[]; }
        }


        /// <summary>
        /// Gets the tooltip (via TooltipAttribute) for the specified field.
        /// </summary>
        /// <returns>The tooltip for the specified field, or null if no TooltipAttribute is present.</returns>
        /// <param name="memberName">Member name.</param>
        protected string getTooltipForField( string memberName ) {
            
            // Look for [Tooltip()]
            MemberInfo[] memberInfo = typeof(T).GetMember( memberName, MemberTypes.Field, BindingFlags.Instance | BindingFlags.Public );
            foreach ( var mi in memberInfo ) {
                TooltipAttribute[] tooltips = mi.GetCustomAttributes( typeof(TooltipAttribute), true ) as TooltipAttribute[];
                if ( tooltips != null && tooltips.Length > 0 ) {
                    return tooltips[ 0 ].tooltip;
                }
            }

            return null;
        }


        public override void OnInspectorGUI() {
            // Print the name of the Editor class at the top of the inspector -- this allows the user to easily identify where the updated inspector is coming from
            EditorGUILayout.LabelField( this.GetType().Name, CustomStyles.Italic );
        }
    }
}
