using CallServer.Dto;
using CallServer.Models;

namespace CallServer.Services
{
    public interface IStatusService
    {
        Task<Status?> GetStatusByIdAsync(int statusId);
        Task<IEnumerable<Status>> GetAllStatusAsync();
        Task<Status> AddStatusAsync(string statusDescription);
        Task<IEnumerable<StatusResponseDto>> GetStatusResponseDtosAsync();
    }
}