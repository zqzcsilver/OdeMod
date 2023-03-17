Texture2D SpriteTexture;
float uAlpha;
float uMaxDistance;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 coord = input.TextureCoordinates;
    float2 center = float2(0.5, 0.5);
	
    return tex2D(SpriteTextureSampler, coord) * min(1, (pow(max(0, uMaxDistance - distance(center, coord)), 2)) * uAlpha);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile ps_3_0 MainPS();
	}
};