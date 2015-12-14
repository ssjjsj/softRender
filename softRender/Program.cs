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


            c.setLookAt(new Vector3(20, 20, 0), new Vector3(-0.1f, 0.5f, 1), new Vector3(0, 1, 0));

            Vertex[] vertexs;
            List<int[]> trianglesIndex;

            ObjPaser p = new ObjPaser();
            List<Pass.PassData> dataList = p.PaserObj("media/cube.obj");

            Model m1 = new Model(dataList);

            Vertex v1 = new Vertex();
            v1.pos = new Vector4(-0.5f, -0.5f, 11f, 0);
            v1.color = new Color4(1.0f, 0.5f, 0.5f, 0.5f);
            Vertex v2 = new Vertex();
            v2.pos = new Vector4(-0.5f, 0.5f, 11f, 0);
            v2.color = new Color4(1.0f, 0.5f, 0.5f, 0.5f);
            Vertex v3 = new Vertex();
            v3.pos = new Vector4(0.5f, -0.5f, 11f, 0);
            v3.color = new Color4(1.0f, 0.5f, 0.5f, 0.5f);
            Vertex v4 = new Vertex();
            v4.pos = new Vector4(0.5f, 0.5f, 11f, 0);
            v4.color = new Color4(1.0f, 0.5f, 0.5f, 0.5f);

            Pass.PassData data = new Pass.PassData();
            data.vertexs = new Vertex[4];
            data.vertexs[0] = v1;
            data.vertexs[1] = v2;
            data.vertexs[2] = v3;
            data.vertexs[3] = v4;

            data.triangleIndexs = new List<int[]>();
            int[] triangle1 = new int[3] { 0, 3, 2 };
            int[] triangle2 = new int[3] { 3, 0, 1 };
            data.triangleIndexs.Add(triangle1);
            data.triangleIndexs.Add(triangle2);

            data.materail = new Material();

            Model m2 = new Model(new List<Pass.PassData>() { data });
            m2.Render(Matrix.Invert(SRDevice.Device.Camera.getViewMatrix()));

            m1.transform.localPosition = new Vector4(0.0f, 0.0f, 10.0f, 0.0f);
            m1.transform.localScale = new Vector4(10.0f, 10.0f, 10.0f, 0.0f);
            //m1.transform.localRotation = new Vector4(0.0f, (float)Math.PI / 4, 0.0f, 0.0f);

            //m1.Render();
            SRDevice.Device.Present();
        }
    }
}
