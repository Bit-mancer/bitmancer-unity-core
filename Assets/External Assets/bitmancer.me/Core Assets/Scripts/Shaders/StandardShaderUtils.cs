using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bitmancer.Core.Shaders {

    /// <summary>
    /// Utilities for Unity's Standard Shader.
    /// </summary>
    public static class StandardShaderUtils {

        public static void setBlendModeToOpaque( Material material ) {

            Assert.IsNotNull( material );

#if UNITY_EDITOR
            Assert.IsTrue( material.shader.name == "Standard", "This function is only intended for the Unity Standard Shader." );
#endif

            // From StandardShaderGUI.cs

            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
        }


        public static void setBlendModeToTransparent( Material material ) {
            
            Assert.IsNotNull( material );

#if UNITY_EDITOR
            Assert.IsTrue( material.shader.name == "Standard", "This function is only intended for the Unity Standard Shader." );
#endif
            
            // From StandardShaderGUI.cs
            
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
        }


        public static void setAlbedoColor( Material material, Color c ) {

            Assert.IsNotNull( material );
            
#if UNITY_EDITOR
            Assert.IsTrue( material.shader.name == "Standard", "This function is only intended for the Unity Standard Shader." );
#endif
            
            // From StandardShaderGUI.cs
            material.SetColor( "_Color", c );
        }
    }
}
