using CallServer.Models;

namespace CallServer.Repositories
{
    public interface IStatusRepository
    {
        Task<Status?> GetStatusByIdAsync(int statusId);
        Task<IEnumerable<Status>> GetAllStatusAsync();
        Task<Status> AddStatusAsync(Status status);
    }
}