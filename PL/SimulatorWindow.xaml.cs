
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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



        public string timerText
        {
            get { return (string)GetValue(timerTextProperty); }
            set { SetValue(timerTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for timerText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty timerTextProperty =
            DependencyProperty.Register("timerText", typeof(string), typeof(SimulatorWindow));



        public int maxBar
        {
            get { return (int)GetValue(MymaxBarProperty); }
            set { SetValue(MymaxBarProperty, value); }
        }
        public static readonly DependencyProperty MymaxBarProperty =
            DependencyProperty.Register("maxBar", typeof(int), typeof(SimulatorWindow));


        public static readonly DependencyProperty MyBarProperty =
           DependencyProperty.Register("BarProgress", typeof(int), typeof(SimulatorWindow));

        public int BarProgress
        {
            get { return (int)GetValue(MyBarProperty); }
            set { SetValue(MyBarProperty, value); }
        }



        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Time.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(string), typeof(SimulatorWindow));



        public string CuurentTime
        {
            get { return (string)GetValue(CuurentTimeProperty); }
            set { SetValue(CuurentTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Time.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CuurentTimeProperty =
            DependencyProperty.Register("CuurentTime", typeof(string), typeof(SimulatorWindow));



        public BO.Order currentOrder
        {
            get { return (BO.Order)GetValue(currentOrderProperty); }
            set { SetValue(currentOrderProperty, value); }
        }
        // Using a DependencyProperty as the backing store for currentOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentOrderProperty =
            DependencyProperty.Register("currentOrder", typeof(BO.Order), typeof(SimulatorWindow));



        public string oldStatus
        {
            get { return (string)GetValue(oldStatusProperty); }
            set { SetValue(oldStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for oldStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty oldStatusProperty =
            DependencyProperty.Register("oldStatus", typeof(string), typeof(SimulatorWindow));


        DispatcherTimer dispatcherTimer;


        public SimulatorWindow()
        {

            InitializeComponent();
            oldStatus = "";
            stopWatch = new Stopwatch();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += dispatcherTimer_Tick;

            if (bl.Order.precedenceOrder() == null)
            {
                if (isTimerRun)
                {
                    stopWatch.Stop();
                    isTimerRun = false;
                }
                MessageBox.Show(
                                    "All orders have been processed!",
                                    "OVER",
                                     MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation,
                                     MessageBoxResult.OK
                                    /*  MessageBoxOptions.RtlReading*/);
                this.Close();

            }

        }

        private void dispatcherTimer_Tick(object sender, EventArgs eventArgs)
        {
            BarProgress += 1;
            timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            //this.Timer.Text = timerText;
            int progress = (int)(((float)BarProgress / (float)maxBar) * 100);

            resultLabel.Content = (progress + "%");

        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bl.Order.precedenceOrder() == null)
                {
                    if (isTimerRun)
                    {
                        stopWatch.Stop();
                        isTimerRun = false;
                    }
                    MessageBox.Show(
                                        "All orders have been processed!",
                                        "OVER",
                                         MessageBoxButton.OK,
                                        MessageBoxImage.Exclamation,
                                         MessageBoxResult.OK
                                        /*  MessageBoxOptions.RtlReading*/);
                    this.Close();


                }
                else
                {
                    dispatcherTimer.Start();

                    if (!isTimerRun && bl.Order.precedenceOrder != null)
                    {
                        stopWatch.Start();
                        isTimerRun = true;

                        Simulator.Simulator.SubscribeToUpdateSimulation(updateWindowView, finishSimulator);
                        Simulator.Simulator.startSimulation();

                    }
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Somthing is going wrong, close and try again", "Bay", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }

        }


        private void finishSimulator()
        {
            if (bl.Order.precedenceOrder() == null)
            {
                if (isTimerRun)
                {
                    stopWatch.Stop();
                    isTimerRun = false;
                }
                MessageBox.Show(
                                    "All orders have been processed!",
                                    "OVER",
                                     MessageBoxButton.OK,
                                    MessageBoxImage.None,
                                     MessageBoxResult.OK
                                    /*  MessageBoxOptions.RtlReading*/);
                            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {

            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
            Simulator.Simulator.stopSimulation();
            dispatcherTimer.Stop();

        }


        private void updateWindowView(object sender, BO.Order? e)
        {
            StatusUpdating(e.Status.ToString());
            EstimatedTime((int)e.TotalPrice);
            CurrentOrder(bl.Order.GetOrder(e.idOfOrder));

        }

        private void StatusUpdating(string stt)
        {
            if (!CheckAccess())
            {
                Action<string> d = StatusUpdating;
                Dispatcher.BeginInvoke(d, new object[] { stt });

            }
            else
            {
                oldStatus = stt;
            }
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
                Time = (DateTime.Now + TimeSpan.FromSeconds(estimatedTime)).ToString();
                CuurentTime = DateTime.Now.ToString();
            }
        }


        //private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

            this.Closing -= Window_Closing;
            this.Close();
        }


    }
}