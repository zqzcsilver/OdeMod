sampler SpriteTextureSampler : register(s0);
sampler MappingSpriteTextureSampler : register(s1);

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float4 MainImage(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(SpriteTextureSampler, coords);
    if (!any(color))
        return color;
    
    return tex2D(MappingSpriteTextureSampler, coords);
}
technique SpriteDrawing
{
    pass Pass0
    {
        PixelShader = compile ps_3_0 MainImage();
    }
};