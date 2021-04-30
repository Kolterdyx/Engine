using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Engine.Shape
{
    class Rect : Polygon
    {
        
        public Rect(Vector2 pos, Vector2 size) : 
            base(new float[]
            {
                pos.X, pos.Y, 0f,
                pos.X, size.Y, 0f,
                size.X, pos.Y, 0f,
                size.X, size.Y, 0f,
            })
        {
            //initialize


            
            
        }

    }
}
