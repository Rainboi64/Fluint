#version 460 core 

layout (location = 0) in vec3 position;
layout (location = 1) in vec4 color;

layout (location = 2) uniform mat4 ml_matrix = mat4( 1.0, 0.0, 0.0, 0.0,
											         0.0, 1.0, 0.0, 0.0, 
											         0.0, 0.0, 1.0, 0.0,
													 0.0, 0.0, 0.0, 1.0);

layout (location = 3) out vec4 ucolor;

void main()
{
	ucolor = color;
	gl_Position = ml_matrix * vec4(position, 1);
}