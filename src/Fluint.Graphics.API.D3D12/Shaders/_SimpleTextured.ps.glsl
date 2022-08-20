#version 430 core

out vec4 o_frag_color;
  
in vec4 ps_vertex_color;
in vec2 ps_vertex_uv;

layout(binding = 0) uniform sampler2D t_texture;

void main()
{
    o_frag_color = texture(t_texture, ps_vertex_uv);
} 