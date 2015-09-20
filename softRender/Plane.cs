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
        public Plane(Vector4 point, Vector4 normal)
        {
            this.normal = normal;
            this.point = point;
        }
        public Vector4 normal;
        public Vector4 point;

        public float getDotValue(Vector4 p)
        {
            float temp = Vector4.Dot(normal, point-p);
            if (Math.Abs(temp) < 0.0001)
                temp = 0.0f;
            return temp;
        }

        public Vertex getInsertValue(Vertex p1, Vertex p2)
        {
            Vector4 pos1 = p1.pos;
            Vector4 pos2 = p2.pos;
            
            Vector3 dir = new Vector3(pos2.X - pos1.X, pos2.Y - pos1.Y, pos2.Z - pos1.Z);
            Vector3 p = new Vector3(point.X, point.Y, point.Z);
            Vector3 pos = new Vector3(pos1.X, pos1.Y, pos1.Z);
            Vector3 n = new Vector3(normal.X, normal.Y, normal.Z);
            Vector3 tempV = p-pos;

            float temp = Vector3.Dot(tempV, n) / Vector3.Dot(n, dir);

            Vertex v = new Vertex();
            v.pos = p1.pos + temp * pos2;
            v.color = p1.color + temp * p1.color;

            return v;
        }
    }
}
