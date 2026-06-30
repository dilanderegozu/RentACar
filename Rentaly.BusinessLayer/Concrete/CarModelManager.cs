using Rentaly.DataAccessLayer.Abstract;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentaly.BusinessLayer.Concrete
{
    public class CarModelManager : ICarModel
    {
        private readonly ICarModel _carModel;

        public CarModelManager(ICarModel carModel)
        {
            _carModel = carModel;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CarModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CarModel>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(CarModel entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CarModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
