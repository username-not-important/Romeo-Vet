using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Resources;
using Assisticant;
using Assisticant.Fields;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model;
using RomeoVet.Mesh;
using RomeoVet.Models;
using RomeoVet.ViewModels.Common;
using Camera = HelixToolkit.Wpf.SharpDX.Camera;
using Geometry3D = HelixToolkit.Wpf.SharpDX.Geometry3D;
using Material = HelixToolkit.Wpf.SharpDX.Material;

namespace RomeoVet.ViewModels
{
    public class AnatomyDisplayViewModel : AsynchronusViewModel
    {
        private AnatomyDisplayModel _display;

        public AnatomyDisplayViewModel(AnatomyDisplayModel display)
        {
            _display = display;
        }

        #region Skeleton

        public IList<BatchedMeshGeometryConfig> SkeletonBatch => _display.SkeletonBatch;
        public IList<Material> MaterialBatch => _display.MaterialBatch;

        public Geometry3D SelectedMesh
        {
            get { return _display.SelectedMesh; }
            set { _display.SelectedMesh = value; }
        }
        public Material MainSkeletonMaterial => _display.MainSkeletonMaterial;
        public Material SelectedMaterial => _display.SelectedMaterial;

        public Transform3D BatchedTransform => _display.BatchedTransform;

        #endregion

        #region Skin

        public Geometry3D SkinMesh
        {
            get { return _display.SkinMesh; }
            set { _display.SkinMesh = value; }
        }
        public Material SkinMaterial => _display.SkinMaterial;
        public Transform3D SkinTransform => _display.SkinTransform;

        #endregion

        public Stream ViewBoxTexture => Application.GetResourceStream(new Uri("Assets/ViewCube.jpg", UriKind.Relative)).Stream;

        public bool ShowSkin
        {
            get { return _display.ShowSkin; } set { _display.ShowSkin = value; }
        }
        public bool ShowSkeleton
        {
            get { return _display.ShowSkeleton; } set { _display.ShowSkeleton = value; }
        }

        public event EventHandler ModelChanged;

        public void LoadModels(IBatchProvider skeletonProvider,
                               IModelProvider skinProvider)
        {
            Perform(async delegate
            {
                await Task.Run(() =>
                {
                    var skeletonBatch = skeletonProvider.BuildModel();
                    var skinModel = skinProvider.BuildModel();

                    _display.ImportSkeleton(skeletonBatch);
                    _display.ImportSkin(skinModel);

                    //TODO: import names from batch

                    OnModelChanged();
                });

            });
        }

        protected virtual void OnModelChanged()
        {
            ModelChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
