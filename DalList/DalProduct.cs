using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;
namespace Dal;
using DalApi;

internal class DalProduct : IProduct
{
    public int Add(Product product)
    {
        product.idOfProduct = _Config.get_ProductID;
        int index = search(product.idOfProduct);
        if (index != -1)
        {
            throw new duplication();
        }
        listProduct.Add(product);
        return product.idOfProduct;
    }


    //public Product? get(int idOfProduct)
    //{
    //    foreach(var product in listProduct)
    //    {
    //        if (idOfProduct == product?.idOfProduct)
    //        {
    //            return product;
    //        }
    //    }
    //    throw new notExist();
    //}
    public IEnumerable<Product?> getAllByParam(Func<Product?, bool>? param)
    {
        if (param == null)
        {
            return listProduct;
        }
        var list = from item in listProduct where param(item) select item;
        return list.AsEnumerable();
    }


    public void delete(int id)
    {
        listProduct.Remove(getOneByParam(x => id == x?.idOfProduct));        
    }


  
    public void update(Product newProduct)
    {
        int index = search(newProduct.idOfProduct);
        if (index == -1)
        {
            throw new notExist();
        }
        listProduct[index] = newProduct;
    }


    public int search(int id)//help function for delete, get id and check if the id exist in the product
    {
        int counter = 0;
        foreach (var item in listProduct)
        {
            if (id == item?.idOfProduct)
            {
                return counter;
            }
            counter++;
        }
        return -1;
    }


    public void print()
    {
        foreach (var item in listProduct)
        {
            Console.WriteLine(item);
        }
    }


    public int length() { return listProduct.Count(); }


    public Product? getOneByParam(Func<Product?, bool>? param)
    {
        foreach (var item in listProduct)
        {
            if (param(item))
            {
                return item;
            }
        }
        throw new notExist();

    }
}

