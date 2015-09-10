using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Surface
    {
        protected int width;
        protected int height;
        public Surface(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public virtual void Present(Buffer<Color4> buf)
        {

        }

        public virtual void Clear()
        {

        }
    }
}
