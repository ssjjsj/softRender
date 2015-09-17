using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Culler
    {
        public class CullPlane
        {
            private List<Plane> cullPlanes;

            public List<Plane> getCullPlanes()
            {
                return cullPlanes;
            }

            public CullPlane(Plane left, Plane right, Plane top, Plane buttom, Plane front, Plane back)
            {
                cullPlanes.Add(left);
                cullPlanes.Add(right);
                cullPlanes.Add(top);
                cullPlanes.Add(buttom);
                cullPlanes.Add(front);
                cullPlanes.Add(back);
            }
        }

        private List<Vertex[]> needCullTriangles;
        //private List<Vertex[]> culledTriangles;

        private List<Vertex> cullLine(Vertex p1, Vertex p2, Plane p)
        {
            List<Vertex> list = new List<Vertex>();
            float value1 = p.getDotValue(p1.pos);
            float value2 = p.getDotValue(p2.pos);

            float temp = value1 * value2;
            if (temp > 0)
            {
                if (value1>0 && value2 > 0)
                {
                    list.Add(p1);
                    list.Add(p2);
                }
            }
            else
            {

            }

            return list;
        }

        private void cullTriangle(Vertex[] triangle, Plane p)
        {
            List<Vertex> vertexs = new List<Vertex>();
            vertexs.AddRange(cullLine(triangle[0], triangle[1], p));
            vertexs.AddRange(cullLine(triangle[1], triangle[2], p));
            vertexs.AddRange(cullLine(triangle[2], triangle[0], p));
 
            if (vertexs.Count == 3)
            {
                needCullTriangles.Add(vertexs.ToArray());
            }
            else if (vertexs.Count == 4)
            {
                Vertex[] newTriangle = new Vertex[3];
                newTriangle[0] = vertexs[0];
                newTriangle[1] = vertexs[1];
                newTriangle[2] = vertexs[2];
                needCullTriangles.Add(newTriangle);

                newTriangle = new Vertex[3];
                newTriangle[0] = vertexs[2];
                newTriangle[1] = vertexs[3];
                newTriangle[2] = vertexs[0];
                needCullTriangles.Add(newTriangle);
            }
        }

        public List<Vertex[]> CullTriangles(List<Vertex[]> triangles, CullPlane cullPlanes)
        {
            foreach (Plane p in cullPlanes.getCullPlanes())
            {
                foreach (Vertex[] triangle in needCullTriangles)
                {
                    cullTriangle(triangle, p);
                }
            }
        }
    }
}
