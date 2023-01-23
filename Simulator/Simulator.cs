﻿using System;
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
        thread = new Thread(Simulation);
        thread.Start();
    }


    public static void Simulation()//the function gat the right order and take care about her until the order changed the status
    {

        while (!isStop && bl.Order.precedenceOrder() != null)
        {
            Order? order = bl.Order.GetOrder(bl.Order.precedenceOrder());// get the oldest order

            int time = new Random().Next(2, 10) * 1000;

            order.TotalPrice = time; //use the total price as saver of the time

            updateSimulation.Invoke(null,order);

            Thread.Sleep(time);

           
            if (order.DateOfShipping == null)
            {
               order= bl.Order.UpdateSupplyDelivery(order.idOfOrder);
            }
            else
            {
               order= bl.Order.updateDliveryOrder(order.idOfOrder);
            }

            order.TotalPrice = 0;

           updateSimulation.Invoke(null, order);
        }
        stopSimulation();
    }

    public static void stopSimulation()
    {
       // isStop = true;
        thread.Interrupt();
    }



    public static void SubscribeToUpdateSimulation(EventHandler<Order?> handler)
    {
        updateSimulation += handler;
    }



  

}