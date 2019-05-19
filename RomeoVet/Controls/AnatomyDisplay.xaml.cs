using System;
using System.Collections;
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
using Assisticant.Collections;
using HelixToolkit.Wpf.SharpDX;
using RomeoVet.Util;
using RomeoVet.ViewModels;
using RomeoVet.ViewModels.Locators;
using SharpDX;
using Material = HelixToolkit.Wpf.SharpDX.Material;

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

                var vm = ForView.Unwrap<AnatomyDisplayViewModel>(DataContext);
                batchedMesh.BatchedGeometries = new List<BatchedMeshGeometryConfig>(vm.SkeletonBatch);
                batchedMesh.BatchedMaterials = new List<Material>(vm.MaterialBatch);

                _Viewport.SetView(new Point3D(-7.5, 15, 11), new Vector3D(11, -11, -11), new Vector3D(0, 1, 0), 1000);
            });

        }

        DateTime lastHit = DateTime.Now;
        private void Skeleton_Mouse3DUp(object sender, MouseUp3DEventArgs e)
        {
            if (DateTime.Now.Subtract(lastHit).TotalMilliseconds > 400)
            {
                lastHit = DateTime.Now;
                return;
            }

            var dc = ForView.Unwrap<AnatomyDisplayViewModel>(DataContext);
            var treedc = ForView.Unwrap<AnatomyExplorerViewModel>(App.VMLocator<MainVMLocator>().ExplorerVM);

            string bone = dc.GetBoneName(e.HitTestResult.Geometry.GUID);

            if (dc.SelectedMesh == e.HitTestResult.Geometry)
            {
                dc.SelectedMesh = null;
                treedc.DiscardSelection();
            }
            else
            {
                dc.SelectedMesh = e.HitTestResult.Geometry;
                treedc.SelectBone(bone);

                _Viewport.ZoomExtents(e.HitTestResult.PointHit.ToPoint3D(), 1, 350);
            }

        }

        private Rect3D ToRect3D(BoundingBox b)
        {
            return new Rect3D(b.Minimum.X, b.Minimum.Y, b.Minimum.Z, b.Size.X, b.Size.Y, b.Size.Z);
        }
    }
}
