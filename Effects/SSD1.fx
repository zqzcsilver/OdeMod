sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    if (!any(color))
        return color;
    float2 pos = float2(0.5, 0.5);
    float2 offset = (coords - pos);
    float2 rpos = offset * float2(uScreenResolution.x / uScreenResolution.y, 1);
    float dis = length(rpos);
    float r = 3.14159;
    float2 target = mul(offset, float2x2(cos(r), -sin(r), sin(r), cos(r)));
    return tex2D(uImage0, pos + target);
}

technique Technique1 {
	pass Rotate {
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}