#version 330

in vec3 fragPos;
in vec2 texCoord;
in vec3 normal;
out vec4 outputColor;

uniform sampler2D texture0;
uniform vec3 lightPos;
uniform vec3 lightColor;
uniform vec3 viewPos;

void main()
{
  float ambientStrength = 0.6;
  vec3 ambientColor = vec3(1.0, 1.0, 1.0);
  vec3 ambient = ambientStrength * ambientColor;

  vec3 lightDir = normalize(lightPos - fragPos);
  vec3 diffuse = max(dot(normal, lightDir), 0.0) * lightColor;
  
  float specularStrength = 0.5;
  vec3 viewDir = normalize(viewPos - fragPos);
  vec3 reflectDir = reflect(-lightDir, normal);
  float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
  vec3 specular = specularStrength * spec * lightColor;

  float kc = 1.0;
  float kl = 0.14;
  float kq = 0.07;
  float d = length(lightPos - fragPos);
  float fatt = 1.0 / (kc + kl * d + kq * d * d);

  vec3 light = ambient + (diffuse + specular) * fatt;
  outputColor = vec4(vec3(texture(texture0, texCoord)) * light, 1.0);
}