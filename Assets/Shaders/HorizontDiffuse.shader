Shader "Shaders/HorizontDiffuze" {
   Properties {
      _MainTex ("Texture", 2D) = "white" {}
    }
    
    SubShader {
      Tags { "RenderType"="Opaque" }
     Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			//#pragma fragmentoption ARB_precision_hint_fastest 

            sampler2D _MainTex;
			
			struct v2f {
			    float4 pos : SV_POSITION;
			    float4 uv : TEXCOORD0;
			};

			v2f vert (appdata_full v)
			{
				float _Dist=100;			
			    v2f o;
			    float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
			    float zOff = vPos.z/_Dist;
			    
			    vPos += float4(4,-12,0,0)*zOff*zOff;
			    
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
}
