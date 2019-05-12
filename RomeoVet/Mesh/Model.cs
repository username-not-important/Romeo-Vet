using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelixToolkit.Wpf.SharpDX;

namespace RomeoVet.Mesh
{
    public class Model
    {
        public Geometry3D Geometry { get; set; }
        public Material Material { get; set; }

        public Model()
        {
            
        }

        public Model(Geometry3D geometry, Material material)
        {
            Geometry = geometry;
            Material = material;
        }
    }
}
