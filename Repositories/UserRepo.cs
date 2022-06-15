using RouletteApi.Models.Entities;

namespace RouletteApi.Repositories;

public class UserRepo : Repository<User>
{
    public UserRepo(RouletteDbContext rouletteDbContext) : base(rouletteDbContext)
    {
    }
}
