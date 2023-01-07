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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace PL.ProductWindows
{
    /// <summary>
    /// Interaction logic for ProductList.xaml

    /// </summary>
    public partial class ProductList : Window
    {

        private static BlApi.IBl? bl = BlApi.Factory.Get();

        public IEnumerable<BO.Enum.Category> Categories { set; get; }

        public bool hasSorted = true;

        public ObservableCollection<ProductForList?> ProductsForList//dependency proprty in order to use 'data binding'
        {
            get { return (ObservableCollection<ProductForList?>)GetValue(ProductsForListProperty); }
            set { SetValue(ProductsForListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductsForList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductsForListProperty =
            DependencyProperty.Register("ProductsForList", typeof(ObservableCollection<ProductForList?>), typeof(ProductList));


        public ProductList()
        {
            ProductsForList = new ObservableCollection<ProductForList?>(bl.Product.getListOfProduct());
            Categories = System.Enum.GetValues(typeof(BO.Enum.Category)).Cast<BO.Enum.Category>();////////////
            InitializeComponent();
        }

        private void List_of_product_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Sort_By_Colmun_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader)!;

            if (gridViewColumnHeader is not null)
            {
                string name = (gridViewColumnHeader.Tag as string)!;
                var listTemp = bl.Product.getListOfProduct();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(List_of_product.ItemsSource);

                view.SortDescriptions.Clear();
                if (hasSorted)
                {

                    view.SortDescriptions.Add(new(name, ListSortDirection.Descending));
                    hasSorted = false;
                }
                else
                {
                    view.SortDescriptions.Add(new SortDescription(name, ListSortDirection.Ascending));
                    hasSorted = true;
                }
            }

        }


        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Object selectedItem = Category_selector.SelectedItem;

                if (selectedItem.ToString() == "All")
                    ProductsForList = new ObservableCollection<ProductForList?>(bl.Product.getListOfProduct());

                else
                    ProductsForList = new ObservableCollection<ProductForList?>(bl.Product.getListOfProduct(a => a?.ProductCategory.ToString() == selectedItem.ToString()));
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
        private void addProductForList(int productId)
        => ProductsForList.Add(bl.Product.GetProductForList(productId));

        private void updateProductForList(int productId)
            => ProductsForList.Remove(bl.Product.GetProductForList(productId));

        private void AddProductWindow_Click(object sender, RoutedEventArgs e)
        {//move to add product window
            new ProductWindows.AddProduct(addProductForList).Show();
        }
        private void List_of_product_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {//move to updete product window
            new ProductWindows.UpdateProduct(updateProductForList, (ProductForList)List_of_product.SelectedItem).Show();

        }

        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            //move to Main window
            new MainWindow().Show();
            this.Close();
        }


    }
}
