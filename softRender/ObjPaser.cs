﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace softRender
{
    class ObjPaser
    {
        List<SlimDX.Vector4> posList = new List<SlimDX.Vector4>();
        List<SlimDX.Vector2> uvList = new List<SlimDX.Vector2>();
        List<SlimDX.Vector4> normalList = new List<SlimDX.Vector4>();

        private void PaserObjSingle(IEnumerable<string> lines, out Vertex[] vertexs, out List<int[]> triangleIndexs)
        {
            List<Vertex> vertexList = new List<Vertex>();
            triangleIndexs = new List<int[]>();
            foreach (string line in lines)
            {
                if (line.StartsWith("#"))
                {

                }
                else if (line.StartsWith("v"))
                {
                    string temp;
                    temp = line.Substring(3, line.Length - 3);
                    string[] tempAry = temp.Split(new char[1] { ' ' });
                    SlimDX.Vector4 pos = new SlimDX.Vector4();
                    pos.X = System.Convert.ToSingle(tempAry[0]);
                    pos.Y = System.Convert.ToSingle(tempAry[1]);
                    pos.Z = System.Convert.ToSingle(tempAry[2]);
                    pos.W = 1.0f;

                    posList.Add(pos);
                }
                else if (line.StartsWith("vt"))
                {
                    string temp;
                    temp = line.Substring(3, line.Length - 3);
                    string[] tempAry = temp.Split(new char[1] { ' ' });
                    SlimDX.Vector2 uv = new SlimDX.Vector2();
                    uv.X = System.Convert.ToSingle(tempAry[0]);
                    uv.Y = System.Convert.ToSingle(tempAry[1]);

                    uvList.Add(uv);
                }
                else if (line.StartsWith("vn"))
                {
                    string temp;
                    temp = line.Substring(3, line.Length - 3);
                    string[] tempAry = temp.Split(new char[1] { ' ' });
                    SlimDX.Vector4 normal = new SlimDX.Vector4();
                    normal.X = System.Convert.ToSingle(tempAry[0]);
                    normal.Y = System.Convert.ToSingle(tempAry[1]);
                    normal.Z = System.Convert.ToSingle(tempAry[2]);

                    normalList.Add(normal);
                }
                else if (line.StartsWith("f"))
                {
                    string temp;
                    temp = line.Substring(2, line.Length - 2);
                    string[] tempAry = temp.Split(new char[1] { ' ' });

                    int[] indexs = new int[3];
                    for (int i=0; i<tempAry.Length; i++)
                    {
                        string s = tempAry[i];
                        Vertex v = new Vertex();
                        string[] IndexAry = s.Split(new char[1] { '/' });
                        indexs[i] = Convert.ToInt32(IndexAry[0]) - 1;
                        if (IndexAry.Length == 1)
                        {
                            int posIndex = Convert.ToInt32(IndexAry[0]) - 1;
                            v.pos = posList[posIndex];
                        }
                        else if (IndexAry.Length == 2)
                        {
                            int posIndex = Convert.ToInt32(IndexAry[0]) - 1;
                            int uvIndex = Convert.ToInt32(IndexAry[1]) - 1;
                            v.pos = posList[posIndex];
                            v.uv = uvList[uvIndex];
                        }
                        else if (IndexAry.Length == 3)
                        {
                            int posIndex = Convert.ToInt32(IndexAry[0]) - 1;
                            int uvIndex = Convert.ToInt32(IndexAry[1]) - 1;
                            int normalIndex = Convert.ToInt32(IndexAry[2]) - 1;
                            v.pos = posList[posIndex];
                            v.uv = uvList[uvIndex];
                        }

                        vertexList.Add(v);
                        triangleIndexs.Add(indexs);
                    }
                }
            }

            vertexs = vertexList.ToArray();
        }

        public void  PaserObj(string name, out Vertex[] vertexs, out List<int[]> triangleIndexs)
        {
            System.IO.File.ReadLines(name)
            {

            }
        }
    }
}