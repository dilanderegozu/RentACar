using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.BusinessLayer.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public Task TDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Car> TGetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Car>> TGetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task TInsertAsync(Car entity)
        {
            throw new NotImplementedException();
        }

        public Task TUpdateAsync(Car entity)
        {
            throw new NotImplementedException();
        }
    }
}