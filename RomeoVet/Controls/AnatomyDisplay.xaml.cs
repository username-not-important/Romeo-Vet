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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Assisticant;
using RomeoVet.Util;
using RomeoVet.ViewModels;

namespace RomeoVet.Controls
{
    /// <summary>
    /// Interaction logic for AnatomyDisplay.xaml
    /// </summary>
    public partial class AnatomyDisplay : UserControl
    {
        public AnatomyDisplay()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = ForView.Unwrap<AnatomyDisplayViewModel>(e.NewValue);
            vm.ModelChanged += VmOnModelChanged;
        }

        private void VmOnModelChanged(object sender, EventArgs eventArgs)
        {
            ViewFit();
        }

        public void ViewFit()
        {
            Dispatcher.Invoke(() =>
            {
                _Viewport.SetView(new Point3D(-7.5, 13.7, 11), new Vector3D(11, -11, -11), new Vector3D(0, 1, 0), 1000);
            });

        }
    }
}
