using Microsoft.EntityFrameworkCore;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.RepositoryDesignPattern;
using Rentaly.EntityLayer.Entities;

public class EfActivityDal : GenericRepository<Activity>, IActivityDal
{
    public EfActivityDal(RentalyContext context) : base(context)
    {
    }

    public async Task<List<Activity>> GetRecentAsync(int count)
    {
        return await _context.Activities
            .OrderByDescending(a => a.CreatedDate)
            .Take(count)
            .ToListAsync();
    }
}