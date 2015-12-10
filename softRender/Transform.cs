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
            Matrix m0 = Matrix.Scaling(localScale.X, localScale.Y, localScale.Z);
            Matrix m1 = Matrix.RotationX(localRotation.X);
            Matrix m2 = Matrix.RotationY(localRotation.Y);
            Matrix m3 = Matrix.RotationZ(localRotation.Z);

            Matrix m4 = Matrix.Translation(localPosition.X, localPosition.Y, localPosition.Z);

            return m0 * m1 * m2 * m3 * m4;
        }
    }
}
