using System;
using System.Collections.Generic;

namespace DamageIncaps
{
    class Program
    {
        class Car
        {
            private Man driver;

            public Car(Man driver)
            {
                this.driver = driver;
            }
            public Man Driver { set { driver = value; } get { return new Man (driver); } }
        }
        class Man
        {
            private string name;
            public Man(string name)
            {
                this.name = name;
            }
            public Man(Man exsemplar)
            {
                name = exsemplar.Name;
            }
            public string Name { set { name = value; } get { return name; } }
        }

        static void Main(string[] args)
        {
            Man person = new Man("Sergey");
            Car avto = new Car(person);
            Man person2 = avto.Driver;

            List<Man> listMen = new List<Man>();
            listMen.Add(new Man("Vasya"));
            listMen.Add(person);
            listMen.Add(person2);

            person2.Name = "Vlad";
            //person.Name = "Sasha";

            Man person3 = listMen[0];
            person3.Name = "Kolivan";

            foreach (var item in listMen)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
