using CallServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;
        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHospitals()
        {
            var response = await _hospitalService.GetHospitalResponseDtosAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddHospital(string hospitalName, string location)
        {
            var hospital = await _hospitalService.AddHospitalAsync(hospitalName, location);
            return Created("/Hospital", hospital);
        }
    }
}
