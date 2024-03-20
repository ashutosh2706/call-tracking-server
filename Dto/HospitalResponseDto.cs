namespace CallServer.Dto
{
    public class HospitalResponseDto
    {
        public long HospitalId { get; set; }
        public string HospitalName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
