using RouletteApi.Enums;
using System.Text.Json.Serialization;

namespace RouletteApi.Models.Entities;

public class Bet : Entity
{
    public BetType Type { get; set; }
    public string Numbers { get; set; }
    public double Amount { get; set; }

    public int UserRoundId { get; set; }

    [JsonIgnore] public virtual UserRound UserRound { get; set; }
}
