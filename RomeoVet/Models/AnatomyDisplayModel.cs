using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Assisticant.Collections;
using Assisticant.Fields;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model;
using RomeoVet.Mesh;
using SharpDX;
using Camera = HelixToolkit.Wpf.SharpDX.Camera;
using Color = SharpDX.Color;
using Geometry3D = HelixToolkit.Wpf.SharpDX.Geometry3D;
using Material = HelixToolkit.Wpf.SharpDX.Material;
using PerspectiveCamera = HelixToolkit.Wpf.SharpDX.PerspectiveCamera;

namespace RomeoVet.Models
{
    public class AnatomyDisplayModel
    {
        private Observable<Geometry3D> _selectedMesh = new Observable<Geometry3D>();

        public List<BatchedMeshGeometryConfig> _skeletonBatch = new List<BatchedMeshGeometryConfig>();
        public List<Material> _materialBatch = new List<Material>();

        public IList<BatchedMeshGeometryConfig> SkeletonBatch => _skeletonBatch;
        public IList<Material> MaterialBatch => _materialBatch;

        public Geometry3D SelectedMesh
        {
            get { return _selectedMesh; }
            set { _selectedMesh.Value = value; }
        }

        public Material MainSkeletonMaterial { get; } = PhongMaterials.White;
        
        public Material SelectedMaterial { get; } =
        new PhongMaterial
        {
            DiffuseColor = Color4.White,
            EmissiveColor = new Color4(new Vector4(0.3f, 0.3f, 0.3f, 0.3f))
        };


    public Transform3D BatchedTransform
        {
            get;
        } = new Transform3DGroup()
        {
            Children = new Transform3DCollection()
            {
                new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1,0,0), -90))
                , new ScaleTransform3D(2,2,2)
                , new TranslateTransform3D(0, 6, 0)
                //,new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0,1,0), -90))
                //,new ScaleTransform3D(0.1,0.1,0.1)
            }
        };

        public void ImportBatch(Batch batch)
        {
            _materialBatch.Clear();
            _materialBatch.AddRange(batch.Materials);

            _skeletonBatch.Clear();
            _skeletonBatch.AddRange(batch.MeshList);
        }
    }
}
