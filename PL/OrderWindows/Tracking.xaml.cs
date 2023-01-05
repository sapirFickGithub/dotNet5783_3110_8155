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
using System.Collections.ObjectModel;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
namespace PL.OrderWindows
{
    /// <summary>
    /// Interaction logic for Tracking.xaml
    /// </summary>

    public partial class Tracking : Window
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();

 public BO.OrderTracking orderTrack { set; get; }

        public Tracking(int id)
        {
            orderTrack = bl?.Order.orderTracking(id);
            InitializeComponent();
        }
        //private string _status;
        //public string Status
        //{
        //    get { return _status; }
        //    set
        //    {
        //        if (PropertyChanged != null)
        //            PropertyChanged(this, new PropertyChangedEventArgs(_status));

        //    }
        //}

        //public event PropertyChangedEventHandler? PropertyChanged;
       
        //private void OnPropertyChanged(string track)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(track));
        //}
       //private void GoTracking_Click(object sender, RoutedEventArgs e)
       // {
       //     int.TryParse(ID.Text, out int id);
       //     orderTrack = bl?.Order.orderTracking(id);
       //     _status = orderTrack.Status.ToString();
       // }      
        
        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }


    }
}
