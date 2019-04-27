using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Assisticant;
using Assisticant.Fields;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model;
using RomeoVet.Annotations;
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

        public event EventHandler ModelChanged;
        
        public void LoadModel(IMeshProvider provider)
        {
            Perform(async delegate
            {
                await Task.Run(() =>
                {
                    var batch = provider.BuildModel();

                    _display.ImportBatch(batch);
                    
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
