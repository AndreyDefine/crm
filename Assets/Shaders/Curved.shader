Shader "Shaders/Curved" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass
		{
			CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it does not contain a surface program or both vertex and fragment programs.
            #pragma exclude_renderers gles
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

            sampler2D _MainTex;
			
			struct v2f {
			    float4 pos : SV_POSITION;
			    float4 uv : TEXCOORD0;
			};

			v2f vert (appdata_full v)
			{
				float _Dist=100;
				float4	_QOffset=float4(5,5,0,0);
				
			    v2f o;
			    float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
			    float zOff = vPos.z/_Dist;
			    vPos += _QOffset*zOff*zOff;
			    
			    o.pos = mul (UNITY_MATRIX_P, vPos);
			    o.uv = mul( UNITY_MATRIX_TEXTURE0, v.texcoord );
			    return o;
			}
			
			half4 frag (v2f i) : COLOR
			{
			    half4 col = tex2D(_MainTex, i.uv.xy);
			    return col;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
