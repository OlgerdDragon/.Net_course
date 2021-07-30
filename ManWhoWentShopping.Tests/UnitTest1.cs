using NUnit.Framework;
using System.Collections.Generic;
using ManWhoWentShopping;
using System;

namespace ManWhoWentShopping.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void SetWantedList_Wife_4element()
        {
            List<Product> ecexpected = new List<Product>();
            ecexpected.Add(new Product("Sony"));
            ecexpected.Add(new Product("Bread"));
            ecexpected.Add(new Product("Lenovo"));
            ecexpected.Add(new Product("Onion"));

            Wife woman = new Wife();
            List<Product> productWantedList = woman.GetWantedList();

            Assert.AreEqual(ecexpected[3].Name, productWantedList[3].Name);

        }
        [Test]
        public void SearchNeededProduct_Shop_4element()
        {
            List<Shop> shops = new List<Shop>();
            shops.Add(new Shop("multiElectric"));
            shops[0].Add("Sony", 100);
            shops[0].Add("Samsung", 80);
            shops[0].Add("Apple", 1);
            shops[0].Add("Aser", 40);

            Product test = new Product("Apple");
            Product product = shops[0].SearchNeededProduct(test);

            Product ecexpected = new Product("Apple", 1);

            Assert.AreEqual(ecexpected.Price, product.Price);
                        
        }
        [Test]
        public void SearchProduct_Husband_4element()
        {
            List<Shop> shops = new List<Shop>();
            shops.Add(new Shop("multiElectric"));
            shops[0].Add("Sony", 100);
            shops[0].Add("Samsung", 80);
            shops[0].Add("Apple", 1);
            shops[0].Add("Aser", 40);

            Husband man = new Husband();
            man.AddWomen();
            man.SearchProduct(shops);

            Product ecexpected = new Product("Sony", 100);
            Assert.AreEqual(ecexpected.Name, man.searchedProduct[0].Name);

        }

        [Test]
        public void SearchProduct_Husband_NULL()
        {
            
            Husband man = new Husband();
            try
            {
                man.SearchProduct(null);
            }
            catch(Exception e)
            {
                NullReferenceException ecexpected = new NullReferenceException();
                if (e.Message == ecexpected.Message)
                Assert.Pass();
                else
                    Assert.Fail();
            }
        }
        [Test]
        public void GetWantedList_NULL_NullReferenceException()
        {
            Shop shop = new Shop();
            shop.Add("Sony", 100);
            shop.Add("Samsung", 80);
            shop.Add("Apple", 1);
            shop.Add("Aser", 40);
            try
            {
                shop.SearchNeededProduct(null);
            }
            catch (Exception e)
            {
                NullReferenceException ecexpected = new NullReferenceException();
                if (e.Message == ecexpected.Message)
                    Assert.Pass();
                else
                    Assert.Fail();
            }
}
        [Test]
        public void GetWantedList2__NULL_NullReferenceException()
        {
            Shop shop = new Shop();
            shop.Add("Sony", 100);
            shop.Add("Samsung", 80);
            shop.Add("Apple", 1);
            shop.Add("Aser", 40);

            Assert.Throws<NullReferenceException>(() => shop.SearchNeededProduct(null));
            
        }

    }
}