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
        public struct CullPlane
        {
            private Plane left;
            private Plane right;
            private Plane top;
            private Plane buttom;
            private Plane front;
            private Plane back;

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
            float value1 = p.getInserValue(p1);
            float value2 = p.getInserValue(p2);

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
                Vertex[] triangle = new Vertex[3];
                triangle[0] = vertexs[0];
                triangle[1] = vertexs[1];
                triangle[2] = vertexs[2];
                needCullTriangles.Add(triangle);

                triangle = new Vertex[3];
                triangle[0] = vertexs[2];
                triangle[1] = vertexs[3];
                triangle[2] = vertexs[0];
                needCullTriangles.Add(triangle);
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
