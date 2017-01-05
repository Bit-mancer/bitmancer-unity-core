/**
 * This is based on Unity's built-in Sprites/Diffuse shader, modified to enable shadows.
 *
 * Note that for your sprites to actually receive shadows, the Receive Shadows option must be enabled on each
 * Sprite Renderer. Receive Shadows is not shown in the inspector, so you can either enable this option
 * programmatically, or you can temporarily change the inspector display to Debug mode by clicking the "hamburger"
 * dropdown icon in the upper-right of the Unity Editor (to the right of the small lock icon), and select Debug.
 * Once you enable Receive Shadows, set Debug back to Normal.
 */
Shader "Sprites/Shadowed Diffuse" {
    Properties {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }

    SubShader {
        Tags {
            /**
             * Queue controls draw-order
             *   Background: 1000
             *   Geometry (opaque): 2000
             *   AlphaTest (alpha-tested geometry): 2450
             *   Transparency (alpha-blended): 3000
             *   Overlay: 4000
             *
             * Queues up to 2500 (Geometry+500) are considered "opaque" and optimize the drawing order;
             * shadows are supported.
             * Higher queues are considered for "transparent objects" and sort objects by distance,
             * furthest-to-closest; shadows are not supported.
             * Skyboxes are rendered between all opaque and all transparent objects.
             */
            //"Queue"="Transparent" // Sprites/Diffuse
            "Queue"="AlphaTest+1" // render just after alpha-tested geometry

            /**
             * Shader category, used by shader replacement, and when rendering camera depth textures.
             *   Opaque (most shaders, e.g. Normal, Self-Illuminated, Reflective, terrain shaders)
             *   Transparent (most semi-transparent shaders, e.g.Transparent, Particle, Font, terrain additive pass)
             *   TransparentCutout (masked transparency shaders, e.g. Transparent Cutout, two-pass vegetation)
             *   Background (Skybox shaders)
             *   Overlay (GUITexture, Halo, Flare)
             *   (...and other terrain/grass options)
             */
            //"RenderType"="Transparent" // Sprites/Diffuse
            "RenderType"="TransparentCutout"

            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        CGPROGRAM

        /**
         * Shadow support:
         *   addshadow: adds a shadow caster pass
         *   fullforwardshadows: support point and spot lights in forward rendering
         */
        //#pragma surface surf Lambert vertex:vert nofog keepalpha // Sprites/Diffuse
        #pragma surface surf Lambert vertex:vert nofog keepalpha addshadow fullforwardshadows

        #pragma multi_compile _ PIXELSNAP_ON
        #pragma multi_compile _ ETC1_EXTERNAL_ALPHA

        sampler2D _MainTex;
        fixed4 _Color;
        sampler2D _AlphaTex;


        struct Input {
            float2 uv_MainTex;
            fixed4 color;
        };

        void vert (inout appdata_full v, out Input o) {
#if defined(PIXELSNAP_ON)
            v.vertex = UnityPixelSnap (v.vertex);
#endif

            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.color = v.color * _Color;
        }

        fixed4 SampleSpriteTexture (float2 uv) {
            fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
            color.a = tex2D (_AlphaTex, uv).r;
#endif //ETC1_EXTERNAL_ALPHA

            return color;
        }

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = SampleSpriteTexture (IN.uv_MainTex) * IN.color;
            o.Albedo = c.rgb * c.a;
            o.Alpha = c.a;
        }

        ENDCG
    }

    Fallback "Sprites/Diffuse"
}
