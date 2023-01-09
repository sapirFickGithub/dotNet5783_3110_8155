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

        public BO.OrderForList? order { get; set; }



        public ViewOrder(OrderForList? orderForList)
        {
            order = orderForList;
            status = System.Enum.GetValues(typeof(BO.Enum.OrderStatus)).Cast<BO.Enum.OrderStatus>();

            InitializeComponent();
        }

        private void BackToOrderListWindow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateOrder_click(object sender, RoutedEventArgs e)
        {
            if (is_supply.IsChecked == true)
                bl.Order.UpdateSupplyDelivery(order.idOfOrder);
            if(Status_selector.Text== "Dlivery")
            bl.Order.updateDliveryOrder(order.idOfOrder);
            if(order.)
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
