using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalApi;
using System.Threading.Tasks;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class DalProduct : IProduct
{
    const string productPath = "Product";
    static XElement config = XmlTools.LoadConfig();
    Random random = new Random();
    public int search(int id)//help function, get id and check if the id exist in the product and send the index of the list
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);
        return listProduct.FindIndex(item => id == item?.idOfProduct);
    }
    public int length()
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);
        return listProduct.Count();
    }
    public void print()
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);
        listProduct.ForEach(item => Console.WriteLine(item));
    }

    public int Add(Product product)
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);
        product.idOfProduct= random.Next(100000, 1000000);
        int index = search(product.idOfProduct);
        while (index != -1)
        {
            product.idOfProduct = random.Next(100000, 1000000);
            index = search(product.idOfProduct);
        }
        listProduct.Add(product);
        XmlTools.SaveListToXMLSerializer(listProduct, productPath);
        return product.idOfProduct;

    }

    

    public void update(Product product)
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        Product? isNULL = getOneByParam(
            a => a?.idOfProduct == product.idOfProduct
                            );
        int I = listProduct.IndexOf(isNULL);
        if (I != -1) // if the ID exist update the details else throw an Error
        {
            listProduct[I] = product;
            XmlTools.SaveListToXMLSerializer(listProduct, productPath);
        }
        else
        {
            throw new Exception("object doesn't exist - Update");
        }
    }

    public void delete(int idProduct)
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        listProduct.Remove(getOneByParam(x => idProduct == x?.idOfProduct));
        XmlTools.SaveListToXMLSerializer(listProduct, productPath);
    }

    public IEnumerable<Product?> getAllByParam(Func<Product?, bool>? func = null)
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        //List<Product?> finalResult = new List<Product?>();
        if (func != null)
        {
            var finalResult = from item in listProduct
                              where func(item)
                              select item;

            return finalResult;
        }
        else
        {
            List<Product?> finalResult = new List<Product?>();

            finalResult = listProduct;
            return finalResult;
        }
    }

    public Product? getOneByParam(Func<Product?, bool>? func)
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        if (func != null)
        {
            var product = listProduct.Find(x => func(x));
            if (product != null)
            {
                return (Product)product;
            }
        }

        throw new notExist();
    }


}
