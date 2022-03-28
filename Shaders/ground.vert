#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;
out vec2 texCoord;
out vec4 fragColor;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main()
{
  gl_Position = vec4(aPosition, 1.0) * model* view * projection;
  texCoord = aTexCoord;
  fragColor = 0.1 * vec4(1.0, 1.0, 1.0, 1.0);
}