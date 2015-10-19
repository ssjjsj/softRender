using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softRender
{
    class Pass
    {
        public class PassData
        {
            public Vertex[] vertexs;
            public List<int[]> triangleIndexs;
            public Material m;
        }

        private PassData data;
    }
}
