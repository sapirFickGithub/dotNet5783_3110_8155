using System;
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

    public class outOfStock :Exception
    {
        public outOfStock(int idOfProduct)
        {
            this.idOfProduct = idOfProduct;
        }

       public int idOfProduct { set; get; }
        
        public string? Messege => "OUT OF STOCK!\n the id of the order is:"+ idOfProduct;

        public string? ToString()
        {
            return Messege;
        }
    }

    public class BlMissingEntityException: Exception
    {
        public BlMissingEntityException(string message, Exception innerException)
            : base(message, innerException) { }
        public override string ToString() =>
            base.ToString() + $" Missing entity";
        
    }
    public class BlAlreadyExistEntityException : Exception
    {
        public BlAlreadyExistEntityException(string message, Exception innerException)
            : base(message, innerException) { }
        public override string ToString() =>
            base.ToString() + $"Entity is already exists";


    }
}