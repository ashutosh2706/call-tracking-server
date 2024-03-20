using CallServer.Dto;
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

        public async Task<IEnumerable<StatusResponseDto>> GetStatusResponseDtosAsync()
        {
            var statuses = await _statusRepository.GetAllStatusAsync();
            List<StatusResponseDto> responseDtos = new List<StatusResponseDto>();
            foreach (var status in statuses)
            {
                responseDtos.Add(new StatusResponseDto
                {
                    statusId = status.StatusId,
                    statusDescription = (status.Description) ?? "NULL"
                });
            }

            return responseDtos;
        }

        public async Task<Status?> GetStatusByIdAsync(int statusId)
        {
            return await _statusRepository.GetStatusByIdAsync(statusId);
        }

        public async Task<Status> AddStatusAsync(string statusDescription)
        {
            return await _statusRepository.AddStatusAsync(new Status { Description = statusDescription });
        }
    }
}
