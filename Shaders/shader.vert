#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aColor;
layout(location = 2) in vec2 aTexCoord;
out vec3 fragColor;
out vec2 texCoord;

void main(void)
{
  gl_Position = vec4(aPosition, 1.0);
  fragColor = aColor;
  texCoord = aTexCoord;
}