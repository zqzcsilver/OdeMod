sampler SpriteTextureSampler : register(s0);
float2 uLightRange;

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float4 MainImage(VertexShaderOutput input) : COLOR
{
    float2 coord = input.TextureCoordinates;
    float4 color = tex2D(SpriteTextureSampler, coord);
    if (!any(color))
        return color;
    float light = color.r * 0.299f + color.g * 0.587f + color.b * 0.114f;
    light *= color.a;
    if (light >= uLightRange.x && light <= uLightRange.y)
        return color;
    else
        return float4(0, 0, 0, 0);
}

technique SpriteDrawing
{
    pass Pass0
    {
        PixelShader = compile ps_3_0 MainImage();
    }
};