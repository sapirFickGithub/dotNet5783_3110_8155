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
        if (search(product))
        {
            throw new duplication();
        }
        listProduct.Add(product);
        //listProduct[_Config._ProductIndex++] = product;
        return product.idOfProduct;
    }
    public Product get(int id)
    {
        foreach(Product product in listProduct)
        {
            if (id == product.idOfProduct)
            {
                return product;
            }
        }
        throw new notFound();
        //return arrayProduct[(id - 100000) / 23];//in the class config we colculate the id eith the formula "100000+ _ProductIndex * 20 + _ProductIndex * 3" so to get the right index, we did the opposite formula
    }
    public IEnumerable<Product?> getAll(Func<Product?, bool>? param)
    {

        if (param == null)//לחזור לראות אם זה תקין
        {
            return listProduct;
        }
        List<Product?> list = new List<Product?>();
        foreach (var item in listProduct)
        {
            if (param(item))
            {
                list.Add(item);
            }
        }
        return list;
    }
    public void delete(int id)
    {
     listProduct.Remove(get(id));        
    }
    public bool search(Product find)//help function
    {
        int count = 0;
        int i;
        for (i = 0; i <= listProduct.Count(); i++)
        {
            if (find.idOfProduct == listProduct[i]?.idOfProduct)
            {
                //count++;
                //if (count == 0)
                //{
                    return true;
               // }
            }
        }
        return false;
    }
    public void update(Product newProduct)
    {
        if (search(newProduct))
        {
            for (int i = 0; i < listProduct.Count(); i++)
            {
                if (newProduct.idOfProduct == listProduct[i]?.idOfProduct)
                {
                    listProduct[i] = newProduct;
                    return;
                }
            }
        }
        else
        {
            throw new notFound();
        }
    }
    public int search(int id)//help function for delete, get id and check if the id exist in the product
    {
        int i;
        for (i = 0; i < listProduct.Count(); i++)
        {
            if (id == listProduct[i]?.idOfProduct)
            {
                return i;
            }
        }
        return -1;
    }
    public void print(int n)
    {
        Console.WriteLine(listProduct[n]);
    }
    public int length() { return listProduct.Count(); }
    public Product getByParam(Func<Product?, bool>? param)
    {
        foreach (var item in listProduct)
        {
            if (param(item))
            {
                Product? product = new Product();
                product = item;
                return (Product)product;
            }
        }
        throw new Exception("NOT EXIST!");

    }
}

