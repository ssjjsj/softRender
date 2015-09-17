using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Plane
    {
        public Vector4 normal;
        public Vector4 point;

        public float getInserValue(Vector4 p)
        {
            return Vector4.Dot(normal, point - p);
        }
    }
}
