sampler SpriteTextureSampler : register(s0);
float2 uScaleFactor;

float gauss[11];

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 BlurV(VertexShaderOutput input) : COLOR
{
    float2 coord = input.TextureCoordinates;
    float4 color = tex2D(SpriteTextureSampler, coord);
    color = float4(0, 0, 0, 0);
    for (int j = -5; j <= 5; j++)
    {
        color += gauss[j + 5] * tex2D(SpriteTextureSampler, coord + float2(0, j * uScaleFactor.y));
    }
    return color;
}
float4 BlurH(VertexShaderOutput input) : COLOR
{
    float2 coord = input.TextureCoordinates;
    float4 color = tex2D(SpriteTextureSampler, coord);
    color = float4(0, 0, 0, 0);
    for (int i = -5; i <= 5; i++)
    {
        color += gauss[i + 5] * tex2D(SpriteTextureSampler, coord + float2(i * uScaleFactor.x, 0));
    }
    return color;
}

technique SpriteDrawing
{
    pass BlurH
    {
        PixelShader = compile ps_3_0 BlurH();
    }
    pass BlurV
    {
        PixelShader = compile ps_3_0 BlurV();
    }
};