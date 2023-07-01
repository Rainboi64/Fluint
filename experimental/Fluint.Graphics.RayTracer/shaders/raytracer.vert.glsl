#version 460 core

layout (location = 0) in vec3 i_Position;

out vec3 vert;
out gl_PerVertex
{
    vec4 gl_Position;
};

void main(void)
{
    vert = i_Position;
    gl_Position = vec4(i_Position, 1.0);
}