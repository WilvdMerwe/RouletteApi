using RouletteApi.Models.Entities;

namespace RouletteApi.Repositories;

public class RoundRepo : Repository<Round>
{
    public RoundRepo(RouletteDbContext rouletteDbContext) : base(rouletteDbContext)
    {
    }


}
