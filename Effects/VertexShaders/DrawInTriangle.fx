matrix uWorldViewProjection;
Texture2D SpriteTexture;
sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};
float2 uScaleFactor;
float2 uPositionFactor;

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(in VertexShaderOutput input) : COLOR
{
    float2 coord = input.TextureCoordinates;
    coord.x *= uScaleFactor.x;
    coord.y *= uScaleFactor.y;
    coord.x += uPositionFactor.x;
    coord.y += uPositionFactor.y;
    
    if (coord.x > 1 || coord.y > 1)
        return float4(0, 0, 0, 0);
    
    return tex2D(SpriteTextureSampler, coord);
}

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    VertexShaderOutput output;

    output.Position = mul(input.Position, uWorldViewProjection);
    output.Color = input.Color;
    output.TextureCoordinates = input.TextureCoordinates;
    return output;
}

technique BasicColorDrawing
{
    pass P0
    {
        VertexShader = compile vs_3_0 MainVS();
        PixelShader = compile ps_3_0 MainPS();
    }
};