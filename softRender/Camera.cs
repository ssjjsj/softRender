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

        public Camera(float near, float far, float fov, float width, float height)
        {
            this.aspect = (float)width/(float)height;
            this.near = near;
            this.far = far;
            this.width = width;
            this.height = height;
            this.viewField = fov;
        }

        public Matrix getClipMatrix()
        {
            Matrix m = new Matrix();
            
            float h, w, Q;
            w = (float)1 / (float)Math.Tan(viewField * 0.5f);
            h = w / aspect;
            Q = far/(far - near);

            m.M11 = w;
            m.M22 = h;
            m.M33 = Q;
            m.M43 = -Q * near;
            m.M34 = 1;

            return m;
        }

        public Matrix getClipToScreenMatrix()
        {
            Matrix m = new Matrix();
            m.M11 = -width / 2;
            m.M22 = -height / 2;
            m.M41 = (width-1) / 2;
            m.M42 = (height-1) / 2;
            m.M33 = 1;
            m.M44 = 1;

            return m;
        }
    }
}
