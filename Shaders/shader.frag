#version 330

in vec3 fragPos;
in vec2 texCoord;
in vec3 normal;
out vec4 outputColor;

uniform sampler2D texture0;
uniform vec3 lightPos;
uniform vec3 lightColor;

void main()
{
  float ambientStrength = 0.1;
  vec3 ambientColor = vec3(1.0, 1.0, 1.0);
  vec3 ambient = ambientStrength * ambientColor;

  vec3 lightDir = normalize(lightPos - fragPos);
  vec3 diffuse = max(dot(normal, lightDir), 0.0) * lightColor;
  vec3 light = ambient + diffuse;
  outputColor = vec4(vec3(texture(texture0, texCoord)) * light, 1.0);
}