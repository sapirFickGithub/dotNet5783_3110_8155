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
        public UpdateProduct()
        {
            InitializeComponent();
            Category_selector.ItemsSource = System.Enum.GetValues(typeof(BO.Enum.Category));
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {//Receives the new data to update the product and performs input integrity checks.
                int.TryParse(ID.Text, out int id);
                if (id <= 99999 || id >= 1000000)
                    MessageBox.Show(
                        "The ID you enter is incorrect",
                        "Invalid input",
                        MessageBoxButton.OK,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);

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
                BO.Product product = new BO.Product()
                {
                    idOfProduct = id,
                    Name = Name.Text,
                    InStock = inStock,
                    Price = price,
                    ProductCategory = category
                };
                bl.Product.update(product);

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
                        // MessageBoxButton.OKCancel,
                         MessageBoxButton.OK,
                         MessageBoxImage.Hand,
                         MessageBoxResult.Cancel,
                         MessageBoxOptions.RtlReading);
            }
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

        private void BackToProductListWindow_Click(object sender, RoutedEventArgs e)
        {
            //move to Main window
            new ProductWindows.ProductList().Show();
            this.Close();
        }
    }
}
