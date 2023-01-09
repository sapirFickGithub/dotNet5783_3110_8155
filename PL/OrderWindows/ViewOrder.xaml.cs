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
using PL.ProductWindows;

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

        public BO.OrderForList order { get; set; }
        public bool hasSorted = true;

        public ObservableCollection<OrderItem> Items
        {
            get { return (ObservableCollection<OrderItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<OrderItem>), typeof(ViewOrder));


        public ViewOrder(OrderForList? orderForList)
        {
            order = orderForList;
            status = System.Enum.GetValues(typeof(BO.Enum.OrderStatus)).Cast<BO.Enum.OrderStatus>();
            Items = new ObservableCollection<OrderItem>(bl.Order.GetOrder(order.idOfOrder).Items);

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

        }

        private void Sort_By_Colmun_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader)!;

            if (gridViewColumnHeader is not null)
            {
                string name = (gridViewColumnHeader.Tag as string)!;
                ObservableCollection<BO.OrderForList?> listTemp = (ObservableCollection<OrderForList?>)(bl?.Order?.GetAllOrderForList());
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(List_of_orders.ItemsSource);

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
    }
}
