#version 330

in vec3 fragColor;
in vec2 texCoord;
out vec4 outputColor;

uniform sampler2D uniTexture;

void main()
{
  outputColor = texture(uniTexture, texCoord);
}