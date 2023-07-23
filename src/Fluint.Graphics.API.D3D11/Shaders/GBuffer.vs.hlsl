#include "Common.hlsl"

struct PixelInputType
{
    float4 PositionCS : SV_POSITION;
    float2 Uv : TEXCOORD;
    float3 Normal : NORMAL;
    float3 Tangent : TANGENT;
    float4 PositionVS : POSITIONT0;
    float4 PositionWS : POSITIONT1;
    float4 PositionCS_Current : SCREEN_POS;
    float4 PositionCS_Previous : SCREEN_POS_PREVIOUS;
};

cbuffer ObjectBuffer : register(b2)
{
matrix mModel;
matrix mMVP_current;
matrix mMVP_previous;
};

PixelInputType Main(VertexPositionTextureNormalTangent input)
{
    PixelInputType output;

    input.Position.w = 1.0f;
    output.PositionWS = mul(input.Position, mModel);
    output.PositionVS = mul(output.PositionWS, G_View);
    output.PositionCS = mul(output.PositionVS, G_Projection);
    output.PositionCS_Current = mul(input.Position, mMVP_current);
    output.PositionCS_Previous = mul(input.Position, mMVP_previous);
    output.Normal = normalize(mul(input.Normal, (float3x3)mModel)).xyz;
    output.Tangent = normalize(mul(input.Tangent, (float3x3)mModel)).xyz;
    output.Uv = input.Uv;

    return output;
}
