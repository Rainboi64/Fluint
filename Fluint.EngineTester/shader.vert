
#version 460 core 

layout (location = 0) in vec3 position;
layout (location = 1) in vec4 color;

uniform mat4 ml_matrix = mat4(1.0);

out DATA
{
	vec4 color;
} vs_out;


void main()
{
	color.r = color.r / 255;
	color.g = color.g / 255;
	color.b = color.b / 255;
	color.a = color.a / 255;

	vs_out.color = color;
	gl_Position = ml_matrix * vec4(position, 1);
}