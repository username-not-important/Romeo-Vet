using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace RomeoVet.Mesh
{
    public class PerformanceMeshProvider : IMeshProvider
    {


        public Model3D BuildMesh()
        {
            var modelGroup = new Model3DGroup();

            // Create a mesh builder and add a box to it
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddBox(new Point3D(0, 0, 1), 1, 2, 0.5);
            meshBuilder.AddBox(new Rect3D(0, 0, 1.2, 0.5, 1, 0.4));

            // Create a mesh from the builder (and freeze it)
            var mesh = meshBuilder.ToMesh(true);

            // Create some materials
            var blueMaterial = MaterialHelper.CreateMaterial(Colors.BlueViolet);
            var insideMaterial = MaterialHelper.CreateMaterial(Colors.Yellow);

            for (int i = -30; i < 30; i++)
            {
                for (int j = -30; j < 30; j++)
                {
                    modelGroup.Children.Add(new GeometryModel3D {
                        Geometry = mesh,
                        Material = blueMaterial,
                        BackMaterial = insideMaterial,
                        Transform = new TranslateTransform3D(-3*i, -3*j, 0)
                    });
                }
            }
            
            return modelGroup;
        }
    }
}
