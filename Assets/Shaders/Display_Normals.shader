// unlit, vertex colour, alpha blended
// cull off

Shader "Shaders/Display Normals" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,0.5)
    _MainTex ("Texture", 2D) = "white" { }
}
SubShader {
    Pass {

CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

float4 _Color;
sampler2D _MainTex;

struct v2f {
    float4  pos : SV_POSITION;
    float2  uv : TEXCOORD0;
};

//struct appdata {
//    float4 vertex : POSITION;
//    float3 normal : NORMAL;
//    float4 tangent : TANGENT;
//};

float4 _MainTex_ST;

v2f vert (appdata_base v)
{
    v2f o;
    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
    o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
    //v.POSITION.x=v.POSITION.x+1;
    return o;
}

half4 frag (v2f i) : COLOR
{
    half4 texcol = tex2D (_MainTex, i.uv);
    return texcol * _Color;
}
ENDCG

    }
}
Fallback "VertexLit"
} 