Shader "Effects/PostFXShader"
{
//show values to edit in inspector
Properties{
    _MainTex("Texture", 2D) = "white" {}
    _DitherPattern("Dithering Pattern", 2D) = "white" {}
    _Color1("Dither Color 1", Color) = (0, 0, 0, 1)
    _Color2("Dither Color 2", Color) = (1, 1, 1, 1)
        _DitherScale("Dither Scale", Float) = 1
        _ColorFactor("Color Factor", Float) = 4
        _Contrast("Contrast", Float) = 1
        _OverlayTransparency("Overlay Transparency", Float) = 1
        _Overlay("Overlay Texture",2D) = "white" {}
}

SubShader{
        //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
        Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

        Pass{
            CGPROGRAM

            //include useful shader functions
            #include "UnityCG.cginc"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag

            //texture and transforms of the texture
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _Overlay;

            //The dithering pattern
            sampler2D _DitherPattern;
            float4 _DitherPattern_TexelSize;

            //Dither colors
            float4 _Color1;
            float4 _Color2;
            float _DitherScale;
            float _ColorFactor;
            float _Contrast;
            float _OverlayTransparency;

            //the object data that's put into the vertex shader
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            //the data that's used to generate fragments and can be read by the fragment shader
            struct v2f {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 screenPosition : TEXCOORD1;
            };

            //the vertex shader
            v2f vert(appdata v) {
                v2f o;
                //convert the vertex positions from object space to clip space so they can be rendered
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPosition = ComputeScreenPos(o.position);
                return o;
            }

            //the fragment shader
            fixed4 frag(v2f i) : SV_TARGET{
                //texture value the dithering is based on
                float4 ogTexColor = tex2D(_MainTex, i.uv);

                float4 ogOverlay = tex2D(_Overlay, i.uv);

            
            //posterize texColor
            ogTexColor = floor(ogTexColor * _ColorFactor) / _ColorFactor;

            //posterize overlay
            ogOverlay = floor(ogOverlay * _ColorFactor) / _ColorFactor;

            //grayscale filter
            float texColor = (ogTexColor.r + ogTexColor.g + ogTexColor.b) / 3.0;

            float overlayColor = (ogOverlay.r + ogOverlay.g + ogOverlay.b) / 3.0;

            //contrast
            texColor = pow(texColor, _Contrast);

            overlayColor = pow(overlayColor, _Contrast);

            //value from the dither pattern
            float2 screenPos = i.screenPosition.xy / i.screenPosition.w;
            float2 ditherCoordinate = screenPos * _ScreenParams.xy * _DitherPattern_TexelSize.xy * _DitherScale;
            float ditherValue = tex2D(_DitherPattern, ditherCoordinate).r;
            
            //combine dither pattern with texture value to get final result
            float ditheredValue = step(ditherValue, texColor);
            float4 col = lerp(_Color1, _Color2, ditheredValue);

            float overlayDitheredValue = step(ditherValue, overlayColor);
            float4 overlayCol = lerp(_Color1, _Color2, overlayDitheredValue);

            float ditheredAlpha = step(ditherValue, ogOverlay.a * _OverlayTransparency);

            col = lerp(col, overlayCol, ditheredAlpha);

            return col;
        }

        ENDCG
    }
    }

        Fallback "Standard"
}