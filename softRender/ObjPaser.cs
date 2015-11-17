using System;
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

        public List<Pass.PassData> PaserObj(string objName)
        {
            List<Pass.PassData> datalist = new List<Pass.PassData>();
            Dictionary<string, Material> matDic = new Dictionary<string,Material>();

            foreach (string line in System.IO.File.ReadAllLines(objName))
            {
                if (line.StartsWith("mtllib"))
                {
                    string matLibName = line.Substring(7);
                    matDic = parseMat("media/" + matLibName);
                }
                else if (line.StartsWith("vt"))
                {
                    string[] tempAry = line.Split(new char[1] { ' ' });
                    tempAry = Array.FindAll<string>(tempAry, x =>
                        {
                            float temp;
                            return float.TryParse(x, out temp);
                        });
                    SlimDX.Vector2 uv = new SlimDX.Vector2();
                    uv.X = System.Convert.ToSingle(tempAry[0]);
                    uv.Y = 1.0f - System.Convert.ToSingle(tempAry[1]);

                    uvList.Add(uv);
                }
                else if (line.StartsWith("vn"))
                {
                    string[] tempAry = line.Split(new char[1] { ' ' });
                    tempAry = Array.FindAll<string>(tempAry, x =>
                        {
                            float temp;
                            return float.TryParse(x, out temp);
                        });
                    SlimDX.Vector4 normal = new SlimDX.Vector4();
                    normal.X = System.Convert.ToSingle(tempAry[0]);
                    normal.Y = System.Convert.ToSingle(tempAry[1]);
                    normal.Z = System.Convert.ToSingle(tempAry[2])-1.0f;

                    normalList.Add(normal);
                }
                else if (line.StartsWith("v"))
                {
                    string[] tempAry = line.Split(new char[1] { ' ' });
                    tempAry = Array.FindAll<string>(tempAry, x =>
                        {
                            float temp;
                            return float.TryParse(x, out temp);
                        });
                    SlimDX.Vector4 pos = new SlimDX.Vector4();
                    pos.X = System.Convert.ToSingle(tempAry[0]);
                    pos.Y = System.Convert.ToSingle(tempAry[1]);
                    pos.Z = System.Convert.ToSingle(tempAry[2])*(-1.0f);
                    pos.W = 1.0f;

                    posList.Add(pos);
                }
            }


            List<Vertex> vertexList = new List<Vertex>();
            List<int[]> triangleIndexList = new List<int[]>();
            Material m = null;
            Pass.PassData data = null;

            Dictionary<string, int> cache = new Dictionary<string, int>();

            int dataCount = 0;
            foreach (string line in System.IO.File.ReadAllLines(objName))
            {
                if (line.StartsWith("usemtl"))
                {
                    if (data != null)
                    {
                        data.triangleIndexs = triangleIndexList;
                        data.vertexs = vertexList.ToArray();
                        data.materail = m;
                        datalist.Add(data);
                        dataCount++;
                        System.Console.WriteLine(dataCount);
                    }
                    data = new Pass.PassData();
                    string[] tempAry = line.Split(new char[1] { ' ' });
                    string matName = tempAry[1];
                    m = matDic[matName];
                    vertexList = new List<Vertex>();
                    triangleIndexList = new List<int[]>();

                    cache.Clear();
                }
                else if (line.StartsWith("f"))
                {
                    string[] tempAry = line.Split(new char[1] { ' ' });

                    int[] indexs = new int[3];

                    for (int i=3; i>=1; i--)
                    {
                        string s = tempAry[i];
                        if (string.IsNullOrWhiteSpace(s) || string.IsNullOrEmpty(s))
                            continue;
     
                        string[] IndexAry = s.Split(new char[1] { '/' });
                        if (IndexAry.Length == 1)
                        {
                            int posIndex = Convert.ToInt32(IndexAry[0]) - 1;
                            string cacheString = "pos" + posIndex.ToString();
                            if (cache.ContainsKey(cacheString))
                            {
                                int index = cache[cacheString];
                                indexs[i] = index;
                            }
                            else
                            {
                                Vertex newVertex = new Vertex();
                                newVertex.pos = posList[posIndex];
                                vertexList.Add(newVertex);
                                indexs[i] = vertexList.Count - 1;
                                cache[cacheString] = vertexList.Count - 1;
                            }
                            //triangleIndexList.Add(indexs);
                        }
                        else if (IndexAry.Length == 2)
                        {
                            int posIndex = Convert.ToInt32(IndexAry[0]) - 1;
                            int uvIndex = Convert.ToInt32(IndexAry[1]) - 1;
                            string cacheString = "pos" + posIndex.ToString() + "uv" + uvIndex.ToString();
                            if (cache.ContainsKey(cacheString))
                            {
                                int index = cache[cacheString];
                                indexs[i] = index;
                            }
                            else
                            {
                                Vertex newVertex = new Vertex();
                                newVertex.pos = posList[posIndex];
                                newVertex.uv = uvList[uvIndex];
                                vertexList.Add(newVertex);
                                indexs[i] = vertexList.Count - 1;
                                cache[cacheString] = vertexList.Count - 1;
                            }
                            //triangleIndexList.Add(indexs);
                        }
                        else if (IndexAry.Length == 3)
                        {
                            int posIndex = Convert.ToInt32(IndexAry[0]) - 1;
                            int uvIndex = Convert.ToInt32(IndexAry[1]) - 1;
                            int normalIndex = Convert.ToInt32(IndexAry[2]) - 1;
                            string cacheString = "pos" + posIndex.ToString() + "uv" + uvIndex.ToString() + "normal"+normalIndex.ToString();
                            if (cache.ContainsKey(cacheString))
                            {
                                int index = cache[cacheString];
                                indexs[i-1] = index;
                            }
                            else
                            {
                                Vertex newVertex = new Vertex();
                                newVertex.pos = posList[posIndex];
                                newVertex.uv = uvList[uvIndex];
                                newVertex.normal = normalList[normalIndex];
                                vertexList.Add(newVertex);
                                indexs[i-1] = vertexList.Count - 1;
                                cache[cacheString] = vertexList.Count - 1;
                            }
                        }
                    }
                    int index1 = indexs[0];
                    int index2 = indexs[1];
                    int index3 = indexs[2];

                    indexs[0] = index3;
                    indexs[1] = index2;
                    indexs[2] = index1;
                    triangleIndexList.Add(indexs);
                }
            }

            if (data != null)
            {
                data.triangleIndexs = triangleIndexList;
                data.vertexs = vertexList.ToArray();
                data.materail = m;
                datalist.Add(data);
            }

            return datalist;
        }

        Dictionary<string, Material> parseMat(string name)
        {
            Dictionary<string, Material> matDic = new Dictionary<string, Material>();
            Material m = null;
            foreach (string line in System.IO.File.ReadLines(name))
            {
                if (line.StartsWith("newmtl"))
                {
                    string[] tempAry = line.Split(new char[1] { ' ' });
                    string matName = tempAry[1];
                    m = new Material();
                    matDic[matName] = m;
                }
                else if (line.StartsWith("map_Kd"))
                {
                    string[] tempAry = line.Split(new char[1] { ' ' });
                    string texName = tempAry[1];
                    m.textureName = texName;
                }
            }

            return matDic;
        }
    }
}
