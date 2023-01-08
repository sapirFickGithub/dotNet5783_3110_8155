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

        public event Action<int> action;

        // public BO.Cart? MyCart { set; get; }


        public ObservableCollection<BO.Cart?> MyCart//dependency proprty in order to use 'data binding'
        {
            get { return (ObservableCollection<BO.Cart?>)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for order utem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartProperty =
            DependencyProperty.Register("Cart", typeof(ObservableCollection<BO.Cart?>), typeof(CartNew));


        public CartNew(BO.Cart? myCart)
        {

            MyCart = new ObservableCollection<BO.Cart?>((IEnumerable<Cart?>)(myCart));
            this.action = action;
            InitializeComponent();
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
               int idOrder= bl.Cart.approvment(MyCart[0]);
                if (idOrder>0)
                {
                    MessageBox.Show(
                            "Your order is being processed" +
                            "your order number is: " + idOrder+
                            "Thanks for buying!",
                            "Approvment",
                            MessageBoxButton.OK,
                            MessageBoxImage.Hand,
                            MessageBoxResult.None,
                            MessageBoxOptions.RtlReading);

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


        private void Update_in_Cart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

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

        private void Catalog_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
