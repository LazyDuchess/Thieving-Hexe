Shader "Witch/Forward Standard" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
    }
        SubShader{
        Tags { "RenderType" = "Opaque" }
        CGPROGRAM
          #pragma surface surf SimpleLambert

          half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half atten) {
              half NdotL = dot(s.Normal, lightDir);
              half4 c;
              c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
              c.a = s.Alpha;
              return c;
          }

        struct Input {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        float4 _Color;

        void surf(Input IN, inout SurfaceOutput o) {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
        }
        ENDCG
    }
        Fallback "Diffuse"
}