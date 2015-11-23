﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Pass
    {
        public Pass(PassData data)
        {
            this.data = data;
        }

        private PassData data;
        public class PassData
        {
            public Vertex[] vertexs;
            public List<int[]> triangleIndexs;
            public Material materail;
        }

        public void Render()
        {
            Vertex[] vertexs = data.vertexs;
            List<int[]> triangleIndexs = data.triangleIndexs;
            Camera c = SRDevice.Device.Camera;
            Matrix m2 = Matrix.Translation(0.0f, 0.0f, 50.0f);
            Matrix m1 = Matrix.RotationY((float)Math.PI/4*3);
            //Matrix m2 = Matrix.Scaling(new Vector3(0.1f, 0.1f, 0.1f));
            Matrix m = m1*m2;

            for (int i = 0; i < vertexs.Length; i++)
            {
                vertexs[i].pos = Vector4.Transform(vertexs[i].pos, m);
            }

            //foreach (int[] triangle in triangleIndexs.ToArray())
            //{

            //    Vector4 v1 = vertexs[triangle[0]].pos - vertexs[triangle[1]].pos;
            //    Vector4 v2 = vertexs[triangle[2]].pos - vertexs[triangle[1]].pos;

            //    Vector3 n = Vector3.Cross(new Vector3(v1.X, v1.Y, v1.Z), new Vector3(v2.X, v2.Y, v2.Z));

            //    if (Vector3.Dot(n, new Vector3(0, 0, 1)) < 0)
            //        triangleIndexs.Remove(triangle);
            //}

            //System.Console.WriteLine("start cull" + System.DateTime.Now);
            //Culler.CullPlane plane = new Culler.CullPlane();
            //Culler.CullResult result = SRDevice.Device.Cull.CullTriangles(vertexs, triangleIndexs, plane);

            //vertexs = result.vertexs;
            //triangleIndexs = result.trianglesIndex;


            for (int i = 0; i < vertexs.Length; i++)
            {
                vertexs[i].pos = Vector4.Transform(vertexs[i].pos, c.getClipMatrix());
                vertexs[i].pos = new Vector4(vertexs[i].pos.X / vertexs[i].pos.W, vertexs[i].pos.Y / vertexs[i].pos.W, vertexs[i].pos.Z / vertexs[i].pos.W, 1.0f);
            }



            for (int i = 0; i < vertexs.Length; i++)
            {
                vertexs[i].pos = Vector4.Transform(vertexs[i].pos, c.getClipToScreenMatrix());
            }

            System.Console.WriteLine("start drawTriangle" + System.DateTime.Now);
            if (!string.IsNullOrEmpty(data.materail.textureName))
            {
                System.Console.WriteLine(data.materail.textureName);
                SRDevice.Device.CurTexture = Texture.LoadImage(data.materail.textureName);
            }
            SRDevice.Device.drawTriangle(vertexs, triangleIndexs);
            if (SRDevice.Device.CurTexture != null)
            {
                SRDevice.Device.CurTexture.Release();
                SRDevice.Device.CurTexture = null;
            }
        }
    }
}
