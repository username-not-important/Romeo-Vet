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
using HelixToolkit.Wpf.SharpDX.Model;
using HelixToolkit.Wpf.SharpDX.Model.Scene;
using Material = HelixToolkit.Wpf.SharpDX.Material;

namespace RomeoVet.Mesh
{
    public class FileMeshProvider : IMeshProvider
    {
        public Batch BuildModel()
        {
            Importer i = new Importer();

            var scene = i.Load(Environment.CurrentDirectory + "\\horse-d.3ds");

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

            int count = 0;
            Dictionary<MaterialCore, int> materialDict = new Dictionary<MaterialCore, int>();

            foreach (var model in list)
            {
                if (materialDict.ContainsKey(model.Material))
                {
                    continue;
                }
                materialDict.Add(model.Material, count++);
            }

            var modelList = new List<BatchedMeshGeometryConfig>(list.Count);
            foreach (var model in list)
            {
                
                //model.Geometry.UpdateOctree();
                if (model.Transform != null && model.Transform.Count != 0)
                    foreach (var transform in model.Transform)
                        modelList.Add(new BatchedMeshGeometryConfig(model.Geometry, transform, materialDict[model.Material]));
                else
                    modelList.Add(new BatchedMeshGeometryConfig(model.Geometry, Matrix.Identity, materialDict[model.Material]));
            }

            Material[] materials = new Material[materialDict.Count];
            foreach (var m in materialDict.Keys)
            {
                //materials[materialDict[m]] = m.ConvertToMaterial();
                PhongMaterial mt = PhongMaterials.White;
                materials[materialDict[m]] = mt;

            }

            return new Batch(modelList, materials.ToList());
        }

    }
}
