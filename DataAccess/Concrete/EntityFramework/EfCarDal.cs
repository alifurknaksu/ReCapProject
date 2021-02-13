﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarProjectContext>, ICarDal
    {

        public List<CarDetailDto> GetCarDetails()
        {
            using(CarProjectContext context = new CarProjectContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             select new CarDetailDto {
                             
                             CarName = c.CarName, 
                             BrandName = b.BrandName, 
                             DailyPrice = c.DailyPrice, 
                             ColorName = c.Description
                             
                             };
                return result.ToList();
            }
        }
    }
}
