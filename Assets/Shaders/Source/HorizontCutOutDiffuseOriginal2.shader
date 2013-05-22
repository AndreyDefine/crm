Shader "Shaders/Source/HorizontCutOutDiffuseOriginal2" {
Properties 
{
    _MainTex ("Texture", 2D) = "white" {}
    _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}
SubShader 
{
    Cull Off
    //Blend SrcAlpha Zero 
    //Blend SrcAlpha OneMinusSrcAlpha 
    
    Tags 
    {
        "Queue" = "Transparent" 
        "IgnoreProjector" = "True" 
        "RenderType" = "TransparentCutoff"
    }
    
    Pass{
    Alphatest Greater [_Cutoff]
    AlphaToMask True
    ColorMask RGB
    	SetTexture [_MainTex] 
        {
           Combine texture, texture
      }
    }
}
}