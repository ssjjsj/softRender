using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softRender
{
    class Program
    {
        static void Main(string[] args)
        {
            Surface s = new ImageSurface(300, 300);
            Buffer b = new Buffer(300, 300);
            Rasterization r = new Rasterization();
            Vertex[] vertexList = new Vertex[3];
            
            vertexList[0] = new Vertex();
            vertexList[0].pos = new SlimDX.Vector4(0, 300, 0, 0);
            vertexList[0].color = new SlimDX.Color4(1.0f, 1.0f, 0.0f, 0.0f);

            vertexList[1] = new Vertex();
            vertexList[1].pos = new SlimDX.Vector4(300, 300, 0, 0);
            vertexList[1].color = new SlimDX.Color4(1.0f, 0.0f, 1.0f, 0.0f);

            vertexList[2] = new Vertex();
            vertexList[2].pos = new SlimDX.Vector4(150, 0, 0, 0);
            vertexList[2].color = new SlimDX.Color4(1.0f, 0.0f, 0.0f, 1.0f);

            r.drawTriange(vertexList, b);
            s.Present(b);
        }
    }
}
