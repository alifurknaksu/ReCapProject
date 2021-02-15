using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarProjectContext>, IRentalDal
    {
       
        public List<RentalDetailDto> GetRentalDetails(System.Linq.Expressions.Expression<Func<Rental, bool>> filter = null)
        {
            using (CarProjectContext context = new CarProjectContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars
                             on r.CarId equals c.CarId
                             join u in context.Users
                             on r.RentId equals u.Id
                             join cu in context.Customers
                             on r.CustomerId equals cu.UserId
                             select new RentalDetailDto
                             {
                                 RentId = r.RentId,
                                 CarId = c.CarId,
                                 UserId = u.Id,
                                 CarName = c.CarName,

                                 UserName = u.FirstName + " " + u.LastName,

                                 RentDate = r.RentDate,
                                 ReturnDate = (DateTime)r.ReturnDate

                             };
                return result.ToList();
            }
        }
    }
}
