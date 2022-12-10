sampler uImage0 : register(s0);
sampler uImage1 : register(s1);


float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 c = tex2D(uImage0, coords);
    float a = max(c.r, max(c.g, c.b));
    if (a > 0.62)//明度超过m的部分被替换为背景图片
    {
        float4 c1 = tex2D(uImage1, coords);
        return c1;
    }
    else if (abs(a - 0.62) < 0.01)//明度与m差值小于n的部分，替换为纯色当作描边
        return float4(0.4, 0.1, 0.7, 1);
    else
        return c * a;
}


technique Technique1 {
	pass Star {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}