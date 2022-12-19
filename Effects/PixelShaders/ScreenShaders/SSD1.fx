sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
sampler uImage4 : register(s4);
sampler uImage5 : register(s5);
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

float uAlpha;
float uMaxDistance;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float4 color4 = tex2D(uImage4, coords);
    float4 color5 = tex2D(uImage5, coords);
    float2 center = float2(0.5, 0.5);
    float factor = min(1, pow(max(0, uMaxDistance - abs(length(center - coords))), 2) );
    
    return color + (color4 + color5) * (1 - factor) * uAlpha;
}

technique Technique1 {
	pass Rotate {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}