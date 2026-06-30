using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentaly.DataAccessLayer.EntityFramework
{
    internal class EfRentalDal : GenericRepository<Rental>, IRentalDal
    {
        public EfRentalDal(RentalyContext context) : base(context)
        {
        }
    }
}
