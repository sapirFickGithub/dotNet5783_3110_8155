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
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace PL.CartWindows
{
    /// <summary>
    /// Interaction logic for CartNew.xaml
    /// </summary>
    public partial class CartNew : Window
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();

        public bool hasSorted = true;

       
        public ObservableCollection<BO.OrderItem> items
        {
            get { return (ObservableCollection<BO.OrderItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("items", typeof(ObservableCollection<BO.OrderItem>), typeof(CartNew));



        public ObservableCollection<BO.Cart> MyCart//dependency proprty in order to use 'data binding'
        {
            get { return (ObservableCollection<BO.Cart>)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }
        public static readonly DependencyProperty CartProperty =
           DependencyProperty.Register("MyCart", typeof(ObservableCollection<BO.Cart>), typeof(CartNew));


        public double TotalPrice
        {
            get { return (double)GetValue(TotalPrieProperty); }
            set { SetValue(TotalPrieProperty, value); }
        }
        public static readonly DependencyProperty TotalPrieProperty =
            DependencyProperty.Register("TotalPrice", typeof(double), typeof(CartNew));



        public ProductWindows.ProductItemList parent;

        public CartNew(BO.Cart myCart, ProductWindows.ProductItemList parnt)
        {
            parent = parnt;
            TotalPrice = myCart.TotalPrice;
            items = new ObservableCollection<BO.OrderItem>(myCart.itemList);
            MyCart = new ObservableCollection<BO.Cart> { myCart };
            InitializeComponent();
        }

        private void List_of_product_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void approve_orer_Click(object sender, RoutedEventArgs e)
        {
            if (customer_Name.Text == "")
                MessageBox.Show(
                            "Please enter your name",
                            "Waiting for input...",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.Hand,
                            MessageBoxResult.Cancel,
                            MessageBoxOptions.RtlReading);
            else if (customer_Address.Text == "") MessageBox.Show(
                        "Please enter your address",
                        "Waiting for input...",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);
            else if ((customer_Mail.Text == "") || (!customer_Mail.Text.EndsWith("@gmail.com"))) MessageBox.Show(
                        "Please enter correct Email address.",
                        "Waiting for correct input...",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);

            MyCart[0].CustomerName = customer_Name.Text;
            MyCart[0].CustomerAddress = customer_Address.Text;
            MyCart[0].CustomerMail = customer_Mail.Text;
            try
            {
                int idOrder = bl.Cart.approvment(MyCart[0]);
                if (idOrder > 0)
                {
                    MessageBox.Show(
                            "Your order is being processed " +
                            "your order number is:   " + idOrder +
                            "   Thanks for buying!",
                            "Approvment",
                            MessageBoxButton.OK,
                            MessageBoxImage.Hand,
                            MessageBoxResult.None,
                            MessageBoxOptions.RtlReading);

                    new MainWindow().Show();

                    parent.Close();
                    this.Close();
                }
            }
            catch (BO.outOfStock a)
            {
                MessageBox.Show(
                           "Your order is being processed" +
                          a.idOfProduct,
                           "Approvment",
                           MessageBoxButton.OK,
                           MessageBoxImage.Hand,
                           MessageBoxResult.None,
                           MessageBoxOptions.RtlReading);
            }

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






        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            OrderItem orderItem = button.DataContext as OrderItem;

            var p = bl.Product.GetProduct(orderItem.idOfProduct);

            int productId = (int)button.Tag;

            if ((p.InStock - orderItem.amount < 0))
                MessageBox.Show(
                         "No more of this item in stock :( ",
                         "Out of stock",
                         MessageBoxButton.OK,
                         MessageBoxImage.Hand,
                         MessageBoxResult.Cancel,
                         MessageBoxOptions.RtlReading);

            else
            {
                bl.Cart.add(MyCart[0], productId);

                // Find the product in the collection and update its amount
                var product = parent.Items.FirstOrDefault(p => p.idOfProduct == productId);
                if (product != null)
                {
                    product.Amount++;
                }
                this.TotalPrice = MyCart[0].TotalPrice;
                //עידכון המחיר הכולל
               //this.TotalPrice[0] += product.Price;

                // Update the OrderForObservableCollection in the parent window
                this.Dispatcher.Invoke(() =>
                {
                    parent.Items = new ObservableCollection<BO.ProductItem>(parent.Items);
                    this.items = new ObservableCollection<BO.OrderItem>(this.items);
                    this.TotalPrice = this.TotalPrice;
                });
            }
        }




        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            Button myButtom = sender as Button;
            int productId = (int)myButtom.Tag;


            OrderItem orderItem = myButtom.DataContext as OrderItem;


            if (orderItem.amount == 0)
                MessageBox.Show(
                         "Cant ",
                         "ERROR",
                         MessageBoxButton.OK,
                         MessageBoxImage.Hand,
                         MessageBoxResult.Cancel,
                         MessageBoxOptions.RtlReading);
            else
            {

                // Find the product in the `productItemForObservableCollection` list
                var product = parent.Items.FirstOrDefault(item => item.idOfProduct == productId);

                if (product != null)
                {
                    product.Amount--;

                    // If the product's amount is now zero, remove it from the `listOfOrderItemForObservableCollection` list
                    if (product.Amount == 0)

                        this.items.Remove(
                            this.items.FirstOrDefault(x => x.idOfProduct == product.idOfProduct)
                           

                            );

                    // Otherwise, update the product's quantity in the `dataCart` object
                    bl.Cart.updete(MyCart[0], productId, (int)product.Amount);


                    this.TotalPrice = MyCart[0].TotalPrice;


                    this.Dispatcher.Invoke(() =>
                    {
                        parent.Items = new ObservableCollection<BO.ProductItem>(parent.Items);
                        this.items = new ObservableCollection<BO.OrderItem>(this.items);

                       this.TotalPrice = this.TotalPrice;
                    });



                }
            }
        }

       
    }
}

