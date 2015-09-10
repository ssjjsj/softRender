using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Transform
    {
        public Vector4 localPosition;
        public Vector4 localRotation;
        public Vector4 localScale;

        private List<Transform> childNodes;
        private Transform parentNode;

        public Matrix getMatrix()
        {
            return Matrix.Identity;
        }
    }
}
