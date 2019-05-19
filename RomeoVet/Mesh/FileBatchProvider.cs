using System;
using System.Collections.Generic;
using System.Linq;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Assimp;
using HelixToolkit.Wpf.SharpDX.Model;
using HelixToolkit.Wpf.SharpDX.Model.Scene;
using SharpDX;
using Material = HelixToolkit.Wpf.SharpDX.Material;

namespace RomeoVet.Mesh
{
    public class FileBatchProvider : IBatchProvider
    {
        public Batch BuildModel()
        {
            Importer i = new Importer();

            var scene = i.Load(Environment.CurrentDirectory + "\\Models\\horse-skeleton2.3ds");

            var list = new List<Object3D>();
            foreach (var node in scene.Root.Traverse())
            {
                if (node is MeshNode m)
                {
                    var object3D = new Object3D
                    {
                        Geometry = m.Geometry,
                        Material = m.Material,
                        Name = m.Parent.Name,
                        Transform = new List<Matrix>()
                    };

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

            Dictionary<string, Guid> names = new Dictionary<string, Guid>();

            var modelList = new List<BatchedMeshGeometryConfig>(list.Count);
            foreach (var model in list)
            {
                if (model.Transform != null && model.Transform.Count != 0)
                    foreach (var transform in model.Transform)
                    {
                        modelList.Add(new BatchedMeshGeometryConfig(model.Geometry, transform, materialDict[model.Material]));
                        names.Add(model.Name, model.Geometry.GUID);
                    }
                else
                {
                    modelList.Add(new BatchedMeshGeometryConfig(model.Geometry, Matrix.Identity, materialDict[model.Material]));
                    names.Add(model.Name, model.Geometry.GUID);
                }
            }

            Color4 boneColor = new Color4(new Color3(0.89f, 0.855f, 0.788f));

            Material[] materials = new Material[materialDict.Count];
            foreach (var m in materialDict.Keys)
            {
                //materials[materialDict[m]] = m.ConvertToMaterial();
                //PhongMaterial mt = PhongMaterials.White;
                
                PhongMaterial mt = new PhongMaterial
                {
                    DiffuseColor = boneColor,
                    AmbientColor = new Color4(new Color3(0.05f, 0.05f, 0.1f)),
                    SpecularColor = new Color4(new Color3(0.04f, 0.04f, 0.04f)),
                    SpecularShininess = 0.7f
                };

                materials[materialDict[m]] = mt;
            }

            return new Batch(modelList, materials.ToList(), names);
        }

    }
}
