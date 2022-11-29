using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class duplication : Exception
    {
        public string Messege => "DUPLICATION!";
        public string ToString()
        {
            return Messege;
        }
    }
    public class notFound : Exception
    {
        public string Messege => "NOT FOUND!";
        public string ToString()
        {
            return Messege;
        }
    }
}
