using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;

namespace Bitmancer.Core.Editor {

    [CanEditMultipleObjects, CustomEditor( typeof(Bitmancer.Core.UI.StrobingButton) )]
    public sealed class StrobingButtonEditor : BaseEditor<Bitmancer.Core.UI.StrobingButton> {

        /**
         * Execution Order:
         * 
         *  Awake()
         *  OnEnable() / OnDisable()
         *  Start()
         *  FixedUpdate() [0..n times]
         *  Update()
         *  LateUpdate()
         * */

        void OnEnable() {
            EditorApplication.update += onEditorApplicationUpdate;
        }


        void OnDisable() {
            EditorApplication.update -= onEditorApplicationUpdate;
        }


        private void onEditorApplicationUpdate() {
            // Relying solely on OnInspectorGUI to be called results in a debug field that doesn't update quickly enough 
            // to be useful, so let's repaint on Update (which will be a lot, but this is only run when the editor is displayed)
            this.Repaint();
        }


        public override void OnInspectorGUI() {

            base.OnInspectorGUI();

            DrawDefaultInspector();

            if ( Application.isPlaying ) {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField( "Debug", CustomStyles.Bold );

                EditorGUILayout.LabelField( 
                    "Current Alpha", 
                    string.Format( "{0}", target.GetRequiredComponent<CanvasRenderer>().GetAlpha().ToString( "0.00" ) ) );
            }
        }
    }
}
