using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RouletteApi.Models.Entities;

public class UserRound : Entity
{
    [Required] public int UserId { get; set; }
    [Required] public int RoundId { get; set; }

    [JsonIgnore] public virtual ICollection<Bet> Bets { get; set; }

    [JsonIgnore] public virtual User User { get; set; }
    [JsonIgnore] public virtual Round Round { get; set; }
}
