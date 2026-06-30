using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentaly.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public Task TDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> TGetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> TGetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task TInsertAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task TUpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
