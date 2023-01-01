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
using System.ComponentModel;


namespace PL.CartWindows
{
    /// <summary>
    /// Interaction logic for CartNew.xaml
    /// </summary>
    public partial class CartNew : Window
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();
        public bool hasSorted = true;
        public CartNew()
        {
            InitializeComponent();
            List_of_product.ItemsSource = bl.Product.getListOfProductItem();
        }

        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            //move to Main window
            new MainWindow().Show();
            this.Close();
        }

        private void Sort_By_Name_Click(object sender, RoutedEventArgs e)
        {
            var listTemp = bl.Product.getListOfProduct();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(List_of_product.ItemsSource);
            view.SortDescriptions.Clear();
            if (hasSorted)
            {

                view.SortDescriptions.Add(new("Name", ListSortDirection.Descending));
                hasSorted = false;
            }
            else
            {
                view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                hasSorted = true;
            }
        }

        private void Sort_By_ID_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(List_of_product.ItemsSource);
            view.SortDescriptions.Clear();
            if (hasSorted)
            {
                view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Descending));
                hasSorted = false;
            }
            else
            {
                view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
                hasSorted = true;
            }
        }

        private void Sort_By_Price_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(List_of_product.ItemsSource);
            view.SortDescriptions.Clear();
            if (hasSorted)
            {
                view.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));
                hasSorted = false;
            }
            else
            {
                view.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
                hasSorted = true;
            }
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Object selectedItem = Category_selector.SelectedItem;
                if (selectedItem.ToString() == "All")
                    List_of_product.ItemsSource = bl.Product.getListOfProductItem();
                else
                    List_of_product.ItemsSource = bl.Product.getListOfProductItem(a => a?.ProductCategory.ToString() == selectedItem.ToString());
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

        private void List_of_product_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
