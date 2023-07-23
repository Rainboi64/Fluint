struct PixelPosition
{
    float4 Position: SV_POSITION;
};

struct PixelPositionTexture
{
    float4 Position: SV_POSITION;
    float2 Uv: TEXCOORD;
};

struct PixelPositionColor
{
    float4 Position: SV_POSITION;
    float2 Color;
};
