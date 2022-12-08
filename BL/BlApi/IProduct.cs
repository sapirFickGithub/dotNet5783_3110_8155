using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface IProduct
    {
        /// <summary>
        /// For the manager screen and,
        /// for the buyer's catalog screen:
        /// Returns a list of all existing products.
        /// </summary>
        /// <IEnumerable<BO.ProductForList>></returns>
        public IEnumerable<BO.ProductForList> getListOfProduct(Func<DO.Product?, bool>? param=null);
        /// <summary>
        /// For admin screen:
        /// Gets a product ID number and returns an object with all its details
        /// - if it exists.
        /// </summary>
        /// <param name="idProduct"></param>
        /// <Product></returns>
        public BO.Product GetProduct(int idProduct);
        /// <summary>
        /// For a buyer screen - from the catalog:
        /// Gets a product ID number and returns an object with all its details
        /// - if it exists
        /// </summary>
        /// <param name="idProduct"></param>
        /// <param name="cart"></param>
        /// <ProductItem></returns>
        public BO.ProductItem GetDetails(int idProduct, BO.Cart cart);
        /// <summary>
        /// For admin screen:
        /// Adding a product to the catalog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="productCategory"></param>
        /// <param name="price"></param>
        /// <param name="inStock"></param>
        public void addProduct(int idOfProduct, string name, BO.Enum.Category productCategory, double price, int inStock);
        /// <summary>
        /// Deletion of a product from the inventory
        /// </summary>
        /// <param name="idProduct"></param>
        public void delete(int idOfProduct);
        /// <summary>
        /// For admin screen:
        /// Update product details.
        /// </summary>
        /// <param name="product"></param>
        public void update(BO.Product idOfProduct);
      
    }
}
