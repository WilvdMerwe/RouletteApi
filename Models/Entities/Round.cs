using RouletteApi.Enums;
using System.Text.Json.Serialization;

namespace RouletteApi.Models.Entities;

public class Round : Entity
{
    public RoundStatus Status { get; set; }
    public double MinimumBet { get; set; }
    public int ResultNumber { get; set; } = -1;

    [JsonIgnore] public virtual ICollection<UserRound> UserRounds { get; set; }
}
