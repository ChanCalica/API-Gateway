namespace API_Gateway.Models.Request
{
    public class MemberRequest
    {
        public string mobileNumber { get; set; }
        public DateTime? birthDate { get; set; }
        public string Query { get; set; }
        public string QueryValue { get; set; }
    }
}
