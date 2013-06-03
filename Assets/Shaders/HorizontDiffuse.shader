Shader "Shaders/HorizontDiffuze" {
   Properties {
      _MainTex ("Texture", 2D) = "white" {}
    }
    
    SubShader {
      Tags { "RenderType"="Opaque" "Queue" = "Geometry" }
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
				float _Dist=80;
				float nechuvstv=0.40;
				
			    v2f o;
			    float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
			    
			    float xsmeh=sin(_WorldSpaceCameraPos.z/120);
			 			    
			    float zOff = vPos.z/_Dist;
			    
			    float4	_QOffset=float4(14*xsmeh,-12,0,0);

			    if(zOff<-nechuvstv)
			    {
			    	zOff+=nechuvstv;
			   		vPos += _QOffset*zOff*zOff;
			    }
			    
			    o.pos = mul (UNITY_MATRIX_P, vPos);
			    o.uv = v.texcoord;
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
