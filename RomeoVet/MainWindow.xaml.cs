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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Assisticant;
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
    }
}
