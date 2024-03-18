using CallServer.Data;
using CallServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CallServer.Repositories
{
    public class CallDetailRepository : ICallDetailRepository
    {
        private readonly CallTrackingDbContext _dbContext;
        public CallDetailRepository(CallTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CallDetail>> GetAllCallDetailsAsync()
        {
            return await _dbContext.CallDetails.ToListAsync();
        }


        public async Task<CallDetail?> GetCallDetailByIdAsync(string callId)
        {
            return await _dbContext.CallDetails.FindAsync(callId);
        }

        public async Task AddCallDetailAsync(CallDetail callDetail)
        {
            _dbContext.CallDetails.Add(callDetail);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCallDetailAsync(CallDetail callDetail)
        {
            _dbContext.CallDetails.Update(callDetail);
            await _dbContext.SaveChangesAsync();
        }
    }
}
