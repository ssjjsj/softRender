using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softRender
{
    class VertexBuffer
    {
        Vertex[] data;
        public VertexBuffer(Vertex[] data)
        {
            this.data = new Vertex[data.Count()];

            for (int i = 0; i < data.Count(); i++ )
            {
                Vertex v = data[i];
                Vertex newV = new Vertex();
                newV.pos = v.pos;
                newV.normal = v.normal;
                newV.color = v.color;
                newV.uv = v.uv;
                this.data[i] = newV;
            }
        }

        public Vertex[] getData()
        {
            return data;
        }
    }
}
