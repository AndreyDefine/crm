Shader "Shaders/HorizontCutOutDiffuse" {
Properties 
{
    _MainTex ("Texture", 2D) = "white" {}
    _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}
SubShader 
{
    Cull Off    
    Tags 
    {
        "Queue" = "Transparent" 
        "IgnoreProjector" = "True" 
        "RenderType" = "TransparentCutoff"
    }
    
    CGINCLUDE

		#include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Cutoff;
			
			struct v2f {
			    float4 pos : SV_POSITION;
			    float4 uv : TEXCOORD0;
			};

			v2f vert (appdata_full v)
			{
				float _Dist=100;
				float4	_QOffset=float4(4,-10,0,0);
				
			    v2f o;
			    float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
			    float zOff = vPos.z/_Dist;
			    /////vPos += _QOffset*zOff*zOff;
			    
			    o.pos = mul (UNITY_MATRIX_P, vPos);
			    o.uv = mul( UNITY_MATRIX_TEXTURE0, v.texcoord );
			    return o;
			}
			
			half4 frag (v2f i) : COLOR
			{
			    half4 col = tex2D(_MainTex, i.uv.xy);
			    if(col.a<_Cutoff)discard;
			    return col;
			}
			ENDCG
    
    Pass {
	
		CGPROGRAM
		
		#pragma vertex vert
		#pragma fragment frag
		//#pragma fragmentoption ARB_precision_hint_fastest 
		
		ENDCG
		 
		}
				
	} 
}