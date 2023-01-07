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
using System.Windows.Shapes;

namespace PL.OrderWindows
{
    /// <summary>
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();
        public event Action<int> action;
        public AddOrder(Action<int> action)
        {
            this.action = action;
            InitializeComponent();
        }

        private void CustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
