Texture2D SpriteTexture;
sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

Texture2D MappingSpriteTexture;
sampler2D MappingSpriteTextureSampler = sampler_state
{
    Texture = <MappingSpriteTexture>;
};

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
        PixelShader = compile ps_2_0 MainImage();
    }
};