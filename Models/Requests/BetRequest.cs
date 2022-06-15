using RouletteApi.Enums;

namespace RouletteApi.Models.Requests
{
    public class BetRequest
    {
        public int UserId { get; set; }
        public BetType Type { get; set; }
        public List<int> Numbers { get; set; }
        public double Amount { get; set; }
    }
}
