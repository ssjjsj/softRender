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
            int width = 1024;
            int height = 1024;
            float near = 10f;
            float far = 100.0f;

            SRDevice.Device.Init(width, height);


            Camera c = new Camera(near, far, (float)Math.PI/2,width, height);
            Culler cull = new Culler();
            SRDevice.Device.Camera = c;
            SRDevice.Device.Cull = cull;

            //Cube cube = new Cube(new Vector4(-5f, -5f, -5f, 1f), new Vector4(5f, 5f, 5f, 1.0f));
            //Matrix m1 = Matrix.Identity;
            //Matrix m2 = Matrix.Identity;
            //Matrix m3 = Matrix.Identity;
            //Matrix m = new Matrix();
            //Matrix.Scaling(10f, 10f, 10f, out m1);
            //Matrix.RotationY((float)Math.PI / 4, out m2);
            //Matrix.Translation(0.0f, 0.0f, 125.0f, out m3);
            //m = m1* m2 * m3;

            Vertex[] vertexs;
            List<int[]> trianglesIndex;

            ObjPaser p = new ObjPaser();
            //List<Pass.PassData> dataList = p.PaserObj("media/cube.obj");

            List<Pass.PassData> dataList = new List<Pass.PassData>();
            Pass.PassData passData = new Pass.PassData();

            Cube cube = new Cube(new Vector4(-10, -10, -10, 1), new Vector4(10, 10, 10, 1));
            passData.vertexs = cube.getVertexs();
            passData.triangleIndexs = cube.getTriagngles();
            passData.materail = new Material();
            passData.materail.textureName = "sponza_thorn_diff.bmp";
            dataList.Add(passData);

            List<Pass> renderList = new List<Pass>();
            foreach (Pass.PassData data in dataList)
            {
                Pass pass = new Pass(data);
                renderList.Add(pass);
            }

            //while (true)
            //{
                int i = 0;
                System.Console.WriteLine("render count" + renderList.Count);
                foreach(Pass pass in renderList)
                {
                    System.Console.WriteLine(i);
                    i++;
                    pass.Render();
                }
            //}
            
            SRDevice.Device.Present();
        }
    }
}
