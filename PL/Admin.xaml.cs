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

namespace PL
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void ProductListWindow_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindows.ProductList().Show();
            this.Close();
        }

        private void OrderListWindow_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindows.OrderList().Show();
            this.Close();
        }
    }
}
