using System;
using System.Linq;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;

namespace RomeoVet.Mesh
{
    public class PerformanceMeshProvider : IBatchProvider
    {
        
        Geometry3D BuildMesh()
        {
            // scene model3d
            var b1 = new MeshBuilder();
            b1.AddSphere(new Vector3(0, 0, 0), 0.5);
            b1.AddBox(new Vector3(0, 0, 0), 1, 0.5, 2, BoxFaces.All);

            var meshGeometry = b1.ToMeshGeometry3D();
            meshGeometry.Colors = new Color4Collection(meshGeometry.TextureCoordinates.Select(x => x.ToColor4()));
            return meshGeometry;
            
        }

        public Object3D BuildModel()
        {
            throw new NotImplementedException();
        }

        Batch IBatchProvider.BuildModel()
        {
            throw new NotImplementedException();
        }
    }
}
