using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Assisticant;
using Microsoft.Expression.Interactivity.Core;
using RomeoVet.Mesh;
using RomeoVet.Models.Anatomy;
using RomeoVet.Util;
using RomeoVet.ViewModels;
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
            #region catalog

            //string catalog = "";

            //Animal a = new Animal();
            //a.BodyParts = new ObservableCollection<BodyPart>()
            //{
            //    new BodyPart()
            //    {
            //        Name = "Head", PartType = BodyPartType.Limb,
            //        Children = new ObservableCollection<BodyPart>()
            //        {
            //            new BodyPart(){Name = "Bones", PartType = BodyPartType.Category, Children = new ObservableCollection<BodyPart>()
            //            {
            //                new BodyPart() {Name = "Skull", PartType = BodyPartType.Bone}
            //            }}
            //        }
            //    },
            //};

            //catalog = TextSerializer.XmlSerializeToString(a);

            #endregion

            string catalog = File.ReadAllText(Environment.CurrentDirectory + "\\Models\\Anatomy.xml");
            App.VMLocator<MainVMLocator>().LoadAnatomy(catalog);

            //IMeshProvider b = new PerformanceMeshProvider();
            IBatchProvider skeletonProvider = new FileBatchProvider();
            IModelProvider skinProvider = new FileModelProvider();

            App.VMLocator<MainVMLocator>().LoadModel(skeletonProvider, skinProvider);

        }

        private void AnatomyTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is BodyPart p)
            {
                if (p.PartType == BodyPartType.Bone)
                {

                }
            }
        }

        private void _BodyPartIsolate_OnClick(object sender, RoutedEventArgs e)
        {
            _BodyPartSelect_OnClick(sender, e);
            _isolateViewOpen();
        }

        private void _BodyPartSelect_OnClick(object sender, RoutedEventArgs e)
        {
            BodyPart part = (sender as FrameworkElement).DataContext as BodyPart;
            if (part == null) return;

            part.IsSelected = true;

            ForView.Unwrap<AnatomyDisplayViewModel>(App.VMLocator<MainVMLocator>().DisplayVM).SelectItem(part.Name, part.PartType);
        }

        private void _isolateViewOpen()
        {
            if (!"Open".Contains(IsolatedViewStates.CurrentState?.Name ?? "Closed"))
                VisualStateManager.GoToElementState(_ParentGrid, "Open", true);
        }


        private void _IsolatedDisplay_OnClosed(object sender, RoutedEventArgs e)
        {
            if ("Open".Contains(IsolatedViewStates.CurrentState?.Name ?? "Closed"))
                VisualStateManager.GoToElementState(_ParentGrid, "Closed", true);
        }

    }
}
