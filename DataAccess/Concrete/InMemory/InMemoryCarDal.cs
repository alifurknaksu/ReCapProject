using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car> {
                new Car{CarId=1,CarName="Model X",BrandId=1,ColorId=2,ModelYear=2018,DailyPrice=1000,Description="Otomatik, Elektrikli, Otonom, 4 Koltuk, Tesla Model X"},
                new Car{CarId=2,CarName="Cayenne",BrandId=2,ColorId=1,ModelYear=2020,DailyPrice=1400,Description="Otomatik, Benzinli, Klimalı, 4 Koltuk, Navigasyonlu,Porsche Cayenne "},
                new Car{CarId=3,CarName="Kadjar",BrandId=3,ColorId=1,ModelYear=2020,DailyPrice=750,Description="Otomatik, Benzinli, 4 Koltuk, Renault Kadjar"},
                new Car{CarId=4,CarName="AMG Gt-S",BrandId=5,ColorId=7,ModelYear=2020,DailyPrice=1250,Description="Otomatik, Benzin, 2 Koltuk, Mercedes AMG Gt-S"},
                new Car{CarId=5,CarName="Corvette C8",BrandId=7,ColorId=1,ModelYear=2020,DailyPrice=1400,Description="Otomatik, Benzinli, 2 Koltuk , Chevrolet Corvette C8"},
                new Car{CarId=6,CarName="Cayman",BrandId=2,ColorId=1,ModelYear=2020,DailyPrice=1100,Description ="Otomatik, Benzinli, 4 Koltuk, Porsche Cayman"}
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToRemove = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            _cars.Remove(carToRemove);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car GetById(int id)
        {
            return _cars.Single(c => c.CarId == id);
         
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.CarName = car.CarName;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;

        }

   
    }
}