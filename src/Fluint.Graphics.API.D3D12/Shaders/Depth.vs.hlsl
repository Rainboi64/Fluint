#include "Common.hlsl"

cbuffer Input
{
matrix M_MVP;
};

PixelPosition Main(VertexPosition input)
{
    PixelPosition output;

    input.Position.w = 1.0f;
    output.Position = mul(input.Position, M_MVP);

    return output;
}
