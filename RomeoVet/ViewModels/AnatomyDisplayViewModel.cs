using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
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

        public Geometry3D Model => _display.Mesh;
        public MaterialCore Material => _display.Material;

        public event EventHandler ModelChanged;
        
        public void LoadModel(IMeshProvider provider)
        {
            Perform(async delegate
            {
                await Task.Run(() =>
                {
                    var model = provider.BuildModel();
                    //mesh.Freeze();

                    _display.Mesh = model.Geometry;
                    _display.Material = model.Material;

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
