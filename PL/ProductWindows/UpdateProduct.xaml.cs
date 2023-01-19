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
        private static BlApi.IBl? bl = BlApi.Factory.Get();

        public event Action<int> action;
        public IEnumerable<BO.Enum.Category> Categories { set; get; }
        //public BO.Enum.Category Categories { set; get; }


        public BO.Product product
        {
            get { return (BO.Product)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }

        // Using a DependencyProperty as the backing store for product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productProperty =
            DependencyProperty.Register("product", typeof(BO.Product), typeof(UpdateProduct));



        //public BO.Product? product { set; get; } 
        public UpdateProduct(Action<int> action, ProductForList productForList)
        {
             product =bl?.Product?.GetProduct(productForList.idOfProduct);
           
            Categories = System.Enum.GetValues(typeof(BO.Enum.Category)).Cast<BO.Enum.Category>();
            this.action = action;
            InitializeComponent();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                double.TryParse(Price.Text, out double price);
                if (price < 0)
                    MessageBox.Show(
                        "The price you enter is incorrect",
                        "Invalid input",
                        MessageBoxButton.OK,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);

                int.TryParse(InStock.Text, out int inStock);
                if (inStock < 0)
                    MessageBox.Show(
                        "The in stock you enter is incorrect",
                        "Invalid input",
                        MessageBoxButton.OK,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);

                BO.Enum.Category.TryParse(Category_selector.Text, out BO.Enum.Category category);
                if (Category_selector.Text == "")
                    MessageBox.Show(
                        "You did not choose a category",
                        "Invalid input",
                        MessageBoxButton.OK,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);
                if (Name.Text == "")
                {
                    MessageBox.Show(
                        "You did not enter a name for the product.",
                        "Invalid input",
                        MessageBoxButton.OK,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);
                }
                BO.Product productn = new BO.Product()
                {
                    idOfProduct = product.idOfProduct,
                    Name = Name.Text,
                    InStock = inStock,
                    Price = price,
                    ProductCategory = category
                };
                bl?.Product.Update(productn);

                action(product.idOfProduct);

                new ProductWindows.ProductList().Show();
                this.Close();
            }
            catch (BO.notExist)
            {
                MessageBox.Show(
                         "Product dosent exist\n make sure all the dtails are correct\n and try again",
                         "Not Found",
                         MessageBoxButton.OK,
                         MessageBoxImage.Hand,
                         MessageBoxResult.Cancel,
                         MessageBoxOptions.RtlReading);
            }
            catch (BO.incorrectData)
            {
                MessageBox.Show(
                           "Incorrect data. please try again.",
                           "Data error",
                           MessageBoxButton.OK,
                           MessageBoxImage.Hand,
                           MessageBoxResult.Cancel,
                           MessageBoxOptions.RtlReading);
            }
            catch (Exception)
            {
                MessageBox.Show(
                         "Somthing went worng...\n please try again later",
                         "Unknown error",

                         MessageBoxButton.OK,
                         MessageBoxImage.Hand,
                         MessageBoxResult.Cancel,
                         MessageBoxOptions.RtlReading);
            }
        }

   
        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void InStock_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BackToProductListWindow_Click(object sender, RoutedEventArgs e)
        {
            //move to Main window
            // new ProductWindows.ProductList().Show();
            this.Close();
        }
    }
}
