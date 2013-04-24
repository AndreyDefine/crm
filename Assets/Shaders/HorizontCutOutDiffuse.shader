Shader "Shaders/HorizontCutOutDiffuse" {
Properties {
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}
 
SubShader {
    Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
    LOD 200
    Cull Off
 
CGPROGRAM
#pragma surface surf Lambert alphatest:_Cutoff vertex:vert
 
sampler2D _MainTex;
 
struct Input {
    float2 uv_MainTex;
};
 
      void vert (inout appdata_full v) {
		//float curpos = length(mul (UNITY_MATRIX_P, v.vertex).xyz); 
		float pos = length(mul (UNITY_MATRIX_MV, v.vertex).xyz);
		
		//curpos=sin(curpos/150);	 
		pos-=30;
		
		if(pos>0)
		{
			pos/=100;
			pos*=pos;
			//v.vertex.y -= pos * 9;
			//v.vertex.x -= pos * 10*curpos;
		}
          
      }

void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
    o.Albedo = c.rgb;
    o.Alpha = c.a;
}
ENDCG
}
}