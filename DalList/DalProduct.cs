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
        return listProduct.FindIndex(item => id == item?.idOfProduct);
    }


    public void print()
    {
        listProduct.ForEach(item => Console.WriteLine(item));
    }


    public int length() { return listProduct.Count(); }


    public Product? getOneByParam(Func<Product?, bool>? param)
    {
        return listProduct.FirstOrDefault(param);
        throw new notExist();

    }
}

