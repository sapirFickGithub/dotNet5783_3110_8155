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
using BO;
using BlImplementation;
using BlApi;

namespace PL.ProductWindows
{
    /// <summary>
    /// Interaction logic for ProductList.xaml

    /// </summary>
    public partial class ProductList : Window
    {
        private static IBl bl = new BL();
        public ProductList()
        {
            InitializeComponent();
            List_of_product.ItemsSource = bl.Product.getListOfProduct();
            Category_selector.ItemsSource = System.Enum.GetValues(typeof(BO.Enum.Category));//////////////////////////////
        }

        private void List_of_product_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

      
     

        private void EditProductWindow_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindows.EditProduct().Show();
            this.Close();
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = Category_selector.SelectedItem;
            if (selectedItem.ToString() == "All")
                List_of_product.ItemsSource = bl.Product.getListOfProduct();
            else
                List_of_product.ItemsSource = bl.Product.getListOfProduct(a => a?.ProductCategory.ToString() == selectedItem.ToString());
        }
    }
}
