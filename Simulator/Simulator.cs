//using System;
//using System.Threading;
//using System.ComponentModel;
//using BlApi;
//using BO;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Input;
//using Microsoft.VisualBasic;


//namespace Simulator;


//public static class Simulator
//{

//    private static IBl bl = Factory.Get();
//    private static Thread thread;
//    private static volatile bool isStop = false;//check if the user clicked on the stop button


//    public static Order oredr { get; set; }

//    private static event EventHandler<Order?> updateSimulation;//

//    public static void startSimulation()
//    {
//        isStop = false;
//        thread = new Thread(Simulation);
//        thread.Start();
//    }


//    public static void Simulation()//the function gat the right order and take care about her until the order changed the status
//    {
//        int? idOrder = bl.Order.precedenceOrder() ?? throw new BO.notExist();

//        while (!isStop && idOrder != null)
//        {  
//            Order? order = bl.Order.GetOrder(idOrder);// get the oldest order
//            int time = new Random().Next(2, 7);

//            order.TotalPrice = time; //use the total price as saver of the time

//            updateSimulation.Invoke(null, order);
//            try
//            {
//                Thread.Sleep(time * 1000);
//            }
//            catch (ThreadInterruptedException)
//            {

//            }
//            try
//            {

//                if (order.DateOfShipping == null)
//                {
//                    order = bl.Order.UpdateSupplyDelivery(order.idOfOrder);
//                }
//                else
//                {
//                    order = bl.Order.updateDliveryOrder(order.idOfOrder);
//                }

//                idOrder = bl.Order.precedenceOrder() ?? throw new BO.notExist();
//            }
//            catch (Exception)
//            {
//                return;// System.Windows.MessageBox.Show("The simulator is over!", "Bay", MessageBoxButton.OK, MessageBoxImage., MessageBoxResult.OK);
//            }

//            updateSimulation.Invoke(null, order);

//        }
//        stopSimulation();
//    }

//    public static void stopSimulation()
//    {
//        isStop = true;
//        thread.Interrupt();

//    }



//    public static void SubscribeToUpdateSimulation(EventHandler<Order?> handler)
//    {
//        updateSimulation += handler;
//    }





//}



using System;
using System.Threading;
using System.ComponentModel;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualBasic;


namespace Simulator;


public static class Simulator
{

    private static IBl bl = Factory.Get();
    private static Thread thread;
    private static volatile bool isStop = false;//check if the user clicked on the stop button





    public static Order oredr { get; set; }

    private static event EventHandler<Order?> updateSimulation;//

    public static void startSimulation()
    {
        isStop = false;
        thread = new Thread(Simulation);
        thread.Start();
    }


    public static void Simulation()//the function gat the right order and take care about her until the order changed the status
    {

        while (!isStop && bl.Order.precedenceOrder() != null)
        {
            Order? order = bl.Order.GetOrder(bl.Order.precedenceOrder() ?? throw new NullReferenceException());// get the oldest order

            int time = new Random().Next(2, 7);

            order.TotalPrice = time; //use the total price as saver of the time

            updateSimulation.Invoke(null, order);
            try
            {
                Thread.Sleep(time * 1000);
            }
            catch (ThreadInterruptedException)
            {

            }
            try
            {

                if (order.DateOfShipping == null)
                {
                    order = bl.Order.UpdateSupplyDelivery(order.idOfOrder);
                }
                else
                {
                    order = bl.Order.updateDliveryOrder(order.idOfOrder);
                }
            }
            catch (Exception)
            {
                // System.Windows.MessageBox.Show("The simulator is over!", "Bay", MessageBoxButton.OK, MessageBoxImage., MessageBoxResult.OK);
            }

            //order.TotalPrice = 0;

            updateSimulation.Invoke(null, order);
        }
        stopSimulation();
    }

    public static void stopSimulation()
    {
        isStop = true;
        thread.Interrupt();
    }



    public static void SubscribeToUpdateSimulation(EventHandler<Order?> handler)
    {
        updateSimulation += handler;
    }





}