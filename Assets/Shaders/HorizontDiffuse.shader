Shader "Shaders/HorizontDiffuze" {
   Properties {
      _MainTex ("Texture", 2D) = "white" {}
    }
    
    SubShader {
      Tags { "RenderType" = "Transparent" }
      CGPROGRAM
      #pragma surface surf Lambert vertex:vert
      struct Input {
           float2 uv_MainTex;
      };
      
void vert (inout appdata_full v) {
float curpos = mul (_Object2World, v.vertex).z;//length(mul (_Object2World, v.vertex).xyz); 
float pos = length(mul (UNITY_MATRIX_MV, v.vertex).xyz);

curpos=sin(curpos/50);	 
pos-=30;

if(pos>0)
{
	pos/=100;
	pos*=pos;
	v.vertex.y -= pos * 9;
	//v.vertex.x += pos * 10*curpos;
}
  
}
      
      sampler2D _MainTex;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
      }
      ENDCG
    } 

  }