#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;
layout(location = 2) in vec3 aNormal;
out vec2 texCoord;
out vec3 normal;
out vec3 fragPos;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;
uniform int size;

void main()
{
  gl_Position = vec4(aPosition, 1.0) * model * view * projection;
  texCoord = aTexCoord * size / 16;
  fragPos = vec3(vec4(aPosition, 1.0) * model);
  normal = aNormal;
}