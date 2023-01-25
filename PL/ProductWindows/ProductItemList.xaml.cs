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
        public IEnumerable<BO.Enum.CategoryView> Categories { set; get; }
        public BO.ProductItem productItem { get; set; }
        public BO.Product? product { get; set; }


        public bool hasSorted = true;



        public ObservableCollection<ProductItem> Items
        {
            get { return (ObservableCollection<ProductItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<ProductItem>), typeof(ProductItemList));

        ICollectionView collectionView;
        PropertyGroupDescription propertyGroupDescription;


        public ProductItemList(BO.Cart cart)
        {


            product = new BO.Product();
            productItem = new BO.ProductItem();
            MyCart = cart;
            Items = new ObservableCollection<ProductItem>(bl.Product.getListOfProductItem());
            Categories = System.Enum.GetValues(typeof(BO.Enum.CategoryView)).Cast<BO.Enum.CategoryView>();
            InitializeComponent();
            itemsListInitialize();
            collectionView = CollectionViewSource.GetDefaultView(Items);
            propertyGroupDescription = new PropertyGroupDescription("Category");
        }


        private void itemsListInitialize()
        {
            if (MyCart.TotalPrice != 0)//its meen the cart is empty
            {
                foreach (var item in MyCart.itemList)
                {
                    foreach (var it in Items)
                    {
                        if (it.idOfProduct == item.idOfProduct)
                            it.Amount = item.amount;
                    }
                }
            }
        }


        private void Sort_By_Colmun_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader)!;

            if (gridViewColumnHeader is not null)
            {
                string name = (gridViewColumnHeader.Tag as string)!;
                var listTemp = bl?.Product.getListOfProductItem();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(((ListView)sender).ItemsSource);

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
                Object selectedItem = (BO.Enum.CategoryView)((ComboBox)sender).SelectedItem;

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


        private void Go_to_Cart_Click(object sender, RoutedEventArgs e)
        {
            if (MyCart != null)
                new CartWindows.CartNew(MyCart, this).ShowDialog();

        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {

            var myButtom = sender as Button;

            ProductItem productItem = myButtom.DataContext as ProductItem;

            var p = bl.Product.GetProduct(productItem.idOfProduct);


            if ((productItem.InStock == false) && (p.InStock - productItem.Amount <= 0))
                MessageBox.Show(
                         "No more of this item in stock :( ",
                         "Out of stock",
                         MessageBoxButton.OK,
                         MessageBoxImage.Hand,
                         MessageBoxResult.Cancel,
                         MessageBoxOptions.RtlReading);
            else
            {
                Items.Remove(productItem);

                productItem.Amount++;
                Items.Add(productItem);

                bl.Cart.add(MyCart, productItem.idOfProduct);


            }

        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {


            var myButtom = sender as Button;

            ProductItem productItem = myButtom.DataContext as ProductItem;

            var p = bl.Product.GetProduct(productItem.idOfProduct);


            if (productItem.Amount == 0)
                MessageBox.Show(
                         "Cant ",
                         "ERROR",
                         MessageBoxButton.OK,
                         MessageBoxImage.Hand,
                         MessageBoxResult.Cancel,
                         MessageBoxOptions.RtlReading);
            else
            {
                Items.Remove(productItem);
                productItem.Amount--;
                Items.Add(productItem);

                bl.Cart.updete(MyCart, productItem.idOfProduct, productItem.Amount);


            }
        }

        private void GroupingCheak_Checked(object sender, RoutedEventArgs e)
        {

            collectionView.GroupDescriptions.Add(propertyGroupDescription);

        }

        private void GroupingCheak_UnChecked(object sender, RoutedEventArgs e)
        {

            collectionView.GroupDescriptions.Remove(propertyGroupDescription);
        }
    }
}





