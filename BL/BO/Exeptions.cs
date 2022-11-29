﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class notExist : Exception
    {
        public string? Messege => "NOT EXIST!";
        public string? ToString()
        {
            return Messege;
        }
    }
    public class incorrectData : Exception
    {
        public string? Messege => "INCORRECT DATA!";
        public string? ToString()
        {
            return Messege;
        }
    }
    public class existInOrders : Exception
    {
        public string? Messege => "EXIST IN ORDER!";
        public string? ToString()
        {
            return Messege;
        }
    }

    public class outOfStock : Exception
    {
        public string? Messege => "OUT OF STOCK!";
        public string? ToString()
        {
            return Messege;
        }
    }
}