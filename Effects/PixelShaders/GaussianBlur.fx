sampler SpriteTextureSampler : register(s0);
float2 uScaleFactor;

float gauss[21];

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
    for (int j = -10; j <= 10; j++)
    {
        color += gauss[j + 10] * tex2D(SpriteTextureSampler, coord + float2(0, j * uScaleFactor.y));
    }
    return color;
}
float4 BlurH(VertexShaderOutput input) : COLOR
{
    float2 coord = input.TextureCoordinates;
    float4 color = tex2D(SpriteTextureSampler, coord);
    color = float4(0, 0, 0, 0);
    for (int i = -10; i <= 10; i++)
    {
        color += gauss[i + 10] * tex2D(SpriteTextureSampler, coord + float2(i * uScaleFactor.x, 0));
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