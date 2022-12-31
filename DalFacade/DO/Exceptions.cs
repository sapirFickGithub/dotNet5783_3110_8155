using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class duplication : Exception
    {
        public string Messege => "DUPLICATION!";
        public string ToString()
        {
            return Messege;
        }
    }
    public class notExist : Exception
    {
        public string Messege => "NOT EXIST!";
        public string ToString()
        {
            return Messege;
        }
    }
    public class overload : Exception
    {
        public string Messege => "OVERLOAD!";
        public string ToString()
        {
            return Messege;
        }
    }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

