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
            float near = 1f;
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
            List<Pass.PassData> dataList = p.PaserObj("media/cube.obj");

            Model m1 = new Model(dataList);
            Model m2 = new Model(dataList);

            m1.transform.localPosition = new Vector4(0.0f, 0.0f, 50.0f, 0.0f);
            m1.transform.localScale = new Vector4(20.0f, 2.0f, 20.0f, 0.0f);
            m1.transform.localRotation = new Vector4(0.0f, (float)Math.PI / 4, 0.0f, 0.0f);
            
            m2.transform.localPosition = new Vector4(0.0f, 0.0f, 50.0f, 0.0f);
            m2.transform.localScale = new Vector4(10.0f, 10.0f, 10.0f, 0.0f);
            m2.transform.localRotation = new Vector4(0.0f, (float)Math.PI / 4, 0.0f, 0.0f);

            //m1.Render();
            m2.Render();


            
            SRDevice.Device.Present();
        }
    }
}
