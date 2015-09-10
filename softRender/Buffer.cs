using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Buffer<T>
    {
        private T[] rawData;
        private int width;
        private int height;
 
        public Buffer(int width, int height, T defaultData)
        {
            rawData = new T[width*height];
            for (int i=0; i<rawData.Length; i++)
            {
                rawData[i] = defaultData;
            }
            this.width = width;
            this.height = height;
        }

        public void writeOneData(int x, int y, T data)
        {
            rawData[width*y + x] = data;
        }

        public T readOneData(int x, int y)
        {
            return rawData[y*width + x];
        }
    }
}
