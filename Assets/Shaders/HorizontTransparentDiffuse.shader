Shader "Shaders/HorizontTransparentDiffuse" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 200

CGPROGRAM
#pragma surface surf Lambert alpha vertex:vert

sampler2D _MainTex;

struct Input {
	float2 uv_MainTex;
};

void vert (inout appdata_full v) {
float pos = length(mul (UNITY_MATRIX_MV, v.vertex).xyz);

float curpos = length(mul (UNITY_MATRIX_MVP, v.vertex).xyz);
curpos=sin(curpos/50); 
pos-=30;

if(pos>0)
{
	pos/=100;
	pos*=pos;
	v.vertex.y -= pos * 10;
	//v.vertex.x += pos * 10*curpos;
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
