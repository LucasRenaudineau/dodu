Shader "Custom/arena"{
    Properties{
        _HoleClipRadius("Hole Clip Radius", Range(0.,720.)) = 500
        _BaseColor("Base Color", Color) = (.5,.5,.5,1.)
        _ShadowOrder("Shadow Order", Integer) = 2
        _SmoothnessOrder("Smoothness Order", Integer) = 2
        _SpikesDensity("Spikes Denstiy", Range(10,1000)) = 100
    }

    SubShader{
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}

        Pass{
            ZWrite Off
            ZTest Always
            HLSLPROGRAM


            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            float _HoleClipRadius;
            float4 _BaseColor;
            int _ShadowOrder;
            int _SmoothnessOrder;
            float _SpikesDensity;

            struct Attributes { float4 positionOS : POSITION; float2 uv : TEXCOORD0; };
            struct Varyings { float4 positionHCS : SV_POSITION; float2 uv : TEXCOORD0; };

            #define mParams (min(_ScreenParams.x, _ScreenParams.y))
            #define MParams (max(_ScreenParams.x, _ScreenParams.y))
            

            Varyings vert (Attributes i){
                Varyings o;
                o.positionHCS = TransformObjectToHClip(i.positionOS.xyz);
                o.uv = i.uv;
                return o;
            }
            
            // Hash function to pseudo-randomize
            float hash(float x)
            {
                return frac(sin(x * 12.9898) * 43758.5453);
            }

            // Linear interpolation
            float lerp(float a, float b, float t)
            {
                return a + (b - a) * t;
            }

            // 1D noise
            float noise1D(float x)
            {
                float i = floor(x);
                float f = frac(x);

                // smooth interpolation
                float u = pow(f,_SmoothnessOrder) * (3.0 - 2.0 * f);  

                return lerp(hash(i), hash(i + 1.), u);
            }

            float heightDampenFactor(float distToCenter){
                if (distToCenter < _HoleClipRadius){
                    return 0.;
                }
                return pow(lerp(0.,1., (distToCenter - _HoleClipRadius) / MParams),_ShadowOrder);
            }

            float fracture(float x){
                return abs(1 - abs(2 * floor(x / 2.)  - x));
            }

            float fracturedAngle(float angle){
                return fracture(noise1D(angle / (2*PI) * _SpikesDensity));
            }

            float4 frag(Varyings i) : SV_Target{
                float2 fragCoord = i.positionHCS.xy;
                float2 center = _ScreenParams.xy / 2.;
                float2 toCenter = fragCoord - center;
                float distToCenter = length(toCenter);

                float angle = atan2(toCenter.y,toCenter.x);
                if (angle < 0.0) angle += 2*PI;
                return float4(_BaseColor.rgb * fracturedAngle(angle) *  heightDampenFactor(distToCenter), 1.);

            }
            ENDHLSL
        }
    }
}
