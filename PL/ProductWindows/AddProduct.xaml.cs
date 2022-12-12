using BlApi;
using BlImplementation;
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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        private static IBl bl = new BL();
        public AddProduct()
        {
            InitializeComponent();
            Category_selector.ItemsSource = System.Enum.GetValues(typeof(BO.Enum.Category));
        }

       
        private void Name_TextChanged_1(object sender, TextChangedEventArgs e)
        {
          
        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Category_selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { }

        private void InStock_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
        private void AddProduc_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(ID.Text, out int id);
            double.TryParse(Price.Text, out double price);
            int.TryParse(InStock.Text, out int inStock);
            BO.Enum.Category.TryParse(Category_selector.Text, out BO.Enum.Category category);
            bl.Product.addProduct(id, Name.Text, category, price, inStock);
            //new ProductWindows.ProductList().Show();
            //this.Close();
        }
    }

}
