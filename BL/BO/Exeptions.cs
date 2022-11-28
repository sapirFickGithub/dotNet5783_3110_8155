using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class notExist : Exception
    {
        public string Messege => "NOT EXIST!";
        public string ToString()
        {
            return Messege;
        }
    }
    public class IncorrectData : Exception
    {
        public string Messege => "INCORRECT DATA!";
        public string ToString()
        {
            return Messege;
        }
    }
    public class exitInOrders : Exception
    {
        public string Messege => "EXIST IN ORDER!";
        public string ToString()
        {
            return Messege;
        }
    }

}
