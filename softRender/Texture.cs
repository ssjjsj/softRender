using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace softRender
{
    class Texture
    {
        public virtual SlimDX.Color4 getPixel(float u, float v)
        {
            return new SlimDX.Color4();
        }

        public virtual void Release()
        {

        }

        public static Texture LoadImage(string name)
        {
            BitmapTexture t = new BitmapTexture(name);
            return t;
        }
    }



    class BitmapTexture : Texture
    {
        Bitmap image;
        public BitmapTexture(string name)
        {
            image = new Bitmap(name, true);
        }

        public override void Release()
        {
            image.Dispose();
        }

        public override SlimDX.Color4 getPixel(float u, float v)
        {
            u = u % 1;
            v = v % 1;

            if (u > 1.0f)
                u = u - 1.0f;

            if (v > 1.0f)
                v = v - 1.0f;

            if (u < 0.0f)
                u = 1.0f + u;

            if (v < 0.0f)
                v = 1.0f + v;


            int x = (int)(image.Width * u)%image.Width;
            int y = (int)(image.Height * v)%image.Height;

            Color c = image.GetPixel(x, y);
            return new SlimDX.Color4((float)c.A / 255, (float)c.R / 255, (float)c.G / 255, (float)c.B / 255);
        }
    }
}
