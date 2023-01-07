using System;
using System.Collections.Concurrent;
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
using BlApi;
using BlImplementation;
using BO;


namespace PL.OrderWindows
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : Window
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();
       
        private readonly DependencyProperty ordersForListtProperty;

        public BO.OrderForList? order { set; get; }

        public ObservableCollection<OrderForList?> ordersForListt//dependency proprty in order to use 'data binding'
        {
            get { return (ObservableCollection<OrderForList?>)GetValue(ordersForListtProperty); }
            set { SetValue(ordersForListtProperty, value); }
        }
        public static readonly DependencyProperty ProductsForListProperty =
           DependencyProperty.Register("ordersForListt", typeof(ObservableCollection<OrderForList?>), typeof(OrderList));
        public OrderView(ObservableCollection<OrderForList?> ordersForList,OrderForList orderForList)
        {
            ordersForListt = ordersForList;
            order = bl?.Order?.GetOneOrderForList(orderForList.idOfOrder);
            InitializeComponent();
        }


        private void List_of_order_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BackToOrderListWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void updateOrderForList(int orderId)
          => ordersForListt.Remove(bl?.Order.GetOneOrderForList(orderId));

        private void UpdateOrder_click(object sender, RoutedEventArgs e)
        {
            new OrderWindows.UpdateOrder(updateOrderForList, order);
        }
        //  private void List_of_orders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //=> new OrderWindows.OrderView((OrderForList)List_of_orders.SelectedItem).Show();

    }
}
