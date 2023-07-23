#include "Common.hlsl"
#include "Include.ParallaxMapping.hlsl"
//=============================

//= TEXTURES ===========================
Texture2D T_Albedo: register (t0);
Texture2D T_Roughness: register (t1);
Texture2D T_Metallic: register (t2);
Texture2D T_Normal: register (t3);
Texture2D T_Height: register (t4);
Texture2D T_Occlusion: register (t5);
Texture2D T_Emission: register (t6);
Texture2D T_Mask: register (t7);
//======================================

//= SAMPLERS =============================
SamplerState samplerAniso : register (s0);
//========================================

cbuffer MaterialBuffer : register(b1)
{
float4 materialAlbedoColor;
float2 materialTiling;
float2 materialOffset;
float materialRoughness;
float materialMetallic;
float materialNormalStrength;
float materialHeight;
float materialShadingMode;
float3 padding2;
};

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

struct PixelOutputType
{
	float4 Albedo : SV_Target0;
	float4 Normal : SV_Target1;
	float4 Material : SV_Target2;
	float2 Velocity : SV_Target3;
};

PixelOutputType Main(PixelInputType input)
{
	PixelOutputType gBuffer;

	float2 texCoords = float2(input.Uv.x * materialTiling.x + materialOffset.x,
	                          input.Uv.y * materialTiling.y + materialOffset.y);
	float4 albedo = materialAlbedoColor;
	float roughness = materialRoughness;
	float metallic = saturate(materialMetallic);
	float3 normal = input.Normal.xyz;
	float normal_intensity = clamp(materialNormalStrength, 0.012f, materialNormalStrength);
	float emission = 0.0f;
	float occlusion = 1.0f;

	//= VELOCITY ==============================================================================
	float2 position_current = (input.PositionCS_Current.xy / input.PositionCS_Current.w);
	float2 position_previous = (input.PositionCS_Previous.xy / input.PositionCS_Previous.w);
	float2 position_delta = position_current - position_previous;
	float2 velocity = (position_delta - G_TaaJitterOffset) * float2(0.5f, -0.5f);
	//=========================================================================================

	// Make TBN
	float3x3 TBN = MakeTBN(input.Normal, input.Tangent);

#if HEIGHT_MAP
	// Parallax Mapping
	float height_scale = materialHeight * 0.04f;
	float3 camera_to_pixel = normalize(g_camera_position - input.PositionWS.xyz);
	texCoords = ParallaxMapping(texHeight, samplerAniso, texCoords, camera_to_pixel, TBN, height_scale);
#endif

#if MASK_MAP
	float3 maskSample = T_Mask.Sample(samplerAniso, texCoords).rgb;
	float threshold = 0.6f;
	if (maskSample.r <= threshold && maskSample.g <= threshold && maskSample.b <= threshold)
	{
		discard;
	}
#endif

#if ALBEDO_MAP
	albedo *= T_Albedo.Sample(samplerAniso, texCoords);
#endif

#if ROUGHNESS_MAP
	if (materialRoughness >= 0.0f)
	{
		roughness *= T_Roughness.Sample(samplerAniso, texCoords).r;
	}
	else
	{
		roughness *= 1.0f - T_Roughness.Sample(samplerAniso, texCoords).r;
	}
#endif

#if METALLIC_MAP
	metallic *= T_Metallic.Sample(samplerAniso, texCoords).r;
#endif

#if NORMAL_MAP
	// Get tangent space normal and apply intensity
	float3 tangent_normal = normalize(Unpack(T_Normal.Sample(samplerAniso, texCoords).rgb));
	tangent_normal.xy *= saturate(normal_intensity);
	normal = normalize(mul(tangent_normal, TBN).xyz); // Transform to world space
#endif

#if OCCLUSION_MAP
	occlusion = T_Occlusion.Sample(samplerAniso, texCoords).r;
#endif

#if EMISSION_MAP
	emission = T_Emission.Sample(samplerAniso, texCoords).r;
#endif

	// Write to G-Buffer
	gBuffer.Albedo = albedo;
	gBuffer.Normal = float4(NormalEncode(normal), occlusion);
	gBuffer.Material = float4(roughness, metallic, emission, materialShadingMode);
	gBuffer.Velocity = velocity;

	return gBuffer;
}
