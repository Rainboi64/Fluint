static const int g_steps = 16;
static const int g_binarySearchSteps = 16;
static const float g_binarySearchThreshold = 0.01f;

float2 SSR_BinarySearch(float3 ray_dir, inout float3 ray_pos, Texture2D tex_depth, SamplerState sampler_point_clamp)
{
	for (int i = 0; i < g_binarySearchSteps; i++)
	{
		float2 ray_uv = Project(ray_pos, G_Projection);
		float depth = GetLinearDepth(tex_depth, sampler_point_clamp, ray_uv);
		float depth_delta = ray_pos.z - depth;

		if (depth_delta <= 0.0f)
		{
			ray_pos += ray_dir;
		}

		ray_dir *= 0.5f;
		ray_pos -= ray_dir;
	}

	float2 ray_uv = Project(ray_pos, G_Projection);
	float depth_sample = GetLinearDepth(tex_depth, sampler_point_clamp, ray_uv);
	float depth_delta = ray_pos.z - depth_sample;

	return abs(depth_delta) < g_binarySearchThreshold ? Project(ray_pos, G_Projection) : 0.0f;
}

float2 SSR_RayMarch(float3 ray_pos, float3 ray_dir, Texture2D tex_depth, SamplerState sampler_point_clamp)
{
	for (int i = 0; i < g_steps; i++)
	{
		// Step ray
		ray_pos += ray_dir;
		float2 ray_uv = Project(ray_pos, G_Projection);

		// Compute depth
		float depth_current = ray_pos.z;
		float depth_sampled = GetLinearDepth(tex_depth, sampler_point_clamp, ray_uv);
		float depth_delta = ray_pos.z - depth_sampled;

		[branch]
		if (depth_delta > 0.0f)
		{
			return SSR_BinarySearch(ray_dir, ray_pos, tex_depth, sampler_point_clamp);
		}
	}

	return 0.0f;
}

float3 SSR(float3 position, float3 normal, float2 uv, float roughness, Texture2D tex_color, Texture2D tex_depth, SamplerState sampler_point_clamp)
{
	float noise_scale = 0.5f; // scale the noise down a bit for now as this using jitter needs a blur pass as well (for acceptable results)
	float3 jitter = float(Randomize(uv) * 2.0f - 1.0f) * roughness * noise_scale;

	// Convert everything to view space
	float3 viewPos = mul(float4(position, 1.0f), G_View).xyz;
	float3 viewNormal = normalize(mul(float4(normal, 0.0f), G_View).xyz);
	float3 viewRayDir = normalize(reflect(viewPos, viewNormal) + jitter);

	float3 ray_pos = viewPos;
	float2 reflection_uv = SSR_RayMarch(ray_pos, viewRayDir, tex_depth, sampler_point_clamp);
	float2 edgeFactor = float2(1, 1) - pow(saturate(abs(reflection_uv - float2(0.5f, 0.5f)) * 2), 8);
	float screenEdge = saturate(min(edgeFactor.x, edgeFactor.y));

	return tex_color.Sample(sampler_point_clamp, reflection_uv).rgb * screenEdge * (1.0f - roughness);
}