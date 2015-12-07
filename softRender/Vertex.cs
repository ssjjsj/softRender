using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Vertex
    {
        public Vector4 pos;
        public Color4 color;
        public Vector2 uv;
        public Vector4 normal;

        public string ToString()
        {
            return pos.ToString() + color.ToString() + uv.ToString() + normal.ToString();
        }
    }
}
