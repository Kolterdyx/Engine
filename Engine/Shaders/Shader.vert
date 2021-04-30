﻿#version 330 core
layout (location = 0) in vec3 position;

uniform mat4 projection;
uniform vec3 offset;

void main()
{
    gl_Position = projection * vec4(position + offset, 1.0);
}