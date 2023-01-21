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
using DO;

namespace PL.OrderWindows
{
    /// <summary>
    /// Interaction logic for ViewOrder.xaml
    /// </summary>
    public partial class ViewOrder : Window
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();
        // public event Action<int> action;
        public IEnumerable<BO.Enum.OrderStatus> status { set; get; }



        public double totalPrice
        {
            get { return (double)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(double), typeof(ViewOrder));



        public BO.Order order
        {
            get { return (BO.Order)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderProperty =
            DependencyProperty.Register("order", typeof(BO.Order), typeof(ViewOrder));



        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), typeof(ViewOrder));



        //public bool flag
        //{
        //    get { return (bool)GetValue(flagProperty); }
        //    set { SetValue(flagProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for flag.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty flagProperty =
        //    DependencyProperty.Register("flag", typeof(bool), typeof(ViewOrder));





        public BO.OrderForList orderForList { get; set; }
        //public BO.Order orderDeatails { get; set; }

        public bool hasSorted = true;
        public PL.OrderWindows.OrderList parent;

        public ObservableCollection<BO.OrderItem> Items
        {
            get { return (ObservableCollection<BO.OrderItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<BO.OrderItem>), typeof(ViewOrder));


        public ViewOrder(OrderForList orderForLst , PL.OrderWindows.OrderList orderList)
        {

            parent = orderList;
            orderForList = orderForLst;
            order = bl.Order.GetOrder(orderForList.idOfOrder);
            //orderDeatails = new BO.Order(bl.Order.GetOrder(orderForList.idOfOrder));
            //flag = order.Status == BO.Enum.OrderStatus.DELIVERY ?true:false; 
            status = System.Enum.GetValues(typeof(BO.Enum.OrderStatus)).Cast<BO.Enum.OrderStatus>();
            totalPrice = order.TotalPrice;
            Items = new ObservableCollection<BO.OrderItem>(bl.Order.GetOrder(orderForList.idOfOrder).Items);
            Status = order.Status.ToString();
            InitializeComponent();
        }

        private void BackToOrderListWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateOrder_click(object sender, RoutedEventArgs e)
        {
            //if (is_supply.IsChecked == true)
            //    bl.Order.UpdateSupplyDelivery(orderForList.idOfOrder);
            //if(Status_selector.Text== "Dlivery")
            bl.Order.updateDliveryOrder(orderForList.idOfOrder);
            parent.Dispatcher.Invoke(() =>
            {
                parent.OrdersForList = new ObservableCollection<OrderForList?>(bl.Order.GetAllOrderForList());
            });


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Supply_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Update_amount_Click(object sender, MouseButtonEventArgs e)
        {
            new UpdateAmountItem();
        }



        private void List_of_orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void update_ShipDate_Click(object sender, RoutedEventArgs e)
        {
            this.order = bl.Order.UpdateSupplyDelivery(order.idOfOrder);
            parent.Dispatcher.Invoke(() =>
            {
                parent.OrdersForList = new ObservableCollection<OrderForList?>(bl.Order.GetAllOrderForList());
            });
        }

        private void update_DeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            this.order = bl.Order.updateDliveryOrder(order.idOfOrder);
            parent.Dispatcher.Invoke(() =>
            {
                parent.OrdersForList = new ObservableCollection<OrderForList?>(bl.Order.GetAllOrderForList());
            });

        }

        private void Sort_By_Colmun_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void List_of_orders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            Button myButtom = sender as Button;
            int productId = (int)myButtom.Tag;
            BO.OrderItem orderItem = myButtom.DataContext as BO.OrderItem;
            orderItem.amount--;
            bl.Order.updateAdmin(orderItem.idOfOrder, orderItem.idOfProduct, orderItem.amount);
            order = bl.Order.GetOrder(orderForList.idOfOrder);

            parent.Dispatcher.Invoke(()=>
            {
                parent.OrdersForList =  new ObservableCollection<OrderForList?> (bl.Order.GetAllOrderForList());
            });

        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            Button myButtom = sender as Button;
            int productId = (int)myButtom.Tag;
            BO.OrderItem orderItem = myButtom.DataContext as BO.OrderItem;
            orderItem.amount++;
            bl.Order.updateAdmin(orderItem.idOfOrder, orderItem.idOfProduct, orderItem.amount);
            order = bl.Order.GetOrder(orderForList.idOfOrder);

            parent.Dispatcher.Invoke(() =>
            {
                parent.OrdersForList = new ObservableCollection<OrderForList?>(bl.Order.GetAllOrderForList());
            });


        }
    }
}
