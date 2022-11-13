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
        if (_Config._ProductIndex == 50)
        {
            throw new Exception("OVERLOAD! no more space in arrayProduct");
        }
        if (search(product))
        {
            throw new Exception("DUPLICATION! this product already exist");
        }
        product.ID = _Config.get_ProductID;//צריך לבדוק אם מותר לעשות בדיקה ידנית
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
        throw new Exception("PRODUCT NOT FOUND!");
        //return arrayProduct[(id - 100000) / 23];//in the class config we colculate the id eith the formula "100000+ _ProductIndex * 20 + _ProductIndex * 3" so to get the right index, we did the opposite formula
    }
    public Product[] get()
    {
        return arrayProduct;
    }
    public void delete(int id)
    {
        Product[] Product = new Product[50];
        int i;
        for (i = 0; i < arrayProduct.Length; i++)
        {
            if (id == arrayProduct[i].ID)
            {
                break;
            }
        }
        for (int j = 0; j < i-1; j++)
        {
            Product[j]= arrayProduct[j];
        }
        for (int j = i+1; j < arrayProduct.Length; j++)
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
    }


}
