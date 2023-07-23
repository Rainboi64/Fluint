static const int MAX_SAMPLES = 16;

float4 MotionBlur(float2 texCoord, Texture2D texture_color, Texture2D texture_velocity, SamplerState sampler_bilinear)
{
    float4 color = texture_color.Sample(sampler_bilinear, texCoord);
    float2 velocity = GetVelocity_Dilate_Average(texCoord, texture_velocity, sampler_bilinear);

    // Make velocity scale based on user preference instead of frame rate
    float velocity_scale = (G_FPS_Current / G_FPS_Target) * G_MotionBlurStrength;
    velocity *= velocity_scale;

    // Early exit
    if (abs(velocity.x) + abs(velocity.y) < EPSILON)
        return color;

    // Improve performance by adapting sample count to velocity
    float speed = length(velocity / G_TexelSize);
    int samples = clamp(int(speed), 1, MAX_SAMPLES);

    for (int i = 1; i < samples; ++i)
    {
        float2 offset = velocity * (float(i) / float(samples - 1) - 0.5f);
        color += texture_color.SampleLevel(sampler_bilinear, texCoord + offset, 0);
    }
    color /= float(samples);

    return color;
}
