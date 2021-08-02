using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    interface ITown
    {
        public void NewDay();
    }

    public class Wife: IWoman
    {
        public List<Product> productList = new List<Product>();
        public List<Product> boughtList { get; set; }
        public void AddWanted(string name)
        {
            productList.Add(Get.Product(name));
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
    public class Husband :IMan
    {
        public List<Product> productWantedList;
        public List<Product> searchedProduct = new List<Product>();

        List<Wife> womans = new List<Wife>();

        public void AddWomen()
        {
            womans.Add(Get.Wife());
        }
        void SetWantedList(IWoman woman)
        {
            productWantedList = woman.GetWantedList();
        }
        void GetWantedList(IWoman woman)
        {
            woman.boughtList = searchedProduct;
        }

        public void SearchProduct(List<Shop> shops)
        {
            if (shops == null) throw new NullReferenceException();
            foreach (var woman in womans)
            {
                SetWantedList(woman);
                foreach (var item in shops)
                {
                    foreach (var neededProduct in productWantedList)
                    {
                        Product searched = item.SearchNeededProduct(neededProduct);
                        if (searched != null)
                            searchedProduct.Add(Get.Product(searched));
                    }
                }
                GetWantedList(woman);
            }
        }
        
        public void ShowShearcheRezult()
        {
            Console.WriteLine("Bought:");
            if (searchedProduct.Count != 0)
            {
                int totalPrice = 0;
                foreach (var product in searchedProduct)
                {
                    Console.WriteLine(product.Name + " - " + product.Price);
                    totalPrice += product.Price;
                }
                Console.WriteLine(" Total price = " + totalPrice);
            }
            else Console.WriteLine("I runing in ALL shops, but haven't what you want!");
        }
    }
    
    public class Product : IThing
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
    public class Shop : IShop
    {
        public string Name {get;set;}
        public Shop(string name = "default")
        {
            Name = name;
        }
        public List<Product> product = new List<Product>();
        
        public void Add(string name, int price)
        {
            product.Add(Get.Product(name,price));
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
    public class Town :ITown
    {
        List<Shop> shops = new List<Shop>();
        List<Husband> husbands = new List<Husband>();
        List<Task> tasks = new List<Task>();
        public void NewDay()
        {
            CreateShops();
            CreateHusbands();

            foreach (var husband in husbands)
            {
                husband.SearchProduct(shops);
            }
            
            ConsoleShowAllProductList();
            foreach (var husband in husbands)
            {
                husband.ShowShearcheRezult();
            }
        }
        void CreateShops()
        {
            ShopsLockated();
            UploadProductList();
        }
        void CreateHusbands()
        {
            for (int i = 0; i < 10; i++)
            {
                husbands.Add(Get.Husband());
                husbands[i].AddWomen();
            }
        }
        void ShopsLockated()
        {
            shops.Add(Get.Shop("multiElectric"));
            shops.Add(Get.Shop("yourGarden"));
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
            Town Pogrebichche = Get.Town();
            Pogrebichche.NewDay();
            

        }
        static void Main(string[] args)
        {
            Start();
            
        }
    }

    class Get
    { 
        static public Wife Wife()
        {
            return new Wife();
        }
        static public Husband Husband()
        {
            return new Husband();
        }
        static public Shop Shop(string name = "default")
        {
            return new Shop(name);
        }
        static public Product Product(string name)
        {
            return new Product(name);
        }
        static public Product Product(string name, int price)
        {
            return new Product(name,price);
        }
        static public Product Product(Product a)
        {
            return new Product(a);
        }
        static public Town Town()
        {
            return new Town();
        }
    }
}
