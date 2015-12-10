using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softRender
{
    class IndexBuffer
    {
        private List<int[]> data = new List<int[]>();
        public IndexBuffer(List<int[]> data)
        {
            foreach (int[] triange in data)
            {
                int[] newTriangle = new int[3];
                newTriangle[0] = triange[0];
                newTriangle[1] = triange[1];
                newTriangle[2] = triange[2];
                this.data.Add(newTriangle);
            }
        }

        public List<int[]> getData()
        {
            return data;
        }
    }
}
