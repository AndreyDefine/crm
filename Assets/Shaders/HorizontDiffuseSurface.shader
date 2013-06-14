Shader "Shaders/HorizontDiffuzeSurface" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		//LOD 200

		CGPROGRAM
		#pragma surface surf Lambert noforwardadd
		// vertex:vert

		sampler2D _MainTex;
		
		struct Input {
			float2 uv_MainTex;
		};

		//void vert (inout appdata_full v) {
			//float _Dist=80;
			//float nechuvstv=0.40;
			
		   // float4 vPos = mul (UNITY_MATRIX_MV, v.vertex);
		    
		   // float xsmeh=sin(_WorldSpaceCameraPos.z/120);
		 			    
		   // float zOff = vPos.z/_Dist;
		    
		    //float4	_QOffset=float4(14*xsmeh,-12,0,0);
		    
		   // if(zOff<-nechuvstv)
		   // {
		    //	zOff+=nechuvstv;
		   	//	vPos += _QOffset*zOff*zOff;
		    //}
		    
		    //v.vertex = mul (UNITY_MATRIX_P, vPos);
		//}

		void surf (Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb;
				//o.Alpha = c.a;
			}
		ENDCG
		}

	Fallback Off
//"Mobile/VertexLit"
}
