using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softRender
{
    class Culler
    {
        public struct CullPlane
        {
            public float left;
            public float right;
            public float top;
            public float buttom;
        }

        public List<Vertex[]> cull(CullPlane plane, List<Vertex[]> tringles)
        {
            List<Vertex[]> newTriangles = new List<Vertex[]>();

            foreach (Vertex[] triangle in tringles)
            {
                List<Vertex> newVertexs = new List<Vertex>();



            }
        }

        private List<Vertex> getCulledVertexs(Vertex start, Vertex end, CullPlane plane)
        {

        }
    }
}
