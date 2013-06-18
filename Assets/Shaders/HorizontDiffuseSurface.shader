// Upgrade NOTE: replaced 'PositionFog()' with multiply of UNITY_MATRIX_MVP by position
// Upgrade NOTE: replaced 'V2F_POS_FOG' with 'float4 pos : SV_POSITION'


// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.
Shader "Shaders/HorizontDiffuzeSurface" {


    Properties {


        _DiffuseTex ("Diffuse (RGB)", 2D) = "white" {}


        _NormalTex ("Normal (RGB)", 2D) = "bump" {}


        _SpecularTex ("Specular (R) Gloss (G)", 2D) = "gray" {}


    }


 


    SubShader {


 


        Pass {


            Name "ContentBase"


            Tags {"LightMode" = "ForwardBase"}


            


            CGPROGRAM


                #pragma vertex vert


                #pragma fragment frag


                #pragma multi_compile_fwdbase


                #pragma fragmentoption ARB_precision_hint_fastest


                


                #include "UnityCG.cginc"


                #include "AutoLight.cginc"


                


                struct v2f


                {


                    float4  pos : SV_POSITION;


                    float2  uv : TEXCOORD0;


                    float3  lightDirT : TEXCOORD1;


                    float3  viewDirT : TEXCOORD2;


                    LIGHTING_COORDS(3,4)


                }; 


 


                v2f vert (appdata_tan v)


                {


                    v2f o;


                    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);


                    o.uv = v.texcoord.xy;


                    TANGENT_SPACE_ROTATION;


                    o.lightDirT = mul(rotation, ObjSpaceLightDir(v.vertex));


                    o.viewDirT = mul(rotation, ObjSpaceViewDir(v.vertex));


                    TRANSFER_VERTEX_TO_FRAGMENT(o);


                    return o;


                }


                


                sampler2D _DiffuseTex;


                sampler2D _SpecularTex;


                sampler2D _NormalTex;


                


                float4 _LightColor0;


 


                float4 frag(v2f i) : COLOR


                {


                    float3 normal = normalize(tex2D(_NormalTex, i.uv).xyz * 2 - 1);


                    float NdotL = dot(normal, i.lightDirT);


                    float3 halfAngle = normalize(i.lightDirT + i.viewDirT);


                    float atten = LIGHT_ATTENUATION(i);


                    float3 specularity = (pow(saturate(dot(normal, halfAngle)), tex2D(_SpecularTex, i.uv).g * 200) * tex2D(_SpecularTex, i.uv).r)  * _LightColor0;


                    


                    float4 result;


                    result.rgb = tex2D(_DiffuseTex, i.uv).rgb * NdotL * atten * _LightColor0 + specularity;


                    result.a = 0;


                    return result;


                }


            ENDCG


        }


 


        Pass {


            Name "ContentAdd"


            Tags {"LightMode" = "ForwardAdd"}


            Blend One One


            


            CGPROGRAM


                #pragma vertex vert


                #pragma fragment frag


                #pragma multi_compile_fwdadd


                #pragma fragmentoption ARB_precision_hint_fastest


                


                #include "UnityCG.cginc"


                #include "AutoLight.cginc"


                


                struct v2f


                {


                    float4  pos : SV_POSITION;


                    float2  uv : TEXCOORD0;


                    float3  lightDirT : TEXCOORD1;


                    float3  viewDirT : TEXCOORD2;


                    LIGHTING_COORDS(3,4)


                }; 


 


                v2f vert (appdata_tan v)


                {


                    v2f o;


                    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);


                    o.uv = v.texcoord.xy;


                    TANGENT_SPACE_ROTATION;


                    o.lightDirT = mul(rotation, ObjSpaceLightDir(v.vertex));


                    o.viewDirT = mul(rotation, ObjSpaceViewDir(v.vertex));


                    TRANSFER_VERTEX_TO_FRAGMENT(o);


                    return o;


                }


                


                sampler2D _DiffuseTex;


                sampler2D _SpecularTex;


                sampler2D _NormalTex;


                


                float4 _LightColor0;


 


                float4 frag(v2f i) : COLOR


                {


                    float3 normal = normalize(tex2D(_NormalTex, i.uv).xyz * 2 - 1);


                    float NdotL = dot(normal, i.lightDirT);


                    float3 halfAngle = normalize(i.lightDirT + i.viewDirT);


                    float atten = LIGHT_ATTENUATION(i);


                    float3 specularity = (pow(saturate(dot(normal, halfAngle)), tex2D(_SpecularTex, i.uv).g * 200) * tex2D(_SpecularTex, i.uv).r)  * _LightColor0;


                    


                    float4 result;


                    result.rgb = tex2D(_DiffuseTex, i.uv).rgb * NdotL * atten * _LightColor0 + specularity;


                    result.a = 0;


                    return result;


                }


            ENDCG


        }


    }


}