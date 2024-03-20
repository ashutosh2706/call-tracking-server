using CallServer.Data;
using CallServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CallServer.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly CallTrackingDbContext _dbContext;
        public HospitalRepository(CallTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Agent>> GetAgentsByHospitalIdAsync(long id)
        {
            return await _dbContext.Hospitals
                .Where(H => H.Hid == id)
                .SelectMany(h => h.Agents)
                .ToListAsync();
        }


        public async Task<IEnumerable<Hospital>> GetAllHospitalsAsync()
        {
            return await _dbContext.Hospitals.ToListAsync();
        }

        public async Task<Hospital> AddHospitalAsync(Hospital hospital)
        {
            _dbContext.Hospitals .Add(hospital);
            await _dbContext.SaveChangesAsync();
            return hospital;
        }

    }
}
