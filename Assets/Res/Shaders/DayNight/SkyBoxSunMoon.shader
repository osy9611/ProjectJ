Shader "Custom/SkyBoxSunMoon"
{
    Properties
    {
        _SunRadius("Sun Radius",Range(0,1)) = 0.05
        _SunPower("Sun Power",Range(1,10)) = 5.6

        _MoonRadius("Moon Radius",Range(0,1)) = 0.05
        _MoonPower("Moon Power",Range(0,10)) = 5.6

        _CloudTexture("Cloud Texture",2D) = "black"{}
        _CloudFarTiling("Cloud Far Tiling",Float) = 0
        _CloudTiling("Cloud Tiling",Float) = 0
        _CloudOffset("Cloud Offset",Float) = 0
        _CloudOpacity("CloudOpacity",Float) = 0
        _CloudDistance("Cloud Distance",Float) = 0
        _CloudCutoff("Cloud Cutoff",Float) = 0

        _StarTexture("StarTexture",2D) = "black" {}
        _StarIntensity("Star Intensity",Float) = 0
        _StarOffset("Star Offset",Vector) = (0,0,0,0)
        _StarFadeout("Star Fadeout",Range(0.1,1)) = 0

        _SkyExponent1("Sky Exponent1",Float) = 0
        _SkyExponent2("Sky Exponent2",Float) = 0

        _SkyColor1("Sky Color1",Color) = (1,1,1,1)
        _SkyColor2("Sky Color2",Color) = (1,1,1,1)
        _SkyColor3("Sky Color3",Color) = (1,1,1,1)
        _SkyIntensity("Sky Intensity",Float) = 0
    }
        SubShader
        {
            Tags { "Queue" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox" }
            LOD 100

            Pass
            {
                Name "Universal Forward"
                Tags {"LighMode" = "UniversialForward"}

                HLSLPROGRAM

                #pragma prefer_hlslcc gles
                #pragma exclude_renderers d3d11_9x
                #pragma vertex vert
                #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #pragma multi_compile __ UNITY_COLORSPACE_GAMMA
            //cg shader는 .cginc를 hlsl shader는 .hlsl을 include 한다.
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
            float3 _SunDir, _MoonDir;
            float3 _SunColor ,_MoonColor;
            float3 _SkyColor1,_SkyColor2,_SkyColor3;
            float _SunRadius, _MoonRadius;
            float _SunPower, _MoonPower;
            float _SkyExponent1,_SkyExponent2,_SkyIntensity;
            float _CloudFarTiling, _CloudTiling,_CloudOffset,_CloudOpacity,_CloudDistance,_CloudCutoff;
            float _StarIntensity,_StarFadeout;
            float2  _StarOffset;
            CBUFFER_END

            TEXTURE2D(_CloudTexture);      SAMPLER(sampler_CloudTexture);
            TEXTURE2D(_StarTexture);      SAMPLER(sampler_StarTexture);

            //vertext buffer에서 읽어올 정보를 선언
            struct VertexInput
            {
                float4 vertex : POSITION;
            };
            //보간기를 통해 버텍스 셰이더에서 픽셀 셰이더로 전달할 정보를 선언
            struct VertexOutput
            {
                float4 vertex : SV_POSITION;
                float3 viewDirWS    : TEXCOORD0;
                half3 sunColor : TEXCOORD1;
                half3 moonColor : TEXCOORD2;
                half3 rayDir : TEXCOORD3;
            };

            //Vertex Shader
            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                //Object-> World-> View-> Clip Space로 되어있고 이걸 다시 -1~1 범위인 NDC 좌표계(정규 좌표계)로 변환한다.
                //그 후 Projection Parameter를 연산한다.
                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
                o.vertex = vertexInput.positionCS;
                o.viewDirWS = vertexInput.positionWS;
                half sunLightColorIntensity = clamp(length(_SunColor.xyz), 0.25,1);
                o.sunColor = _SunPower * _SunColor / sunLightColorIntensity;
                half moonLightColorIntensity = clamp(length(_MoonColor.xyz), 0.25,1);
                o.moonColor = _MoonPower * _MoonColor / moonLightColorIntensity;

                float3 eyeRay = normalize(mul((float3x3)unity_ObjectToWorld, v.vertex.xyz));
                o.rayDir = half3(-eyeRay);
                return o;
            }

            float GetMask(float viewDot, float radius)
            {
                float stepRadius = 1 - radius * radius;
                return step(stepRadius, viewDot);
            }

            //광선 방향(보는 방향), 구의 위치(달의 방향) 및 반지름(달 반지름)
            //카메라에서 구체와의 교차점까지의 거리를 출력하고 교차점이 없으면 -1을 반환한다.
            float sphIntersect(float3 rayDir, float3 spherePos, float radius)
            {
                float3 oc = -spherePos;
                float b = dot(oc, rayDir);
                float c = dot(oc, oc) - radius * radius;
                float h = b * b - c;
                if (h < 0.0)
                    return -1.0;
                h = sqrt(h);
                return -b - h;
            }

            float invLerp(float from,float to, float value)
            {
                return (value - from) / (to - from);
            }

            float remap(float value,float inMin,float inMax,float outMin,float outMax)
            {
                float result = invLerp(inMin,inMax,value);
                return lerp(outMin,outMax,result);
            }

            //Pixel Shader
            half4 frag(VertexOutput i) : SV_Target
            {
                float3 viewDir = normalize(i.viewDirWS);
                float2 viewDirXZ = float2(viewDir.x,viewDir.z);

                //Main angles
                float sunViewDot = dot(_SunDir, viewDir);   //태양과 보는 방향 사이의 각도
                float moonViewDot = dot(_MoonDir, viewDir);

                //The Sun 
                float sunMask = 0;
                if (i.rayDir.y < 0.01)
                {
                    sunMask = GetMask(sunViewDot, _SunRadius);
                }
                //The Moon
                float moonIntersect = 0;
                float moonMask = 0;

                if (i.rayDir.y < 0.01)
                {
                    moonIntersect = sphIntersect(viewDir, _MoonDir, _MoonRadius);
                    //moonMask = moonIntersect > -0.99 ? 1 : 0;
                    moonMask = GetMask(moonViewDot, _MoonRadius);
                }
                float3 moonColor = moonMask * i.moonColor;
                float3 sunColor = sunMask * i.sunColor;

                //The Cloud
                float divideCloud = remap(abs(viewDir.y),0,1,_CloudFarTiling,1);
                float2 cloudUV = viewDirXZ / divideCloud * _CloudTiling + _Time.x * _CloudOffset;
                float cloudDistance = pow(clamp(_CloudDistance * viewDir.y,0,1),_CloudCutoff);
                float4 cloudColor = SAMPLE_TEXTURE2D(_CloudTexture,sampler_CloudTexture,cloudUV) * _MainLightColor;
                cloudDistance *= cloudColor.a * _CloudOpacity;

                //The Star
                float2 starUV = ((1 - clamp(viewDir.y,0,1)) * viewDirXZ) + viewDirXZ + _StarOffset;
                float4 starColor = SAMPLE_TEXTURE2D(_StarTexture,sampler_StarTexture,starUV);
                starColor *= _StarIntensity * pow(clamp(viewDir.y,0,1),_StarFadeout);
                starColor *= lerp(1,0,remap(_SunDir,-1,1,0,1));

                //SkyBox Color Up,Down,Middle
                float3 skyUp = 1 - pow(min(1,1 - viewDir.y),_SkyExponent1);
                float3 skyUpColor = skyUp * _SkyColor1;
                float3 skyDown = 1 - pow(min(1,1 + viewDir.y),_SkyExponent2);
                float3 skyDownColor = skyDown * _SkyColor3;
                float3 skyMiddleColor = ((1 - skyUp) - skyDown) * _SkyColor2;
                float3 skyColor = (skyUpColor + skyMiddleColor + skyDownColor) * _SkyIntensity;

                float3 col = sunColor + moonColor + skyColor + starColor;
                col = lerp(col,cloudColor,cloudDistance);


                //float3 col = saturate(float3(step(0.9,dot(_SunDir, viewDir)), step(0.9,dot(_MoonDir, viewDir)), 0)).rgb;
                return half4(col, 1);
                //return col;
            }

            ENDHLSL
        }
        }
}
