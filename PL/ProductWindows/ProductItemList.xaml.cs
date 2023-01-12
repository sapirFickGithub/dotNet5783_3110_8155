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
using DO;


namespace PL.ProductWindows
{
    /// <summary>
    /// Interaction logic for ProductItemList.xaml
    /// </summary>
    public partial class ProductItemList : Window
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Cart MyCart { get; set; }
        public IEnumerable<BO.Enum.Category> Categories { set; get; }
        public BO.ProductItem productItem { get; set; }
        public BO.Product? product { get; set; }


        public bool hasSorted = true;

        //public bool CartShow = true;

        public ObservableCollection<ProductItem> Items
        {
            get { return (ObservableCollection<ProductItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<ProductItem>), typeof(ProductItemList));

        public ProductItemList()
        {
            product = new BO.Product();
            // productItem = new BO.ProductItem();
            MyCart = new BO.Cart();
            Items = new ObservableCollection<ProductItem>(bl.Product.getListOfProductItem());
            Categories = System.Enum.GetValues(typeof(BO.Enum.Category)).Cast<BO.Enum.Category>();
            InitializeComponent();
        }

        private void Sort_By_Colmun_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader)!;

            if (gridViewColumnHeader is not null)
            {
                string name = (gridViewColumnHeader.Tag as string)!;
                var listTemp = bl?.Product.getListOfProductItem();
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
                    Items = new ObservableCollection<ProductItem>(bl.Product.getListOfProductItem());

                else
                    Items = new ObservableCollection<ProductItem>(bl.Product.getListOfProductItem((a => a?.ProductCategory.ToString() == selectedItem.ToString())));
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
        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            //move to Main window
            new MainWindow().Show();
            this.Close();
        }
        private void Add_to_cart(object sender, MouseButtonEventArgs e)
        {
            // productItem = bl?.Product.GetProduct(((ProductItem)List_of_product.SelectedItem).idOfProduct);

            

            product = bl?.Product.GetProduct(((ProductItem)List_of_product.SelectedItem).idOfProduct);
            try
            {
                if (product?.InStock - 1 < 0)
                {
                    throw new BO.outOfStock(product.idOfProduct);
                }

                BO.ProductItem deltemp = new ProductItem();
                productItem =  bl.Product.GetProductItem(((ProductItem)List_of_product.SelectedItem).idOfProduct);
         
                bl.Product.delete(productItem.idOfProduct);
                productItem.Amount++;
                MyCart = bl?.Cart.add(MyCart, (productItem.idOfProduct))?? throw new BO.incorrectData();
                Items.Add(productItem);
            }
            catch (BO.outOfStock ex)
            {
                MessageBox.Show(
                        "The product " + ex.idOfProduct +
                        "is out of stock",
                       "Out of stock",
                        MessageBoxButton.OK,
                        MessageBoxImage.Hand,
                        MessageBoxResult.OK,
                        MessageBoxOptions.RtlReading);
            }
            catch (BO.incorrectData)
            {
                MessageBox.Show(
                       "There is a data problem; try again.",
                       "Incorrect data:",
                        MessageBoxButton.OK,
                        MessageBoxImage.Hand,
                        MessageBoxResult.OK,
                        MessageBoxOptions.RtlReading);
            }
        }

        private void Go_to_Cart_Click(object sender, RoutedEventArgs e)
        {
            new CartWindows.CartNew(MyCart);
            ///this.Close();
        }
    }
}





