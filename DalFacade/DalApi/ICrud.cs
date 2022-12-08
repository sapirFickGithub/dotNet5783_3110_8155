using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
   public interface ICrud<T> where T : struct
    { 
        public int Add(T ob);
        public void update(T ob);
        public void delete(int ob);
        public T get(int ob);
        public IEnumerable<T?> getAll(Func<T?, bool>? param = null);
        public T getByParam(Func<T?, bool>? param);
    }

}
