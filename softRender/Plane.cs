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
            float temp = Vector4.Dot(normal, p-point);
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
            Vector3 result = pos + temp * dir;
            v.pos = new Vector4(result.X, result.Y, result.Z, 1.0f);
            v.color = p1.color + temp * (p1.color-p2.color);
            v.uv = p1.uv + temp * (p2.uv - p1.uv);

            float temp1 = Vector4.Dot((v.pos - point), normal);

            v.color.Alpha = range(v.color.Alpha, 0, 1);
            v.color.Red = range(v.color.Red, 0, 1);
            v.color.Green = range(v.color.Green, 0, 1);
            v.color.Blue = range(v.color.Blue, 0, 1);

            return v;
        }

        private float range(float v, float min, float max)
        {
            if (v < min)
                v = min;

            if (v > max)
                v = max;

            return v;
        }
    }
}
