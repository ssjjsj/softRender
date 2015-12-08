using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class SRDevice
    {
        private int width;
        private int height;

        private Rasterization r = new Rasterization();
        private Buffer<float> zBuffer;
        private Buffer<Color4> b;
        private Surface s;


        public void Init(int width, int height)
        {
            this.width = width;
            this.height = height;
            b = new Buffer<Color4>(width, height, new Color4(1.0f, 1.0f, 1.0f, 1.0f));
            zBuffer = new Buffer<float>(width, height, 1.0f);
            s = new ImageSurface(width, height);
        }

        private Camera camera;
        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        private Culler cull;
        public Culler Cull
        {
            get { return cull; }
            set { cull = value; }
        }

        private static SRDevice device;

        public static SRDevice Device
        {
            get 
            {
                if (device == null)
                    device = new SRDevice();

                return device;
            }
        }

        public Buffer<Color4> getBackSurface()
        {
            return b;
        }

        public Buffer<float> getZBuffer()
        {
            return zBuffer;
        }

        public Surface getSurface()
        {
            return s;
        }

        private Texture curTexture;
        public Texture CurTexture
        {
            get { return curTexture; }
            set { curTexture = value; }
        }

        public void drawTriangle(Vertex[] vertexs, List<int[]> triangleIndexs)
        {
            foreach (int[] triangle in triangleIndexs)
            {
                r.drawTriange(vertexs, triangle, b, zBuffer, curTexture);
            }
        }


        public void drawLine(Vertex[] vertexs, List<int[]> lines)
        {
            foreach (int[] line in lines)
            {
                r.drawLine(vertexs, line, b);
            }
        }

        public void Present()
        {
            s.Present(b);
        }


        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }
    }
}
