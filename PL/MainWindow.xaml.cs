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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using BlImplementation;
using BlApi;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Admin_Click(object ob, RoutedEventArgs e)
        {
            new Admin().Show();
            this.Close();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            BO.Cart cart = new BO.Cart();
            new ProductWindows.ProductItemList(cart).Show();
            this.Close();
        }

        private void Track_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(ID_to_track.Text, out int id);
            new OrderWindows.Tracking(id).Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void open_simolator(object sender, RoutedEventArgs e)
        {
            new SimulatorWindow().Show();
        }

     
    }
}
