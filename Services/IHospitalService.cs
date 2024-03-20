
using CallServer.Dto;
using CallServer.Models;

namespace CallServer.Services
{
    public interface IHospitalService
    {
        Task<IEnumerable<HospitalResponseDto>> GetHospitalResponseDtosAsync();
        Task<Hospital> AddHospitalAsync(string hospitalName, string location);
        Task<bool> ConnectAgentAsync(long hospitalId, string channelId);
        Task DisconnectAgentAsync(string channelId);
    }
}