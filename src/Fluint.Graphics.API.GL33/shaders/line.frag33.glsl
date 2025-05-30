#version 330 core

in vec2 _u_aa_radius;                             
in vec4 v_col;
in vec4 ps_vertex_position;
in float v_u;
in float v_v;
in float v_line_width;
in float v_line_length;

out vec4 frag_color;

void main()
{
    float au = 1.0 - smoothstep( 1.0 - ((2.0*_u_aa_radius[0]) / v_line_width),  1.0, abs( v_u / v_line_width ) );
    float av = 1.0 - smoothstep( 1.0 - ((2.0*_u_aa_radius[1]) / v_line_length), 1.0, abs( v_v / v_line_length ) );
    frag_color = v_col;
    frag_color.a *= min(au, av);
    
    float distance = length(ps_vertex_position);

    float fog_start = 25;
    float fog_end = 44;

    // linear interpolation
    float fog_factor = (distance - fog_start) / (fog_end - fog_start);
    fog_factor = 1.0 - clamp(fog_factor, 0, 1);

    frag_color = frag_color * vec4(fog_factor);
}