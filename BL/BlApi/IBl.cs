using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBl
    {
        public IProduct Product { get; }
        public IProductForList ProductForList { get; }
        public IProductItem productItem { get; }
        public IOrder Order { get; }
        public IOrderItem OrderItem { get; }
        public ICart Cart { get; }
        public IOrderForList orderForList { get; }
        public IOrderTracking orderTracking { get; }

    }
}

