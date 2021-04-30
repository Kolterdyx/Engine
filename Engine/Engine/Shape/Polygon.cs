using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Shaders;
using Engine;

namespace Engine.Engine.Shape
{
    class Polygon
    {

        float[] vertices, originalVertices;
        List<uint> indexes;
        uint[] indices;
        int VBO, VAO, EBO;
        Shader shader;
        Matrix4 projection;
        Vector3 offset;

        public Polygon(float[] vertices)
        {
            this.originalVertices = vertices;
            this.vertices = vertices;
            this.projection = Screen.projection;
            //for (int i = 0; i < vertices.Length; i += 3)
            //{
            //    vertices[i] *= projection.Row0.X;
            //    vertices[i + 1] *= -projection.Row1.Y;
            //    vertices[i] -= 1f;
            //    vertices[i + 1] += 1f;
            //}

            offset = new Vector3(10, 10, 0);

            // This creates an index array for the EBO to use
            indexes = new List<uint>();
            for (uint curr_vert = 1; curr_vert < vertices.Length / 3 - 1; curr_vert++)
            {
                // For each triangle of the polygon, I select a fixed vertex (index 0), the current vertex and the next vertex
                uint[] triangle = new uint[] { 0, curr_vert, curr_vert + 1 };
                foreach (uint x in triangle)
                {
                    //I use this list to make life easier
                    indexes.Add(x);
                }
            }
            // Now that the list is complete I convert it into an array
            indices = indexes.ToArray();
        }

        public void Load()
        {
            string path = "../../../Shaders/";
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);



            // We create/bind the Element Buffer Object EBO the same way as the VBO, except there is a major difference here which can be REALLY confusing.
            // The binding spot for ElementArrayBuffer is not actually a global binding spot like ArrayBuffer is. 
            // Instead it's actually a property of the currently bound VertexArrayObject, and binding an EBO with no VAO is undefined behaviour.
            // This also means that if you bind another VAO, the current ElementArrayBuffer is going to change with it.
            // Another sneaky part is that you don't need to unbind the buffer in ElementArrayBuffer as unbinding the VAO is going to do this,
            // and unbinding the EBO will remove it from the VAO instead of unbinding it like you would for VBOs or VAOs.
            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            // We also upload data to the EBO the same way as we did with VBOs.
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            // The EBO has now been properly setup. Go to the Render function to see how we draw our rectangle now!

            shader = new Shader(path + "shader.vert", path + "shader.frag");
            shader.Use();
            


        }

        public void Show()
        {

            shader.Use();
            shader.SetMatrix4("projection", projection);
            shader.SetVector3("offset", offset);
            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

    }
}
