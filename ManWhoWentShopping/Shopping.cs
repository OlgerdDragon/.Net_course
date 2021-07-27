using System;
using System.Collections.Generic;

namespace ManWhoWentShopping
{
    interface IShop
    {
        public void Add(string name, int price);
        public Product SearchNeededProduct(Product neededProduct);

    }
    interface IMan
    {
        public void AddWomen();
        public void SearchProduct(List<Shop> shops);

    }
    interface IWoman
    {
        public List<Product> boughtList { get; set; }
        void AddWanted(string name);
        public List<Product> GetWantedList();
    }
    interface IThing
    {
        public string Name { get; set; }
        public int Price { get; set; }

    }
    interface Itower
    {
        public void NewDay();
    }

    class Wife: IWoman
    {
        public List<Product> productList = new List<Product>();
        public List<Product> boughtList { get; set; }
        public void AddWanted(string name)
        {
            productList.Add(new Product(name));
        }
        public List<Product> GetWantedList()
        {
            productList?.Clear();
            
            AddWanted("Sony");
            AddWanted("Bread");
            AddWanted("Lenovo");
            AddWanted("Onion");
            
            return productList;
        }


    }
    class Husband :IMan
    {
        public List<Product> productWantedList;
        public List<Product> SearchedProduct = new List<Product>();

        List<Wife> womans = new List<Wife>();

        public void AddWomen()
        {
            womans.Add(new Wife());
        }
        void SetWantedList(IWoman woman)
        {
            productWantedList = woman.GetWantedList();
        }
        void GetWantedList(IWoman woman)
        {
            woman.boughtList = SearchedProduct;
        }


        public void SearchProduct(List<Shop> shops)
        {
            foreach (var woman in womans)
            {
                SetWantedList(woman);
                foreach (var item in shops)
                {
                    foreach (var neededProduct in productWantedList)
                    {
                        Product searched = item.SearchNeededProduct(neededProduct);
                        if (searched != null)
                            SearchedProduct.Add(new Product(searched));
                    }
                }
            }
        }
        
        public void ShowShearcheRezult()
        {
            Console.WriteLine("Bought:");
            if (SearchedProduct.Count != 0)
            {
                int totalPrice = 0;
                foreach (var product in SearchedProduct)
                {
                    Console.WriteLine(product.Name + " - " + product.Price);
                    totalPrice += product.Price;
                }
                Console.WriteLine(" Total price = " + totalPrice);
            }
            else Console.WriteLine("I runing in ALL shops, but haven't what you want!");
        }
    }
    
    class Product : IThing
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
    class Tower :Itower
    {
        List<Shop> shops = new List<Shop>();
        
        Husband pedro = new Husband();
        public void NewDay()
        {
            CreateShops();
            pedro.AddWomen();
            pedro.SearchProduct(shops);
            ConsoleShowAllProductList();
            pedro.ShowShearcheRezult();
        }
        void CreateShops()
        {
            ShopsLockated();
            UploadProductList();
        }
        void ShopsLockated()
        {
            shops.Add(new Shop("multiElectric"));
            shops.Add(new Shop("yourGarden"));
        }
        void UploadProductList()
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
        
        public void ConsoleShowAllProductList()
        {
            Console.WriteLine("Total list:");
            foreach (var shop in shops)
            {
                foreach (var product in shop.product)
                {
                    Console.WriteLine(product.Name + " - " + product.Price);
                }
            }
            Console.WriteLine("");
        }
    }
    class Shopping 
    {
        static void Start()
        {
            Tower Pogrebichche = new Tower();
            Pogrebichche.NewDay();
            

        }
        static void Main(string[] args)
        {
            Start();
            
        }
    }
}
