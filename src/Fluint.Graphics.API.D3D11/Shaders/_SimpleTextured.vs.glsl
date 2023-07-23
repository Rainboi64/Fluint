#version 430 core

out gl_PerVertex
{
    vec4 gl_Position;
};

layout (location = 0) in vec3 i_position;
layout (location = 1) in vec2 i_uv;

layout (std140, binding = 0) uniform Matrices
{
    mat4 u_mvp;
};

out vec4 ps_vertex_color;
out vec2 ps_vertex_uv;

void main()
{
    gl_Position = u_mvp * vec4(i_position, 1.0);
    ps_vertex_color = vec4(0.5, 0.0, 0.0, 1.0);
    ps_vertex_uv = i_uv;
}