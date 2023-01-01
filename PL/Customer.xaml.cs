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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        public Customer()
        {
            InitializeComponent();
        }

        private void List_Product_View(object sender, RoutedEventArgs e)
        {
            new CartWindows.CartNew().Show();
            this.Close();
        }

        private void BackToProductListWindow_Click(object sender, RoutedEventArgs e)
        {
            //move to Main window
            new MainWindow().Show();
            this.Close();
        }
    }
}
