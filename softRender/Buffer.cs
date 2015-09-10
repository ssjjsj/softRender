using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Buffer
    {
        private Color4[] rawData;
        private int width;
        private int height;
 
        public Buffer(int width, int height)
        {
            rawData = new Color4[width*height];
            for (int i=0; i<rawData.Length; i++)
            {
                rawData[i] = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
            }
            this.width = width;
            this.height = height;
        }

        public void writeOneData(int x, int y, Color4 data)
        {
            rawData[width*y + x] = data;
        }

        public Color4 readOneData(int x, int y)
        {
            return rawData[y*width + x];
        }
    }
}
