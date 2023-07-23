#include "Common.hlsl"

PixelPositionTexture Main(VertexPositionTexture input)
{
    PixelPositionTexture output;

    input.Position.w = 1.0f;
    output.Position = mul(input.Position, G_MVP);
    output.Uv = input.Uv;

    return output;
}
