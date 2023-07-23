struct VertexPosition
{
    float4 Position: POSITION0;
};

struct VertexPositionTexture
{
    float4 Position: POSITION0;
    float2 Uv: TEXCOORD0;
};

struct VertexPositionColor
{
    float4 Position: POSITION0;
    float4 Color: COLOR0;
};

struct VertexPositionTextureNormalTangent
{
    float4 Position : POSITION0;
    float2 Uv: TEXCOORD0;
    float3 Normal: NORMAL0;
    float3 Tangent: TANGENT0;
};
