#version 400 core

layout(location = 0) in vec2 inPosition;
layout(location = 1) in vec2 instancePosition; // Using vec3 for world coordinates

uniform mat4 u_MVP;
uniform vec2 u_Position;
uniform vec2 u_Size;
uniform float u_Rotation;
uniform vec2 u_CameraPosition;
uniform mat4 u_Camera;
uniform float u_CameraRotation;


void main()
{
    vec2 position = inPosition;
    position = position * u_Size;
    position = vec2(position.x * cos(u_Rotation) - position.y * sin(u_Rotation), position.x * sin(u_Rotation) + position.y * cos(u_Rotation));
    position += u_Position;
    position -= u_CameraPosition;
    position = vec2(position.x * cos(u_CameraRotation) - position.y * sin(u_CameraRotation), position.x * sin(u_CameraRotation) + position.y * cos(u_CameraRotation));
    position +=  u_CameraPosition;
    gl_Position = vec4(inPosition, 1.0,1.0) * u_Camera;

   
}