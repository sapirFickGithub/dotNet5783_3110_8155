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
    /// Interaction logic for ProductItemList.xaml
    /// </summary>
    public partial class ProductItemList : Window
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();

        public IEnumerable<BO.Enum.Category> Categories { set; get; }

        public bool hasSorted = true;

        public ObservableCollection<ProductItem?> Items
        {
            get { return (ObservableCollection<ProductItem?>)GetValue(ProductsForListProperty); }
            set { SetValue(ProductsForListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductsForListProperty =
            DependencyProperty.Register("ProductsForList", typeof(ObservableCollection<ProductItem?>), typeof(ProductList));

        public ProductItemList()
        {
            Items = new ObservableCollection<ProductItem?>(bl.Product.getListOfProductItem());
            Categories = System.Enum.GetValues(typeof(BO.Enum.Category)).Cast<BO.Enum.Category>();
            InitializeComponent();
        }

        private void Sort_By_Colmun_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader)!;

            if (gridViewColumnHeader is not null)
            {
                string name = (gridViewColumnHeader.Tag as string)!;
                var listTemp = bl.Product.getListOfProductItem();
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
                    Items = new ObservableCollection<ProductItem?>(bl?.Product.getListOfProductItem());

                else
                    Items = new ObservableCollection<ProductItem?>(bl?.Product.getListOfProductItem((a => a?.ProductCategory.ToString() == selectedItem.ToString())));
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
        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            //move to Main window
            new MainWindow().Show();
            this.Close();
        }

        private void update_Item_list(int id)
        {

        }
        private void Add_to_cart(object sender, MouseButtonEventArgs e)
        {

            new CartWindows.CartNew(update_Item_list,(ProductItem)List_of_product.SelectedItem).Show();
            
        }
    }
}





