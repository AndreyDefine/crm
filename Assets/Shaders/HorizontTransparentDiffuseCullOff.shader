Shader "Shaders/HorizontTransparentDiffuseCullOff" {
Properties 
{
    _MainTex ("Texture", 2D) = "white" {}
}
SubShader 
{
    Cull Off
    //Blend SrcAlpha Zero 
    Blend SrcAlpha OneMinusSrcAlpha 
    //Alphatest Greater [_Cutoff]
    //AlphaToMask True
    //ColorMask RGB
    
    Tags 
    {
        "Queue" = "Transparent" 
        "IgnoreProjector" = "True" 
        "RenderType" = "TransparentCutoff"
    }
    
    //Pass{
    //	SetTexture [_MainTex] 
    //    {
    //        Combine texture, texture
    //   }
    //}
    
    Pass{
    //Blend SrcAlpha OneMinusSrcAlpha 
      
    	CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest 

            sampler2D _MainTex;
            float _Cutoff;
			
			struct v2f {
			    float4 pos : SV_POSITION;
			    float4 uv : TEXCOORD0;
			};

			v2f vert (appdata_full v)
			{
				float _Dist=90;
				float4	_QOffset=float4(4,-8,0,0);
				
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
}