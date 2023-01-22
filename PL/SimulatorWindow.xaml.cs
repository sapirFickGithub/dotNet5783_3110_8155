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
using System.Windows.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {

      //  private StopWatch timer { get; set; }

        public SimulatorWindow()
        {
            InitializeComponent();
        }

     
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            
        }

  

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;    
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Closing-= Window_Closing;
            this.Close();
        }
    }
}
