using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using Engine.Engine.Shape;
using Shaders;
using System.Collections;

namespace Engine
{
    class Screen : GameWindow
    {

        public static Matrix4 projection;
        public ArrayList Children;


        public Screen(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) 
        {
            

            Children = new ArrayList();

            projection = Matrix4.CreateOrthographicOffCenter(0.0f, width, 0.0f, height, 0.1f, 100.0f);

        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            foreach(Polygon p in Children)
            {
                p.Load();
            }

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (Polygon p in Children)
            {
                p.Show();
            }

            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            //projection = Matrix4.CreateOrthographicOffCenter(0.0f, Width, 0.0f, Height, 0.1f, 100.0f);
            base.OnResize(e);
        }
    }
}
