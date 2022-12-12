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

namespace PL.ProductWindows
{
    /// <summary>
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Window
    {
        public UpdateProduct()
        {
            InitializeComponent();
            Category_selector.ItemsSource = System.Enum.GetValues(typeof(BO.Enum.Category));
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindows.ProductList().Show();
            this.Close();
        }
    }
}
