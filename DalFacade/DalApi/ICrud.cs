using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
   public interface ICrud<T>
    {
        public int Add(T ob);
        public void update(T ob);
        public void delete(int ob);
        public T get(int ob);
        public List<T> getAll();
    }

}
