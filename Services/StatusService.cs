using CallServer.Models;
using CallServer.Repositories;

namespace CallServer.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<Status>> GetAllStatusAsync()
        {
            return await _statusRepository.GetAllStatusAsync();
        }

        public async Task<Status?> GetStatusByIdAsync(int statusId)
        {
            return await _statusRepository.GetStatusByIdAsync(statusId);
        }
    }
}
