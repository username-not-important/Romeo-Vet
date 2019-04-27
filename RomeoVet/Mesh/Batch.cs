using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelixToolkit.Wpf.SharpDX;

namespace RomeoVet.Mesh
{
    public class Batch
    {
        public List<BatchedMeshGeometryConfig> MeshList { get; set; }
        public List<Material> Materials { get; set; }

        public Batch()
        {
            
        }

        public Batch(List<BatchedMeshGeometryConfig> meshList, List<Material> materials)
        {
            MeshList = meshList;
            Materials = materials;
        }
    }
}
