using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private static BlApi.IBl? bl = BlApi.Factory.Get();
        public IEnumerable<BO.Enum.Category> Categories { set; get; }

        public event Action<int> action;
        public AddProduct(Action<int> action)
        {
            InitializeComponent();
            Categories = System.Enum.GetValues(typeof(BO.Enum.Category)).Cast<BO.Enum.Category>();
            this.action = action;
            
        }
        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
           
                Object selectedItem = Category_selector.SelectedItem;

                
            
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
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {//Receiving values for the product and input tests.
            try
            {
                int.TryParse(ID.Text, out int id);
                if (id <= 99999 || id >= 1000000)
                    MessageBox.Show(
                        "The ID you enter is incorrect",
                        "Invalid input",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);

                double.TryParse(Price.Text, out double price);
                if (price < 0)
                    MessageBox.Show(
                        "The price you enter is incorrect",
                        "Invalid input",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);

                int.TryParse(InStock.Text, out int inStock);
                if (inStock < 0)
                    MessageBox.Show(
                        "The in stock you enter is incorrect",
                        "Invalid input",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);

                BO.Enum.Category.TryParse(Category_selector.Text, out BO.Enum.Category category);
                if (Category_selector.Text == "")
                    MessageBox.Show(
                        "You did not choose a category",
                        "Invalid input",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);

                if (Name.Text == "")
                {
                    MessageBox.Show(
                        "You did not enter a name for the product.",
                        "Invalid input",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);
                }

                bl.Product.addProduct(id, Name.Text, category, price, inStock);

                action(id);
                this.Close();
            }
            catch (BO.incorrectData)
            {
                MessageBox.Show(
                           "Incorrect data. please try again.",
                           "Data error",
                           MessageBoxButton.OKCancel,
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

        private void BackToProductListWindow_Click(object sender, RoutedEventArgs e)
        {
            //move to Main window
           // new ProductWindows.ProductList().Show();
            this.Close();
        }
    }

}
