Texture2D T_Albedo: register(t0);
Texture2D T_Normal: register(t1);
Texture2D T_Depth: register(t2);
Texture2D T_Material: register(t3);
Texture2D T_Shadows: register(t4);
Texture2D T_SSAO: register(t5);
Texture2D T_Frame: register(t6);
Texture2D T_Environment: register(t7);
Texture2D T_LutIBL: register(t8);

SamplerState S_Linear_Clamp : register(s0);
SamplerState S_Point_Clamp : register(s1);

#define MaxLights 64
cbuffer MiscBuffer : register(b1)
{
matrix mWorldViewProjection;
matrix mViewProjectionInverse;

float4 dirLightColor;
float4 dirLightIntensity;
float4 dirLightDirection;

float4 pointLightPosition[MaxLights];
float4 pointLightColor[MaxLights];
float4 pointLightIntenRange[MaxLights];

float4 spotLightColor[MaxLights];
float4 spotLightPosition[MaxLights];
float4 spotLightDirection[MaxLights];
float4 spotLightIntenRangeAngle[MaxLights];

float pointlightCount;
float spotlightCount;
float2 padding2;
};

#include "Common.hlsl"
#include "Include.BRDF.hlsl"
#include "Include.IBL.hlsl"
#include "Include.SSR.hlsl"

float4 Main(PixelPositionTexture input) : SV_TARGET
{
    float2 texCoord = input.Uv;
    float3 color = float3(0, 0, 0);

    // Sample from textures
    float4 albedo = Degamma(T_Albedo.Sample(S_Linear_Clamp, texCoord));
    float4 normalSample = T_Normal.Sample(S_Linear_Clamp, texCoord);
    float3 normal = NormalDecode(normalSample.xyz);
    float4 materialSample = T_Material.Sample(S_Linear_Clamp, texCoord);
    float occlusion_texture = normalSample.w;
    float occlusion_ssao = T_SSAO.Sample(S_Linear_Clamp, texCoord).r;
    float shadow_directional = T_Shadows.Sample(S_Linear_Clamp, texCoord).r;

    // Create material
    Material material;
    material.Albedo = albedo.rgb;
    material.Roughness = materialSample.r;
    material.Metallic = materialSample.g;
    material.Emissive = materialSample.b;
    material.F0 = lerp(0.04f, material.Albedo, material.Metallic);
    material.RoughnessAlpha = max(0.001f, material.Roughness * material.Roughness);

    // Compute common values
    float depth = T_Depth.Sample(S_Linear_Clamp, texCoord).r;
    float3 worldPos = GetWorldPositionFromDepth(depth, mViewProjectionInverse, texCoord);
    float3 camera_to_pixel = normalize(worldPos.xyz - G_CameraPosition.xyz);

    // Sky
    if (materialSample.a == 0.0f)
    {
        color = T_Environment.Sample(S_Linear_Clamp, DirectionToSphereUV(camera_to_pixel)).rgb;
        color *= clamp(dirLightIntensity.r, 0.01f, 1.0f);
        return float4(color, 1.0f);
    }

    //= Ambient light ==========================================================================================
    float factor_occlusion = occlusion_texture == 1.0f ? occlusion_ssao : occlusion_texture;
    float factor_self_shadowing = shadow_directional * saturate(dot(normal, normalize(-dirLightDirection).xyz));
    float factor_sky_light = clamp(dirLightIntensity.r * factor_self_shadowing, 0.025f, 1.0f);
    float ambient_light = factor_sky_light * factor_occlusion;
    //==========================================================================================================

    //= IBL - Image-based lighting =================================================================================================
    color += ImageBasedLighting(material, normal, camera_to_pixel, T_Environment, T_LutIBL, S_Linear_Clamp) *
        ambient_light;
    //==============================================================================================================================

    //= SSR - Screen space reflections ==============================================
    if (padding2.x != 0.0f)
    {
        float3 ssr = SSR(worldPos, normal, texCoord, material.Roughness, T_Frame, T_Depth, S_Point_Clamp);
        color += ssr * ambient_light;
    }
    //===============================================================================

    //= Emissive ============================================
    float3 emissive = material.Emissive * albedo.rgb * 10.0f;
    color += emissive;
    //=======================================================

    //= Directional Light ===============================================================================================
    Light directionalLight;
    directionalLight.Color = dirLightColor.rgb;
    directionalLight.Direction = normalize(-dirLightDirection).xyz;
    float directional_shadow = MicroShadow(factor_occlusion, normal, directionalLight.Direction, shadow_directional);
    directionalLight.Intensity = dirLightIntensity.r * directional_shadow;

    // Compute illumination
    if (directionalLight.Intensity > 0.0f)
    {
        color += BRDF(material, directionalLight, normal, camera_to_pixel);
    }
    //===================================================================================================================

    //= Point lights ====================================================
    Light pointLight;
    for (int i = 0; i < pointlightCount; i++)
    {
        // Get light data
        pointLight.Color = pointLightColor[i].rgb;
        float3 position = pointLightPosition[i].xyz;
        pointLight.Intensity = pointLightIntenRange[i].x * factor_occlusion;
        float range = pointLightIntenRange[i].y;

        // Compute light
        pointLight.Direction = normalize(position - worldPos);
        float dist = length(worldPos - position);
        float attenuation = saturate(1.0f - dist / range);
        attenuation *= attenuation;
        pointLight.Intensity *= attenuation;

        // Compute illumination
        if (dist < range)
        {
            color += BRDF(material, pointLight, normal, camera_to_pixel);
        }
    }
    //===================================================================

    //= Spot Lights =========================================================================================================
    Light spotLight;
    for (int j = 0; j < spotlightCount; j++)
    {
        // Get light data
        spotLight.Color = spotLightColor[j].rgb;
        float3 position = spotLightPosition[j].xyz;
        spotLight.Intensity = spotLightIntenRangeAngle[j].x * factor_occlusion;
        spotLight.Direction = normalize(-spotLightDirection[j].xyz);
        float range = spotLightIntenRangeAngle[j].y;
        float cutoffAngle = 1.0f - spotLightIntenRangeAngle[j].z;

        // Compute light
        float3 direction = normalize(position - worldPos);
        float dist = length(worldPos - position);
        float theta = dot(direction, spotLight.Direction);
        float epsilon = cutoffAngle - cutoffAngle * 0.9f;
        float attunation = saturate((theta - cutoffAngle) / epsilon); // attunate when approaching the outer cone
        attunation *= saturate(1.0f - dist / range);
        attunation *= attunation; // attunate with distance as well
        spotLight.Intensity *= attunation;

        // Compute illumination
        if (theta > cutoffAngle)
        {
            color += BRDF(material, spotLight, normal, camera_to_pixel);
        }
    }
    //=======================================================================================================================

    return float4(color, 1.0f);
}
