using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
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
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using BlApi;
using System.Windows.f;
using Simulator;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();

        private Stopwatch stopWatch;//counting the time.

        private bool isTimerRun;

        BackgroundWorker timerworker;


        public static readonly DependencyProperty MyEstimatedTimeProperty =
      DependencyProperty.Register("estimatedTime", typeof(int), typeof(SimulatorWindow));
        public int estimatedTime
        {
            get { return (int)GetValue(MyEstimatedTimeProperty); }
            set { SetValue(MyEstimatedTimeProperty, value); }
        }

        public static readonly DependencyProperty MymaxBarProperty =
            DependencyProperty.Register("maxBar", typeof(int), typeof(SimulatorWindow));

        public int maxBar
        {
            get { return (int)GetValue(MymaxBarProperty); }
            set { SetValue(MymaxBarProperty, value); }
        }

        public static readonly DependencyProperty MyBarProperty =
           DependencyProperty.Register("BarProgress", typeof(int), typeof(SimulatorWindow));

        public int BarProgress
        {
            get { return (int)GetValue(MyBarProperty); }
            set { SetValue(MyBarProperty, value); }
        }




        // Using a DependencyProperty as the backing store for currentOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentOrderProperty =
            DependencyProperty.Register("currentOrder", typeof(BO.Order), typeof(SimulatorWindow));
        public BO.Order currentOrder
        {
            get { return (BO.Order)GetValue(currentOrderProperty); }
            set { SetValue(currentOrderProperty, value); }
        }


        public SimulatorWindow()
        {

            InitializeComponent();
            stopWatch = new Stopwatch();
            timerworker = new BackgroundWorker();
            timerworker.DoWork += Worker_DoWork;
            timerworker.DoWork += Worker_DoWork2;
            timerworker.ProgressChanged += Worker_ProgressChanged;
            timerworker.WorkerReportsProgress = true;
            timerworker.WorkerSupportsCancellation = true;

        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (timerworker.IsBusy != true)
                // Start the asynchronous operation. 
                timerworker.RunWorkerAsync(12);

            if (!isTimerRun)
            {
                stopWatch.Start();
                isTimerRun = true;
                // timerworker.RunWorkerAsync();
                Simulator.Simulator.SubscribeToUpdateSimulation(updateWindowView);
                Simulator.Simulator.startSimulation();
            }

        }


        private void Stop_Click(object sender, RoutedEventArgs e)
        {

            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
        }


        private void updateWindowView(object sender, BO.Order? e)
        {

            EstimatedTime((int)e.TotalPrice);
            CurrentOrder(bl.Order.GetOrder(e.idOfOrder));
        }

        private void CurrentOrder(BO.Order a)
        {
            if (!CheckAccess())
            {
                Action<BO.Order> d = CurrentOrder;
                Dispatcher.BeginInvoke(d, new BO.Order() { idOfOrder = a.idOfOrder, Status = a.Status });

            }
            else
            {
                currentOrder = a;
            }
        }

        private void EstimatedTime(int a)
        {
            if (!CheckAccess())
            {
                Action<int> d = EstimatedTime;
                Dispatcher.BeginInvoke(d, new object[] { a });
            }
            else
            {
                estimatedTime = a;
                maxBar = a;
                BarProgress = 0;
            }
        }


        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                // e.Result throw System.InvalidOperationException
                resultLabel.Content = "Canceled!";
            }
            else if (e.Error != null)
            {
                // e.Result throw System.Reflection.TargetInvocationException
                resultLabel.Content = "Error: " + e.Error.Message; //Exception Message
            }
            else
            {
                long result = (long)e.Result;
                if (result < 1000)
                    resultLabel.Content = "Done after " + result + " ms.";
                else
                    resultLabel.Content = "Done after " + result / 1000 + " sec.";
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            this.Timer.Text = timerText;
            int progress = e.ProgressPercentage;
            resultLabel.Content = (progress + "%");
            BarProgress = progress;
        }




        //private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    string timerText = stopWatch.Elapsed.ToString();
        //    timerText = timerText.Substring(0, 8);
        //    this.Timer.Text = timerText;
        //}


        private void Worker_DoWork2(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerworker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }


        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            // BackgroundWorker worker = sender as BackgroundWorker;
            int length = (int)e.Argument;

            for (int i = 1; i <= length; i++)
            {

                if (timerworker.CancellationPending == true)
                {
                    e.Cancel = true;
                    e.Result = stopwatch.ElapsedMilliseconds; // Unnecessary
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(500);
                    timerworker.ReportProgress(i * 100 / length);
                }
            }

            e.Result = stopwatch.ElapsedMilliseconds;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Closing -= Window_Closing;
            this.Close();
        }

        private BO.Order? ShowOrder(BO.Order? order)
        {
            // Simulator.Simulator.SubscribeToUpdateSimulation(ShowOrder);
            return order;
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }


    }
}
