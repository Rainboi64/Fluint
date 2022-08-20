#include "Common.hlsl"
#include "Include.Sharpening.hlsl"
#include "Include.ChromaticAberration.hlsl"
#include "Include.Blur.hlsl"
#include "Include.ToneMapping.hlsl"
#include "Include.ResolveTAA.hlsl"
#include "Include.MotionBlur.hlsl"
#include "Include.Dithering.hlsl"
#include "Include.Scaling.hlsl"
#define FXAA_PC 1
#define FXAA_HLSL_5 1
#define FXAA_QUALITY__PRESET 39
#include "Include.FXAA.hlsl"

Texture2D T_Source1: register(t0);
Texture2D T_Source2: register(t1);
Texture2D T_Source3: register(t2);
Texture2D T_Source4: register(t3);
SamplerState S_Sampler: register(s0);

cbuffer BlurBuffer : register(b1)
{
	float2 blur_direction;
	float blur_sigma;
	float blur_padding;
}

float4 Main(PixelPositionTexture input) : SV_TARGET
{
	float2 texCoord = input.Uv;
	float4 color = float4(1.0f, 0.0f, 0.0f, 1.0f);

#if PASS_GAMMA_CORRECTION
	color = T_Source1.Sample(S_Sampler, texCoord);
	color = Gamma(color);
#endif

#if PASS_TONEMAPPING
	color = T_Source1.Sample(S_Sampler, texCoord);
	color.rgb = ToneMap(color.rgb, G_Exposure);
#endif

#if PASS_TEXTURE
	color = T_Source1.Sample(S_Sampler, texCoord);
#endif

#if PASS_FXAA
	// Requirements: Bilinear sampler
	FxaaTex tex = { S_Sampler, T_Source1 };
	float2 fxaaQualityRcpFrame = G_TexelSize;

	color.rgb = FxaaPixelShader
	(
		texCoord, 0, tex, tex, tex,
		fxaaQualityRcpFrame, 0, 0, 0,
		G_FXAA_SubPix,
		G_FXAA_EdgeThreshold,
		G_FXAA_EdgeThresholdMin,
		0, 0, 0, 0
	).rgb;
	color.a = 1.0f;
#endif

#if PASS_CHROMATIC_ABERRATION
	// Requirements: Bilinear sampler
	color.rgb = ChromaticAberration(texCoord, G_TexelSize, T_Source1, S_Sampler);
#endif

#if PASS_SHARPENING
	// Requirements: Bilinear sampler
	color.rgb = LumaSharpen(texCoord, T_Source1, S_Sampler, G_Resolution, G_SharpenStrength, G_SharpenClamp);
#endif

#if PASS_DOWNSAMPLE_BOX
	color = Downsample_Box13Tap(texCoord, G_TexelSize, T_Source1, S_Sampler);
#endif

#if PASS_UPSAMPLE_BOX
	color = Upsample_Box(texCoord, G_TexelSize, T_Source1, S_Sampler, 4.0f);
#endif

#if PASS_BLUR_BOX
	color = Blur_Box(texCoord, G_TexelSize, blur_sigma, T_Source1, S_Sampler);
#endif

#if PASS_BLUR_GAUSSIAN
	// Requirements: Bilinear sampler
	color = Blur_Gaussian(texCoord, T_Source1, S_Sampler, G_TexelSize, blur_direction, blur_sigma);
#endif

#if PASS_BLUR_BILATERAL_GAUSSIAN
	// Requirements: Bilinear sampler
	color = Blur_GaussianBilateral(texCoord, T_Source1, T_Source2, T_Source3, S_Sampler, G_TexelSize, blur_direction, blur_sigma);
#endif

#if PASS_BRIGHT
	color = T_Source1.Sample(S_Sampler, texCoord);
	color = Luminance(color.rgb) * color;
#endif

#if PASS_BLEND_ADDITIVE
	float4 sourceColor = T_Source1.Sample(S_Sampler, texCoord);
	float4 sourceColor2 = T_Source2.Sample(S_Sampler, texCoord);
	color = sourceColor + sourceColor2 * G_BloomIntensity;
#endif

#if PASS_LUMA
	color = T_Source1.Sample(S_Sampler, texCoord);
	color.a = Luminance(color.rgb);
#endif

#if PASS_DITHERING
	color = T_Source1.Sample(S_Sampler, texCoord);
	color = Dither_Ordered(color, texCoord);
#endif

#if PASS_TAA_RESOLVE
	color = ResolveTAA(texCoord, T_Source1, T_Source2, T_Source3, T_Source4, S_Sampler);
#endif

#if PASS_MOTION_BLUR
	color = MotionBlur(texCoord, T_Source1, T_Source2, S_Sampler);
#endif

	return color;
}