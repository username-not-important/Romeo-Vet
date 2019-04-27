using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using Assimp.Unmanaged;
using SharpDX;
using HelixToolkit.Wpf.SharpDX;
using System.Threading;
using HelixToolkit.Wpf.SharpDX.Assimp;
using HelixToolkit.Wpf.SharpDX.Model.Scene;

namespace RomeoVet.Mesh
{
    public class FileMeshProvider : IMeshProvider
    {
        public Object3D BuildModel()
        {
            Importer i = new Importer();

            var scene = i.Load(Environment.CurrentDirectory + "\\horse.3ds");

            var list = new List<Object3D>();
            foreach (var node in scene.Root.Traverse())
            {
                if (node is MeshNode m)
                {
                    var object3D = new Object3D() { Geometry = m.Geometry, Material = m.Material, Transform = new List<Matrix>() };
                    //object3D.Transform.Add(m.);
                    list.Add(object3D);
                }
            }
            
            return list[1];
        }
        
    }
}
