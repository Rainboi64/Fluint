cbuffer GlobalBuffer : register(b0)
{
	matrix G_MVP;
	matrix G_View;
	matrix G_Projection;
	matrix G_ProjectionOrtho;
	matrix G_ViewProjection;
	matrix G_ViewProjectionInverted;
	matrix G_ViewProjectionOrtho;

	float G_CameraNear;
	float G_CameraFar;
	float2 G_Resolution;

	float3 G_CameraPosition;
	float G_FXAA_SubPix;

	float G_FXAA_EdgeThreshold;
	float G_FXAA_EdgeThresholdMin;
	float G_BloomIntensity;
	float G_SharpenStrength;

	float G_SharpenClamp;
	float G_MotionBlurStrength;
	float G_FPS_Current;
	float G_FPS_Target;

	float G_Gamma;
	float2 G_TaaJitterOffset;
	float G_ToneMapping;

	float G_Exposure;
	float3 G_Padding;
}

#define G_TexelSize float2(1.0f / G_Resolution.x, 1.0f / G_Resolution.y)