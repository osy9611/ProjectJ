Shader "Study/CustomCartoon2"
{
    Properties
    {
        [MainTexture] _BaseMap("Base Map",2D) = "white" {}
        [MainColor][HDR] _BaseColor("Base Color",Color) = (1,1,1,1)

        [KeywordEnum(Hair, Face, Skin)] _Kind("Kine Type", Float) = 0

        _LightSmooth("Light Smooth", Float) = 0.1
        _ShadowColor("ShadowColor",Color) = (1,1,1,1)

        _HairMaskTexture("Hair Mask Texture",2D) = "white" {}
        _HairFresnelPow("Hair Fresnel Power",Float) = 0
        _HairFresnelIntensity("Hair Fresnel Intensity",Float) = 0

        [NoScaleOffset]_Ramp("Ramp",2D) = "white"{}

        _HeadForward("Head Forward",Vector) = (0,1,0,1)
        _HeadRight("Head Right",Vector) = (-1,0,0,1)
        _ShadowTex("Shadow Tex",2D) = "white" {}

        _MatalicNormalTex("Matalic Normal Texture",2D) = "white" {}
        _GradientTex("Gradient Texture",2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Name "Universal Forward"
            Tags {"LightMode" = "UniversalForward"}

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            #pragma multi_compile_instancing
            #pragma shader_feature _ _KIND_SKIN _KIND_HAIR _KIND_FACE

            //Receiving Shadow Options
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT

            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                
            };

            struct VertexOutput
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD1;
                //Shadow
                float3 shadowCoord : TEXCOORD2;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            TEXTURE2D(_HairMaskTexture);
            SAMPLER(sampler_HairMaskTexture);

            TEXTURE2D(_Ramp);
            SAMPLER(sampler_Ramp);

            TEXTURE2D(_ShadowTex);
            SAMPLER(sampler_ShadowTex);

            TEXTURE2D(_GradientTex);
            SAMPLER(sampler_GradientTex);

            TEXTURE2D(_MatalicNormalTex);
            SAMPLER(sampler_MatalicNormalTex);

            CBUFFER_START(UnityPerMaterial) 
                float4 _BaseMap_ST;
                half4 _BaseColor;
                float _LightSmooth;
                float4 _ShadowColor;
                float _HairFresnelPow;
                float _HairFresnelIntensity;
                float4 _HeadForward;
                float4 _HeadRight;
            CBUFFER_END

            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz); 
                o.uv = TRANSFORM_TEX(v.uv, _BaseMap);
                o.normal = TransformObjectToWorldNormal(v.normal);
                o.viewDir = normalize(_WorldSpaceCameraPos.xyz - TransformObjectToWorld(v.vertex.xyz));
               
               
                return o;
            }

            half4 frag(VertexOutput i) : SV_Target
            {
                float4 mainTexture = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv);
                
                Light light = GetMainLight();

                float3 lightDir = normalize(light.direction);
                float3 normal = normalize(i.normal);
                float3 viewDir = normalize(i.viewDir);

                float NdotL = saturate(dot(normal, lightDir));
                float smooth = smoothstep(0,_LightSmooth,NdotL);
                float3 lerpResult = lerp(_ShadowColor,_BaseColor,smooth);

                half4 color = 1;

                float Kd = saturate(dot(normal,lightDir));
                Kd = Kd * 0.5 + 0.49;

                #if defined(_KIND_HAIR)
                    float hairFresnel = pow((1.0-saturate(dot(normal,viewDir))),_HairFresnelPow) * _HairFresnelIntensity;
                    float3 hairTexture =  SAMPLE_TEXTURE2D(_HairMaskTexture, sampler_HairMaskTexture,i.uv).rgb;
                    float anisoHair = saturate(1-hairFresnel) * hairTexture.r * NdotL;
 
                    color.rgb *=(hairTexture * anisoHair) + (lerpResult * mainTexture.rgb );
                #elif defined(_KIND_FACE)
                    float3 shadowTexture = SAMPLE_TEXTURE2D(_ShadowTex,sampler_ShadowTex,i.uv).rgb;
                    float3 shadowTexture2 = SAMPLE_TEXTURE2D(_ShadowTex,sampler_ShadowTex,float2(1-i.uv.x,i.uv.y)).rgb;
                    
                    float dotF = dot(_HeadForward.xyz,lightDir.xyz);
                    float dotR = dot(_HeadRight.xyz,lightDir.xyz);

                    float dotFStep = step(dotF,0);
                    float dotRAcos = (acos(dotR)/PI) * 2;
                    float dotRAcosDir = (dotR> 0)? 1 - dotRAcos : dotRAcos - 1;
                    //float3 texShadowDir = (dotR > 0)? shadowTexture : shadowTexture2;
                    float3 texShadowDir = (dotR > 0)? shadowTexture.r : shadowTexture.g;
                    float3 shadowDir = step(dotRAcosDir,texShadowDir) * dotFStep;

                    color.rgb *= lerp(mainTexture.rgb *  _BaseColor ,mainTexture.rgb * _BaseColor * _ShadowColor,1-shadowDir) * light.color * light.shadowAttenuation;
                #else
                    color.rgb *= lerpResult * mainTexture.rgb;
                #endif

                //Metalic
                //float2 metalicUV = dot(normal, normalize(viewDir+lightDir));  //UV ï¿?                //float3 metalicTex = SAMPLE_TEXTURE2D(_MatalicNormalTex,sampler_MatalicNormalTex,i.uv).rgb;
                //float3 normalBlend =  normalize(float3(normal.rg+metalicTex.rg,normal.b*metalicTex.b));
                //float2 metalicUV = dot(normalBlend,normalize(viewDir+lightDir));
                //float3 metalicColor = SAMPLE_TEXTURE2D(_GradientTex,sampler_GradientTex,metalicUV);
                //color.rgb += metalicColor * _BaseColor * light.color;

                //Final Caculate
                
                
                return color;
            }
            ENDHLSL
        }

        Pass
        {
            Name "Outline"
            Cull Front

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float _Outline;
                float _OutlineFactor;
                float4 _OutlineColor;
            CBUFFER_END

             struct VertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;
            };

              struct VertexOutput
            {
                float4 vertex : SV_POSITION;
            };

            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                float3 pos = normalize(v.vertex.xyz);
                float3 normal = normalize(v.normal);

                float D = dot(pos,normal);
                pos *= sign(D);
                pos = lerp(normal,pos, _OutlineFactor);
                v.vertex.xyz += pos * _Outline;
                o.vertex = TransformObjectToHClip(v.vertex);
                //o.vertex = TransformObjectToHClip(v.vertex.xyz + v.normal * _OutlineFactor)
                return o;
            }

            half4 frag(VertexOutput i) : SV_Target
            {
                half4 color = 1;

                return color;
            }
            ENDHLSL
        }
    }
}
