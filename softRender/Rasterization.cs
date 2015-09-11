using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Rasterization
    {
        public void drawTriange(Vertex[] vertexList, Buffer<Color4> outPutBuffer, Buffer<float> zBuffer)
        {
            float tMinX = vertexList[0].pos.X;
            float tMinY = vertexList[0].pos.Y;
            float tMaxX = vertexList[0].pos.X;
            float tMaxY = vertexList[0].pos.Y;

            for (int i=0; i<3; i++)
            {
                if (vertexList[i].pos.X < tMinX)
                    tMinX = vertexList[i].pos.X;

                if (vertexList[i].pos.Y < tMinY)
                    tMinY = vertexList[i].pos.Y;

                if (vertexList[i].pos.X > tMaxX)
                    tMaxX = vertexList[i].pos.X;

                if (vertexList[i].pos.Y > tMaxY)
                    tMaxY = vertexList[i].pos.Y;
            }


            int minX = (int)Math.Floor(tMinX); 
            int maxX = (int)Math.Ceiling(tMaxX);
            int minY = (int)Math.Floor(tMinY);
            int maxY = (int)Math.Ceiling(tMaxY);

            for (int posX=minX; posX<=maxX; posX++)
            {
                for (int posY=minY; posY<=maxY; posY++)
                {
                    float posX0 = vertexList[0].pos.X;
                    float posX1 = vertexList[1].pos.X;
                    float posX2 = vertexList[2].pos.X;
                    float posY0 = vertexList[0].pos.Y;
                    float posY1 = vertexList[1].pos.Y;
                    float posY2 = vertexList[2].pos.Y;

                    float index0 = ((posY1 - posY2) * posX + (posX2 - posX1) * posY + posX1 * posY2 - posX2 * posY1) /
                        ((posY1 - posY2) * posX0 + (posX2 - posX1) * posY0 + posX1 * posY2 - posX2 * posY1);

                    float index1 = ((posY2 - posY0) * posX + (posX0 - posX2) * posY + posX2 * posY0 - posX0 * posY2) /
                        ((posY2 - posY0) * posX1 + (posX0 - posX2) * posY1 + posX2 * posY0 - posX0 * posY2);

                    float index2 = ((posY0 - posY1) * posX + (posX1 - posX0) * posY + posX0 * posY1 - posX1 * posY0)/
                        ((posY0 - posY1) * posX2 + (posX1 - posX0) * posY2 + posX0 * posY1 - posX1 * posY0);

                    if (index0>0 && index1>0 && index2>0)
                    {
                        Color4 c = new Color4();
                        c.Alpha = vertexList[0].color.Alpha * index0 + vertexList[1].color.Alpha * index1 + vertexList[2].color.Alpha * index2;
                        c.Red = vertexList[0].color.Red * index0 + vertexList[1].color.Red * index1 + vertexList[2].color.Red * index2;
                        c.Green = vertexList[0].color.Green * index0 + vertexList[1].color.Green * index1 + vertexList[2].color.Green * index2;
                        c.Blue = vertexList[0].color.Blue * index0 + vertexList[1].color.Blue * index1 + vertexList[2].color.Blue * index2;
                        float z = vertexList[0].pos.Z*index0 + vertexList[1].pos.Z*index1 + vertexList[2].pos.Z*index2;

                        float curZ = zBuffer.readOneData(posX, posY);
                        if (z > curZ)
                        {
                            zBuffer.writeOneData(posX, posY, z);
                            outPutBuffer.writeOneData(posX, posY, c);
                        }
                    }
                }
            }
        }


        public void drawLine(Vertex[] line, Buffer<Color4> outPut)
        {
            Color4 c = new Color4(1.0f, 1.0f, 0.0f, 0.0f);
            int posX1 = (int)line[0].pos.X;
            int posY1 = (int)line[0].pos.Y;
            int posX2 = (int)line[1].pos.X;
            int posY2 = (int)line[1].pos.Y;

            float k = ((float)(posY2 - posY1)) / ((float)(posX2 - posX1));
            float d = posY1-k*posX1;
            if (k>=1)
            {
                int posX = posX1;
                for (int posY=posY1; posY<=posY2; posY++)
                {
                    outPut.writeOneData(posX, posY, c);
                    if (posY+1-k*(posX+0.5)+d>0)
                    {
                        posX = posX + 1;
                    }
                }
            }
            else if (k>0 && k<1)
            {
                int posY = posY1;
                for (int posX=posX1; posX<=posX2; posX++)
                {
                    outPut.writeOneData(posX, posY, c);
                    if (posY+0.5-k*(posX+1)+d<0)
                    {
                        posY = posY + 1;
                    }
                }
            }
            else if (k<0 && k>-1)
            {
                int posY = posY1;
                for (int posX = posX1; posX <= posX2; posX++)
                {
                    outPut.writeOneData(posX, posY, c);
                    if (posY + 0.5 - k * (posX + 1) + d > 0)
                    {
                        posY = posY - 1;
                    }
                }
            }
            else if (k<= -1)
            {
                int posX = posX1;
                for (int posY = posY1; posY >= posY2; posY--)
                {
                    outPut.writeOneData(posX, posY, c);
                    if (posY-1 - k * (posX + 0.5) + d > 0)
                    {
                        posX = posX + 1;
                    }
                }
            }
        }
    }
}
