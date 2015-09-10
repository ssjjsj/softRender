using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Program
    {
        static void Main(string[] args)
        {
            Surface s = new ImageSurface(300, 300);
            Buffer b = new Buffer(300, 300);
            Rasterization r = new Rasterization();
            Vertex[] tringles = new Vertex[3];

            tringles[0] = new Vertex();
            tringles[0].pos = new SlimDX.Vector4(-1, 1, -1, 1);
            tringles[0].color = new SlimDX.Color4(1.0f, 1.0f, 0.0f, 0.0f);

            tringles[1] = new Vertex();
            tringles[1].pos = new SlimDX.Vector4(1, 1, -1, 1);
            tringles[1].color = new SlimDX.Color4(1.0f, 0.0f, 1.0f, 0.0f);

            tringles[2] = new Vertex();
            tringles[2].pos = new SlimDX.Vector4(0, -1, -1, 1);
            tringles[2].color = new SlimDX.Color4(1.0f, 0.0f, 0.0f, 1.0f);

            Camera c = new Camera(30f, 1, 1, 100, 300, 300);

            //Cube cube = new Cube(new Vector4(-1f, -1f, -1f, 1f), new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
            //Matrix m = new Matrix();
            //Matrix.RotationX(30.0f, out m);
            //Matrix.Translation(0.0f, 0.0f, 5.0f);

            //List<Vertex[]> vertexs = cube.getVertex();

            //foreach (Vertex[] tringles in vertexList)
            //{
                for (int i=0; i<3; i++)
                {
                    tringles[i].pos = Vector4.Transform(tringles[i].pos, c.getClipMatrix());
                    tringles[i].pos = new Vector4(tringles[i].pos.X / tringles[i].pos.W, tringles[i].pos.Y / tringles[i].pos.W, tringles[i].pos.Z / tringles[i].pos.W, 1.0f);
                    tringles[i].pos = Vector4.Transform(tringles[i].pos,  c.getClipToScreenMatrix());
                }

                r.drawTriange(tringles, b);
            //}
            s.Present(b);
        }
    }
}
