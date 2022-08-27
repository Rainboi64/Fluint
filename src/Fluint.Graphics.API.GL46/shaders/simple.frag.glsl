#version 430 core

out vec4 o_frag_color;

in vec4 ps_vertex_color;
in vec4 ps_vertex_position;
in mat4 ps_mvp;

void main()
{
    float distance = length(ps_vertex_position);

    float fog_start = 100;
    float fog_end = 175;

    // linear interpolation
    float fog_factor = (distance - fog_start) / (fog_end - fog_start);
    fog_factor = 1.0 - clamp(fog_factor, 0, 1);

    o_frag_color = ps_vertex_color * vec4(fog_factor);
} 