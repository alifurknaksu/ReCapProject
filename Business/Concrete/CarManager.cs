using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        ICustomerService _customerService;
        public CarManager(ICarDal carDal, ICustomerService customerService)
        {
            _carDal = carDal;
            _customerService = customerService;
        }

        [SecuredOperation("admin,editor")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfCarCountOfCategoryCorrect(car.CarId), CheckIfCarNameExists(car.CarName), CheckIfCustomerLimitExiceded());
            if (result != null)
            {
                return result;
            }
            _carDal.Add(car);

            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour==24)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>( _carDal.GetAll(),Messages.CarsListed);
        }
             
        public IDataResult<Car> GetByCarId(int id)
        {
            return new SuccessDataResult<Car>( _carDal.Get(c=>c.CarId == id));
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>( _carDal.GetAll(p => p.BrandId == id)); 
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>> (_carDal.GetAll(p => p.ColorId == id));
        }

        public IDataResult<List<Car>> GetCatsByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>> (_carDal.GetAll(p => p.DailyPrice >= min && p.DailyPrice <= max));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        private IResult CheckIfCarCountOfCategoryCorrect(int carId)
        {
            var result = _carDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.CarCountOfCategoryError);
            }
            return new SuccessResult();
        }

       
        private IResult CheckIfCarNameExists(string carName)
        {
            var result = _carDal.GetAll(c => c.CarName == carName).Any();
            if (result)
            {
                return new ErrorResult(Messages.CarNameAllreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCustomerLimitExiceded()
        {
            var result = _customerService.GetAllCustomers();
            if (result.Data.Count > 20)
            {
                return new ErrorResult(Messages.CustomerLimitExiceded);
            }
            return new SuccessResult();
        }
    }
}