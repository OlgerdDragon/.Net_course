using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

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
        

    }
    interface IWoman
    {
        public string boughtList { get; set; }
        void AddWanted(string name);
        //public List<Product> GetWantedList();
        public string GetWantedList();
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
        public string Name { get; set; }
        Random rnd = new Random();
        public List<Product> productList = new List<Product>();
        public string boughtList { get; set; }
        public bool manCanSearch = false;
        public object listMark = new object();
        public Wife(string name = "Julia")
        {
            Name = name;
            new Thread(new ThreadStart(WifeJob)).Start();
        }
        public void AddWanted(string name)
        {
            productList.Add(Get.Product(name));
        }
        public string GetWantedList()
        //public List<Product> GetWantedList()
        {
            productList?.Clear();
            
            AddWanted("Sony");
            AddWanted("Bread");
            AddWanted("Lenovo");
            AddWanted("Onion");

            string jsonWantedList = JsonSerializer.Serialize<List<Product>>(productList);
            return jsonWantedList;

            //return productList;

        }
        void WifeJob()
        {
            while (true)
            {
                lock (listMark)
                {
                    while (manCanSearch)
                    {
                        Monitor.Wait(listMark);
                    }
                    Thread.Sleep(1000 * rnd.Next(1, 10));

                    manCanSearch = true;
                    Monitor.Pulse(listMark);
                }
            }
        }

    }

    public class Husband :IMan
    {
        public string Name { get; set; }
        Random rnd = new Random();
        public List<Product> productWantedList;
        
        List<Shop> shopsList = new List<Shop>();
        List<Wife> womans = new List<Wife>();

        public Husband(string name = "Pilipko")
        {
            Name = name;
            AddWomen();
            new Thread(new ThreadStart(HusbandJob)).Start();
        }
        void HusbandJob()
        {
            while (true)
            {
                foreach (var woman in womans)
                {
                    lock (woman.listMark)
                    {
                        while (!woman.manCanSearch)
                        {
                            Monitor.Wait(woman.listMark);
                        }
                        List<Product> searchedProduct = new List<Product>();
                        SearchProduct(shopsList, woman, ref searchedProduct);
                        
                        ShowShearcheRezult(woman, searchedProduct);

                        Thread.Sleep(1000 * rnd.Next(1, 3));
                        woman.manCanSearch = false;
                        Monitor.Pulse(woman.listMark);
                    }

                }
            }
        }

        public void AddWomen()
        {
            womans.Add(Get.Wife());
            womans.Add(Get.Wife("Maria"));
            womans.Add(Get.Wife("Nadiya"));
        }
        void SetWantedList(IWoman woman)
        {
            string jsonWantedList = woman.GetWantedList();
            productWantedList = JsonSerializer.Deserialize<List<Product>>(jsonWantedList);
            //JsonSerializer.Deserialize<List<Product>>(jsonWantedList);

            //productWantedList = woman.GetWantedList();
        }
        void GetWantedList(IWoman woman, List<Product> searchedProduct)
        {
            string jsonSearchedList = JsonSerializer.Serialize(searchedProduct);
            woman.boughtList = jsonSearchedList;
        }

        void SearchProduct(List<Shop> shops, Wife woman, ref List<Product> searchedProduct)
        {
            
            if (shops == null) throw new NullReferenceException();
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
            GetWantedList(woman, searchedProduct);
        }
        public void UpdateShopList(List<Shop> shops)
        {
            shopsList = shops;
        }
        
        public void ShowShearcheRezult(Wife woman, List<Product> searchedProduct)
        {
            Console.WriteLine(" " + Name + " -> " + woman.Name);
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
            else Console.WriteLine("I have been in ALL shops, but there aren't what you want there!");
        }
    }
    
    public class Product : IThing
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public Product()
        {
        }
        
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
        //List<Task> tasks = new List<Task>();
        public void NewDay()
        {
            CreateShops();
            CreateHusbands();
            UpdateShopsInHusband();
            //Runsearching(husbands);


            ConsoleShowAllProductList();
        }
        void UpdateShopsInHusband()
        {
            foreach (var husband in husbands)
            {
                husband.UpdateShopList(shops);
            }
        }
        //async void Runsearching(List<Husband> men)
        //{
        //    foreach (var man in men)
        //    {
        //        tasks.Add(Task.Run(() => man.SearchProduct(shops)));
        //    }
        //    await Task.WhenAll(tasks.ToArray());
        //}

        void CreateShops()
        {
            ShopsLockated();
            UploadProductList();
        }
        void CreateHusbands()
        {
            for (int i = 0; i < 3; i++)
            {
                husbands.Add(Get.Husband(Convert.ToString(i)));
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
        static public Wife Wife(string name = "Julia")
        {
            return new Wife(name);
        }
        static public Husband Husband(string name = "Pilipko")
        {
            return new Husband(name);
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
