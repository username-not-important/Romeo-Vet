using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RomeoVet.Mesh
{
    public interface IMeshProvider
    {
        Model3D BuildMesh();
    }
}
