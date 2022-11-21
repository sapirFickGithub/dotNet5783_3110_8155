using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
   public interface ICrud<T>
    {
        public T Add(T ob);
        public T updat(T ob);
        public T delete(T ob);
        public T get(T ob);
        public IEnumerable<T> getAll();
    }

}
