using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentaly.DataAccessLayer.Abstract
{
    public interface IActivityDal : IGenericDal<Activity>
    {
        Task<List<Activity>> GetRecentAsync(int count);
    }
}
