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
        List<int[]> lines = new List<int[]>();
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

            triangleIndexs.Add(new int[3] { 2, 3, 0 });
            triangleIndexs.Add(new int[3] { 0, 1, 2 });
            triangleIndexs.Add(new int[3] { 6, 2, 1 });
            triangleIndexs.Add(new int[3] { 1, 5, 6 });
            triangleIndexs.Add(new int[3] { 4, 7, 6 });
            triangleIndexs.Add(new int[3] { 6, 5, 4 });
            triangleIndexs.Add(new int[3] { 3, 7, 4 });
            triangleIndexs.Add(new int[3] { 4, 0, 3 });
            triangleIndexs.Add(new int[3] { 6, 7, 3 });
            triangleIndexs.Add(new int[3] { 3, 2, 6 });
            triangleIndexs.Add(new int[3] { 0, 4, 5 });
            triangleIndexs.Add(new int[3] { 5, 1, 0 });


            lines.Add(new int[2] { 0, 1 });
            lines.Add(new int[2] { 1, 2 });
            lines.Add(new int[2] { 2, 3 });
            lines.Add(new int[2] { 3, 0 });

            lines.Add(new int[2] { 4, 5 });
            lines.Add(new int[2] { 5, 6 });
            lines.Add(new int[2] { 6, 7 });
            lines.Add(new int[2] { 7, 4 });

            lines.Add(new int[2] { 0, 4 });
            lines.Add(new int[2] { 1, 5 });
            lines.Add(new int[2] { 2, 6 });
            lines.Add(new int[2] { 3, 7 });
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

        public List<int[]> getLines()
        {
            return lines;
        }
    }
}
