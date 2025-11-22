Shader "Custom/arena"{
    Properties{
        _Color("Arena Color", Color) = (1, 1, 1, 1)
        _BorderColor("Border Color", Color) = (1, 0, 0, 1)
        _borderRad("Border Radius", Range(0, 1)) = .2
        _smoothness("Smoothness", Range(0,.1)) = 0.1
        _rad("Total Radius", Range(0,1)) = .995
    }

    SubShader{
        Tags { "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" "Queue"="Transparent"}

        Pass{
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            ZTest Always
            HLSLPROGRAM


            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            float4 _Color;
            float4 _BorderColor;
            float _borderRad;
            float _rad;
            float _smoothness;

            struct Attributes { float4 positionOS : POSITION; float2 uv : TEXCOORD0; };
            struct Varyings { float4 positionHCS : SV_POSITION; float2 uv : TEXCOORD0; };

            Varyings vert (Attributes i){
                Varyings o;
                o.positionHCS = TransformObjectToHClip(i.positionOS.xyz);
                o.uv = i.uv;
                return o;
            }

            float4 frag(Varyings i) : SV_Target{
                float2 uv = i.uv * 2 - 1;
                float dist = length(uv);

                float _inner_rad = max(_rad - _borderRad, 0.0);

                float inner = smoothstep(_inner_rad - _smoothness, _inner_rad, dist);
                float outer = smoothstep(_rad - _smoothness, _rad, dist);

                // Compute mask values
                float fillMask   = 1.0 - inner;
                float borderMask = inner * (1.0 - outer);

                float4 color = _Color * fillMask + _BorderColor * borderMask;
                return color;

            }
            ENDHLSL
        }
    }
}
