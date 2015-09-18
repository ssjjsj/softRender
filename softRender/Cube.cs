using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Cube
    {
        Vector4 minPos;
        Vector4 maxPos;

        Vertex[] vertexs = new Vertex[8];
        List<int[]> triangleIndexs = new List<int[]>();
        List<Vertex[]> lines = new List<Vertex[]>();
        public Cube(Vector4 minPos, Vector4 maxPos)
        {
            this.minPos = minPos;
            this.maxPos = maxPos;

            Vertex v1 = new Vertex();
            v1.pos = new Vector4(minPos.X, minPos.Y, minPos.Z, 1.0f);
            v1.color = new Color4(1.0f, 1.0f, 0.0f, 0.0f);

            Vertex v2 = new Vertex();
            v2.pos = new Vector4(maxPos.X, minPos.Y, minPos.Z, 1.0f);
            v2.color = new Color4(1.0f, 1.0f, 0.0f, 0.0f);

            Vertex v3 = new Vertex();
            v3.pos = new Vector4(maxPos.X, maxPos.Y, minPos.Z, 1.0f);
            v3.color = new Color4(1.0f, 1.0f, 0.0f, 0.0f);

            Vertex v4 = new Vertex();
            v4.pos = new Vector4(minPos.X, maxPos.Y, minPos.Z, 1.0f);
            v4.color = new Color4(1.0f, 1.0f, 0.0f, 0.0f);

            Vertex v5 = new Vertex();
            v5.pos = new Vector4(minPos.X, minPos.Y, maxPos.Z, 1.0f);
            v5.color = new Color4(1.0f, 0.0f, 0.0f, 1.0f);

            Vertex v6 = new Vertex();
            v6.pos = new Vector4(maxPos.X, minPos.Y, maxPos.Z, 1.0f);
            v6.color = new Color4(1.0f, 0.0f, 0.0f, 1.0f);

            Vertex v7 = new Vertex();
            v7.pos = new Vector4(maxPos.X, maxPos.Y, maxPos.Z, 1.0f);
            v7.color = new Color4(1.0f, 0.0f, 0.0f, 1.0f);

            Vertex v8 = new Vertex();
            v8.pos = new Vector4(minPos.X, maxPos.Y, maxPos.Z, 1.0f);
            v8.color = new Color4(1.0f, 0.0f, 0.0f, 1.0f);

            vertexs[0] = v1;
            vertexs[1] = v2;
            vertexs[2] = v3;
            vertexs[3] = v4;
            vertexs[4] = v5;
            vertexs[5] = v6;
            vertexs[6] = v7;
            vertexs[7] = v8;

            triangleIndexs.Add(new int[3] { 0, 1, 3 });
            triangleIndexs.Add(new int[3] { 1, 2, 3 });
            triangleIndexs.Add(new int[3] { 4, 5, 7 });
            triangleIndexs.Add(new int[3] { 5, 6, 7 });
            triangleIndexs.Add(new int[3] { 0, 1, 4 });
            triangleIndexs.Add(new int[3] { 1, 5, 4 });
            triangleIndexs.Add(new int[3] { 3, 2, 7 });
            triangleIndexs.Add(new int[3] { 2, 6, 7 });
            triangleIndexs.Add(new int[3] { 0, 4, 3 });
            triangleIndexs.Add(new int[3] { 4, 7, 3 });
            triangleIndexs.Add(new int[3] { 1, 5, 2 });
            triangleIndexs.Add(new int[3] { 5, 6, 2 });


            lines.Add(new Vertex[2] { v1, v2 });
            lines.Add(new Vertex[2] { v2, v3 });
            lines.Add(new Vertex[2] { v3, v4 });
            lines.Add(new Vertex[2] { v4, v1 });

            lines.Add(new Vertex[2] { v5, v6 });
            lines.Add(new Vertex[2] { v6, v7 });
            lines.Add(new Vertex[2] { v7, v8 });
            lines.Add(new Vertex[2] { v8, v5 });

            lines.Add(new Vertex[2] { v1, v5 });
            lines.Add(new Vertex[2] { v2, v6 });
            lines.Add(new Vertex[2] { v3, v7 });
            lines.Add(new Vertex[2] { v4, v8 });
        }

        public void transform(Matrix m)
        {
            for (int i = 0; i < vertexs.Length; i++ )
            {
                vertexs[i].pos = Vector4.Transform(vertexs[i].pos, m);
            }
        }

        public Vertex[] getVertexs()
        {
            return vertexs;
        }

        public List<int[]> getTriagngles()
        {
            return triangleIndexs;
        }

        public List<Vertex[]> getLines()
        {
            return lines;
        }
    }
}
