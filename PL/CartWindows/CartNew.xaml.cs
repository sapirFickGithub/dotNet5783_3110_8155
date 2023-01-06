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
using BlApi;
using BlImplementation;
using BO;

namespace PL.CartWindows
{
    /// <summary>
    /// Interaction logic for CartNew.xaml
    /// </summary>
    public partial class CartNew : Window
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();

        public event Action<int> action;

        private BO.Cart MyCart;

        public CartNew(Action<int> action, ProductItem productItem)
        {
            this.action = action;
            InitializeComponent();
        }

        private void approve_orer_Click(object sender, RoutedEventArgs e)
        {
            bl.Cart.approvment(MyCart);
        }
    }
}
