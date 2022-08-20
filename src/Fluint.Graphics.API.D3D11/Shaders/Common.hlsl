#include "Common.vs.hlsl"
#include "Common.ps.hlsl"
#include "Common.Buffer.hlsl"

#define PI 3.1415926535897932384626433832795
#define INV_PI 1.0 / PI;
#define EPSILON 0.00000001

struct Material
{
	float3 Albedo;
	float Roughness;
	float Metallic;
	float3 Padding;
	float Emissive;
	float3 F0;
	float RoughnessAlpha;
};

struct Light
{
	float3 Color;
	float Intensity;
	float3 Direction;
	float Padding;
};

/*------------------------------------------------------------------------------
							[GAMMA CORRECTION]
------------------------------------------------------------------------------*/
float4 Degamma(float4 color) 
{ 
	return pow(abs(color), G_Gamma); 
}

float3 Degamma(float3 color) 
{ 
	return pow(abs(color), G_Gamma);
}

float4 Gamma(float4 color) 
{ 
	return pow(abs(color), 1.0f / G_Gamma);
}

float3 Gamma(float3 color) 
{ 
	return pow(abs(color), 1.0f / G_Gamma);
}

float2 Project(float4 value) 
{ 
	return (value.xy / value.w) * float2(0.5f, -0.5f) + 0.5f; 
}

float2 Project(float3 position, matrix transform)
{
	float4 projectedCoords = mul(float4(position, 1.0f), transform);
	projectedCoords.xy /= projectedCoords.w;
	projectedCoords.xy = projectedCoords.xy * float2(0.5f, -0.5f) + 0.5f;

	return projectedCoords.xy;
}

/*------------------------------------------------------------------------------
								[PACKING]
------------------------------------------------------------------------------*/
float3 Unpack(float3 value) 
{ 
	return value * 2.0f - 1.0f; 
}

float3 Pack(float3 value) 
{ 
	return value * 0.5f + 0.5f; 
}

float2 Unpack(float2 value) 
{ 
	return value * 2.0f - 1.0f; 
}

float2 Pack(float2 value) 
{ 
	return value * 0.5f + 0.5f; 
}

/*------------------------------------------------------------------------------
								[NORMALS]
------------------------------------------------------------------------------*/
float3x3 MakeTBN(float3 n, float3 t)
{
	// re-orthogonalize T with respect to N
	t = normalize(t - dot(t, n) * n);
	// compute bitangent
	float3 b = cross(n, t);
	// create matrix
	return float3x3(t, b, n);
}

// No decoding required
float3 NormalDecode(float3 normal) 
{ 
	return normalize(normal); 
}

// No encoding required
float3 NormalEncode(float3 normal) 
{ 
	return normalize(normal); 
}

/*------------------------------------------------------------------------------
							[DEPTH/POS]
------------------------------------------------------------------------------*/
float GetDepth(Texture2D tex_depth, SamplerState sampler_linear, float2 tex_coord)
{
	return tex_depth.SampleLevel(sampler_linear, tex_coord, 0).r;
}

float GetLinearDepth(float z)
{
	float z_b = z;
	float z_n = 2.0f * z_b - 1.0f;
	return 2.0f * G_CameraFar * G_CameraNear / (G_CameraNear + G_CameraFar - z_n * (G_CameraNear - G_CameraFar));
}

float GetLinearDepth(Texture2D tex_depth, SamplerState sampler_linear, float2 tex_coord)
{
	float depth = GetDepth(tex_depth, sampler_linear, tex_coord);
	return GetLinearDepth(depth);
}

float3 GetWorldPositionFromDepth(float z, matrix viewProjectionInverse, float2 tex_coord)
{
	float x = tex_coord.x * 2.0f - 1.0f;
	float y = (1.0f - tex_coord.y) * 2.0f - 1.0f;
	float4 pos_clip = float4(x, y, z, 1.0f);
	float4 pos_world = mul(pos_clip, viewProjectionInverse);
	return pos_world.xyz / pos_world.w;
}

float3 GetWorldPositionFromDepth(Texture2D tex_depth, SamplerState sampler_linear, float2 tex_coord)
{
	float depth = GetDepth(tex_depth, sampler_linear, tex_coord);
	return GetWorldPositionFromDepth(depth, G_ViewProjectionInverted, tex_coord);
}

float3 GetWorldPositionFromDepth(float depth, float2 tex_coord)
{
	return GetWorldPositionFromDepth(depth, G_ViewProjectionInverted, tex_coord);
}

/*------------------------------------------------------------------------------
								[LUMINANCE]
------------------------------------------------------------------------------*/
static const float3 lumCoeff = float3(0.299f, 0.587f, 0.114f);

float Luminance(float3 color)
{
	return max(dot(color, lumCoeff), 0.0001f);
}

float Luminance(float4 color)
{
	return max(dot(color.rgb, lumCoeff), 0.0001f);
}

/*------------------------------------------------------------------------------
								[SKY SPHERE]
------------------------------------------------------------------------------*/
float2 DirectionToSphereUV(float3 direction)
{
	float n = length(direction.xz);
	float2 uv = float2((n > 0.0000001) ? direction.x / n : 0.0, direction.y);
	uv = acos(uv) * INV_PI;
	uv.x = (direction.z > 0.0) ? uv.x * 0.5 : 1.0 - (uv.x * 0.5);
	uv.x = 1.0 - uv.x;

	return uv;
}

/*------------------------------------------------------------------------------
								[RANDOM]
------------------------------------------------------------------------------*/
float Randomize(float2 uv)
{
	return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
}

/*------------------------------------------------------------------------------
								[MISC]
------------------------------------------------------------------------------*/
// The Technical Art of Uncharted 4 - http://advances.realtimerendering.com/other/2016/naughty_dog/index.html
float MicroShadow(float ao, float3 N, float3 L, float shadow)
{
	float aperture = 2.0f * ao * ao;
	float microShadow = saturate(abs(dot(L, N)) + aperture - 1.0f);
	return shadow * microShadow;
}

bool IsSaturated(float value) 
{ 
	return value == saturate(value); 
}

bool IsSaturated(float2 value) 
{ 
	return IsSaturated(value.x) && IsSaturated(value.y); 
}