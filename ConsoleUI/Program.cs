using Business.Concrete;
using DataAccess.Concrete;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description + "\nArabanın Günlük Kirası: " + car.DailyPrice);
            }

            Car car1 = carManager.GetbyId(1);
            Console.WriteLine("\n\nId'si 1 olan Otomobilin Kirası: " + car1.DailyPrice);
            Console.WriteLine("Id'si 1 olan Tesla Model X'in günlük kirası 600 olarak güncellendi. \n");
            car1.DailyPrice = 600; 
            carManager.Update(car1);

            Car car6 = carManager.GetbyId(6);
            Console.WriteLine("Id'si 6 olan Porsche Cayonne listeden silindi.\n");
            carManager.Delete(car6);
            
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description + "\nArabanın Günlük Kirası: " + car.DailyPrice);
            }
        }
    }
}
