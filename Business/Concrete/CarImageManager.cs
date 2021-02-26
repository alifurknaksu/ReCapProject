using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilitiess.Business;
using Core.Utilitiess.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckIfCarImagesCountExist(carImage.CarId));

            if (result != null)
            {
                return result;
            }

            _carImageDal.Add(carImage);

            return new SuccessResult(Messages.ImageAdded);
        }

        public IResult Update(CarImage carImage)
        {
            _carImageDal.Update(carImage);

            return new SuccessResult(Messages.ImagesUpdated);
        }

        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);

            return new SuccessResult(Messages.ImageDeleted);
        }

        public IDataResult<List<CarImage>> GetCarImages()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetCarImageById(int carImageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(image => image.Id == carImageId));
        }

        public IDataResult<List<CarImage>> GetCarImageByCarId(int carId)
        {
            var result = BusinessRules.Run(CheckIfCarImageExists(carId));

            if (result != null)
            {
                return new SuccessDataResult<List<CarImage>>(new List<CarImage> { ShowDefaultImage(carId) }, Messages.ImageExist);
            }

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(image => image.CarId == carId));
        }

        private CarImage ShowDefaultImage(int carId)
        {
            var path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName +
                                    @"Images\CarImages\default.jpg");

            return new CarImage() { CarId = carId, ImagePath = path, Date = DateTime.Now };
        }

        private IResult CheckIfCarImageExists(int carId)
        {
            if (_carImageDal.GetAll(image => image.CarId == carId).Count == 0)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        private IResult CheckIfCarImagesCountExist(int carId)
        {
            if (_carImageDal.GetAll(image => image.CarId == carId).Count >= 5)
            {
                return new ErrorResult(Messages.CarImagesCountExceded);
            }

            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }
    }
}
