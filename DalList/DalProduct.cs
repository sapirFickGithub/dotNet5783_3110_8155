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
        int i;
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
    public IEnumerable<Product> getAll()
    {

        return listProduct;

    }
    public void delete(int id)
    {
        if (search(id) < 0)
        {
            throw new notFound();
        }
        List<Product> Product = new List<Product>();
        int i;
        for (i = 0; i <= listProduct.Count(); i++)
        {
            if (id == listProduct[i].idOfProduct)
            {
                Product p = new Product();
                listProduct[i]=p;
            }
        }
        //for (int j = 0; j <= i ; j++)
        //{
        //    Product[j] = listProduct[j];
        //}
        //for (int j = i + 1; j < listProduct.Count(); j++)
        //{
        //    Product[j] =listProduct[j];
        //}
        //listProduct = Product;
    }
    public bool search(Product find)//help function
    {
        int count = 0;
        int i;
        for (i = 0; i < listProduct.Count(); i++)
        {
            if (find.idOfProduct == listProduct[i].idOfProduct)
            {
                count++;
                if (count == 0)
                {
                    return true;
                }
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
                if (newProduct.idOfProduct == listProduct[i].idOfProduct)
                    listProduct[i] = newProduct;
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
            if (id == listProduct[i].idOfProduct)
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
}

