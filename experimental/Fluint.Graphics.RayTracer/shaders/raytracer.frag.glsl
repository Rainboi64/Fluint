#version 460 core

out vec4 outputColor;

in vec3 vert;
in vec4 gl_FragCoord;

float lrand = 0;

float aspect_ratio = 1;
int image_width = 500;
int image_height = 500;

float viewport_height = 2.0;
float viewport_width = aspect_ratio * viewport_height;
float focal_length = 1.0;

vec3 origin = vec3(0, 0, 0);
vec3 horizontal = vec3(viewport_width, 0, 0);
vec3 vertical = vec3(0, viewport_height, 0);
vec3 lower_left_corner = origin - horizontal / 2 - vertical / 2 - vec3(0, 0, focal_length);

float lensqrd(vec3 v)
{
    return v.x * v.x + v.y * v.y + v.z * v.z;
}

float rand() {
    lrand = fract(sin(gl_PrimitiveID + lrand) * 43758.5453123);
    return lrand;
}

float _rand(float min, float max) {
    return min + (max - min) * rand();
}

vec3 randVec3() {
    // Returns a random real in [min,max).
    return vec3(rand(), rand(), rand());
}

vec3 _randVec3(float min, float max) {
    // Returns a random real in [min,max).
    return vec3(_rand(min, max), _rand(min, max), _rand(min, max));
}

vec3 random_in_unit_sphere() {
    while (true) {
        vec3 p = _randVec3(-1, 1);
        if (lensqrd(p) >= 1) continue;
        return p;
    }
}

struct ray {
    vec3 orig;
    vec3 dir;
};

ray r_create(vec3 origin, vec3 direction)
{
    ray r;
    r.orig = origin;
    r.dir = direction;
    return r;
}

vec3 r_origin(ray r) { return r.orig; }
vec3 r_direction(ray r) { return r.dir; }
vec3 r_at(ray r, float t) {
    return r.orig + t * r.dir;
}


float hit_sphere(vec3 center, float radius, ray r) {
    vec3 oc = r_origin(r) - center;
    float a = lensqrd(r_direction(r));
    float half_b = dot(oc, r_direction(r));
    float c = lensqrd(oc) - radius * radius;
    float discriminant = half_b * half_b - a * c;

    if (discriminant < 0) {
        return -1.0;
    } else {
        return (-half_b - sqrt(discriminant)) / a;
    }
}

int depth = 50;
vec3 target;
vec3 p;

vec3 ray_color(ray r)
{

    float t = hit_sphere(vec3(0, 0, -1), 0.5, r);
    if (t > 0.0) {
        vec3 N = normalize(r_at(r, t) - vec3(0, 0, -1));
        p = r_at(r, t);
        target = p + N + random_in_unit_sphere();
        depth--;
        return p;
    }

    vec3 unit_direction = normalize(r_direction(r));
    t = 0.5 * (unit_direction.y + 1.0);
    depth = 0;
    return (1.0 - t) * vec3(1.0, 1.0, 1.0) + t * vec3(0.5, 0.7, 1.0);
}

void main()
{
    float u = (gl_FragCoord.x) / (image_width - 1);
    float v = (gl_FragCoord.y) / (image_height - 1);
    ray r = r_create(origin, lower_left_corner + u * horizontal + v * vertical - origin);
    vec3 pixel_color;
    while (depth > 0)
    {
        pixel_color = ray_color(r);
        r = r_create(p, target - p);
    }
    outputColor = vec4(pixel_color, 1);
}
