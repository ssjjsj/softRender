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
        public Plane(Vector4 normal, Vector4 point)
        {
            this.normal = normal;
            this.point = point;
        }
        public Vector4 normal;
        public Vector4 point;

        public float getDotValue(Vector4 p)
        {
            return Vector4.Dot(normal, point - p);
        }

        public Vertex getInsertValue(Vertex p1, Vertex p2)
        {
            Vector4 pos1 = p1.pos;
            Vector4 pos2 = p2.pos;

            float temp = pos1.X * normal.X + pos1.Y * normal.Y + pos1.Z * normal.Z + pos1.W * normal.W /
                pos2.X * normal.X + pos2.Y * normal.Y + pos2.Z * normal.Z + pos2.W * normal.W;

            Vector4 pos = p1.pos + temp * pos2;
            Color4 c = p1.color + temp * p1.color;

            Vertex v = new Vertex();
            v.pos = pos;
            v.color = c;

            return v;
        }
    }
}
