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
            private List<Plane> cullPlanes = new List<Plane>();

            public List<Plane> getCullPlanes()
            {
                return cullPlanes;
            }

            public CullPlane()
            {
               
                cullPlanes.Add(new Plane(new Vector4(-1, 1, 0, 1), new Vector4(1, 0, 0, 0)));
                cullPlanes.Add(new Plane(new Vector4(1, 1, 0, 1), new Vector4(-1, 0, 0, 0)));
                cullPlanes.Add(new Plane(new Vector4(-1, 1, 0, 1), new Vector4(0, -1, 0, 0)));
                cullPlanes.Add(new Plane(new Vector4(-1, -1, 0, 1), new Vector4(0, 1, 0, 0)));
                cullPlanes.Add(new Plane(new Vector4(-1, 1, 0, 1), new Vector4(0, 0, 1, 0)));
                cullPlanes.Add(new Plane(new Vector4(-1, 1, 1, 1), new Vector4(0, 0, -1, 0)));
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

        private List<Vertex[]> cullTriangles = new List<Vertex[]>();
        private List<Vertex> newVertexs = new List<Vertex>();
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
                else
                {
                    string s = "wtf";
                }
            }
            else
            {
                Vertex v = p.getInsertValue(p1, p2);
                list.Add(v);
                if (value1 > 0)
                {
                    list.Add(p1);
                }
                else
                {
                    list.Add(p2);
                }
            }

            return list;
        }

        private void cullTriangle(Vertex[] triangle, Plane p)
        {
            cullTriangles.Remove(triangle);

            List<Vertex> vertexs = new List<Vertex>();
            vertexs.AddRange(cullLine(triangle[0], triangle[1], p));
            foreach (Vertex v in cullLine(triangle[1], triangle[2], p))
            {
                if (!vertexs.Contains(v))
                    vertexs.Add(v);
            }

            foreach (Vertex v in cullLine(triangle[2], triangle[0], p))
            {
                if (!vertexs.Contains(v))
                    vertexs.Add(v);
            }
 
            if (vertexs.Count == 3)
            {
                cullTriangles.Add(vertexs.ToArray());
            }
            else if (vertexs.Count == 4)
            {
                Vertex[] newTriangle = new Vertex[3];
                newTriangle[0] = vertexs[0];
                newTriangle[1] = vertexs[1];
                newTriangle[2] = vertexs[2];
                cullTriangles.Add(newTriangle);

                newTriangle = new Vertex[3];
                newTriangle[0] = vertexs[2];
                newTriangle[1] = vertexs[3];
                newTriangle[2] = vertexs[0];
                cullTriangles.Add(newTriangle);
            }
        }

        public void CullTriangles(Vertex[] vertexs, List<int[]> trianglesIndex, CullPlane cullPlanes)
        {
            foreach (int[] indexs in trianglesIndex)
            {
                Vertex[] t = new Vertex[3];
                int index1 = indexs[0];
                t[0] = vertexs[index1];
                int index2 = indexs[1];
                t[1] = vertexs[index2];
                int index3 = indexs[2];
                t[2] = vertexs[index3];
                cullTriangles.Add(t);
            }

            foreach (Plane p in cullPlanes.getCullPlanes())
            {
                Vertex[][] triAry = cullTriangles.ToArray();
                int length = triAry.Length;
                
                for (int i = 0; i < triAry.Length; i++)
                {
                    cullTriangle(triAry[i], p);
                }
                List<Vertex> newVertex1 = new List<Vertex>();

                foreach (Vertex[] t in cullTriangles)
                {
                    if (!newVertex1.Contains(t[0]))
                        newVertex1.Add(t[0]);
                    if (!newVertex1.Contains(t[1]))
                        newVertex1.Add(t[1]);
                    if (!newVertex1.Contains(t[2]))
                        newVertex1.Add(t[2]);
                }

                System.Console.Write(cullTriangles.Count.ToString() + "\n");
            }

            List<Vertex> newVertex = new List<Vertex>();

            foreach (Vertex[] t in cullTriangles)
            {
                if (!newVertex.Contains(t[0]))
                    newVertex.Add(t[0]);
                if (!newVertex.Contains(t[1]))
                    newVertex.Add(t[1]);
                if (!newVertex.Contains(t[2]))
                    newVertex.Add(t[2]);
            }

            vertexs = newVertex.ToArray();
            trianglesIndex.Clear();

 

            foreach(Vertex[] t in cullTriangles)
            {
                int[] indexs = new int[3];
                indexs[0] = newVertex.IndexOf(t[0]);
                indexs[1] = newVertex.IndexOf(t[1]);
                indexs[2] = newVertex.IndexOf(t[2]);
                trianglesIndex.Add(indexs);
            }
        }
    }
}
