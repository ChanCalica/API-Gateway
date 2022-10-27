namespace API_Gateway.Models.Response
{
    public class AdjustPointsResponse
    {
        public string Type { get; set; }
        public string CardId { get; set; }
        public string TransCode { get; set; }
        public DateTime Created_At { get; set; }
        public string PointsBalance { get; set; }
    }

    public class NewAdjustPointsResponse
    {
        public string? PreviousBalance { get; set; }
        public string? NewBalance { get; set; }
        public string? LastUpdated { get; set; }

    }
}
