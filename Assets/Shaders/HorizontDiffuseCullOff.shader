Shader "Shaders/HorizontDiffuzeCullOff" {
   Properties {
      _MainTex ("Texture", 2D) = "white" {}
    }
    
    SubShader {
      Tags { "RenderType" = "Transparent" }
      Cull Off
      CGPROGRAM
      #pragma surface surf Lambert vertex:vert
      struct Input {
           float2 uv_MainTex;
      };
      void vert (inout appdata_full v) {
		
	  	 float pos = length(mul (UNITY_MATRIX_MV, v.vertex).xyz);
	  	 float camerapos = length(mul (UNITY_MATRIX_MV, float4(0,0,0,0)).xyz);
	  	 
	  	pos-=camerapos+30;
	  	 
	  	 if(pos>0)
	  	 {
	  	 	pos/=80;
	     	//v.vertex.y -= pos*pos * 10;
	     	//v.vertex.x -=pos*pos * 10;
	     }
          
      }
      sampler2D _MainTex;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
      }
      ENDCG
    } 

  }