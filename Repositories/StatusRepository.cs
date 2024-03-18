using CallServer.Data;
using CallServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CallServer.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly CallTrackingDbContext _dbContext;
        public StatusRepository(CallTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Status?> GetStatusByIdAsync(int statusId)
        {
            return await _dbContext.Statuses.FindAsync(statusId);
        }

        public async Task<IEnumerable<Status>> GetAllStatusAsync()
        {
            return await _dbContext.Statuses.ToListAsync();
        }
    }
}
