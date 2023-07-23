struct VSInput
{
    float3 Position: POSITION;
    float4 Color: COLOR0;
};

struct PSInput
{
    float4 Position: SV_POSITION;
    float4 Color: COLOR0;
};

cbuffer Input
{
float4x4 M_MVP;
float4x4 Padding1;
float4x4 Padding2;
};

PSInput Main(VSInput input)
{
    PSInput output = (PSInput)0;
    output.Position = mul(M_MVP, float4(input.Position, 1.0f));
    output.Color = input.Color;
    return output;
}
