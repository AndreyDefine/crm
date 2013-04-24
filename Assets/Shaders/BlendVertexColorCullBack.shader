// unlit, vertex colour, alpha blended
// cull off

Shader "Shaders/BlendVertexColorCullBack" 
{
	Properties 
	{
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}
	
	
	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		//ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha 
		
		Cull Off
		Pass 
		{
			//Lighting Off
		
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
			
			float4 _MainTex_ST;
			
			v2f vert (appdata_base v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MV, v.vertex);
				 //float curpos = length(mul (UNITY_MATRIX_P, v.vertex).xyz); 
		         float pos = length(mul (UNITY_MATRIX_MV, v.vertex).xyz);
			 
			     //curpos=sin(curpos/150);	 
		         pos-=30;
			 
				 if(pos>0)
			  	 {
					pos/=100;
					pos*=pos;
					//o.pos.y -= pos * 9;
					//o.pos.x -= pos * 10*curpos;
			     }
				 o.pos = mul (UNITY_MATRIX_P, o.pos);
				 o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
				 return o;
			}
			
			half4 frag (v2f i) : COLOR
			{
				half4 texcol = tex2D (_MainTex, i.uv);
				return texcol;
			}
			ENDCG
			
		}
		
	}
} 

