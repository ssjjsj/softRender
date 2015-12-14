using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softRender
{
    class Model
    {
        private List<Pass> renderPass = new List<Pass>();
        public Transform transform = new Transform();
        public Model(List<Pass.PassData> dataList)
        {
            foreach (Pass.PassData data in dataList)
            {
                Pass p = new Pass(data);
                renderPass.Add(p);
            }
        }

        public void Render()
        {
            foreach (Pass p in renderPass)
                p.Render(transform.getMatrix());
        }

        public void Render(SlimDX.Matrix m)
        {
            foreach (Pass p in renderPass)
                p.Render(m);
        }
    }
}
