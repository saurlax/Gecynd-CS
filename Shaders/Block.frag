#version 330

in vec2 texCoord;

out vec4 outputColor;

uniform sampler2D texture0;
uniform vec4 offset;

void main() {
  outputColor = texture(texture0, texCoord * offset.w / 16.0);
}