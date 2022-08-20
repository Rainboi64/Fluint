#version 430 core

out vec4 o_frag_color;
  
in vec4 ps_vertex_color;

void main()
{
    o_frag_color = ps_vertex_color;
} 