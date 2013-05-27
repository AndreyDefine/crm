Shader "Hidden/Render Overdraw" {
SubShader {
	Tags { "RenderType"="Overlay" }
	Pass {
		Fog { Mode Off }
		ZWrite Off ZTest Always Blend One One
		Color (0.1, 0.04, 0.02, 0)		
	}
} 
SubShader {
	Tags { "RenderType"="Overlay" }
	Pass {
		Fog { Mode Off }
		ZWrite Off ZTest Always Cull Off Blend One One
		Color (0.1, 0.04, 0.02, 0)		
	}
} 


}
