using Rentaly.BusinessLayer.Abstract;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentaly.BusinessLayer.Concreate
{
    public class ActivityManager:IActivityService
    {
        private readonly IActivityDal _activityDal;
        public ActivityManager(IActivityDal activityDal)
        {
            _activityDal = activityDal;
        }

        public async Task TDeleteAsync(int id)
        {
           await _activityDal.DeleteAsync(id);
        }

        public async Task<Activity> TGetByIdAsync(int id)
        {
            return await _activityDal.GetByIdAsync(id);
        }

        public async Task<List<Activity>> TGetListAsync()
        {
            return await _activityDal.GetListAsync();
        }

        public async Task TInsertAsync(Activity entity)
        {
            await _activityDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Activity entity)
        {
            await _activityDal.UpdateAsync(entity);
        }
        public async Task<List<Activity>> GetRecentAsync(int count)
        {
            return await _activityDal.GetRecentAsync(count);
        }
    }
}
