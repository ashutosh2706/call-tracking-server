using CallServer.Models;

namespace CallServer.Repositories
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Agent>> GetAgentsByHospitalIdAsync(long id);
        Task<IEnumerable<Hospital>> GetAllHospitalsAsync();
        Task<Hospital> AddHospitalAsync(Hospital hospital);
    }
}