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
                new Car{CarId=1,CarName="Tesla",BrandId=1,ColorId=1,ModelYear=2020,DailyPrice=650,Description="Otomatik, Elektrikli, Otonom, 4 Koltuk, Tesla Model X"},
                new Car{CarId=2,CarName="Volvo",BrandId=2,ColorId=2,ModelYear=2015,DailyPrice=250,Description="Manuel, Benzinli, Klimalı, 4 Koltuk, Navigasyonlu, Volvo S60 "},
                new Car{CarId=3,CarName="Porsche",BrandId=3,ColorId=1,ModelYear=2018,DailyPrice=800,Description=" Yarı Otomatik, Benzinli, 2 Koltuk, Porsche Cayman"},
                new Car{CarId=4,CarName="Mecedes",BrandId=4,ColorId=2,ModelYear=2005,DailyPrice=100,Description="Yarı Otomatik, Benzin/LPG, 4 Koltuk, Mercedes Benz CLS 350 AMG"},
                new Car{CarId=5,CarName="Chevrolet",BrandId=5,ColorId=1,ModelYear=1960,DailyPrice=1500,Description="Otomatik, Benzinli, 2 Koltuk , Chevrolet Corvette Stingray"},
                new Car{CarId=6,CarName="Porsche",BrandId=3,ColorId=3,ModelYear=2021,DailyPrice=750,Description ="Yarı Otomatik, Dizel, 4 Koltuk, Porsche Cayenne"}
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