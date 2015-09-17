﻿using System;
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
            Surface s = new ImageSurface(300, 300);
            Buffer<Color4> b = new Buffer<Color4>(300, 300, new Color4(1.0f, 1.0f, 1.0f, 1.0f));
            Buffer<float> zBuffer = new Buffer<float>(300, 300, -1f);
            Rasterization r = new Rasterization();



        //    List<Vertex[]> vertexs = new List<Vertex[]>();

        //    //Vertex[] tringleAry = new Vertex[3];
        //    //tringleAry[0] = new Vertex();
        //    //tringleAry[0].pos = new SlimDX.Vector4(-150, 0, -10, 1);
        //    //tringleAry[0].color = new SlimDX.Color4(1.0f, 1.0f, 0.0f, 0.0f);

        //    //tringleAry[1] = new Vertex();
        //    //tringleAry[1].pos = new SlimDX.Vector4(150, 0, -10, 1);
        //    //tringleAry[1].color = new SlimDX.Color4(1.0f, 0.0f, 1.0f, 0.0f);

        //    //tringleAry[2] = new Vertex();
        //    //tringleAry[2].pos = new SlimDX.Vector4(0, 150, -10, 1);
        //    //tringleAry[2].color = new SlimDX.Color4(1.0f, 0.0f, 0.0f, 1.0f);

        //    //vertexs.Add(tringleAry);

        //    //tringleAry = new Vertex[3];
        //    //tringleAry[0] = new Vertex();
        //    //tringleAry[0].pos = new SlimDX.Vector4(-150, 0, -10, 1);
        //    //tringleAry[0].color = new SlimDX.Color4(1.0f, 1.0f, 0.0f, 0.0f);

        //    //tringleAry[1] = new Vertex();
        //    //tringleAry[1].pos = new SlimDX.Vector4(150, 0, -10, 1);
        //    //tringleAry[1].color = new SlimDX.Color4(1.0f, 0.0f, 1.0f, 0.0f);

        //    //tringleAry[2] = new Vertex();
        //    //tringleAry[2].pos = new SlimDX.Vector4(0, -150, -10, 1);
        //    //tringleAry[2].color = new SlimDX.Color4(1.0f, 0.0f, 0.0f, 1.0f);

        //    //vertexs.Add(tringleAry);

            Camera c = new Camera(-10, -1000, (float)Math.PI/2,300, 300);

            Cube cube = new Cube(new Vector4(-5f, -5f, -20f, 1f), new Vector4(5f, 5.0f, -30f, 1.0f));
            Matrix m = new Matrix();
            //Matrix.Scaling(0.5f, 0.5f, 0.5f, out m);
            //Matrix.RotationZ((float)Math.PI / 4, out m);
            Vector3 v = new Vector3(0f, 0f, -25f);
            Matrix.RotationAxis(ref v, (float)Math.PI / 10, out m);
            //Matrix.Translation(0.0f, 0.0f, -15.0f, out m);
            //Matrix.RotationYawPitchRoll(0.0f, (float)Math.PI / 4, 0.0f, out m);



            //cube.transform(m);

            Vertex[] vertexs = cube.getVertexs();
            List<Vertex[]> lines = cube.getLines();
            List<Vertex[]> triangles = cube.getTriagngles();

            //Vertex[] vertexs = new Vertex[8];
            //vertexs[0] = new Vertex();
            //vertexs[0].pos = new Vector4(0, 25, 10, 1);
            //vertexs[0].color = new Color4(1.0f, 1.0f, 0.0f, 0.0f);

            //vertexs[1] = new Vertex();
            //vertexs[1].pos = new Vector4(25, 25, 35, 1);
            //vertexs[1].color = new Color4(1.0f, 1.0f, 0.0f, 0.0f);

            //vertexs[2] = new Vertex();
            //vertexs[2].pos = new Vector4(0, 25, 60, 1);
            //vertexs[2].color = new Color4(1.0f, 1.0f, 0.0f, 0.0f);

            //vertexs[3] = new Vertex();
            //vertexs[3].pos = new Vector4(-25, 25, 35, 1);
            //vertexs[3].color = new Color4(1.0f, 1.0f, 0.0f, 0.0f);

            //vertexs[4] = new Vertex();
            //vertexs[4].pos = new Vector4(0, -25, 10, 1);
            //vertexs[4].color = new Color4(1.0f, 0.0f, 1.0f, 0.0f);

            //vertexs[5] = new Vertex();
            //vertexs[5].pos = new Vector4(25, -25, 35, 1);
            //vertexs[5].color = new Color4(1.0f, 0.0f, 1.0f, 0.0f);

            //vertexs[6] = new Vertex();
            //vertexs[6].pos = new Vector4(0, -25, 60, 1);
            //vertexs[6].color = new Color4(1.0f, 0.0f, 1.0f, 0.0f);

            //vertexs[7] = new Vertex();
            //vertexs[7].pos = new Vector4(-25, -25, 35, 1);
            //vertexs[7].color = new Color4(1.0f, 0.0f, 1.0f, 0.0f);

            //Vertex v1 = vertexs[0];
            //Vertex v2 = vertexs[1];
            //Vertex v3 = vertexs[2];
            //Vertex v4 = vertexs[3];
            //Vertex v5 = vertexs[4];
            //Vertex v6 = vertexs[5];
            //Vertex v7 = vertexs[6];
            //Vertex v8 = vertexs[7];

            //List<Vertex[]> lines = new List<Vertex[]>();
            //lines.Add(new Vertex[2] { v1, v2 });
            //lines.Add(new Vertex[2] { v2, v3 });
            //lines.Add(new Vertex[2] { v3, v4 });
            //lines.Add(new Vertex[2] { v4, v1 });

            //lines.Add(new Vertex[2] { v5, v6 });
            //lines.Add(new Vertex[2] { v6, v7 });
            //lines.Add(new Vertex[2] { v7, v8 });
            //lines.Add(new Vertex[2] { v8, v5 });

            //lines.Add(new Vertex[2] { v1, v5 });
            //lines.Add(new Vertex[2] { v2, v6 });
            //lines.Add(new Vertex[2] { v3, v7 });
            //lines.Add(new Vertex[2] { v4, v8 });

            for (int i = 0; i < vertexs.Length; i++)
            {
                vertexs[i].pos = mul(vertexs[i].pos, m);
                vertexs[i].pos = mul(vertexs[i].pos, c.getClipMatrix());
                vertexs[i].pos = new Vector4(vertexs[i].pos.X / vertexs[i].pos.W, vertexs[i].pos.Y / vertexs[i].pos.W, vertexs[i].pos.Z / vertexs[i].pos.W, 1.0f);
                vertexs[i].pos = mul(vertexs[i].pos, c.getClipToScreenMatrix());
            }

            foreach (Vertex[] line in lines)
            {
                r.drawLine(line, b);
            }

            s.Present(b);
        }
    }
}
