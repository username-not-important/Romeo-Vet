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
