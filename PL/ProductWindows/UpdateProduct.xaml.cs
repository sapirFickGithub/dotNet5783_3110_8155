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

namespace PL.ProductWindows
{
    /// <summary>
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Window
    { 
        private static IBl bl = new BL();
        public UpdateProduct()
        {
            InitializeComponent();
            Category_selector.ItemsSource = System.Enum.GetValues(typeof(BO.Enum.Category));
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(ID.Text, out int id);
            double.TryParse(Price.Text, out double price);
            int.TryParse(InStock.Text, out int inStock);
            BO.Enum.Category.TryParse(Category_selector.Text, out BO.Enum.Category category);
            BO.Product product = new BO.Product()
            {
                idOfProduct = id,
                Name = Name.Text,
                InStock = inStock,
                Price = price,
                ProductCategory=category    
            };
            bl.Product.update(product);
           
            new ProductWindows.ProductList().Show();
            this.Close();
        }

        private void Category_selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Price_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
        private void InStock_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
