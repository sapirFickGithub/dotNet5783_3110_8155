﻿using System;
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


        public BO.Order currentOrder
        {
            get { return (BO.Order)GetValue(currentOrderProperty); }
            set { SetValue(currentOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for currentOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentOrderProperty =
            DependencyProperty.Register("currentOrder", typeof(BO.Order), typeof(SimulatorWindow));



        public SimulatorWindow()
        {

            InitializeComponent();
            stopWatch = new Stopwatch();
            timerworker = new BackgroundWorker();
            timerworker.DoWork += Worker_DoWork;
            timerworker.ProgressChanged += Worker_ProgressChanged;
            timerworker.WorkerReportsProgress = true;

        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (!isTimerRun)
            {
                stopWatch.Start();
                isTimerRun = true;
                timerworker.RunWorkerAsync();
                Simulator.Simulator.SubscribeToUpdateSimulation(updateWindowView);
                Simulator.Simulator.startSimulation();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Update the value of the progress bar
            

            // Check if the progress bar has reached its maximum value
            
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
                Dispatcher.BeginInvoke(d, new BO.Order() { idOfOrder = a.idOfOrder, Status= a.Status });

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


        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BarProgress+=1;
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            this.Timer.Text = timerText;
        }


        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                BarProgress += 1;
                timerworker.ReportProgress(1);
                Thread.Sleep(1000);
            }
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
