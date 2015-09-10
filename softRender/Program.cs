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
            Buffer<Color4> b = new Buffer<Color4>(300, 300, new Color4(1.0f, 0.0f, 0.0f, 0.0f));
            Buffer<float> zBuffer = new Buffer<float>(300, 300, -1f);
            Rasterization r = new Rasterization();

            List<Vertex[]> vertexs = new List<Vertex[]>();

            //Vertex[] tringleAry = new Vertex[3];
            //tringleAry[0] = new Vertex();
            //tringleAry[0].pos = new SlimDX.Vector4(-150, 0, -10, 1);
            //tringleAry[0].color = new SlimDX.Color4(1.0f, 1.0f, 0.0f, 0.0f);

            //tringleAry[1] = new Vertex();
            //tringleAry[1].pos = new SlimDX.Vector4(150, 0, -10, 1);
            //tringleAry[1].color = new SlimDX.Color4(1.0f, 0.0f, 1.0f, 0.0f);

            //tringleAry[2] = new Vertex();
            //tringleAry[2].pos = new SlimDX.Vector4(0, 150, -10, 1);
            //tringleAry[2].color = new SlimDX.Color4(1.0f, 0.0f, 0.0f, 1.0f);

            //vertexs.Add(tringleAry);

            //tringleAry = new Vertex[3];
            //tringleAry[0] = new Vertex();
            //tringleAry[0].pos = new SlimDX.Vector4(-150, 0, -10, 1);
            //tringleAry[0].color = new SlimDX.Color4(1.0f, 1.0f, 0.0f, 0.0f);

            //tringleAry[1] = new Vertex();
            //tringleAry[1].pos = new SlimDX.Vector4(150, 0, -10, 1);
            //tringleAry[1].color = new SlimDX.Color4(1.0f, 0.0f, 1.0f, 0.0f);

            //tringleAry[2] = new Vertex();
            //tringleAry[2].pos = new SlimDX.Vector4(0, -150, -10, 1);
            //tringleAry[2].color = new SlimDX.Color4(1.0f, 0.0f, 0.0f, 1.0f);

            //vertexs.Add(tringleAry);

            Camera c = new Camera(-10, -100, 300, 300);

            Cube cube = new Cube(new Vector4(-100f, -100f, -100f, 1f), new Vector4(100f, 100.0f, 100f, 1.0f));
            Matrix m = new Matrix();
            //Matrix.Scaling(10f, 10f, 10f, out m);
            Matrix.RotationY((float)Math.PI/4, out m);
            //Matrix.Translation(0.0f, 0.0f, -15.0f, out m);
            //Matrix.RotationYawPitchRoll(0.0f, (float)Math.PI / 4, 0.0f, out m);

            //cube.transform(m);
            vertexs = cube.getVertex();

            foreach (Vertex[] tringles in vertexs)
            {
                for (int i=0; i<3; i++)
                {
                    tringles[i].pos = Vector4.Transform(tringles[i].pos, c.getClipMatrix());
                    tringles[i].pos = new Vector4(tringles[i].pos.X / tringles[i].pos.W, tringles[i].pos.Y / tringles[i].pos.W, tringles[i].pos.Z / tringles[i].pos.W, 1.0f);
                    tringles[i].pos = Vector4.Transform(tringles[i].pos, c.getClipToScreenMatrix());
                }

                r.drawTriange(tringles, b, zBuffer);
            }
            s.Present(b);
        }
    }
}
