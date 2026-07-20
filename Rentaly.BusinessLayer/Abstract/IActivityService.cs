using Rentaly.Businesslayer.Abstract;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentaly.BusinessLayer.Abstract
{
    public interface IActivityService:IGenericService<Activity>
    {
        Task<List<Activity>> GetRecentAsync(int count);
    }
}
