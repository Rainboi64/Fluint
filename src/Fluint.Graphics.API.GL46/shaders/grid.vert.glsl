#version 430 core

out gl_PerVertex
    {
vec4 gl_Position;
};

 layout (location = 0) in vec3 i_position;
                                         layout (location = 1) in vec4 i_color;

                                                                              layout (std140, binding = 0) uniform Matrices
    {
        mat4 u_model;
        mat4 u_view;
        mat4 u_projection;
};

 vec3 gridPlane[6] = vec3[](
                           vec3(1, 1, 0), vec3(-1, -1, 0), vec3(-1, 1, 0),
                                                                         vec3(-1, -1, 0), vec3(1, 1, 0), vec3(1, -1, 0)
                                                                                       );

                                                                                       vec3 UnprojectPoint(float x, float y, float z, mat4 view, mat4 projection) {
                                                                                                                                                                      mat4 viewInv = inverse(view);
                                                                                                                                                                      mat4 projInv = inverse(projection);
                                                                                                                                                                                   vec4 unprojectedPoint =  viewInv * projInv * vec4(x, y, z, 1.0);
                                                                                                                                                                                                                                            return unprojectedPoint.xyz / unprojectedPoint.w;
                                                                                                                                                                  }

                                                                                       out vec4 ps_vertex_color;

                                                                                       out float near; //0.01
                                                                                       out float far; //100

                                                                                                 out vec3 nearPoint;
                                                                                                 out vec3 farPoint;

                                                                                       out mat4 fragView;
                                                                                       out mat4 fragProj;

                                                                                       void main()
                                                                                           {
                                                                                               vec3 p = gridPlane[gl_VertexID].xyz;

                                                                                               near = 0.01;
                                                                                               far = 100.0;

                                                                                               fragView = u_view;
                                                                                                          fragProj = u_projection;

                                                                                                          nearPoint = UnprojectPoint(p.x, p.y, 0.0, u_view, u_projection).xyz; // unprojecting on the near plane
                                                                                                                                                    farPoint = UnprojectPoint(p.x, p.y, 1.0, u_view, u_projection).xyz; // unprojecting on the far plane

                                                                                                                                                                                           gl_Position = vec4(gridPlane[gl_VertexID].xyz, 1.0);
                                                                                               // ps_vertex_color = i_color;
                                                                                       }