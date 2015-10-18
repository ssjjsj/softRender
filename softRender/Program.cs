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
        public static Vector4 mul(Vector4 v, Matrix m)
        {
            Vector4 outPut = new Vector4();
            outPut.X = v.X * m.M11 + v.Y * m.M21 + v.Z * m.M31 + v.W * m.M41;
            outPut.Y = v.X * m.M12 + v.Y * m.M22 + v.Z * m.M32 + v.W * m.M42;
            outPut.Z = v.X * m.M13 + v.Y * m.M23 + v.Z * m.M33 + v.W * m.M43;
            outPut.W = v.X * m.M14 + v.Y * m.M24 + v.Z * m.M34 + v.W * m.M44;

            return outPut;
        }
        static void Main(string[] args)
        {
            int width = 300;
            int height = 300;
            int near = -10;
            int far = -1000;
            Surface s = new ImageSurface(300, 300);
            Buffer<Color4> b = new Buffer<Color4>(300, 300, new Color4(1.0f, 1.0f, 1.0f, 1.0f));
            Buffer<float> zBuffer = new Buffer<float>(300, 300, 1.0f);
            Rasterization r = new Rasterization();


            Camera c = new Camera(10, 1000, (float)Math.PI/2,300, 300);
            Culler cull = new Culler();

            Cube cube = new Cube(new Vector4(-5f, -5f, -5f, 1f), new Vector4(5f, 5f, 5f, 1.0f));
            Matrix m1 = Matrix.Identity;
            Matrix m2 = Matrix.Identity;
            Matrix m3 = Matrix.Identity;
            Matrix m = new Matrix();
            Matrix.Scaling(10f, 10f, 10f, out m1);
            Matrix.RotationY((float)Math.PI / 4, out m2);
            Matrix.Translation(0.0f, 0.0f, 125.0f, out m3);
            m = m1* m2 * m3;

            Vertex[] vertexs;
            List<int[]> trianglesIndex;

            ObjPaser p = new ObjPaser();
            p.PaserObj("media/sponza.obj",  out vertexs, out trianglesIndex);

            Texture t = Texture.LoadImage("seafloor.jpg");

            //Vertex[] vertexs = cube.getVertexs();
            //List<int[]> lines = cube.getLines();
            //List<int[]> trianglesIndex = cube.getTriagngles();

            for (int i = 0; i < vertexs.Length; i++)
            {
                vertexs[i].pos = mul(vertexs[i].pos, m);
                vertexs[i].pos = Vector4.Transform(vertexs[i].pos, c.getClipMatrix());
                vertexs[i].pos = new Vector4(vertexs[i].pos.X / vertexs[i].pos.W, vertexs[i].pos.Y / vertexs[i].pos.W, vertexs[i].pos.Z / vertexs[i].pos.W, 1.0f);
            }

            Culler.CullPlane plane = new Culler.CullPlane();
            cull.CullTriangles(ref vertexs, ref trianglesIndex, plane);

            for (int i = 0; i < vertexs.Length; i++)
            {
                vertexs[i].pos = mul(vertexs[i].pos, c.getClipToScreenMatrix());
            }

            while (true)
            {
                int i = 0;
                foreach (int[] triangle in trianglesIndex)
                {
                    //r.drawLine(vertexs, line, b);
                    r.drawTriange(vertexs, triangle, b, zBuffer, t);
                    i++;
                }
                s.Present(b);
            }
        }
    }
}
