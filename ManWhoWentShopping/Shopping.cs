using System;
using System.Collections.Generic;

namespace ManWhoWentShopping
{
    interface IShop
    {
        public void Add(string name, int price);
        public Product SearchNeededProduct(Product neededProduct);

    }
    
    class Wife
    {
        public List<Product> Product = new List<Product>();
        public void AddWanted(string name)
        {
            Product.Add(new Product(name));
        }

    }
    class Husband
    {
        public List<Product> product = new List<Product>();

        public void CreateTaskList(List<Product> taskList)
        {
            product = taskList;
        }
        public List<Product> SearchedProduct = new List<Product>();
    }
    class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Product(string name)
        {
            Name = name;
        }
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }
        public Product(Product a)
        {
            Name = a.Name;
            Price = a.Price;
        }



    }
    class Shop : IShop
    {
        public string Name {get;set;}
        public Shop(string name = "default")
        {
            Name = name;
        }
        public List<Product> product = new List<Product>();
        
        public void Add(string name, int price)
        {
            product.Add(new Product(name,price));
        }

        public Product SearchNeededProduct(Product neededProduct)
        {
            foreach (var itemProduct in product)
            {
                if (neededProduct.Name == itemProduct.Name)
                {
                    return itemProduct;
                }
            }
            
            return null;
        }
    }

    class Shopping
    {
        static List<Shop> shops = new List<Shop>();
        static void UploadProductList()
        {
            shops[0].Add("Sony", 100);
            shops[0].Add("Samsung", 80);
            shops[0].Add("Apple", 1);
            shops[0].Add("Aser", 40);
            shops[1].Add("Bread", 110);
            shops[1].Add("Sausage", 80);
            shops[1].Add("Butter", 70);
            shops[1].Add("Milk", 300);
            shops[1].Add("Poteto", 40);
            shops[1].Add("Ramen", 100);
        }
        static void Main(string[] args)
        {
            Wife white = new Wife();
            white.AddWanted("Sony");
            white.AddWanted("Bread");
            white.AddWanted("Lenovo");
            white.AddWanted("Onion");

            Husband pedro = new Husband();
            pedro.CreateTaskList(white.Product);

            shops.Add(new Shop("multiElectric"));
            shops.Add(new Shop("yourGarden"));
            UploadProductList();

            foreach (var item in shops)
            {
                foreach (var neededProduct in pedro.product)
                {
                    Product searched = item.SearchNeededProduct(neededProduct);
                    if (searched != null)
                        pedro.SearchedProduct.Add(new Product(searched));
                }
                
            }
            Console.WriteLine("Total list:");
            foreach (var shop in shops)
            {
                foreach (var product in shop.product)
                {
                    Console.WriteLine(product.Name + " - " + product.Price);
                }
            }
            Console.WriteLine("");

            Console.WriteLine("Bought:");
            if (pedro.SearchedProduct.Count != 0)
            {
                int totalPrice = 0;
                foreach (var product in pedro.SearchedProduct)
                {
                    Console.WriteLine(product.Name + " - " + product.Price);
                    totalPrice += product.Price;
                }
                Console.WriteLine(" Total price = " + totalPrice);
            }
        }
    }
}
