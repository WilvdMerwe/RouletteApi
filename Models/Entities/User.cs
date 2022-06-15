namespace RouletteApi.Models.Entities;

public class User : Entity
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public double Balance { get; set; }

    public virtual ICollection<UserRound> UserRounds {  get; set; }      
}
