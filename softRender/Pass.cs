using System;
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

        public void Render(Matrix m)
        {
            VertexBuffer vb = new VertexBuffer(data.vertexs);
            IndexBuffer ib = new IndexBuffer(data.triangleIndexs);
            Vertex[] vertexs = vb.getData();
            List<int[]> triangleIndexs = ib.getData();
            Camera c = SRDevice.Device.Camera;

            for (int i = 0; i < vertexs.Length; i++)
            {
                vertexs[i].color = new Color4(1.0f, 0.0f, 0.0f);
                vertexs[i].pos = Vector4.Transform(vertexs[i].pos, m);
            }

            foreach (int[] triangle in triangleIndexs.ToArray())
            {

                Vector4 v1 = vertexs[triangle[0]].pos - vertexs[triangle[1]].pos;
                Vector4 v2 = vertexs[triangle[2]].pos - vertexs[triangle[1]].pos;

                Vector3 n = Vector3.Cross(new Vector3(v1.X, v1.Y, v1.Z), new Vector3(v2.X, v2.Y, v2.Z));

                if (Vector3.Dot(n, new Vector3(0, 0, 1)) < 0)
                    triangleIndexs.Remove(triangle);
            }

            System.Console.WriteLine("start cull" + System.DateTime.Now);
            Culler.CullPlane plane = new Culler.CullPlane();
            Culler.CullResult result = SRDevice.Device.Cull.CullTriangles(vertexs, triangleIndexs, plane);

            vertexs = result.vertexs;
            triangleIndexs = result.trianglesIndex;


            for (int i = 0; i < vertexs.Length; i++)
            {
                vertexs[i].pos = Vector4.Transform(vertexs[i].pos, c.getClipMatrix());
                vertexs[i].pos = new Vector4(vertexs[i].pos.X / vertexs[i].pos.W, vertexs[i].pos.Y / vertexs[i].pos.W, vertexs[i].pos.Z / vertexs[i].pos.W, 1.0f);
            }



            for (int i = 0; i < vertexs.Length; i++)
            {
                vertexs[i].pos = Vector4.Transform(vertexs[i].pos, c.getClipToScreenMatrix());
            }

            List<int[]> lines = new List<int[]>();
            foreach (int[] tringle in triangleIndexs)
            {
                int[] line1 = new int[2];
                line1[0] = tringle[0];
                line1[1] = tringle[1];

                int[] line2 = new int[2];
                line2[0] = tringle[1];
                line2[1] = tringle[2];

                int[] line3 = new int[2];
                line3[0] = tringle[2];
                line3[1] = tringle[0];

                int[] temp1 = lines.Find(x => x.Contains(line1[0]) && x.Contains(line1[1]));
                if (temp1 == null)
                    lines.Add(line1);
                else
                    lines.Remove(temp1);

                int[] temp2 = lines.Find(x => x.Contains(line2[0]) && x.Contains(line2[1]));
                if (temp2 == null)
                    lines.Add(line2);
                else
                    lines.Remove(temp2);

                int[] temp3 = lines.Find(x => x.Contains(line3[0]) && x.Contains(line3[1]));
                if (temp3 == null)
                    lines.Add(line3);
                else
                    lines.Remove(temp3);
            }


            //SRDevice.Device.drawLine(vertexs, lines);



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
