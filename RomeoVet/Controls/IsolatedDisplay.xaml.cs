using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Assisticant;
using HelixToolkit.Wpf.SharpDX;
using RomeoVet.ViewModels;
using RomeoVet.ViewModels.Locators;
using Geometry3D = HelixToolkit.Wpf.SharpDX.Geometry3D;

namespace RomeoVet.Controls
{
    /// <summary>
    /// Interaction logic for IsolatedDisplay.xaml
    /// </summary>
    public partial class IsolatedDisplay : UserControl
    {
        public IsolatedDisplay()
        {
            InitializeComponent();

            ForView.Unwrap<AnatomyDisplayViewModel>(App.VMLocator<MainVMLocator>().DisplayVM).SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, Geometry3D geometry3D)
        {
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = TimeSpan.FromMilliseconds(100);
            t.Tick += (o, args) => { _Viewport.ZoomExtents(); t.Stop(); };

            t.Start();
            // _Viewport.ZoomExtents(geometry3D.Positions[0].ToPoint3D(), 1, 350);
        }

        #region Events

        public static readonly RoutedEvent ClosedEvent =
            EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(IsolatedDisplay));
        
        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }
        
        protected virtual void RaiseClosedEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClosedEvent);
            RaiseEvent(args);
        }

        #endregion

        private void _Close_Click(object sender, RoutedEventArgs e)
        {
            RaiseClosedEvent();
        }
    }
}
