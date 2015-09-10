using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SlimDX;

namespace softRender
{
    class ImageSurface : Surface
    {
        Bitmap bitmap;
        public ImageSurface(int width, int height)
            : base(width, height)
        {
             bitmap = new Bitmap(width, height);
        }

        public override void Present(Buffer<Color4> buf)
        {
            Graphics g = Graphics.FromImage(bitmap);

            for (int x = 0; x < width; x++)
            {
                for (int y=0; y<height; y++)
                {
                    Color4 data = buf.readOneData(x, y);
                    Color c = Color.FromArgb((int)(data.Alpha*255), (int)(data.Red*255), (int)(data.Green*255), (int)(data.Blue*255));
                    g.DrawRectangle(new Pen(c), x, height-y, 1, 1);
                }
            }

            g.Save();
            g.Dispose();
            bitmap.Save("dd.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
