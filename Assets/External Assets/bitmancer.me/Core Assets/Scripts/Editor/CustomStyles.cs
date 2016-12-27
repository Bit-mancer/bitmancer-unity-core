using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bitmancer.Core.Editor {

    /// <summary>
    /// Static GUIStyle helpers.
    /// </summary>
    public static class CustomStyles {

        /// <summary>
        /// Italic GUIStyle.
        /// </summary>
        /// <value>An italic GUIStyle.</value>
        public static GUIStyle Italic {
            get;
            private set;
        }


        /// <summary>
        /// Bold GUIStyle.
        /// </summary>
        /// <value>A bold GUIStyle.</value>
        public static GUIStyle Bold {
            get;
            private set;
        }


        /// <summary>
        /// Left-aligned GUIStyle.
        /// </summary>
        /// <value>A left-aligned GUIStyle.</value>
        public static GUIStyle AlignLeft {
            get;
            private set;
        }
        
        
        /// <summary>
        /// Right-aligned GUIStyle.
        /// </summary>
        /// <value>A right-aligned GUIStyle.</value>
        public static GUIStyle AlignRight {
            get;
            private set;
        }


        /// <summary>
        /// Bottom-right-aligned GUIStyle.
        /// </summary>
        /// <value>A bottom-right-aligned GUIStyle.</value>
        public static GUIStyle AlignBottomRight {
            get;
            private set;
        }
        

        static CustomStyles() {
            GUIStyle i = new GUIStyle( "Label" );
            i.fontStyle = FontStyle.Italic;
            Italic = i;

            GUIStyle b = new GUIStyle( "Label" );
            b.fontStyle = FontStyle.Bold;
            Bold = b;

            GUIStyle al = new GUIStyle();
            al.alignment = TextAnchor.MiddleLeft;
            AlignRight = al;
            
            GUIStyle ar = new GUIStyle();
            ar.alignment = TextAnchor.MiddleRight;
            AlignRight = ar;

            GUIStyle abr = new GUIStyle();
            abr.alignment = TextAnchor.LowerRight;
            AlignBottomRight = abr;
        }
    }
}
