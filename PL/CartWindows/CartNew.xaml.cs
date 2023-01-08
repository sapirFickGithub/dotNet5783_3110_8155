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

        public event Action<int> action;

        public BO.Cart? MyCart { set; get; }


        public ObservableCollection<BO.OrderItem?> CartItems//dependency proprty in order to use 'data binding'
        {
            get { return (ObservableCollection<BO.OrderItem?>)GetValue(OrderItemProperty); }
            set { SetValue(OrderItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductsForList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderItemProperty =
            DependencyProperty.Register("CartItems", typeof(ObservableCollection<BO.OrderItem?>), typeof(CartNew));


        public CartNew(Action<int> action, ProductItem productItem, BO.Cart? myCart)
        {

            MyCart = myCart;
            CartItems = new ObservableCollection<ProductForList?>(myCart?.itemList);
            this.action = action;
            InitializeComponent();
        }

        private void approve_orer_Click(object sender, RoutedEventArgs e)
        {
            MyCart.CustomerName = costumer_Name.Text;
            MyCart.CustomerAddress = costumer_Address.Text;
            MyCart.CustomerMail=costumer_Mail.Text;
            bl.Cart.approvment(MyCart);
        }
    }
}
