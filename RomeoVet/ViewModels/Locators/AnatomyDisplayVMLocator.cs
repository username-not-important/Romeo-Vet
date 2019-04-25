using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Assisticant;
using HelixToolkit.Wpf;
using RomeoVet.Mesh;
using RomeoVet.Models;

namespace RomeoVet.ViewModels.Locators
{
    public class AnatomyDisplayVMLocator : ViewModelLocatorBase
    {
        private AnatomyDisplayModel mainModel = new AnatomyDisplayModel();
        private AnatomyDisplayViewModel mainViewModel;


        public AnatomyDisplayVMLocator()
        {
            mainViewModel = new AnatomyDisplayViewModel(mainModel);
        }

        public object MainVM => ViewModel(() => mainViewModel);

        public void LoadModel(IMeshProvider provider)
        {
            mainViewModel.LoadModel(provider);
        }
    }
}
