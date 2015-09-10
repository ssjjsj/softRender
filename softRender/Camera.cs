using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace softRender
{
    class Camera
    {
        private float width;
        private float height;
        private float aspect;
        private float viewField;
        private float near;
        private float far;
        private Transform camTransform;

        public Camera(float near, float far, float width, float height)
        {
            this.aspect = (float)width/(float)height;
            this.near = near;
            this.far = far;
            this.width = width;
            this.height = height;
        }

        public Matrix getClipMatrix()
        {
            Matrix m = new Matrix();
            m.M34 = -1;
            m.M11 = -2 * near / width;
            m.M22 = -2*near/height;
            m.M33 = (far + near) / (far - near);
            m.M43 = -2 * far * near / (far - near);
            return m;
        }

        public Matrix getClipToScreenMatrix()
        {
            Matrix m = new Matrix();
            m.M11 = width / 2;
            m.M22 = height / 2;
            m.M41 = (width-1) / 2;
            m.M42 = (height-1) / 2;
            m.M44 = 1;

            return m;
        }
    }
}
