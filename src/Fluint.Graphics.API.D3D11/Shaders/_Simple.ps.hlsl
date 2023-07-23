struct PSInput
{
    float4 Position: SV_POSITION;
    float4 Color: COLOR0;
};

float4 Main(PSInput input) : SV_TARGET
{
    float4 test = float4(0.0, 1.0, 0.0, 1.0);
    float4 output = float4(0.0, 0.0, 0.0, 1.0);
#if TEST
	output = test;
#endif
    return output + input.Color;
}
