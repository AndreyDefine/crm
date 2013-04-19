Shader "Shaders/Normal Extrusion" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _Amount ("Extrusion Amount", Range(-1,1)) = 0.1
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert vertex:vert
      struct Input {
          float2 uv_MainTex;
      };
      float _Amount;
      void vert (inout appdata_full v) {
      
      float Depth;
		// position is in object space
		// outPosition is in camera space
		//outPosition = mul( worldViewProjMatrix, position );
		// calculate distance from camera to vertex
		//Depth = length( position );
		float3 foo = mul(UNITY_MATRIX_P, v.vertex);
		v.vertex.y+=_Amount*foo.z;
      }
      sampler2D _MainTex;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
      }
      ENDCG
    } 
    Fallback "Diffuse"
 }
 
 
 //float4 position : POSITION,
 //  uniform float4x4 worldViewProjMatrix,
 //  uniform float3 eyePosition,
 //  out float4 outPosition : POSITION,
 //  out float Depth
