using System;
using Engine.Engine.Shape;

namespace Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Screen screen = new Screen(800, 600, "OpenTK Testing"))
            {
                float[] vertices = new float[]
                {
                    0.0f, 100f, 0f,
                    0.0f, 0.0f, 0f,
                    10f, 0.0f, 0f,
                    10f, 100f, 0f,
                };

                Polygon p = new Polygon(vertices);
                screen.Children.Add(p);

                screen.Run(60.0);
            }
        }
    }
}
