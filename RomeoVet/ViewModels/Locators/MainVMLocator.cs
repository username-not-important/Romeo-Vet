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
    public class MainVMLocator : ViewModelLocatorBase
    {
        private AnatomyDisplayModel displayModel = new AnatomyDisplayModel();
        private AnatomyDisplayViewModel displayViewModel;

        private AnatomyExplorerViewModel explorerViewModel;


        public MainVMLocator()
        {
            displayViewModel = new AnatomyDisplayViewModel(displayModel);
            explorerViewModel = new AnatomyExplorerViewModel();
        }

        public object DisplayVM => ViewModel(() => displayViewModel);
        public object ExplorerVM => ViewModel(() => explorerViewModel);

        public void LoadModel(IBatchProvider skeletonProvider, IModelProvider skinProvider)
        {
            displayViewModel.LoadModels(skeletonProvider, skinProvider);
        }

        public void LoadAnatomy(string catalog)
        {
            explorerViewModel.ImportAnatomy(catalog);
        }
    }
}
