#version 460 core 

layout (location = 0) in vec3 position;
layout (location = 1) in vec4 color;

struct Material 
{
	sampler2D diffuse_map;
	sampler2D specular_map;
	sampler2D ambient_map;
	sampler2D emissive_map;
	sampler2D normal_map;
	sampler2D opacity_map;
	sampler2D displacement_map;
	int smooth_shading;
	float opacity;
	float reflectivity;
	float shininess;
	vec4 diffuse;
	vec4 ambient;
	vec4 specular;
	vec4 emissive;
}

void main()
{
	ucolor = color;
	gl_Position = ml_matrix * vec4(position, 1);
}