using System;
using System.Collections.Generic;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Assimp;
using HelixToolkit.Wpf.SharpDX.Model.Scene;
using SharpDX;

namespace RomeoVet.Mesh
{
    public class FileModelProvider : IModelProvider
    {
        public Model BuildModel()
        {
            Importer i = new Importer();

            var scene = i.Load(Environment.CurrentDirectory + "\\Models\\horse-skin.3ds");

            Object3D result = null;

            foreach (var node in scene.Root.Traverse())
            {
                if (node is MeshNode m)
                {
                   result = new Object3D()
                    {
                        Geometry = m.Geometry,
                        Material = m.Material,
                        Transform = new List<Matrix>()
                    };
                    break;
                }
            }

            if (result== null)
                throw new ArgumentException("File does not contain any Meshes.");

            return new Model(result.Geometry, new PhongMaterial());
        }
    }
}
