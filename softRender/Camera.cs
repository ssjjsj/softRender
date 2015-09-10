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
        private int width;
        private int height;
        private float aspect;
        private float viewField;
        private float near;
        private float far;
        private Transform camTransform;

        public Camera(float viewField, float aspect, float near, float far, int width, int height)
        {
            this.aspect = aspect;
            this.viewField = viewField;
            this.near = near;
            this.far = far;
            this.width = width;
            this.height = height;
        }

        public Matrix getClipMatrix()
        {
            Matrix m = new Matrix();
            m.M43 = -1;
            m.M11 = (float)(1 / Math.Tan(viewField / 2));
            m.M22 = (float)(aspect / Math.Tan(viewField / 2));
            m.M33 = (far + near) / (far - near);
            m.M34 = -2 * far * near / (far - near);
            m.Invert();
            return m;
        }

        public Matrix getClipToScreenMatrix()
        {
            Matrix m = new Matrix();
            m.M11 = width / 2;
            m.M22 = height / 2;
            m.M41 = (width) / 2;
            m.M42 = (height) / 2;
            m.M44 = 1;

            return m;
        }
    }
}
