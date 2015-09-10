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
        public Cube(Vector4 minPos, Vector4 maxPos)
        {
            this.minPos = minPos;
            this.maxPos = maxPos;
        }

        public List<Vertex[]> getVertex()
        {
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

            List <Vertex[]> vertexList = new List<Vertex[]>();
            vertexList.Add(new Vertex[3]{v1, v2, v4});
            vertexList.Add(new Vertex[3]{v2, v3, v4});
            vertexList.Add(new Vertex[3]{v5, v6, v8});
            vertexList.Add(new Vertex[3]{v6, v7, v8});
            vertexList.Add(new Vertex[3]{v1, v2, v5});
            vertexList.Add(new Vertex[3]{v2, v6, v5});
            vertexList.Add( new Vertex[3]{v4, v3, v8});
            vertexList.Add(new Vertex[3]{v3, v7, v8});
            vertexList.Add(new Vertex[3]{v1, v5, v4});
            vertexList.Add(new Vertex[3]{v5, v8, v4});
            vertexList.Add(new Vertex[3]{v2, v6, v3});
            vertexList.Add(new Vertex[3]{v6, v7, v3});

            return vertexList;
        }
    }
}
