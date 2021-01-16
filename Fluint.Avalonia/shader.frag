#version 460 core 

out vec4 color;

layout (location = 3) in vec4 ucolor;



void main()
{
		color = vec4(ucolor.xyz, 1);
}