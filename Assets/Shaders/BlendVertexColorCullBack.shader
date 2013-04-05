// unlit, vertex colour, alpha blended
// cull off

Shader "Shaders/BlendVertexColorCullBack" 
{
	Properties 
	{
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}

	SubShader 
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True"  "RenderType"="Transparent"}
		LOD 100
		
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off

		BindChannels 
		{
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
			Bind "Color", color
		}
		
		Pass {
            Cull Front
            SetTexture [_MainTex]  { combine texture * primary } 
        }
        // Render the parts of the object facing us.
        // If the object is convex, these will be closer than the
        // back-faces.
        Pass {
            Cull Back
            SetTexture [_MainTex]  { combine texture * primary } 
        }

		//Pass 
		//{
		//	//Lighting Off
		//	SetTexture [_MainTex] { combine texture * primary } 
		//}
	}
}
