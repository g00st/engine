#version 400 core

layout(location = 0) in vec2 inPosition;
uniform mat4 u_MVP;
void main()
{
    gl_Position =  u_MVP* vec4(inPosition,1.0, 1.0);
}