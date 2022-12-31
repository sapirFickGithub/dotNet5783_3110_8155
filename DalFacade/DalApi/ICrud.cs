using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
   public interface ICrud<T> where T : struct
    { 
        /// <summary>
        /// add one object to the list
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public int Add(T ob);


        /// <summary>
        /// updaate object in the list by object
        /// </summary>
        /// <param name="ob"></param>
        public void update(T ob);


        /// <summary>
        /// delete an pbject from the list by id
        /// </summary>
        /// <param name="ob"></param>
        public void delete(int ob);



        /// <summary>
        /// get all the list of the etentetis
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<T?> getAllByParam(Func<T?, bool>? param = null);


        /// <summary>
        /// get an object by func
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public T? getOneByParam(Func<T?, bool>? param);//לבדוק אם צקיך למחוק את get all וכדומה
    }

}
