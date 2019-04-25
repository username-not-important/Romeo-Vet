using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RomeoVet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static T VMLocator<T>()
        {
            string type = typeof(T).Name.Replace("ViewModel", "VM");

            return (T)Current.Resources[type];
        }

        public static T FindResource<T>(string v)
        {
            return (T)Current.Resources[v];
        }
    }
}
