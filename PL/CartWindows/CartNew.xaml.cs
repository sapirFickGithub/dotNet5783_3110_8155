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

       // public BO.Cart? MyCart { set; get; }


        public ObservableCollection<BO.Cart?> MyCart//dependency proprty in order to use 'data binding'
        {
            get { return (ObservableCollection<BO.Cart?>)GetValue(CartProperty); }
            set { SetValue(CartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for order utem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CartProperty =
            DependencyProperty.Register("Cart", typeof(ObservableCollection<BO.Cart?>), typeof(CartNew));


        public CartNew(Action<int> action, ProductItem productItem, BO.Cart? myCart)
        {
           
                MyCart = new ObservableCollection<BO.Cart?>((IEnumerable<Cart?>)(myCart));
                this.action = action;
                InitializeComponent();
        }

        private void approve_orer_Click(object sender, RoutedEventArgs e)
        {
            if(costumer_Name.Text=="")
            MessageBox.Show(
                        "Please enter your name",
                        "Waiting for input...",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Hand,
                        MessageBoxResult.Cancel,
                        MessageBoxOptions.RtlReading);
            else if(costumer_Addre.Text==


            MyCart[0].CustomerName = costumer_Name.Text;
            MyCart[0].CustomerAddress = costumer_Address.Text;
            MyCart[0].CustomerMail=costumer_Mail.Text;
            bl.Cart.approvment(MyCart[0]);
        }
    }
}
