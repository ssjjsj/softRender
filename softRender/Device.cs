using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softRender
{
    class Device
    {
        private static Device device;

        public static Device Device
        {
            get 
            {
                if (device == null)
                    device = new Device();
            }
        }

        private Buffer<Color4> b = new Buffer<Color4>(300, 300, new Color4(1.0f, 1.0f, 1.0f, 1.0f));
        private Buffer<float> zBuffer = new Buffer<float>(300, 300, 1.0f);
        private Rasterization r = new Rasterization();

        public Buffer getSurface()
        {
            return b;
        }

        public Buffer getZBuffer()
        {
            return zBuffer;
        }


    }
}
