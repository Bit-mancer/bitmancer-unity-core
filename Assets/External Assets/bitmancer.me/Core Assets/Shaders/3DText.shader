/**
 * UI text uses the same shader as GUIText, which renders on top of everything. This shader renders text in the transparent queue.
 *
 * Author: Eric Haines
 * URL: http://wiki.unity3d.com/index.php?title=3DText
 * License: CC-BY-SA 3.0
 * License Source: http://creativecommons.org/licenses/by-sa/3.0/
 */
Shader "GUI/3D Text Shader" {
    Properties {
        _MainTex ("Font Texture", 2D) = "white" {}
        _Color ("Text Color", Color) = (1,1,1,1)
    }

    SubShader {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Lighting Off Cull Off ZWrite Off Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass {
            Color [_Color]
            SetTexture [_MainTex] {
                combine primary, texture * primary
            }
        }
    }
}
