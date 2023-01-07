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
using DO;

namespace PL.OrderWindows
{
    /// <summary>
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();
        public event Action<int> action;
        public BO.OrderForList? order { set; get; }
        public UpdateOrder(Action<int> action,OrderForList orderForList)
        {
            order = bl?.Order?.GetOneOrderForList(orderForList.idOfOrder);
            this.action = action;
            InitializeComponent();
        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
