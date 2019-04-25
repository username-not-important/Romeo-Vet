using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RomeoVet.Mesh;
using RomeoVet.ViewModels.Locators;

namespace RomeoVet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //IMeshProvider b = new PerformanceMeshProvider();
            IMeshProvider b = new FileMeshProvider();
            App.VMLocator<AnatomyDisplayVMLocator>().LoadModel(b);

        }
    }
}
