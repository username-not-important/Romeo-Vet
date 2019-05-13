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
using RomeoVet.Mesh;
using SharpDX;
using Color = System.Windows.Media.Color;
using Geometry3D = HelixToolkit.Wpf.SharpDX.Geometry3D;
using Material = HelixToolkit.Wpf.SharpDX.Material;

namespace RomeoVet.Models
{
    public class AnatomyDisplayModel
    {
        private static Color4 boneColor = new Color4(new Color3(0.89f, 0.855f, 0.788f));

        #region Skeleton

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

        public Material MainSkeletonMaterial { get; } = new PhongMaterial
        {
            DiffuseColor = boneColor,
            AmbientColor = new Color4(new Color3(0.05f, 0.05f, 0.1f)),
            SpecularColor = new Color4(new Color3(0.04f, 0.04f, 0.04f)),
            SpecularShininess = 0.7f
        };


        public Material SelectedMaterial { get; } =
            new PhongMaterial
            {
                DiffuseColor = boneColor,
                AmbientColor = new Color4(new Color3(0.05f, 0.05f, 0.1f)),
                SpecularColor = new Color4(new Color3(0.08f, 0.08f, 0.08f)),
                SpecularShininess = 0.4f,

                EmissiveColor = Color4.Subtract(boneColor, new Color4(new Color3(0.7f)))
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
                , new TranslateTransform3D(0, 6, -0.32)
                //,new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0,1,0), -90))
                //,new ScaleTransform3D(0.1,0.1,0.1)
            }
        };

        #endregion

        #region Skin


        private Observable<Geometry3D> _skinMesh = new Observable<Geometry3D>();
        
        public Geometry3D SkinMesh
        {
            get { return _skinMesh; }
            set { _skinMesh.Value = value; }
        }
        
        public Material SkinMaterial { get; } =
            new PhongMaterial
            {
                DiffuseColor = new Color4(new Vector4(0.3f, 0.3f, 0.3f, 0.3f)),
                EmissiveColor = new Color4(new Vector4(0.1f, 0.1f, 0.3f, 0.3f))
            };


        public Transform3D SkinTransform
        {
            get;
        } = new Transform3DGroup()
        {
            Children = new Transform3DCollection()
            {
                new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1,0,0), -90))
                , new ScaleTransform3D(2,2,2)
                , new TranslateTransform3D(0, 6, 0)
            }
        };

        #endregion

        private Observable<bool> _showSkeleton = new Observable<bool>(true);
        private Observable<bool> _showSkin = new Observable<bool>(true);
        
        private Observable<Color> _lightColor = new Observable<Color>(Color.FromRgb(170,170,170));

        public bool ShowSkeleton { get { return _showSkeleton; } set { _showSkeleton.Value = value; } }
        public bool ShowSkin { get { return _showSkin; } set { _showSkin.Value = value; } }
        
        public Color LightColor { get { return _lightColor; } set { _lightColor.Value = value; } }

        public void ImportSkeleton(Batch batch)
        {
            _materialBatch.Clear();
            _materialBatch.AddRange(batch.Materials);

            _skeletonBatch.Clear();
            _skeletonBatch.AddRange(batch.MeshList);
            

        }

        public void ImportSkin(Model model)
        {
            SkinMesh = model.Geometry;
        }
    }
}
