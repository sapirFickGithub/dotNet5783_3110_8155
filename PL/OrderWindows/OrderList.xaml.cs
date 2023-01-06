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
using PL.ProductWindows;
using System.Collections.ObjectModel;

namespace PL.OrderWindows
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class OrderList : Window
    {

        private static BlApi.IBl? bl = BlApi.Factory.Get();

        public bool hasSorted = true;
        
        public ObservableCollection<OrderForList?> OrdersForList
        {
            get { return (ObservableCollection<OrderForList?>)GetValue(OrdersForListProperty); }
            set { SetValue(OrdersForListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdersForListProperty =
            DependencyProperty.Register("OrdersForList", typeof(ObservableCollection<OrderForList?>), typeof(OrderList));

        public OrderList()
        {
            OrdersForList = new ObservableCollection<OrderForList?>(bl.Order.GetAllOrderForList());
            InitializeComponent();
        }

        private void Sort_By_Colmun_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader)!;

            if (gridViewColumnHeader is not null)
            {
                string name = (gridViewColumnHeader.Tag as string)!;
                var listTemp = bl.Order.GetAllOrderForList();
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

        private void addOrderForList(int orderId)
            => OrdersForList.Add(bl.Order.GetOneOrderForList(orderId));


        //private void Add_Order_Window_Click(object sender, RoutedEventArgs e)
        //=> new OrderWindows.AddOrder().Show();



        private void List_of_orders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
           =>  new OrderWindows.UpdateOrder().Show();
        

        private void List_of_orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }

}
