using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Assisticant;
using Assisticant.Fields;
using RomeoVet.Mesh;
using RomeoVet.Models;
using RomeoVet.ViewModels.Common;

namespace RomeoVet.ViewModels
{
    public class AnatomyDisplayViewModel : AsynchronusViewModel
    {
        private AnatomyDisplayModel _display;

        public AnatomyDisplayViewModel(AnatomyDisplayModel display)
        {
            _display = display;
        }

        public Model3D Model => _display.Model;

        public void LoadModel(Model3D model3D)
        {
            Perform(async delegate
            {
                await Task.Run(() => _display.Model = model3D);
            });
        }

        public void LoadModel(IMeshProvider provider)
        {
            Perform(async delegate
            {
                await Task.Run(() =>
                {
                    var mesh = provider.BuildMesh();
                    mesh.Freeze();

                    _display.Model = mesh;
                });
            });
        }
    }
}
