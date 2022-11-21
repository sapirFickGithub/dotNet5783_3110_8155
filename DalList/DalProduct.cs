using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;
namespace Dal;

public class DalProduct
{
    public int Add(Product product)
    {
        product.ID = _Config.get_ProductID;
        if (_Config._ProductIndex == 50)
        {
            throw new overload();
        }
        if (search(product))
        {
            throw new duplication();
        }
        //product.ID = _Config.get_ProductID;//צריך לבדואם מותר לעשות בדיקה ידנית
        arrayProduct[_Config._ProductIndex++] = product;
        return product.ID;
    }
    public Product get(int id)
    {
        int i;
        for (i = 0; i < arrayProduct.Length; i++)
        {
            if (id == arrayProduct[i].ID)
            {
                return arrayProduct[i];
            }
        }
        throw new notFound();
        //return arrayProduct[(id - 100000) / 23];//in the class config we colculate the id eith the formula "100000+ _ProductIndex * 20 + _ProductIndex * 3" so to get the right index, we did the opposite formula
    }
    public Product[] idMatch(int id)
    {
        int counter = 0;
        for (int k = 0; k < arrayProduct.Length; k++)
        {
            if (id == arrayProduct[k].ID)
            {
                counter++;
            }
        }

        Product[] Product = new Product[counter];
        int j = 0;
        for (int i = 0; i < arrayProduct.Length; i++)
        {
            if (0 != arrayProduct[i].ID)
            {

                Product[j] = arrayProduct[i];
                j++;
            }
        }
        return Product;

    }
    public void delete(int id)
    {
        if (search(id) < 0)
        {
            throw new notFound();
        }
        Product[] Product = new Product[50];
        int i;
        for (i = 0; i < arrayProduct.Length; i++)
        {
            if (id == arrayProduct[i].ID)
            {
                break;
            }
        }
        for (int j = 0; j < i - 1; j++)
        {
            Product[j] = arrayProduct[j];
        }
        for (int j = i + 1; j < arrayProduct.Length; j++)
        {
            Product[j] = arrayProduct[j];
        }
        arrayProduct = Product;
    }
    public bool search(Product find)//help function
    {
        int i;
        for (i = 0; i < arrayProduct.Length; i++)
        {
            if (find.ID == arrayProduct[i].ID)
            {
                return true;
            }
        }
        return false;
    }
    public void update(Product newProduct)
    {
        if (search(newProduct))
        {
            for (int i = 0; i < arrayProduct.Length; i++)
            {
                if (newProduct.ID == arrayProduct[i].ID)
                    arrayProduct[i] = newProduct;
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
        for (i = 0; i < arrayProduct.Length; i++)
        {
            if (id == arrayProduct[i].ID)
            {
                return i;
            }
        }
        return -1;
    }


    public void print(int n)
    {
        Console.WriteLine(arrayProduct[n]);
    }

    public int length() { return arrayProduct.Length; }
}

